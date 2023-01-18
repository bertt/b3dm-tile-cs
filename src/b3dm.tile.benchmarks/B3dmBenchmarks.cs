using B3dmCore;
using BenchmarkDotNet.Attributes;
using System.IO;

namespace b3dm.tile.benchmarks;

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
        var b3dm = new B3dm(buildingGlb);
        var res = b3dm.ToBytes();
    }

}
