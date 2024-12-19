namespace Advent_of_code_2024;

public class SolutionDay19() : SolutionBase(19)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var separator = inputSpan.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var towelsSpan = inputSpan[..separator];
        var designsSpan = inputSpan[(separator + Environment.NewLine.Length * 2)..];

        var towels = new HashSet<string>();
        foreach (var towelRange in towelsSpan.Split(", "))
        {
            var towel = towelsSpan[towelRange];
            towels.Add(towel.ToString());
        }

        var res = 0;
        foreach (var design in designsSpan.EnumerateLines())
        {
            var stack = new Stack<int>();
            stack.Push(0);
            while (stack.TryPop(out var idx))
            {
                for (int i = idx; i < design.Length; i++)
                {
                    var part = design[idx..i];

                    if (towels.Contains(part.ToString()))
                    {   
                        if (i == design.Length - 1)
                        {
                            res++;
                            stack.Clear();
                            break;
                        }
                        stack.Push(i);
                    }
                }
            }
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        throw new NotImplementedException();
    }
}