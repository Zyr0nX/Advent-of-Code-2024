namespace Advent_of_code_2024;

public class SolutionDay4() : SolutionBase(4)
{
    public override string Part1Solver()
    {
        var m = Input.IndexOf(Environment.NewLine, StringComparison.Ordinal);
        var rowLength = m + Environment.NewLine.Length;

        Span<char> xmas = ['X', 'M', 'A', 'S'];
        Span<int> directions = 
        [
            -rowLength - 1, -rowLength, -rowLength + 1,
            -1, 1,
            rowLength - 1, rowLength, rowLength + 1
        ];
        var res = 0;
        for (var i = 0; i < Input.Length; i++)
        {
            foreach (var direction in directions)
            {
                int j;
                for (j = 0; j < xmas.Length; j++)
                {
                    var index = i + direction * j;
                    
                    if (index < 0 || index >= Input.Length || Input[index] != xmas[j])
                    {
                        break;
                    }
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

            var topLeft = i - rowLength - 1;
            var topRight = i - rowLength + 1;
            var bottomLeft = i + rowLength - 1;
            var bottomRight = i + rowLength + 1;
            
            if ((Input[topLeft] != 'M' ||
                 Input[bottomRight] != 'S') &&
                (Input[topLeft] != 'S' ||
                 Input[bottomRight] != 'M') ||
                (Input[topRight] != 'M' ||
                 Input[bottomLeft] != 'S') &&
                (Input[topRight] != 'S' ||
                 Input[bottomLeft] != 'M'))
            {
                continue;
            }

            res += 1;
        }

        return res.ToString();
    }
}