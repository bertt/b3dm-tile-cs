using B3dm.Tile;
using BenchmarkDotNet.Attributes;
using System.IO;

namespace b3dm.tile.benchmarks
{
    public class ParsingBenchmark
    {
        private readonly Stream stream;

        public ParsingBenchmark()
        {
            string infile = "testfixtures/1311.b3dm";
            stream = new MemoryStream(File.ReadAllBytes(infile));
        }

        [Benchmark]
        public byte[] ParseB3dmTileFromStream()
        {
            var glb = B3dmParser.ParseB3dm(stream).GlbData;
            return glb;
        }

    }
}
