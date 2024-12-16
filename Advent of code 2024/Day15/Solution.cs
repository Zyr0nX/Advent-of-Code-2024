namespace Advent_of_code_2024;

public class SolutionDay15() : SolutionBase(15)
{
    public string Part1SolverLessOptimze()
    {
        var inputSpan = Input.AsSpan();
        var partSeparatorIdx = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}".AsSpan());
        var map = inputSpan[..partSeparatorIdx];
        var moves = inputSpan[(partSeparatorIdx + Environment.NewLine.Length * 2)..];

        var m = map.IndexOf(Environment.NewLine.AsSpan(), StringComparison.Ordinal) != -1
            ? map.IndexOf(Environment.NewLine.AsSpan(), StringComparison.Ordinal)
            : map.Length;
        var rowLength = m + Environment.NewLine.Length;
        var n = (map.Length + Environment.NewLine.Length) / rowLength;

        var wallCount = map.Count('#');
        var boxCount = map.Count('O');
        var walls = new HashSet<(int, int)>(wallCount);
        var boxes = new HashSet<(int, int)>(boxCount);
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

        foreach (var move in moves)
        {
            (int, int) direction;
            switch (move)
            {
                case '^':
                    direction = (-1, 0);
                    break;
                case 'v':
                    direction = (1, 0);
                    break;
                case '<':
                    direction = (0, -1);
                    break;
                case '>':
                    direction = (0, 1);
                    break;
                default:
                    continue;
            }

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

    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var partSeparatorIdx = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}".AsSpan());
        var map = inputSpan[..partSeparatorIdx];
        var moves = inputSpan[(partSeparatorIdx + Environment.NewLine.Length * 2)..];

        var m = map.IndexOf(Environment.NewLine.AsSpan(), StringComparison.Ordinal) != -1
            ? map.IndexOf(Environment.NewLine.AsSpan(), StringComparison.Ordinal)
            : map.Length;
        var rowLength = m + Environment.NewLine.Length;
        var n = (map.Length + Environment.NewLine.Length) / rowLength;

        var wareHouse = new char[m, n];
        var robot = (0, 0);
        for (var row = 0; row < m; row++)
        {
            for (var col = 0; col < n; col++)
            {
                if (map[row * rowLength + col] == '@')
                {
                    wareHouse[row, col] = '.';
                    robot = (row, col);
                }
                else
                {
                    wareHouse[row, col] = map[row * rowLength + col];
                }
            }
        }

        foreach (var move in moves)
        {
            (int, int) direction;
            switch (move)
            {
                case '^':
                    direction = (-1, 0);
                    break;
                case 'v':
                    direction = (1, 0);
                    break;
                case '<':
                    direction = (0, -1);
                    break;
                case '>':
                    direction = (0, 1);
                    break;
                default:
                    continue;
            }

            var tryPush = robot;
            while (wareHouse[tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2] == 'O')
            {
                tryPush = (tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2);
            }

            if (wareHouse[tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2] == '#') continue;

            if (wareHouse[robot.Item1 + direction.Item1, robot.Item2 + direction.Item2] == 'O')
            {
                wareHouse[robot.Item1 + direction.Item1, robot.Item2 + direction.Item2] = '.';
                wareHouse[tryPush.Item1 + direction.Item1, tryPush.Item2 + direction.Item2] = 'O';
            }

            (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
        }

        var res = 0;
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (wareHouse[i, j] == 'O')
                {
                    res += 100 * i + j;
                }
            }
        }

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
        var robot = (0, 0);
        for (var row = 0; row < m; row++)
        {
            for (var col = 0; col < n; col++)
            {
                switch (mapSpan[row * rowLength + col])
                {
                    case '@':
                        map[row, col * 2] = '.';
                        map[row, col * 2 + 1] = '.';

                        robot = (row, col * 2);
                        break;
                    case 'O':
                        map[row, col * 2] = '[';
                        map[row, col * 2 + 1] = ']';
                        break;
                    default:
                        map[row, col * 2] = mapSpan[row * rowLength + col];
                        map[row, col * 2 + 1] = mapSpan[row * rowLength + col];
                        break;
                }
            }
        }

        var moveBoxes = new List<(int, int)>();
        
        foreach (var move in movesSpan)
        {
            (int, int) direction;
            switch (move)
            {
                case '^':
                    direction = (-1, 0);
                    break;
                case 'v':
                    direction = (1, 0);
                    break;
                case '<':
                    direction = (0, -1);
                    break;
                case '>':
                    direction = (0, 1);
                    break;
                default:
                    continue;
            }

            switch (map[robot.Item1 + direction.Item1, robot.Item2 + direction.Item2])
            {
                case '.':
                    (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
                    break;
                case '[':
                case ']':
                {
                    moveBoxes.Clear();
                    var wall = false;
                    moveBoxes.Add((robot.Item1 + direction.Item1, robot.Item2 + direction.Item2));
                    
                    for (var i = 0; i < moveBoxes.Count; i++)
                    {
                        var (halfBoxRow, halfBoxCol) = moveBoxes[i];
                        if (map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == '#')
                        {
                            wall = true;
                            break;
                        }

                        var halfBox = map[halfBoxRow, halfBoxCol];
                        switch (halfBox)
                        {
                            case '[' when !moveBoxes.Contains((halfBoxRow, halfBoxCol + 1)):
                                moveBoxes.Add((halfBoxRow, halfBoxCol + 1));
                                break;
                            case ']' when !moveBoxes.Contains((halfBoxRow, halfBoxCol - 1)):
                                moveBoxes.Add((halfBoxRow, halfBoxCol - 1));
                                break;
                        }

                        if ((map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == ']' ||
                             map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == '[') &&
                            !moveBoxes.Contains((halfBoxRow + direction.Item1, halfBoxCol + direction.Item2)))
                        {
                            moveBoxes.Add((halfBoxRow + direction.Item1, halfBoxCol + direction.Item2));
                        }
                    }

                    if (!wall)
                    {
                        for (var i = moveBoxes.Count - 1; i >= 0; i--)
                        {
                            map[moveBoxes[i].Item1 + direction.Item1, moveBoxes[i].Item2 + direction.Item2] = map[moveBoxes[i].Item1, moveBoxes[i].Item2];
                            map[moveBoxes[i].Item1, moveBoxes[i].Item2] = '.';
                        }

                        (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
                    }
                    
                    break;
                }
            }
        }

        var res = 0;
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

    public string Part2SolverBfs()
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
        var robot = (0, 0);
        for (var row = 0; row < m; row++)
        {
            for (var col = 0; col < n; col++)
            {
                switch (mapSpan[row * rowLength + col])
                {
                    case '@':
                        map[row, col * 2] = '.';
                        map[row, col * 2 + 1] = '.';

                        robot = (row, col * 2);
                        break;
                    case 'O':
                        map[row, col * 2] = '[';
                        map[row, col * 2 + 1] = ']';
                        break;
                    default:
                        map[row, col * 2] = mapSpan[row * rowLength + col];
                        map[row, col * 2 + 1] = mapSpan[row * rowLength + col];
                        break;
                }
            }
        }
        
        var queue = new Queue<(int, int)>();
        var visited = new HashSet<(int, int)>();
        
        foreach (var move in movesSpan)
        {
            (int, int) direction;
            switch (move)
            {
                case '^':
                    direction = (-1, 0);
                    break;
                case 'v':
                    direction = (1, 0);
                    break;
                case '<':
                    direction = (0, -1);
                    break;
                case '>':
                    direction = (0, 1);
                    break;
                default:
                    continue;
            }

            switch (map[robot.Item1 + direction.Item1, robot.Item2 + direction.Item2])
            {
                case '.':
                    (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
                    break;
                case '[':
                case ']':
                {
                    queue.Clear();
                    visited.Clear();
                    var wall = false;
                    queue.Enqueue((robot.Item1 + direction.Item1, robot.Item2 + direction.Item2));
                    visited.Add((robot.Item1 + direction.Item1, robot.Item2 + direction.Item2));
                    while (queue.Count > 0)
                    {
                        var (halfBoxRow, halfBoxCol) = queue.Dequeue();
                        if (map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == '#')
                        {
                            wall = true;
                            break;
                        }

                        var halfBox = map[halfBoxRow, halfBoxCol];
                        switch (halfBox)
                        {
                            case '[' when visited.Add((halfBoxRow, halfBoxCol + 1)):
                                queue.Enqueue((halfBoxRow, halfBoxCol + 1));
                                break;
                            case ']' when visited.Add((halfBoxRow, halfBoxCol - 1)):
                                queue.Enqueue((halfBoxRow, halfBoxCol - 1));
                                break;
                        }

                        if ((map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == ']' ||
                             map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == '[') &&
                            visited.Add((halfBoxRow + direction.Item1, halfBoxCol + direction.Item2)))
                        {
                            queue.Enqueue((halfBoxRow + direction.Item1, halfBoxCol + direction.Item2));
                        }
                    }

                    if (!wall)
                    {
                        foreach (var box in visited.Reverse())
                        {
                            map[box.Item1 + direction.Item1, box.Item2 + direction.Item2] = map[box.Item1, box.Item2];
                            map[box.Item1, box.Item2] = '.';
                        }

                        (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
                    }
                    
                    break;
                }
            }
        }

        var res = 0;
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
    
    public string Part2SolverListHashSet()
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
        var robot = (0, 0);
        for (var row = 0; row < m; row++)
        {
            for (var col = 0; col < n; col++)
            {
                switch (mapSpan[row * rowLength + col])
                {
                    case '@':
                        map[row, col * 2] = '.';
                        map[row, col * 2 + 1] = '.';

                        robot = (row, col * 2);
                        break;
                    case 'O':
                        map[row, col * 2] = '[';
                        map[row, col * 2 + 1] = ']';
                        break;
                    default:
                        map[row, col * 2] = mapSpan[row * rowLength + col];
                        map[row, col * 2 + 1] = mapSpan[row * rowLength + col];
                        break;
                }
            }
        }

        var moveBoxes = new List<(int, int)>();
        var visited = new HashSet<(int, int)>();
        
        foreach (var move in movesSpan)
        {
            (int, int) direction;
            switch (move)
            {
                case '^':
                    direction = (-1, 0);
                    break;
                case 'v':
                    direction = (1, 0);
                    break;
                case '<':
                    direction = (0, -1);
                    break;
                case '>':
                    direction = (0, 1);
                    break;
                default:
                    continue;
            }

            switch (map[robot.Item1 + direction.Item1, robot.Item2 + direction.Item2])
            {
                case '.':
                    (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
                    break;
                case '[':
                case ']':
                {
                    moveBoxes.Clear();
                    visited.Clear();
                    var wall = false;
                    moveBoxes.Add((robot.Item1 + direction.Item1, robot.Item2 + direction.Item2));
                    visited.Add((robot.Item1 + direction.Item1, robot.Item2 + direction.Item2));
                    var i = 0;
                    while (moveBoxes.Count > i)
                    {
                        var (halfBoxRow, halfBoxCol) = moveBoxes[i];
                        if (map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == '#')
                        {
                            wall = true;
                            break;
                        }

                        var halfBox = map[halfBoxRow, halfBoxCol];
                        switch (halfBox)
                        {
                            case '[' when visited.Add((halfBoxRow, halfBoxCol + 1)):
                                moveBoxes.Add((halfBoxRow, halfBoxCol + 1));
                                break;
                            case ']' when visited.Add((halfBoxRow, halfBoxCol - 1)):
                                moveBoxes.Add((halfBoxRow, halfBoxCol - 1));
                                break;
                        }

                        if ((map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == ']' ||
                             map[halfBoxRow + direction.Item1, halfBoxCol + direction.Item2] == '[') &&
                            visited.Add((halfBoxRow + direction.Item1, halfBoxCol + direction.Item2)))
                        {
                            moveBoxes.Add((halfBoxRow + direction.Item1, halfBoxCol + direction.Item2));
                        }

                        i++;
                    }

                    if (!wall)
                    {
                        moveBoxes.Reverse();
                        foreach (var box in moveBoxes)
                        {
                            map[box.Item1 + direction.Item1, box.Item2 + direction.Item2] = map[box.Item1, box.Item2];
                            map[box.Item1, box.Item2] = '.';
                        }

                        (robot.Item1, robot.Item2) = (robot.Item1 + direction.Item1, robot.Item2 + direction.Item2);
                    }
                    
                    break;
                }
            }
        }

        var res = 0;
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