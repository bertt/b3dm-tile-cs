﻿using B3dm.Tile;
using BenchmarkDotNet.Attributes;
using System.IO;
using System.Reflection;

namespace b3dm.tile.benchmarks
{
    public class ParsingBenchmark
    {
        // why thises not work?
        // private Stream stream;

        [Benchmark]
        public void ParseB3dmTileFromStream()
        {
            var stream = File.OpenRead("1311.b3dm");
            var p = B3dmParser.ParseB3dm(stream);
        }

    }
}
