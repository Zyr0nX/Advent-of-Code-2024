namespace Advent_of_code_2024;

public class SolutionDay7() : SolutionBase(7)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();
        var res = 0L;
        foreach (var equation in inputSpan.EnumerateLines())
        {
            var colonIdx = equation.IndexOf(':');
            var testValue = long.Parse(equation[..colonIdx]);
            var numbers = equation[(colonIdx + 1)..].Trim();
            if (EvaluateWithOperatorsPart1(numbers, 0, testValue))
            {
                res += testValue;
            }
        }

        return res.ToString();
    }

    private static bool EvaluateWithOperatorsPart1(ReadOnlySpan<char> numbers, long current, long target)
    {
        if (numbers.IsEmpty)
        {
            return current == target;
        }

        var nextNumberIdx = numbers.IndexOf(' ');
        int nextNumber;
        if (nextNumberIdx == -1)
        {
            nextNumber = int.Parse(numbers);
            return EvaluateWithOperatorsPart1(ReadOnlySpan<char>.Empty, current + nextNumber, target) ||
                   EvaluateWithOperatorsPart1(ReadOnlySpan<char>.Empty, current * nextNumber, target);
        }

        nextNumber = int.Parse(numbers[..nextNumberIdx]);
        return EvaluateWithOperatorsPart1(numbers[(nextNumberIdx + 1)..], current + nextNumber, target) ||
               EvaluateWithOperatorsPart1(numbers[(nextNumberIdx + 1)..], current * nextNumber, target);
    }


    public override string Part2Solver()
    {
        var inputSpan = Input.AsSpan();
        var res = 0L;
        foreach (var equation in inputSpan.EnumerateLines())
        {
            var colonIdx = equation.IndexOf(':');
            var testValue = long.Parse(equation[..colonIdx]);
            var numbers = equation[(colonIdx + 1)..].Trim();
            if (EvaluateWithOperatorsPart2(numbers, 0, testValue))
            {
                res += testValue;
            }
        }

        return res.ToString();
    }

    private static bool EvaluateWithOperatorsPart2(ReadOnlySpan<char> numbers, long current, long target)
    {
        if (numbers.IsEmpty)
        {
            return current == target;
        }

        var nextNumberIdx = numbers.IndexOf(' ');
        int nextNumber;
        if (nextNumberIdx == -1)
        {
            nextNumber = int.Parse(numbers);
            return EvaluateWithOperatorsPart2(ReadOnlySpan<char>.Empty, current + nextNumber, target) ||
                   EvaluateWithOperatorsPart2(ReadOnlySpan<char>.Empty, current * nextNumber, target) ||
                   EvaluateWithOperatorsPart2(ReadOnlySpan<char>.Empty, current * (long)Math.Pow(10, numbers.Length) + nextNumber, target);
        }

        nextNumber = int.Parse(numbers[..nextNumberIdx]);
        return EvaluateWithOperatorsPart2(numbers[(nextNumberIdx + 1)..], current + nextNumber, target) ||
               EvaluateWithOperatorsPart2(numbers[(nextNumberIdx + 1)..], current * nextNumber, target) ||
               EvaluateWithOperatorsPart2(numbers[(nextNumberIdx + 1)..], current * (long)MathF.Pow(10, nextNumberIdx) + nextNumber, target);
    }
}