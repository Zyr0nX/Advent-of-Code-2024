using Advent_of_code_2024.Day1;
using BenchmarkDotNet.Attributes;

namespace Advent_of_code_2024_Perf;

[MemoryDiagnoser]
public class Day1Benchmark
{
    private readonly Solution solution = new()
    {
        Day = 1
    };

    
    [Benchmark]
    public string Part1() => solution.Part2Solver();

    [Benchmark]
    public string Part2() => solution.Part2Solver();
}