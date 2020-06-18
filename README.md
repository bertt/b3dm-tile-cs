# b3dm-tile-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

Batched 3D specification: https://github.com/AnalyticalGraphicsInc/3d-tiles/blob/master/specification/TileFormats/Batched3DModel/README.md

[![NuGet Status](http://img.shields.io/nuget/v/b3dm-tile.svg?style=flat)](https://www.nuget.org/packages/b3dm-tile/)

## Sample code for conversion b3dm -> glb:

In this sample a B3DM tile is converted to GLB format. 

For glTF reading the SharpGLTF library (https://github.com/vpenades/SharpGLTF) is used.

Sample code:

```
var inputfile = @"testfixtures/662.b3dm";
var outputfile = Path.GetFileNameWithoutExtension(inputfile) + ".glb";
var f = File.OpenRead(inputfile);
var b3dm = B3dmReader.ReadB3dm(f);
var stream = new MemoryStream(b3dm.GlbData);
var gltf = ModelRoot.ReadGLB(stream, new ReadSettings());
gltf.SaveGLB(outputfile);
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

- SharpGLTF https://github.com/vpenades/SharpGLTF

- NETStandard.Library 2.0.3

## Benchmark

```
|                  Method |     Mean |    Error |   StdDev |   Median | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------------------ |---------:|---------:|---------:|---------:|------------:|------------:|------------:|--------------------:|
| ParseB3dmTileFromStream | 70.17 us | 2.062 us | 6.079 us | 68.53 us |     20.3857 |           - |           - |            84.98 KB |```
```

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

