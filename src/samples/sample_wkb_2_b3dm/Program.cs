using System;
using System.IO;
using B3dm.Tile;
using Wkb2Gltf;
using Wkx;

namespace sample_wkb_2_b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample conversion from wkb to b3dm.");
            var inputfile = @"testfixtures/building.wkb";
            var f = File.OpenRead(inputfile);
            var g = Geometry.Deserialize<WkbSerializer>(f);
            var translation = new double[] { 539085.1, 6989220.68, 52.98 };
            var glb = GeometryToGlbConvertor.Convert(g, translation);
            var b3dm = GlbToB3dmConvertor.Convert(glb);
            B3dmWriter.WriteB3dm("building.b3dm", b3dm);
            Console.WriteLine($"File building.b3dm is written...");
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }
    }
}