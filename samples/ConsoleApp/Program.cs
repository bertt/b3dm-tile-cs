using System;
using System.IO;
using B3dm.Tile;

namespace ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            string infile = "testfixtures/1311.b3dm";
            string outfile = "test.glb";

            var memoryStream = new MemoryStream(File.ReadAllBytes(infile));
            Console.WriteLine("B3dm tile sample application");
            Console.WriteLine($"Start parsing {infile}...");
            var b3dm = B3dmParser.ParseB3dm(memoryStream);
            Console.WriteLine($"Start writing output to {outfile}.");

            var fs = File.Create(outfile);
            var bw = new BinaryWriter(fs);
            bw.Write(b3dm.GlbData);
            bw.Close();
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }
}
