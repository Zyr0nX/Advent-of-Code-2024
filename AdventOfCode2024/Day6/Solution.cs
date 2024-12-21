namespace AdventOfCode;

public class SolutionDay6() : SolutionBase(6)
{
    public override string Part1Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        Span<int> directions =
        [
            -rowLength,
            1,
            rowLength,
            -1
        ];
        var guard = Input.IndexOf('^');
        var res = 1;
        var visited = new Dictionary<int, HashSet<int>>();
        var direction = 0;

        while (!visited.TryGetValue(guard, out var visitedDirections) ||
               !visitedDirections.Contains(direction))
        {
            if (!visited.TryAdd(guard, [direction]))
            {
                visited[guard].Add(direction);
            }

            if (guard + directions[direction] < 0 ||
                guard + directions[direction] > Input.Length - 1 ||
                (guard + directions[direction]) % rowLength > m)
            {
                break;
            }

            while (Input[guard + directions[direction]] == '#')
            {
                direction = (direction + 1) % 4;
            }

            guard += directions[direction];


            if (!visited.ContainsKey(guard))
            {
                res++;
            }
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        Span<int> directions =
        [
            -rowLength,
            1,
            rowLength,
            -1
        ];
        var guard = Input.IndexOf('^');
        var res = 0;
        var direction = 0;
        var tempObstacles = new HashSet<int>();
        var visited = new HashSet<(int, int)>();
        while (true)
        {
            if (guard + directions[direction] < 0 ||
                guard + directions[direction] > Input.Length - 1 ||
                (guard + directions[direction]) % rowLength > m)
            {
                break;
            }

            while (Input[guard + directions[direction]] == '#')
            {
                direction = (direction + 1) % 4;
            }
            
            var tempObstacle = guard + directions[direction];

            if (tempObstacles.Add(tempObstacle))
            {
                visited.Clear();
                var tempGuard = guard;
                var tempDirection = direction;
                
                while (visited.Add((tempGuard, tempDirection)))
                {
                    if (tempGuard + directions[tempDirection] < 0 ||
                        tempGuard + directions[tempDirection] > Input.Length - 1 ||
                        (tempGuard + directions[tempDirection]) % rowLength > m)
                    {
                        break;
                    }
                
                    while (Input[tempGuard + directions[tempDirection]] == '#' || tempGuard + directions[tempDirection] == tempObstacle)
                    {
                        tempDirection = (tempDirection + 1) % 4;
                    }
                
                    tempGuard += directions[tempDirection];
                }

                if (!(tempGuard + directions[tempDirection] < 0 ||
                      tempGuard + directions[tempDirection] > Input.Length - 1 ||
                      (tempGuard + directions[tempDirection]) % rowLength > m))
                {
                    res++;
                }
            }
            
            guard += directions[direction];
        }

        return res.ToString();
    }
}