namespace Advent_of_code_2024;

public abstract class SolutionBase
{
    public int Day { get; init; }

    public string Input => LoadInput();

    public string LoadInput()
    {
        var inputLocation = $"./Day{Day}/input";
        return File.ReadAllText(inputLocation);
    }

    public IEnumerable<string> Solve()
    {
        yield return Part1Solver();
        yield return Part2Solver();
    }

    public abstract string Part1Solver();
    public abstract string Part2Solver();
}