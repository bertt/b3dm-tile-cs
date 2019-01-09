using B3dm.Tile;
using BenchmarkDotNet.Attributes;
using System.IO;

namespace b3dm.tile.benchmarks
{
    public class ParsingBenchmark
    {
        [Benchmark]
        public void ParseB3dmTileFromStream()
        {
            var stream = File.OpenRead("1311.b3dm");
            B3dmParser.ParseB3dm(stream);
        }

    }
}
