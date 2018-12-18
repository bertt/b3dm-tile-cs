# b3dm-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

[![NuGet Status](http://img.shields.io/nuget/v/b3dm-tile.svg?style=flat)](https://www.nuget.org/packages/b3dm-tile/)

## Sample code for conversion b3dm -> glb:

```
string infile = "testfixtures/1311.b3dm";
string outfile = "test.glb";

var memoryStream = new MemoryStream(File.ReadAllBytes(infile));
var b3dm = B3dmParser.ParseB3dm(memoryStream);

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
