﻿namespace Advent_of_code_2024;

public class SolutionDay1() : SolutionBase(1)
{
    public override string Part1Solver()
    {
        const int n = 1000;
        var leftList = new int[n];
        var rightList = new int[n];

        var i = 0;
        foreach (var line in Input.AsSpan().EnumerateLines())
        {
            var whitespaceIdx = line.IndexOf(' ');
            var left = int.Parse(line[..whitespaceIdx]);
            var right = int.Parse(line[(whitespaceIdx + 3)..]);
            
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
        var rightFreq = new Dictionary<string, int>();
        var spanRightFreq = rightFreq.GetAlternateLookup<ReadOnlySpan<char>>();
        foreach (var line in Input.AsSpan().EnumerateLines())
        {
            var whitespaceIdx = line.IndexOf(' ');
            var right = line[(whitespaceIdx + 3)..];

            if (!spanRightFreq.TryAdd(right, 1))
            {
                spanRightFreq[right] += 1;
            }
        }

        var res = 0;

        foreach (var line in Input.AsSpan().EnumerateLines())
        {
            var whitespaceIdx = line.IndexOf(' ');
            var left = line[..whitespaceIdx];

            if (spanRightFreq.TryGetValue(left, out var freq))
            {
                res += int.Parse(left) * freq;
            }
        }

        return res.ToString();
    }
}