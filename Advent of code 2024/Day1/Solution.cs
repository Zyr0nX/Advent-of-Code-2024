using System.Buffers;
using System.Text;

namespace Advent_of_code_2024.Day1;

public class Solution : SolutionBase
{
    public override string Part1Solver()
    {
        var n = Input.Length;
        var leftList = new int[n];
        var rightList = new int[n];

        var i = 0;
        foreach (var line in Input)
        {
            var lineSpan = line.AsSpan();
            var whitespaceIdx = lineSpan.IndexOf(' ');
            var left = int.Parse(lineSpan[..whitespaceIdx]);
            var right = int.Parse(lineSpan[(whitespaceIdx + 3)..]);
            
            leftList[i] = left;
            rightList[i] = right;
            i++;
        }
        
        Array.Sort(leftList);
        Array.Sort(rightList);
        var res = 0;
        for (var j = 0; j < n; j++)
        {
            res += Math.Abs(leftList[j] - rightList[j]);
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var n = Input.Length;
        var rightFreq = new Dictionary<int, int>(n);
        var spanRightFreq = rightFreq.GetAlternateLookup<ReadOnlySpan<char>>();
        foreach (var line in Input)
        {
            var lineSpan = line.AsSpan();
            var whitespaceIdx = lineSpan.IndexOf(' ');
            var right = lineSpan[(whitespaceIdx + 3)..];

            if (!spanRightFreq.TryAdd(right, 1))
            {
                spanRightFreq[right] += 1;
            }
        }

        var res = 0;

        foreach (var line in Input)
        {
            var lineSpan = line.AsSpan();
            var whitespaceIdx = lineSpan.IndexOf(' ');
            var left = lineSpan[..whitespaceIdx];

            if (spanRightFreq.TryGetValue(left, out var freq))
            {
                res += int.Parse(left) * freq;
            }
        }

        return res.ToString();
    }
}