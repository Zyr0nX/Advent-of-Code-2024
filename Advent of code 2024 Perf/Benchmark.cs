using Advent_of_code_2024;
using BenchmarkDotNet.Attributes;

namespace Advent_of_code_2024_Perf;

[MemoryDiagnoser]
[ShortRunJob]
public class Benchmark
{
    private readonly SolutionDay1 _solutionDay1 = new();
    private readonly SolutionDay2 _solutionDay2 = new();
    private readonly SolutionDay3 _solutionDay3 = new();
    private readonly SolutionDay4 _solutionDay4 = new();
    private readonly SolutionDay5 _solutionDay5 = new();
    private readonly SolutionDay6 _solutionDay6 = new();
    private readonly SolutionDay7 _solutionDay7 = new();
    private readonly SolutionDay8 _solutionDay8 = new();
    private readonly SolutionDay9 _solutionDay9 = new();
    private readonly SolutionDay10 _solutionDay10 = new();
    private readonly SolutionDay11 _solutionDay11 = new();
    
    // [Benchmark]
    // public string Day1Part1() => _solutionDay1.Part1Solver();
    //
    // [Benchmark]
    // public string Day1Part2() => _solutionDay1.Part2Solver();
    //
    // [Benchmark]
    // public string Day2Part1() => _solutionDay2.Part1Solver();
    //
    // [Benchmark]
    // public string Day2Part2() => _solutionDay2.Part2Solver();
    //
    // [Benchmark]
    // public string Day3Part1() => _solutionDay3.Part1Solver();
    //
    // [Benchmark]
    // public string Day3Part2() => _solutionDay3.Part2Solver();
    //
    // [Benchmark]
    // public string Day4Part1() => _solutionDay4.Part1Solver();
    //
    // [Benchmark]
    // public string Day4Part2() => _solutionDay4.Part2Solver();
    //
    // [Benchmark]
    // public string Day5Part1() => _solutionDay5.Part1Solver();
    //
    // [Benchmark]
    // public string Day6Part1() => _solutionDay6.Part1Solver();
    //
    // [Benchmark]
    // public string Day6Part2() => _solutionDay6.Part2Solver();
    //
    // [Benchmark]
    // public string Day7Part1() => _solutionDay7.Part1Solver();
    //
    // [Benchmark]
    // public string Day7Part2() => _solutionDay7.Part2Solver();
    //
    // [Benchmark]
    // public string Day8Part1() => _solutionDay8.Part1Solver();
    //
    // [Benchmark]
    // public string Day8Part2() => _solutionDay8.Part2Solver();
    //
    // [Benchmark]
    // public string Day9Part1() => _solutionDay9.Part1Solver();
    
    // [Benchmark]
    // public string Day9Part2() => _solutionDay9.Part2Solver();
    
    // [Benchmark]
    // public string Day10Part1() => _solutionDay10.Part1Solver();
    //
    // [Benchmark]
    // public string Day10Part2() => _solutionDay10.Part2Solver();
    
    [Benchmark]
    public string Day11Part1() => _solutionDay11.Part1Solver();
    
    [Benchmark]
    public string Day11Part2() => _solutionDay11.Part2Solver();
}