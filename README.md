# b3dm-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

Batched 3D specification: https://github.com/AnalyticalGraphicsInc/3d-tiles/blob/master/specification/TileFormats/Batched3DModel/README.md

[![NuGet Status](http://img.shields.io/nuget/v/b3dm-tile.svg?style=flat)](https://www.nuget.org/packages/b3dm-tile/)

## Sample code for conversion b3dm -> glb:

In this sample a b3dm is read and written to GLB format and glTF/bin format. 

For unpacking the GLB library glTF2Loader (https://www.nuget.org/packages/glTF2Loader/1.1.3-alpha) is used.

Sample code:

```
Console.WriteLine("Sample code for unpacking a b3dm to glb and glTF/bin file");
var f = File.OpenRead(@"testfixtures/51.b3dm");
var b3dm = B3dmReader.ReadB3dm(f);
Console.WriteLine("b3dm version: " + b3dm.B3dmHeader.Version);
var stream = new MemoryStream(b3dm.GlbData);
var gltf = Interface.LoadModel(stream);
Console.WriteLine("glTF asset generator: " + gltf.Asset.Generator);
Console.WriteLine("glTF version: " + gltf.Asset.Version);
var model = gltf.SerializeModel();
Console.WriteLine("glTF model: " + model);
var bufferBytes = gltf.Buffers[0].ByteLength;
Console.WriteLine("Buffer bytes: " + bufferBytes);
File.WriteAllBytes("testfixtures/51.glb", b3dm.GlbData);
Interface.Unpack("testfixtures/51.glb", "testfixtures");
```

## Sample code for conversion glb -> b3dm:

```
var inputfile = @"testfixtures/building.glb";
var buildingGlb = File.ReadAllBytes(inputfile);
var b3dm = new B3dm.Tile.B3dm(buildingGlb);
B3dmWriter.WriteB3dm($"building.b3dm", b3dm);
```

Example glTF viewers for .glTF: 

- https://gltf-viewer.donmccurdy.com/

- glTF Validator: http://github.khronos.org/glTF-Validator/

- Visual Studio Code: https://github.com/AnalyticalGraphicsInc/gltf-vscode

<img src="gltf.png"/>

## Dependencies

- NETStandard.Library 2.0.3

## History

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

## Benchmark

```
|                  Method |     Mean |    Error |   StdDev |   Median | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |---------:|---------:|---------:|---------:|------------:|------------:|------------:|--------------------:|
| ParseB3dmTileFromStream | 70.17 us | 2.062 us | 6.079 us | 68.53 us |     20.3857 |           - |           - |            84.98 KB |```
```

## Glt2Loader sample code

```
// save gltf bin and gltf file
File.WriteAllBytes($"building.bin", gltf.Body);
gltf.Gltf.SaveModel($"building.gltf");

```