// See https://aka.ms/new-console-template for more information

using Advent_of_code_2024_Perf;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<Day1Benchmark>();