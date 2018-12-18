using B3dm.Tile;
using BenchmarkDotNet.Attributes;
using System.IO;
using System.Reflection;

namespace b3dm.tile.benchmarks
{
    public class ParsingBenchmark
    {
        // why this does not work?
        // private MemoryStream stream;

        [GlobalSetup]
        public void GlobalSetup()
        {
        }

        [Benchmark]
        public void ParseB3dmTileFromStream()
        {
            var stream = new MemoryStream(File.ReadAllBytes("1311.b3dm"));
            var p = B3dmParser.ParseB3dm(stream);
        }

    }
}
