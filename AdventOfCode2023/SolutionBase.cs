namespace AdventOfCode2023;

public abstract class SolutionBase(int day)
{
    protected string Input { get; } = LoadInput(day);

    private static string LoadInput(int day)
    {
        var inputLocation = $"./Day{day}/input";
        return File.ReadAllText(inputLocation);
    }

    public abstract string Part1Solver();
    public abstract string Part2Solver();
}