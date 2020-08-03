using System;
using System.IO;
using B3dm.Tile;

namespace sample_wkb_2_b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample conversion from glb to b3dm.");
            var inputfile = @"testfixtures/building.glb";
            var buildingGlb = File.ReadAllBytes(inputfile);
            var b3dm = new B3dm.Tile.B3dm(buildingGlb);
            var bytes = b3dm.ToBytes();
            var fs = new FileStream("test.b3dm", FileMode.Create, FileAccess.Write);
            fs.Write(bytes, 0, bytes.Length);
            Console.WriteLine($"File building.b3dm is written...");
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }
}