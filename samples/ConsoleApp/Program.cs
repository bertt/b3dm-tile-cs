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
            string path = "testfixtures/1311.b3dm";
            var memoryStream = new MemoryStream(File.ReadAllBytes(path));

            Console.WriteLine("B3dm tile sample application");
            Console.WriteLine($"Start parsing {path}...");
            var b3dm = B3dmParser.ParseB3dm(memoryStream);
            Console.WriteLine($"End parsing {path}.");

            var bin = b3dm.Glb.GltfModelBin;
            var json = b3dm.Glb.GltfModelJson;

            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }
}
