namespace Advent_of_code_2024;

public class SolutionDay4() : SolutionBase(4)
{
    public override string Part1Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        Span<char> xmas = ['X', 'M', 'A', 'S'];
        Span<int> directions = 
        [
            -m - Environment.NewLine.Length - 1, -m - Environment.NewLine.Length, -m - Environment.NewLine.Length + 1,
            -1, 1,
            m + Environment.NewLine.Length - 1, m + Environment.NewLine.Length, m + Environment.NewLine.Length + 1
        ];
        var res = 0;
        for (var i = 0; i < Input.Length; i++)
        {
            foreach (var direction in directions)
            {
                var j = 0;
                while (j < xmas.Length)
                {
                    var index = i + direction * j;
                    if (index < 0 || index >= Input.Length || Input[index] != xmas[j])
                    {
                        break;
                    }

                    j++;
                }

                if (j == xmas.Length)
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