using System.Text.RegularExpressions;

namespace Advent_of_code_2024;

public partial class SolutionDay3() : SolutionBase(3)
{
    public override string Part1Solver()
    {
        var regex = Part1MulRegex();

        var res = 0;
        
        foreach (var valueMatch in regex.EnumerateMatches(Input))
        {
            var mulSpan = Input.AsSpan()[valueMatch.Index..(valueMatch.Length + valueMatch.Index)];
            var commaIdx = mulSpan.IndexOf(',');
            var firstValue = int.Parse(mulSpan[4..commaIdx]) ;
            var secondValue = int.Parse(mulSpan[(commaIdx + 1)..^1]);
            res += firstValue * secondValue;
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var regex = Part2MulRegex();

        var res = 0;
        var isMul = true;
        
        foreach (var valueMatch in regex.EnumerateMatches(Input))
        {
            
            var mulSpan = Input.AsSpan()[valueMatch.Index..(valueMatch.Length + valueMatch.Index)];

            if (mulSpan.Equals("do()", StringComparison.Ordinal))
            {
                isMul = true;
                continue;
            }

            if (mulSpan.Equals("don't()", StringComparison.Ordinal))
            {
                isMul = false;
                continue;
            }

            if (!isMul)
            {
                continue;
            }
            var commaIdx = mulSpan.IndexOf(',');
            var firstValue = int.Parse(mulSpan[4..commaIdx]) ;
            var secondValue = int.Parse(mulSpan[(commaIdx + 1)..^1]);
            res += firstValue * secondValue;
        }

        return res.ToString();
    }

    [GeneratedRegex(@"mul\(\d+,\d+\)")]
    private static partial Regex Part1MulRegex();
    
    [GeneratedRegex(@"mul\(\d+,\d+\)|do\(\)|don't\(\)")]
    private static partial Regex Part2MulRegex();
}