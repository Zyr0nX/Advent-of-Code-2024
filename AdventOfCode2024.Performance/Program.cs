// See https://aka.ms/new-console-template for more information

using AdventOfCode2024.Performance;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<SolutionBenchmark>();