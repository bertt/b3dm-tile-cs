using BenchmarkDotNet.Running;
using System;

namespace b3dm.tile.benchmarks;

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<B3dmBenchmarks>();
        Console.Write(summary);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
