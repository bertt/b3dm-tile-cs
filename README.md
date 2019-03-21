# b3dm-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

Batched 3D specification: https://github.com/AnalyticalGraphicsInc/3d-tiles/blob/master/specification/TileFormats/Batched3DModel/README.md

[![NuGet Status](http://img.shields.io/nuget/v/b3dm-tile.svg?style=flat)](https://www.nuget.org/packages/b3dm-tile/)

## Sample code for conversion b3dm -> glb:

In this sample a b3dm is read and written to GLB format and glTF/bin format. 

For unpacking the GLB library glTF2Loader (https://www.nuget.org/packages/glTF2Loader/1.1.3-alpha) is used.

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

Example glTF viewers for .glTF: 

- https://gltf-viewer.donmccurdy.com/

- glTF Validator: http://github.khronos.org/glTF-Validator/

- Visual Studio Code: https://github.com/AnalyticalGraphicsInc/gltf-vscode

<img src="gltf.png"/>

## Dependencies

- NETStandard.Library 2.0.3

## History

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

## b3dm tooling

- Build:

```
$ cd b3dm-tile-cs\b3dm.tooling
$ dotnet pack
$ dotnet tool install --global --add-source ./nupkg b3dm.tooling
```

or update:

```
$ dotnet tool update --global --add-source ./nupkg b3dm.tooling
```

- run:

1] Command Info b3dm_file gives header info about b3dm file

Example:

```
$ b3dm info test.b3dm
```

2] Command unpack b3dm_filename unpacks a b3dm file to GLB format

Example:

```
$ b3dm unpack test.b3dm

b3dm version: 1
glTF asset generator: py3dtiles
glTF version: 2.0
Buffer bytes: 1848
Glb created test.glb
```

## Glt2Loader sample code

```
// save gltf bin and gltf file
File.WriteAllBytes($"building.bin", gltf.Body);
gltf.Gltf.SaveModel($"building.gltf");

```