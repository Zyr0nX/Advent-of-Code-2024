namespace AdventOfCode;

public class SolutionDay20() : SolutionBase(20)
{
    public override string Part1Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;
        var o = Input.Count(c => c == '#');

        Span<int> directions =
        [
            -rowLength,
            1,
            rowLength,
            -1
        ];

        var start = Input.IndexOf('S');
        var end = Input.IndexOf('E');
        
        var visited = new Dictionary<int, int>(m * n - o) { { start, 0 } };
        var current = start;
        var time = 0;
        while (current != end)
        {
            foreach (var direction in directions)
            {
                var newIdx = current + direction;
                if (newIdx < 0 ||
                    newIdx > Input.Length - 1 ||
                    newIdx % rowLength > m ||
                    Input[newIdx] == '#' ||
                    !visited.TryAdd(newIdx, time))
                {
                    continue;
                }
                current = newIdx;
                time++;
                break;
            }
        }

        var res = 0;
        Span<(int x, int y)> jumps = [(-2, 0), (0, 2), (2, 0), (0, -2)];
        foreach (var cheatStartKv in visited)
        {
            var (cheatStart, cheatStartTime) = cheatStartKv;
            var cheatStartX = cheatStart % rowLength;
            var cheatStartY = cheatStart / rowLength;
            foreach (var (x, y) in jumps)
            {
                var cheatEndX = cheatStartX + x;
                var cheatEndY = cheatStartY + y;
                    
                if (cheatEndY < 0 ||
                    cheatEndY > n - 1 ||
                    cheatEndX < 0 ||
                    cheatEndX > m - 1)
                {
                    continue;
                }
                    
                var cheatEnd = cheatEndY * rowLength + cheatEndX;
                
                if (!visited.TryGetValue(cheatEnd, out var cheatEndTime))
                {
                    continue;
                }

                if (cheatEndTime - cheatStartTime - 2 >= 100)
                {
                    res++;
                }
            }
        }

        return res.ToString();
    }
    
    public override string Part2Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;
        var o = Input.Count(c => c == '#');
        Span<int> directions =
        [
            -rowLength,
            1,
            rowLength,
            -1
        ];

        var start = Input.IndexOf('S');
        var end = Input.IndexOf('E');

        var visited = new Dictionary<int, int>(m * n - o) { { start, 0 } };
        var current = start;
        var time = 0;
        while (current != end)
        {
            foreach (var direction in directions)
            {
                var newIdx = current + direction;
                if (newIdx < 0 ||
                    newIdx > Input.Length - 1 ||
                    newIdx % rowLength > m ||
                    Input[newIdx] == '#' ||
                    !visited.TryAdd(newIdx, time))
                {
                    continue;
                }
                current = newIdx;
                time++;
                break;
            }
        }

        var res = 0;

        foreach (var cheatStartKv in visited)
        {
            var (cheatStart, cheatStartTime) = cheatStartKv;
            var cheatStartX = cheatStart % rowLength;
            var cheatStartY = cheatStart / rowLength;
            
            for (var i = -20; i <= 20; i++)
            {
                for (var j = -20 + Math.Abs(i); j <= 20 - Math.Abs(i); j++)
                {
                    var cheatEndX = cheatStartX + i;
                    var cheatEndY = cheatStartY + j;
                    
                    if (cheatEndY < 0 ||
                        cheatEndY > n - 1 ||
                        cheatEndX < 0 ||
                        cheatEndX > m - 1)
                    {
                        continue;
                    }
                    
                    var cheatEnd = cheatEndY * rowLength + cheatEndX;
                    
                    if (!visited.TryGetValue(cheatEnd, out var cheatEndTime))
                    {
                        continue;
                    }

                    var distance = Math.Abs(i) + Math.Abs(j);

                    if (cheatEndTime - cheatStartTime - distance >= 100)
                    {
                        res++;
                    }
                }
            }
        }

        return res.ToString();
    }
    
    public string Part1SolverBruteForce()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;

        Span<int> directions =
        [
            -rowLength,
            1,
            rowLength,
            -1
        ];

        var start = Input.IndexOf('S');
        var end = Input.IndexOf('E');

        var queue = new Queue<(int index, int time)>(n);
        var visited = new HashSet<int>();

        var originalTime = 0;

        queue.Enqueue((start, 0));
        visited.Add(start);
        while (queue.TryDequeue(out var state))
        {
            var (idx, time) = state;
            if (idx == end)
            {
                originalTime = time;
                break;
            }

            foreach (var direction in directions)
            {
                var newIdx = idx + direction;
                if (newIdx < 0 ||
                    newIdx > Input.Length - 1 ||
                    newIdx % rowLength > m ||
                    Input[idx + direction] == '#' ||
                    !visited.Add(newIdx))
                {
                    continue;
                }

                queue.Enqueue((newIdx, time + 1));
            }
        }

        var res = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            if (Input[i] != '#')
            {
                continue;
            }

            var cheatQueue = new Queue<(int index, int time)>(n);
            var cheatVisited = new HashSet<int>();

            cheatQueue.Enqueue((start, 0));
            cheatVisited.Add(start);

            while (cheatQueue.TryDequeue(out var state))
            {
                var (idx, time) = state;
                if (idx == end)
                {
                    if (time <= originalTime - 100)
                    {
                        res++;
                    }

                    break;
                }

                foreach (var direction in directions)
                {
                    var newIdx = idx + direction;
                    if (newIdx < 0 ||
                        newIdx > Input.Length - 1 ||
                        newIdx % rowLength > m ||
                        (newIdx != i && Input[newIdx] == '#') ||
                        !cheatVisited.Add(newIdx))
                    {
                        continue;
                    }

                    cheatQueue.Enqueue((newIdx, time + 1));
                }
            }
        }

        return res.ToString();
    }

    public string Part1SolverNonOp()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;
        var o = Input.Count(c => c == '#');

        Span<int> directions =
        [
            -rowLength,
            1,
            rowLength,
            -1
        ];

        var start = Input.IndexOf('S');
        var end = Input.IndexOf('E');

        var stack = new Stack<int>((m - 1) * (n / 2) + 1 - o / 2);
        var visited = new Dictionary<int, int>(m * n - o);
        stack.Push(start);
        visited.Add(start, 0);
        while (stack.TryPop(out var idx))
        {
            var time = visited[idx];
            if (idx == end)
            {
                continue;
            }

            foreach (var direction in directions)
            {
                var newIdx = idx + direction;
                if (newIdx < 0 ||
                    newIdx > Input.Length - 1 ||
                    newIdx % rowLength > m ||
                    Input[newIdx] == '#' ||
                    !visited.TryAdd(newIdx, time + 1))
                {
                    continue;
                }

                stack.Push(newIdx);
            }
        }

        var res = 0;

        foreach (var cheatStart in visited.Keys)
        {
            foreach (var cheatEnd in visited.Keys)
            {
                if (cheatStart == cheatEnd)
                {
                    continue;
                }

                var cheatStartX = cheatStart % rowLength;
                var cheatStartY = cheatStart / rowLength;
                var cheatEndX = cheatEnd % rowLength;
                var cheatEndY = cheatEnd / rowLength;

                var distance = Math.Abs(cheatStartX - cheatEndX) + Math.Abs(cheatStartY - cheatEndY);

                if (distance == 2 && visited[cheatEnd] - visited[cheatStart] - distance >= 100)
                {
                    res++;
                }
            }
        }

        return res.ToString();
    }

    public string Part2SolverNonOp()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        var n = (Input.Length + Environment.NewLine.Length) / rowLength;
        var o = Input.Count(c => c == '#');
        Span<int> directions =
        [
            -rowLength,
            1,
            rowLength,
            -1
        ];

        var start = Input.IndexOf('S');
        var end = Input.IndexOf('E');

        var stack = new Stack<int>((m - 1) * (n / 2) + 1 - o / 2);
        var visited = new Dictionary<int, int>(m * n - o);

        stack.Push(start);
        visited.Add(start, 0);
        while (stack.TryPop(out var idx))
        {
            var time = visited[idx];
            if (idx == end)
            {
                continue;
            }

            foreach (var direction in directions)
            {
                var newIdx = idx + direction;
                if (newIdx < 0 ||
                    newIdx > Input.Length - 1 ||
                    newIdx % rowLength > m ||
                    Input[newIdx] == '#' ||
                    !visited.TryAdd(newIdx, time + 1))
                {
                    continue;
                }

                stack.Push(newIdx);
            }
        }

        var res = 0;

        foreach (var cheatStart in visited.Keys)
        {
            foreach (var cheatEnd in visited.Keys)
            {
                if (cheatStart == cheatEnd)
                {
                    continue;
                }

                var cheatStartX = cheatStart % rowLength;
                var cheatStartY = cheatStart / rowLength;
                var cheatEndX = cheatEnd % rowLength;
                var cheatEndY = cheatEnd / rowLength;

                var distance = Math.Abs(cheatStartX - cheatEndX) + Math.Abs(cheatStartY - cheatEndY);

                if (distance <= 20 && visited[cheatEnd] - visited[cheatStart] - distance >= 100)
                {
                    res++;
                }
            }
        }

        return res.ToString();
    }
}