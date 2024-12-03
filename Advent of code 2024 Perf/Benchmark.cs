using Advent_of_code_2024;
using BenchmarkDotNet.Attributes;

namespace Advent_of_code_2024_Perf;

[MemoryDiagnoser]
public class Benchmark
{
    private readonly SolutionDay1 _solutionDay1 = new();
    private readonly SolutionDay2 _solutionDay2 = new();
    private readonly SolutionDay3 _solutionDay3 = new();
    
    [Benchmark]
    public string Day1Part1() => _solutionDay1.Part1Solver();
    
    [Benchmark]
    public string Day1Part2() => _solutionDay1.Part2Solver();
    
    [Benchmark]
    public string Day2Part1() => _solutionDay2.Part1Solver();
    
    [Benchmark]
    public string Day2Part2() => _solutionDay2.Part2Solver();
    
    [Benchmark]
    public string Day3Part1() => _solutionDay3.Part1Solver();
    
    [Benchmark]
    public string Day3Part2() => _solutionDay3.Part2Solver();
    
   
}