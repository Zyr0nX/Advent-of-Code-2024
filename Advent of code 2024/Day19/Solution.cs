namespace Advent_of_code_2024;

public class SolutionDay19() : SolutionBase(19)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var separator = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var towelsSpan = inputSpan[..separator];
        var designsSpan = inputSpan[(separator + Environment.NewLine.Length * 2)..];

        var towelsCount = towelsSpan.Count(',') + 1;
        var towels = new HashSet<string>(towelsCount);
        var towelsAlt = towels.GetAlternateLookup<ReadOnlySpan<char>>();
        var maxTowelLength = 0;
        foreach (var towelRange in towelsSpan.Split(", "))
        {
            var towel = towelsSpan[towelRange];
            towelsAlt.Add(towel);
            maxTowelLength = Math.Max(maxTowelLength, towel.Length);
        }

        var maxDesignLength = 0;
        foreach (var design in designsSpan.EnumerateLines())
        {
            maxDesignLength = Math.Max(maxDesignLength, design.Length);
        }

        var res = 0;
        var stack = new Stack<int>(maxDesignLength);
        var maxStackLength = 0;
        foreach (var design in designsSpan.EnumerateLines())
        {
            stack.Clear();
            stack.Push(0);
            while (stack.TryPop(out var idx))
            {
                maxStackLength = Math.Max(maxStackLength, stack.Count);
                var possible = false;
                for (var i = idx; i < Math.Min(design.Length, idx + maxTowelLength); i++)
                {
                    var part = design[idx..i];

                    if (!towelsAlt.Contains(part)) continue;
                    if (i == design.Length - 1)
                    {
                        possible = true;
                        break;
                    }

                    stack.Push(i);
                }

                if (!possible) continue;
                res++;
                break;
            }
        }

        return res.ToString();
    }

    public string Part1SolverMemo()
    {
        var inputSpan = Input.AsSpan();
        var separator = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var towelsSpan = inputSpan[..separator];
        var designsSpan = inputSpan[(separator + Environment.NewLine.Length * 2)..];

        var towelsCount = towelsSpan.Count(',') + 1;
        var towels = new HashSet<string>(towelsCount);
        var towelsAlt = towels.GetAlternateLookup<ReadOnlySpan<char>>();
        var maxTowelLength = 0;
        foreach (var towelRange in towelsSpan.Split(", "))
        {
            var towel = towelsSpan[towelRange];
            towelsAlt.Add(towel);
            maxTowelLength = Math.Max(maxTowelLength, towel.Length);
        }

        var maxDesignLength = 0;
        foreach (var design in designsSpan.EnumerateLines())
        {
            maxDesignLength = Math.Max(maxDesignLength, design.Length);
        }

        var res = 0;
        var maxVisited = 0;
        var maxStack = 0;
        var visited = new HashSet<int>(maxDesignLength);
        var stack = new Stack<int>(maxDesignLength);
        foreach (var design in designsSpan.EnumerateLines())
        {
            visited.Clear();
            stack.Clear();
            stack.Push(0);
            visited.Add(0);
            while (stack.TryPop(out var idx))
            {
                maxStack = Math.Max(maxStack, stack.Count);
                maxVisited = Math.Max(maxVisited, visited.Count);
                var possible = false;
                for (var i = idx; i < Math.Min(idx + maxTowelLength, design.Length); i++)
                {
                    var part = design[idx..i];

                    if (!towelsAlt.Contains(part)) continue;
                    if (i == design.Length - 1)
                    {
                        possible = true;
                        break;
                    }

                    if (visited.Add(i))
                    {
                        stack.Push(i);
                    }
                }

                if (!possible) continue;
                res++;
                break;
            }
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        var inputSpan = Input.AsSpan();
        var separator = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var towelsSpan = inputSpan[..separator];
        var designsSpan = inputSpan[(separator + Environment.NewLine.Length * 2)..];

        var towelsCount = towelsSpan.Count(',') + 1;
        var towels = new HashSet<string>(towelsCount);
        var towelsAlt = towels.GetAlternateLookup<ReadOnlySpan<char>>();
        var maxTowelLength = 0;
        foreach (var towelRange in towelsSpan.Split(", "))
        {
            var towel = towelsSpan[towelRange];
            towelsAlt.Add(towel);
            maxTowelLength = Math.Max(maxTowelLength, towel.Length);
        }
        
        var maxDesignLength = 0;
        foreach (var design in designsSpan.EnumerateLines())
        {
            maxDesignLength = Math.Max(maxDesignLength, design.Length);
        }

        var dp = new long[maxDesignLength + 1];
        var res = 0L;
        foreach (var design in designsSpan.EnumerateLines())
        {
            Array.Fill(dp, 0);
            dp[0] = 1;

            for (var startIdx = 0; startIdx < design.Length; startIdx++)
            {
                if (dp[startIdx] == 0) continue;

                for (var endIdx = startIdx + 1; endIdx <= Math.Min(startIdx + maxTowelLength, design.Length); endIdx++)
                {
                    var part = design[startIdx..endIdx];

                    if (towelsAlt.Contains(part))
                    {
                        dp[endIdx] += dp[startIdx];
                    }
                }
            }

            res += dp[design.Length];
        }

        return res.ToString();
    }

    public string Part2SolverRecursion()
    {
        var inputSpan = Input.AsSpan();
        var separator = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var towelsSpan = inputSpan[..separator];
        var designsSpan = inputSpan[(separator + Environment.NewLine.Length * 2)..];

        var towels = new HashSet<string>();
        var towelsAlt = towels.GetAlternateLookup<ReadOnlySpan<char>>();
        foreach (var towelRange in towelsSpan.Split(", "))
        {
            var towel = towelsSpan[towelRange];
            towelsAlt.Add(towel);
        }
        
        var maxDesignLength = 0;
        foreach (var design in designsSpan.EnumerateLines())
        {
            maxDesignLength = Math.Max(maxDesignLength, design.Length);
        }

        var totalCount = 0L;
        var dp = new long[maxDesignLength + 1];
        foreach (var design in designsSpan.EnumerateLines())
        {
            Array.Fill(dp, -1);
            totalCount += CountMatchesRecursive(design, 0, towelsAlt, dp);
        }

        return totalCount.ToString();
    }

    private static long CountMatchesRecursive(ReadOnlySpan<char> design, int startIdx, HashSet<string>.AlternateLookup<ReadOnlySpan<char>> towelsAlt,
        long[] dp)
    {
        if (startIdx == design.Length)
        {
            return 1; // Base case: reached the end of the design
        }

        if (dp[startIdx] != -1)
        {
            return dp[startIdx]; // Return cached result if already computed
        }

        long count = 0;
        for (var endIdx = startIdx + 1; endIdx <= design.Length; endIdx++)
        {
            var part = design[startIdx..endIdx];
            if (towelsAlt.Contains(part))
            {
                count += CountMatchesRecursive(design, endIdx, towelsAlt, dp);
            }
        }

        dp[startIdx] = count;
        return count;
    }
}