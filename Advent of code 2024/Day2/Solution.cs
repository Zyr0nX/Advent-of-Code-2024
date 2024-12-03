namespace Advent_of_code_2024;

public class SolutionDay2() : SolutionBase(2)
{
    public override string Part1Solver()
    {
        var res = 0;

        foreach (var line in Input.AsSpan().EnumerateLines())
        {
            int? prevLevel = null;
            int? currLevel = null;
            bool? isIncreasing = null;
            var isSafe = true;
            foreach (var levelRange in line.Split(' '))
            {
                if (currLevel is not null)
                {
                    prevLevel = currLevel;
                }

                currLevel = int.Parse(line[levelRange]);

                if (prevLevel is null) continue;
                
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

        foreach (var line in Input.AsSpan().EnumerateLines())
        {
            foreach (var levelRangeSkip in line.Split(' '))
            {
                int? prevLevel = null;
                int? currLevel = null;
                bool? isIncreasing = null;
                var isSafe = true;
                foreach (var levelRange in line.Split(' '))
                {
                    if (levelRange.Equals(levelRangeSkip))
                    {
                        continue;
                    }
                    if (currLevel.HasValue)
                    {
                        prevLevel = currLevel;
                    }

                    currLevel = int.Parse(line[levelRange]);

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