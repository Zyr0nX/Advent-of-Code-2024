using AdventOfCode;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Performance;

[MemoryDiagnoser]
    [ShortRunJob]
public class SolutionBenchmark
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
    private readonly SolutionDay12 _solutionDay12 = new();
    private readonly SolutionDay13 _solutionDay13 = new();
    private readonly SolutionDay14 _solutionDay14 = new();
    private readonly SolutionDay15 _solutionDay15 = new();
    private readonly SolutionDay16 _solutionDay16 = new();
    private readonly SolutionDay18 _solutionDay18 = new();
    private readonly SolutionDay19 _solutionDay19 = new();
    private readonly SolutionDay20 _solutionDay20 = new();
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
    // public string Day5Part2() => _solutionDay5.Part1Solver();
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
    //
    // [Benchmark]
    // public string Day9Part2() => _solutionDay9.Part2Solver();
    //
    // [Benchmark]
    // public string Day10Part1() => _solutionDay10.Part1Solver();
    //
    // [Benchmark]
    // public string Day10Part2() => _solutionDay10.Part2Solver();
    //
    // [Benchmark]
    // public string Day11Part1() => _solutionDay11.Part1Solver();
    //
    // [Benchmark]
    // public string Day11Part2() => _solutionDay11.Part2Solver();
    //
    // [Benchmark]
    // public string Day12Part1() => _solutionDay12.Part1Solver();
    //
    // [Benchmark]
    // public string Day12Part2() => _solutionDay12.Part2Solver();
    //
    // [Benchmark]
    // public string Day13Part1() => _solutionDay13.Part1Solver();
    //
    // [Benchmark]
    // public string Day13Part2() => _solutionDay13.Part2Solver();
    //
    // [Benchmark]
    // public string Day14Part1() => _solutionDay14.Part1Solver();
    //
    // [Benchmark]
    // public string Day14Part2() => _solutionDay14.Part2Solver();
    //
    // [Benchmark]
    // public string Day15Part1() => _solutionDay15.Part1Solver();
    //
    // [Benchmark]
    // public string Day15Part2() => _solutionDay15.Part2Solver();
    //
    // [Benchmark]
    // public string Day15Part2Bfs() => _solutionDay15.Part2SolverBfs();
    //
    // [Benchmark]
    // public string Day15Part2ListHashSet() => _solutionDay15.Part2SolverListHashSet();
    
    // [Benchmark]
    // public string Day16Part1() => _solutionDay16.Part1Solver();
    
    // [Benchmark]
    // public string Day16Part2() => _solutionDay16.Part2Solver();
    //
    // [Benchmark]
    // public string Day16Part2List() => _solutionDay16.Part2SolverOptimize();
    //
    // [Benchmark]
    // public string Day16Part2List2() => _solutionDay16.Part2SolverOptimize2();
    
    // [Benchmark]
    // public string Day18Part1() => _solutionDay18.Part1Solver();
    //
    // [Benchmark]
    // public string Day18Part2() => _solutionDay18.Part2Solver();
    //
    // [Benchmark]
    // public string Day18Part2BiSec() => _solutionDay18.Part2Solver1Bfs();
    //
    // [Benchmark]
    // public string Day19Part1() => _solutionDay19.Part1Solver();
    //
    // [Benchmark]
    // public string Day19Part1Memo() => _solutionDay19.Part1SolverMemo();
    //
    // [Benchmark]
    // public string Day19Part2() => _solutionDay19.Part2Solver();
    //
    // [Benchmark]
    // public string Day19Part2Recursion() => _solutionDay19.Part2SolverRecursion();
    
    [Benchmark]
    public string Day20Part1() => _solutionDay20.Part1Solver();
    //
    // [Benchmark]
    // public string Day20Part1Stack() => _solutionDay20.Part1SolverStack();
    //
    // [Benchmark]
    // public string Day20Part1Optimize() => _solutionDay20.Part1SolverOptimize();
    
    [Benchmark]
    public string Day20Part2() => _solutionDay20.Part2Solver();
    //
    // [Benchmark]
    // public string Day20Part2Optimize() => _solutionDay20.Part2SolverNonOp();
}