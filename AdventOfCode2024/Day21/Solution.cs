namespace AdventOfCode;

public class SolutionDay21() : SolutionBase(21)
{
    public override string Part1Solver()
    {
        // var inputSpan = Input.AsSpan();
        //
        // char?[][] numericKeypad =
        // [
        //     ['7', '8', '9'],
        //     ['4', '5', '6'],
        //     ['1', '2', '3'],
        //     [null, '0', 'A']
        // ];
        //
        // char?[][] directionalKeypad =
        // [
        //     [null, '^', 'A'],
        //     ['<', 'v', '>'],
        // ];
        //
        // Span<(int, int)> directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];
        //
        // foreach (var code in inputSpan.EnumerateLines())
        // {
        //     var currentCode = code;
        //     (int x, int y) robotNumericKeypad = (2, 3);
        //     (int x, int y) robotDirectionalKeypad1 = (2, 0);
        //     (int x, int y) robotDirectionalKeypad2 = (2, 0);
        //     (int x, int y) robotDirectionalKeypad3 = (2, 0);
        //
        //     for (var i = 0; i < 3; i++)
        //     {
        //         foreach (var key in currentCode)
        //         {
        //             var keyPad = numericKeypad;
        //             var robot = robotNumericKeypad;
        //             switch (i)
        //             {
        //                 case 1:
        //                     keyPad = directionalKeypad;
        //                     robot = robotDirectionalKeypad1;
        //                     break;
        //                 case 2:
        //                     keyPad = directionalKeypad;
        //                     robot = robotDirectionalKeypad2;
        //                     break;
        //                 case 3:
        //                     keyPad = directionalKeypad;
        //                     robot = robotDirectionalKeypad3;
        //                     break;
        //             }
        //
        //             var queue = new Queue<(int, int)>();
        //             var visited = new HashSet<(int, int)>();
        //
        //             queue.Enqueue((2, 3));
        //             visited.Add((2, 3));
        //
        //             while (queue.TryDequeue(out var item))
        //             {
        //                 (var x, var y) = item;
        //
        //                 if (keyPad[y][x] == key)
        //                 {
        //                     robot = (x, y);
        //                     break;
        //                 }
        //
        //                 foreach (var direction in directions)
        //                 {
        //                     var newX = x + direction.Item1;
        //                     var newY = y + direction.Item2;
        //
        //                     if (newX < 0 || newX > 2 || newY < 0 || newY > 2)
        //                     {
        //                         continue;
        //                     }
        //
        //                     if (keyPad[newY][newX] != null && !visited.Contains((newX, newY)))
        //                     {
        //                         queue.Enqueue((newX, newY));
        //                         visited.Add((newX, newY));
        //                     }
        //                 }
        //             }
        //         }
        //     }
        // }

        throw new NotImplementedException();
    }

    public override string Part2Solver()
    {
        throw new NotImplementedException();
    }
}