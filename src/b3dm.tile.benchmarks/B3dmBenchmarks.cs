﻿using B3dm.Tile;
using BenchmarkDotNet.Attributes;
using System.IO;

namespace b3dm.tile.benchmarks
{
    [MemoryDiagnoser]
    public class B3dmBenchmarks
    {
        [Benchmark]
        public void ReadB3dm()
        {
            var stream = File.OpenRead("1311.b3dm");
            B3dmReader.ReadB3dm(stream);
        }

        [Benchmark]
        public void WriteB3dm()
        {
            var buildingGlb = File.ReadAllBytes(@"1.glb");
            var b3dm = new B3dm.Tile.B3dm(buildingGlb);
            B3dmWriter.WriteB3dm("1_new.b3dm", b3dm);

        }

    }
}
