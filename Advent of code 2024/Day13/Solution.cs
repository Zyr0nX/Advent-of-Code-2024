namespace Advent_of_code_2024;

public class SolutionDay13() : SolutionBase(13)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var res = 0L;
        foreach (var clawMachineRange in inputSpan.Split($"{Environment.NewLine}{Environment.NewLine}"))
        {
            var clawMachine = inputSpan[clawMachineRange];
            ReadOnlySpan<char> a = default;
            ReadOnlySpan<char> b = default;
            ReadOnlySpan<char> prize = default;
            var lineNumber = 0;
            foreach (var lineRange in clawMachine.Split(Environment.NewLine.AsSpan()))
            {
                switch (lineNumber)
                {
                    case 0:
                        a = clawMachine[lineRange];
                        break;
                    case 1:
                        b = clawMachine[lineRange];
                        break;
                    case 2:
                        prize = clawMachine[lineRange];
                        break;
                }

                lineNumber++;
            }

            var aCommaIdx = a.IndexOf(',');
            var aX = int.Parse(a[12..aCommaIdx]);
            var aY = int.Parse(a[(aCommaIdx + 4)..]);
            
            var bCommaIdx = b.IndexOf(',');
            var bX = int.Parse(b[12..bCommaIdx]);
            var bY = int.Parse(b[(bCommaIdx + 4)..]);
            
            var prizeCommaIdx = prize.IndexOf(',');
            var prizeX = int.Parse(prize[9..prizeCommaIdx]);
            var prizeY = int.Parse(prize[(prizeCommaIdx + 4)..]);
            
            float determinant = aX * bY - aY * bX;
            var x = (bY * prizeX - bX * prizeY) / determinant;
            var y = (aX * prizeY - aY * prizeX) / determinant;

            if (x % 1 == 0 && y % 1 == 0)
            {
                res += (long)x * 3 + (long)y;
            }
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var inputSpan = Input.AsSpan();
        var res = 0L;
        foreach (var clawMachineRange in inputSpan.Split($"{Environment.NewLine}{Environment.NewLine}"))
        {
            var clawMachine = inputSpan[clawMachineRange];
            ReadOnlySpan<char> a = default;
            ReadOnlySpan<char> b = default;
            ReadOnlySpan<char> prize = default;
            var lineNumber = 0;
            foreach (var lineRange in clawMachine.Split(Environment.NewLine.AsSpan()))
            {
                switch (lineNumber)
                {
                    case 0:
                        a = clawMachine[lineRange];
                        break;
                    case 1:
                        b = clawMachine[lineRange];
                        break;
                    case 2:
                        prize = clawMachine[lineRange];
                        break;
                }

                lineNumber++;
            }

            var aCommaIdx = a.IndexOf(',');
            var aX = int.Parse(a[12..aCommaIdx]);
            var aY = int.Parse(a[(aCommaIdx + 4)..]);
            
            var bCommaIdx = b.IndexOf(',');
            var bX = int.Parse(b[12..bCommaIdx]);
            var bY = int.Parse(b[(bCommaIdx + 4)..]);
            
            var prizeCommaIdx = prize.IndexOf(',');
            var prizeX = int.Parse(prize[9..prizeCommaIdx]) + 10000000000000;
            var prizeY = int.Parse(prize[(prizeCommaIdx + 4)..]) + 10000000000000;
            
            float determinant = aX * bY - aY * bX;
            var x = (bY * prizeX - bX * prizeY) / determinant;
            var y = (aX * prizeY - aY * prizeX) / determinant;

            if (x % 1 == 0 && y % 1 == 0)
            {
                res += (long)x * 3 + (long)y;
            }
        }

        return res.ToString();
    }
}