using System.Collections.ObjectModel;

namespace Advent_of_code_2024;

public class SolutionDay15() : SolutionBase(15)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var partSeparatorIdx = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}".AsSpan());
        var map = inputSpan[..partSeparatorIdx];
        var moves = inputSpan[(partSeparatorIdx + Environment.NewLine.Length * 2)..];
        
        var m = map.IndexOf(Environment.NewLine, StringComparison.Ordinal) != -1
            ? map.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : map.Length;
        var rowLength = m + Environment.NewLine.Length;
        var n = (map.Length + Environment.NewLine.Length) / rowLength;
        
        var walls = new HashSet<(int, int)>();
        var boxes = new HashSet<(int, int)>();
        var robot = (0, 0);
        for (var row = 0; row < m; row++)
        {
            for (var col = 0; col < n; col++)
            {
                switch (map[row * rowLength + col])
                {
                    case '@':
                        robot = (row, col);
                        break;
                    case 'O':
                        boxes.Add((row, col));
                        break;
                    case '#':
                        walls.Add((row, col));
                        break;
                }
            }
        }

        var directions = new Dictionary<char, (int, int)>(4)
        {
            { '^', (-1, 0) },
            { 'v', (1, 0) },
            { '<', (0, -1) },
            { '>', (0, 1) }
        };

        var directionsReadOnly = directions.AsReadOnly();

        foreach (var move in moves)
        {
            if (!directionsReadOnly.TryGetValue(move, out var direction)) continue;
            
            var tryPush = robot;
            while (boxes.Contains((tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2)))
            {
                tryPush = (tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2);
            }

            if (walls.Contains((tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2))) continue;
            
            if (boxes.Contains((robot.Item1 + direction.Item1, robot.Item2 + direction.Item2)))
            {
                boxes.Remove((robot.Item1 + direction.Item1, robot.Item2 + direction.Item2));
                boxes.Add((tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2));
            }

            (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
        }

        var res = boxes.Sum(box => 100 * box.Item1 + box.Item2);

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var inputSpan = Input.AsSpan();
        var partSeparatorIdx = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var mapSpan = inputSpan[..partSeparatorIdx];
        var movesSpan = inputSpan[(partSeparatorIdx + Environment.NewLine.Length * 2)..];
        var m = mapSpan.IndexOf(Environment.NewLine, StringComparison.Ordinal) != -1
            ? mapSpan.IndexOf(Environment.NewLine, StringComparison.Ordinal)
            : mapSpan.Length;
        var rowLength = m + Environment.NewLine.Length;
        var n = (mapSpan.Length + Environment.NewLine.Length) / rowLength;
        var map = new char[m, n * 2];
        var robotRow = 0;
        var robotCol = 0;
        for (var row = 0; row < m; row++)
        {
            for (var col = 0; col < n; col++)
            {
                if (mapSpan[row * rowLength + col] == '@')
                {
                    map[row, col * 2] = '.';
                    map[row, col * 2 + 1] = '.';

                    robotRow = row;
                    robotCol = col * 2;
                }
                else if (mapSpan[row * rowLength + col] == 'O')
                {
                    map[row, col * 2] = '[';
                    map[row, col * 2 + 1] = ']';
                }
                else
                {
                    map[row, col * 2] = mapSpan[row * rowLength + col];
                    map[row, col * 2 + 1] = mapSpan[row * rowLength + col];
                }
            }
        }

        foreach (var move in movesSpan)
        {
            if (move == '^')
            {
                switch (map[robotRow - 1, robotCol])
                {
                    case '.':
                        robotRow--;
                        break;
                    case '[':
                    case ']':
                    {
                        var stack = new Queue<(int, int)>();
                        var visited = new HashSet<(int, int)>();
                        var wall = false;
                        stack.Enqueue((robotRow - 1, robotCol));
                        visited.Add((robotRow - 1, robotCol));
                        while (stack.Count > 0)
                        {
                            var (halfBoxRow, halfBoxCol) = stack.Dequeue();
                            if (map[halfBoxRow - 1, halfBoxCol] == '#')
                            {
                                wall = true;
                                break;
                            }

                            var halfBox = map[halfBoxRow, halfBoxCol];
                            if (halfBox == '[' && visited.Add((halfBoxRow, halfBoxCol + 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol + 1));
                            }
                            else if (halfBox == ']' && visited.Add((halfBoxRow, halfBoxCol - 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol - 1));
                            }

                            if ((map[halfBoxRow - 1, halfBoxCol] == ']' || map[halfBoxRow - 1, halfBoxCol] == '[') &&
                                visited.Add((halfBoxRow - 1, halfBoxCol)))
                            {
                                stack.Enqueue((halfBoxRow - 1, halfBoxCol));
                            }
                        }

                        if (!wall)
                        {
                            foreach (var box in visited.Reverse())
                            {
                                map[box.Item1 - 1, box.Item2] = map[box.Item1, box.Item2];
                                map[box.Item1, box.Item2] = '.';
                            }
                            
                            robotRow--;
                        }

                        

                        break;
                    }
                }
            }

            if (move == 'v')
            {
                switch (map[robotRow + 1, robotCol])
                {
                    case '.':
                        robotRow++;
                        break;
                    case '[':
                    case ']':
                    {
                        var stack = new Queue<(int, int)>();
                        var visited = new HashSet<(int, int)>();
                        var wall = false;
                        stack.Enqueue((robotRow + 1, robotCol));
                        visited.Add((robotRow + 1, robotCol));
                        while (stack.Count > 0)
                        {
                            var (halfBoxRow, halfBoxCol) = stack.Dequeue();
                            if (map[halfBoxRow + 1, halfBoxCol] == '#')
                            {
                                wall = true;
                                break;
                            }

                            var halfBox = map[halfBoxRow, halfBoxCol];
                            if (halfBox == '[' && visited.Add((halfBoxRow, halfBoxCol + 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol + 1));
                            }
                            else if (halfBox == ']' && visited.Add((halfBoxRow, halfBoxCol - 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol - 1));
                            }

                            if ((map[halfBoxRow + 1, halfBoxCol] == ']' || map[halfBoxRow + 1, halfBoxCol] == '[') &&
                                visited.Add((halfBoxRow + 1, halfBoxCol)))
                            {
                                stack.Enqueue((halfBoxRow + 1, halfBoxCol));
                            }
                        }

                        if (!wall)
                        {
                            foreach (var box in visited.Reverse())
                            {
                                map[box.Item1 + 1, box.Item2] = map[box.Item1, box.Item2];
                                map[box.Item1, box.Item2] = '.';
                            }
                            
                            robotRow++;
                        }

                        

                        break;
                    }
                }
            }

            if (move == '<')
            {
                switch (map[robotRow, robotCol - 1])
                {
                    case '.':
                        robotCol--;
                        break;
                    case '[':
                    case ']':
                    {
                        var stack = new Queue<(int, int)>();
                        var visited = new HashSet<(int, int)>();
                        var wall = false;
                        stack.Enqueue((robotRow, robotCol - 1));
                        visited.Add((robotRow, robotCol - 1));
                        while (stack.Count > 0)
                        {
                            var (halfBoxRow, halfBoxCol) = stack.Dequeue();
                            if (map[halfBoxRow, halfBoxCol - 1] == '#')
                            {
                                wall = true;
                                break;
                            }

                            var halfBox = map[halfBoxRow, halfBoxCol];
                            if (halfBox == '[' && visited.Add((halfBoxRow, halfBoxCol + 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol + 1));
                            }
                            else if (halfBox == ']' && visited.Add((halfBoxRow, halfBoxCol - 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol - 1));
                            }

                            if ((map[halfBoxRow, halfBoxCol - 1] == ']' || map[halfBoxRow, halfBoxCol - 1] == '[') &&
                                visited.Add((halfBoxRow, halfBoxCol - 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol - 1));
                            }
                        }

                        if (!wall)
                        {
                            foreach (var box in visited.Reverse())
                            {
                                map[box.Item1, box.Item2 - 1] = map[box.Item1, box.Item2];
                                map[box.Item1, box.Item2] = '.';
                            }

                            robotCol--;
                        }

                        break;
                    }
                }
            }

            if (move == '>')
            {
                switch (map[robotRow, robotCol + 1])
                {
                    case '.':
                        robotCol++;
                        break;
                    case '[':
                    case ']':
                    {
                        var stack = new Queue<(int, int)>();
                        var visited = new HashSet<(int, int)>();
                        var wall = false;
                        stack.Enqueue((robotRow, robotCol + 1));
                        visited.Add((robotRow, robotCol + 1));
                        while (stack.Count > 0)
                        {
                            var (halfBoxRow, halfBoxCol) = stack.Dequeue();
                            if (map[halfBoxRow, halfBoxCol + 1] == '#')
                            {
                                wall = true;
                                break;
                            }

                            var halfBox = map[halfBoxRow, halfBoxCol];
                            if (halfBox == '[' && visited.Add((halfBoxRow, halfBoxCol + 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol + 1));
                            }
                            else if (halfBox == ']' && visited.Add((halfBoxRow, halfBoxCol - 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol - 1));
                            }

                            if ((map[halfBoxRow, halfBoxCol + 1] == ']' || map[halfBoxRow, halfBoxCol + 1] == '[') &&
                                visited.Add((halfBoxRow, halfBoxCol + 1)))
                            {
                                stack.Enqueue((halfBoxRow, halfBoxCol + 1));
                            }
                        }

                        if (!wall)
                        {
                            foreach (var box in visited.Reverse())
                            {
                                map[box.Item1, box.Item2 + 1] = map[box.Item1, box.Item2];
                                map[box.Item1, box.Item2] = '.';
                            }

                            robotCol++;
                        }

                        break;
                    }
                }
            }
        }

        var res = 0L;
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n * 2; j++)
            {
                if (map[i, j] == '[')
                {
                    res += 100 * i + j;
                }
            }
        }

        return res.ToString();
    }
}