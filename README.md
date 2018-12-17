# b3dm-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

[![NuGet Status](http://img.shields.io/nuget/v/b3dm-tile.svg?style=flat)](https://www.nuget.org/packages/b3dm-tile/)


Sample code:

```
string path = "testfixtures/1311.b3dm";
var memoryStream = new MemoryStream(File.ReadAllBytes(path));

Console.WriteLine("B3dm tile sample application");
Console.WriteLine($"Start parsing {path}...");
var b3dm = B3dmParser.ParseB3dm(memoryStream);
Console.WriteLine($"End parsing {path}.");

var bin = b3dm.Glb.GltfModelBin;
var json = b3dm.Glb.GltfModelJson;
```
