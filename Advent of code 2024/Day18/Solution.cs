namespace Advent_of_code_2024;

public class SolutionDay18() : SolutionBase(18)
{
    public override string Part1Solver()
    {
        const int byteFall = 1024;
        var inputSpan = Input.AsSpan();
        var corrupted = new HashSet<(int, int)>(byteFall);

        foreach (var line in inputSpan.EnumerateLines())
        {
            if (corrupted.Count == byteFall)
            {
                break;
            }

            var commaIdx = line.IndexOf(',');
            var x = int.Parse(line[..commaIdx]);
            var y = int.Parse(line[(commaIdx + 1)..]);
            corrupted.Add((x, y));
        }

        (int x, int y) start = (0, 0);
        (int x, int y) end = (70, 70);

        var queue = new Queue<((int x, int y), int)>(Math.Min(end.x, end.y) + 1);
        var visited = new HashSet<(int x, int y)>((end.x + 1) * (end.y + 1) - corrupted.Count);
        var maxQueueSize = 0;
        var maxVisitedSize = 0;
        queue.Enqueue((start, 0));
        visited.Add(start);

        Span<(int x, int y)> directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];

        while (queue.TryDequeue(out var item))
        {
            maxQueueSize = Math.Max(maxQueueSize, queue.Count);
            maxVisitedSize = Math.Max(maxVisitedSize, visited.Count);
            var (coordinates, step) = item;

            if (coordinates == end)
            {
                return step.ToString();
            }

            foreach (var direction in directions)
            {
                if (coordinates.x + direction.x < start.x ||
                    coordinates.x + direction.x > end.x ||
                    coordinates.y + direction.y < start.y ||
                    coordinates.y + direction.y > end.y)
                {
                    continue;
                }

                var newCoordinates = (coordinates.x + direction.x, coordinates.y + direction.y);

                if (visited.Add(newCoordinates) && !corrupted.Contains(newCoordinates))
                {
                    queue.Enqueue((newCoordinates, step + 1));
                }
            }
        }

        throw new Exception("No path found");
    }

    public override string Part2Solver()
    {
        var inputSpan = Input.AsSpan();

        var corruptedCount = inputSpan.Count($"{Environment.NewLine}") + 1;
        var corrupted = new (int, int)[corruptedCount];
        var corruptedIdx = 0;
        foreach (var line in inputSpan.EnumerateLines())
        {
            var commaIdx = line.IndexOf(',');
            var x = int.Parse(line[..commaIdx]);
            var y = int.Parse(line[(commaIdx + 1)..]);
            corrupted[corruptedIdx] = (x, y);
            corruptedIdx++;
        }

        (int x, int y) start = (0, 0);
        (int x, int y) end = (70, 70);

        var low = 0;
        var high = corruptedCount - 1;
        int? prevHigh = null;

        var corruptedSet = new HashSet<(int x, int y)>(corruptedCount);
        var visited = new HashSet<(int x, int y)>((end.x + 1) * (end.y + 1) - corrupted.Length);
        var queue = new Queue<((int x, int y), int)>(Math.Min(end.x, end.y) + 1);

        Span<(int x, int y)> directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];

        while (low < high)
        {
            var middle = (low + high) / 2;

            if (prevHigh.HasValue)
            {
                for (var i = middle + 1; i <= prevHigh; i++)
                {
                    corruptedSet.Remove(corrupted[i]);
                }
            }
            else
            {
                for (var i = low; i <= middle; i++)
                {
                    corruptedSet.Add(corrupted[i]);
                }
            }

            visited.Clear();
            queue.Clear();

            visited.Add(start);
            queue.Enqueue((start, 0));

            var reachEnd = false;

            while (queue.TryDequeue(out var item))
            {
                var (coordinates, step) = item;

                if (coordinates == end)
                {
                    reachEnd = true;
                    low = middle + 1;
                    prevHigh = null;
                    break;
                }

                foreach (var direction in directions)
                {
                    if (coordinates.x + direction.x < start.x ||
                        coordinates.x + direction.x > end.x ||
                        coordinates.y + direction.y < start.y ||
                        coordinates.y + direction.y > end.y)
                    {
                        continue;
                    }

                    var newCoordinates = (coordinates.x + direction.x, coordinates.y + direction.y);

                    if (visited.Add(newCoordinates) && !corruptedSet.Contains(newCoordinates))
                    {
                        queue.Enqueue((newCoordinates, step + 1));
                    }
                }
            }

            if (reachEnd) continue;
            prevHigh = high;
            high = middle;
        }

        (int x, int y) res = corrupted[low];

        return $"{res.x},{res.y}";
    }

    public string Part2Solver1Bfs()
    {
        var inputSpan = Input.AsSpan();

        var corruptedCount = inputSpan.Count($"{Environment.NewLine}") + 1;
        var corrupted = new Dictionary<(int, int), int>(corruptedCount);
        var corruptedIdx = 0;
        foreach (var line in inputSpan.EnumerateLines())
        {
            var commaIdx = line.IndexOf(',');
            var x = int.Parse(line[..commaIdx]);
            var y = int.Parse(line[(commaIdx + 1)..]);
            corrupted.Add((x, y), corruptedIdx);
            corruptedIdx++;
        }

        (int x, int y) start = (0, 0);
        (int x, int y) end = (70, 70);
        var queue = new Queue<((int x, int y), int)>((end.x + 1) * (end.y + 1) - corrupted.Count);
        var reachableUntil = new Dictionary<(int x, int y), int>((end.x + 1) * (end.y + 1));

        Span<(int x, int y)> directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];
        queue.Enqueue((start, corruptedCount));

        while (queue.TryDequeue(out var item))
        {
            var (coordinates, firstByte) = item;

            if (coordinates == end)
            {
                continue;
            }

            foreach (var direction in directions)
            {
                if (coordinates.x + direction.x < start.x ||
                    coordinates.x + direction.x > end.x ||
                    coordinates.y + direction.y < start.y ||
                    coordinates.y + direction.y > end.y)
                {
                    continue;
                }

                var newCoordinates = (coordinates.x + direction.x, coordinates.y + direction.y);

                var newFirstByte = firstByte;

                if (corrupted.TryGetValue(newCoordinates, out var @byte))
                {
                    newFirstByte = Math.Min(newFirstByte, @byte);
                }

                if (reachableUntil.TryGetValue(newCoordinates, out var existingByte) &&
                    existingByte >= newFirstByte) continue;

                reachableUntil[newCoordinates] = newFirstByte;
                queue.Enqueue((newCoordinates, newFirstByte));
            }
        }

        (int x, int y) res = corrupted.First(c => c.Value == reachableUntil[end]).Key;

        return $"{res.x},{res.y}";
    }
}