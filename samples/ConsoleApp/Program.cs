using B3dm.Tile;
using glTFLoader.Schema;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("B3dm tile sample application");

            const string testfile = "ConsoleApp.testfixtures.1311.b3dm";
            var b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            Console.WriteLine($"Start parsing {testfile}...");
            var b3dm = B3dmParser.ParseB3dm(b3dmfile);
            Console.WriteLine($"End parsing {testfile}.");

            var bin = b3dm.Glb.GltfModelBin;
            var json = b3dm.Glb.GltfModelJson;

            var gltf = JsonConvert.DeserializeObject<Gltf>(json);

            // todo: do something with gltf bin object?
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }
}
