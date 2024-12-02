namespace Advent_of_code_2024.Day2;

public class Solution : SolutionBase
{
    public override string Part1Solver()
    {
        var res = 0;

        foreach (var line in Input)
        {
            var lineSpan = line.AsSpan();
            int? prevLevel = null;
            int? currLevel = null;
            bool? isIncreasing = null;
            var isSafe = true;
            foreach (var levelRange in lineSpan.Split(' '))
            {
                if (currLevel is not null)
                {
                    prevLevel = currLevel;
                }

                currLevel = int.Parse(lineSpan[levelRange]);

                if (prevLevel is null) continue;
                
                var diff = currLevel - prevLevel;
                if (diff == 0 || Math.Abs((int)diff) > 3)
                {
                    isSafe = false;
                    break;
                }

                isIncreasing ??= diff > 0;

                if (isIncreasing == true && diff < 0)
                {
                    isSafe = false;
                    break;
                }
                    
                if (isIncreasing == false && diff > 0)
                {
                    isSafe = false;
                    break;
                }
            }

            if (isSafe)
            {
                res++;
            }
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var res = 0;

        foreach (var line in Input)
        {
            var lineSpan = line.AsSpan();
            foreach (var levelRangeSkip in lineSpan.Split(' '))
            {
                int? prevLevel = null;
                int? currLevel = null;
                bool? isIncreasing = null;
                var isSafe = true;
                foreach (var levelRange in lineSpan.Split(' '))
                {
                    if (levelRange.Equals(levelRangeSkip))
                    {
                        continue;
                    }
                    if (currLevel.HasValue)
                    {
                        prevLevel = currLevel;
                    }

                    currLevel = int.Parse(lineSpan[levelRange]);

                    if (!prevLevel.HasValue) continue;
                
                    var diff = currLevel - prevLevel;
                    if (diff == 0 || Math.Abs((int)diff) > 3)
                    {
                        isSafe = false;
                        break;
                    }

                    isIncreasing ??= diff > 0;

                    if ((isIncreasing != true || !(diff < 0)) && (isIncreasing != false || !(diff > 0))) continue;
                    
                    isSafe = false;
                    break;
                }

                if (!isSafe) continue;
                
                res++;
                break;
            }

            
        }

        return res.ToString();
    }
}