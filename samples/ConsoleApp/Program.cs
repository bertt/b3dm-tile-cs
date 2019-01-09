using System;
using System.IO;
using B3dm.Tile;

namespace ConsoleApp
{
    static class Program
    {

        static void Main(string[] args)
        {
            string infile = "testfixtures/2.b3dm";
            string outfile = "2.glb";

            var stream=File.OpenRead(infile);
            Console.WriteLine("B3dm tile sample application");
            Console.WriteLine($"Start parsing {infile}...");
            var b3dm = B3dmParser.ParseB3dm(stream);
            Console.WriteLine($"Start writing output to {outfile}.");

            var fs = File.Create(outfile);
            var bw = new BinaryWriter(fs);
            bw.Write(b3dm.GlbData);
            bw.Close();

            var gltfVersion = GltfVersionChecker.GetGlbVersion(b3dm.GlbData);
            Console.WriteLine($"Gltf version: {gltfVersion}");
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }
}
