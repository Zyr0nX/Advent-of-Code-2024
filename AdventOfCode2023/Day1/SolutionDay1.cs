namespace AdventOfCode2023;

public class SolutionDay1() : SolutionBase(1)
{
    public override string Part1Solver()
    {
        var inputSpan = Input.AsSpan();

        var res = 0;
        foreach (var line in inputSpan.EnumerateLines())
        {
            var firstDigitIndex = 0;
            while (!char.IsDigit(line[firstDigitIndex]))
            {
                firstDigitIndex++;
            }
            
            var secondDigitIndex = inputSpan.Length - 1;
            while (!char.IsDigit(line[secondDigitIndex]))
            {
                secondDigitIndex--;
            }
            
            var calibrateValue = (line[firstDigitIndex] - '0') * 10 + (line[secondDigitIndex] - '0');
            res += calibrateValue;
        }

        return res.ToString();
    }

    public override string Part2Solver()
    {
        throw new NotImplementedException();
    }
}