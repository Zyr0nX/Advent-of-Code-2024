using System.Text;

namespace Advent_of_code_2024;

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