# b3dm-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

Batched 3D specification: https://github.com/AnalyticalGraphicsInc/3d-tiles/blob/master/specification/TileFormats/Batched3DModel/README.md

[![NuGet Status](http://img.shields.io/nuget/v/b3dm-tile.svg?style=flat)](https://www.nuget.org/packages/b3dm-tile/)

## Sample code for conversion b3dm -> glb:

```
string infile = "testfixtures/1311.b3dm";
string outfile = "test.glb";

var stream = File.OpenRead(infile);
var b3dm = B3dmParser.ParseB3dm(stream);

var fs = File.Create(outfile);
var bw = new BinaryWriter(fs);
bw.Write(b3dm.GlbData);
bw.Close();
```

Example glTF viewer for test.gltf: https://gltf-viewer.donmccurdy.com/

glTF Validator: http://github.khronos.org/glTF-Validator/

<img src="gltf.png"/>

## Dependencies

NETStandard.Library 2.0.3

## History

2019-01-09: version 0.4.1

- Added parsing FeatureTableJson, FeatureTableBinary, BatchTableJson, batchTableBinary

2019-01-09: version 0.4

  - Removed parsing Glb code

  - Added helper methode GltfVersionChecker.GetGlbVersion(b3dm.GlbData) for checking version of glTF data

  - Added b3dm headers FeatureTableJsonByteLength, FeatureTableBinaryByteLength, BatchTableJsonByteLength, batchTableBinaryByteLength. 

## Benchmark

```
BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.194 (1809/October2018Update/Redstone5)
Intel Core i7-6820HQ CPU 2.70GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host]     : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT


                  Method |     Mean |     Error |    StdDev |
------------------------ |---------:|----------:|----------:|
 ParseB3dmTileFromStream | 67.98 us | 0.5034 us | 0.4709 us |
```
