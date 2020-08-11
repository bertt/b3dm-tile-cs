# b3dm-tile-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

Batched 3D specification: https://github.com/AnalyticalGraphicsInc/3d-tiles/blob/master/specification/TileFormats/Batched3DModel/README.md

[![NuGet Status](http://img.shields.io/nuget/v/b3dm-tile.svg?style=flat)](https://www.nuget.org/packages/b3dm-tile/)

## Sample code for conversion b3dm -> glb:

In this sample the pyaload of a B3DM tile is converted to GLB file. 

Sample code:

```
var inputfile = @"testfixtures/662.b3dm";
var outputfile = Path.GetFileNameWithoutExtension(inputfile) + ".glb";
var f = File.OpenRead(inputfile);
var b3dm = B3dmReader.ReadB3dm(f);
var stream = new MemoryStream(b3dm.GlbData);
File.WriteAllBytes(outputfile, stream.ToArray());
```

## Sample code for conversion glb -> b3dm:

```
var inputfile = @"testfixtures/building.glb";
var buildingGlb = File.ReadAllBytes(inputfile);
var b3dm = new B3dm.Tile.B3dm(buildingGlb);
var bytes = b3dm.ToBytes(b3dm);
File.WriteAllBytes("test.b3dm", bytes);
```

## Dependencies

- NETStandard.Library 2.0.3

## Benchmarks

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.329 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT

|    Method |      Mean |    Error |    StdDev |    Median |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|---------- |----------:|---------:|----------:|----------:|--------:|--------:|--------:|----------:|
|  ReadB3dm |  40.81 us | 0.930 us |  2.742 us |  39.88 us | 13.6719 |  0.2441 |       - |   85.1 KB |
```

## History

2020-08-03: Version 0.10 + 0.11 + 0.12 breaking change: B3dmWriter for writing to file removed, use b3dm.ToBytes() instead

2020-07-01: Version 0.9 adding 8 byte padding rules

2019-07-10: Version 0.8

- adding featuretable / batchtable support (json and binary formats) for reading and writing b3dm tiles

2019-07-05: Version 0.7

2019-05-16: Version 0.6

2019-05-09: version 0.5

- Added pack glb file to b3dm

2019-01-09: version 0.4.1

- Added parsing FeatureTableJson, FeatureTableBinary, BatchTableJson, batchTableBinary

2019-01-09: version 0.4

  - Removed parsing Glb code

  - Added helper methode GltfVersionChecker.GetGlbVersion(b3dm.GlbData) for checking version of glTF data

  - Added b3dm headers FeatureTableJsonByteLength, FeatureTableBinaryByteLength, BatchTableJsonByteLength, batchTableBinaryByteLength. 

