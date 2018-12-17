# b3dm-cs

.NET Standard 2.0 Library for (de)serializing B3dm files to/from glTF

Sample code:

```
const string testfile = "ConsoleApp.testfixtures.1311.b3dm";
var b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
Console.WriteLine($"Start parsing {testfile}...");
var b3dm = B3dmParser.ParseB3dm(b3dmfile);
Console.WriteLine($"End parsing {testfile}.");

var bin = b3dm.Glb.GltfModelBin;
var json = b3dm.Glb.GltfModelJson;

var gltf = JsonConvert.DeserializeObject<Gltf>(json);
```
