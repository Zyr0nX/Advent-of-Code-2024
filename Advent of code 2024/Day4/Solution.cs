namespace Advent_of_code_2024;

public class SolutionDay4() : SolutionBase(4)
{
    public override string Part1Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        var res = 0;
        for (var i = 0; i < Input.Length; i++)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                for (var dx = -1; dx <= 1; dx++)
                {
                    if (dy == 0 && dx == 0)
                        continue;
                    
                    var index1 = i + 0 * (dx + dy * rowLength);
                    var index2 = i + 1 * (dx + dy * rowLength);
                    var index3 = i + 2 * (dx + dy * rowLength);
                    var index4 = i + 3 * (dx + dy * rowLength);
                    
                    if (index1 >= 0 && index1 < Input.Length && Input[index1] == 'X' &&
                        index2 >= 0 && index2 < Input.Length && Input[index2] == 'M' &&
                        index3 >= 0 && index3 < Input.Length && Input[index3] == 'A' &&
                        index4 >= 0 && index4 < Input.Length && Input[index4] == 'S')
                    {
                        res++;
                    }
                }
            }
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;
        var res = 0;
        for (var i = rowLength; i < Input.Length - rowLength; i++)
        {
            if (Input[i] != 'A') continue;

            if ((Input[i - rowLength - 1] != 'M' ||
                 Input[i + rowLength + 1] != 'S') &&
                (Input[i - rowLength - 1] != 'S' ||
                 Input[i + rowLength + 1] != 'M') ||
                (Input[i - rowLength + 1] != 'M' ||
                 Input[i + rowLength - 1] != 'S') &&
                (Input[i - rowLength + 1] != 'S' ||
                 Input[i + rowLength - 1] != 'M'))
            {
                continue;
            }

            res += 1;
        }

        return res.ToString();
    }
}