using BenchmarkDotNet.Running;

namespace b3dm.tile.benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ParsingBenchmark>();
        }
    }
}
