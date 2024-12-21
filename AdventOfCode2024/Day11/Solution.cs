namespace AdventOfCode;

public class SolutionDay11() : SolutionBase(11)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var res = 0L;
        var memo = new Dictionary<(long, int), long>();
        foreach (var stoneRange in inputSpan.Split(' '))
        {
            var stone = long.Parse(inputSpan[stoneRange]);
            res += GetTotal(stone, 25, memo);
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var inputSpan = Input.AsSpan();
        var res = 0L;
        var memo = new Dictionary<(long, int), long>();
        foreach (var stoneRange in inputSpan.Split(' '))
        {
            var stone = long.Parse(inputSpan[stoneRange]);
            
            res += GetTotal(stone, 75, memo);
        }

        return res.ToString();
    }
    
    private static long GetTotal(long stone, int blinks, Dictionary<(long, int), long> memo)
    {
        if (blinks == 0)
        {
            return 1;
        }
        
        blinks--;
        var key = (stone, blinks);
        
        if (memo.TryGetValue(key, out var cached))
        {
            return cached;
        }
        
        if (stone == 0)
        {
            memo[key] = GetTotal(1, blinks, memo);
            return memo[key];
        }
        
        var digits = Math.Floor(Math.Log10(stone)) + 1;

        if (digits % 2 == 0)
        {
            var power = (long)Math.Pow(10, digits / 2);
            var a = stone / power;
            var b = stone % power;
            
            memo[key] = GetTotal(a, blinks, memo) + GetTotal(b, blinks, memo);
            return memo[key];
        }

        memo[key] = GetTotal(stone * 2024, blinks, memo);
        return memo[key];
    }
}