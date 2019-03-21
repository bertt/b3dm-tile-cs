using System;
using System.IO;
using B3dm.Tile;
using glTFLoader;
using Wkb2Gltf.Core;
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
            var polyhedralsurface = ((PolyhedralSurface)g);
            var translation = new float[] { 539085.1f, 6989220.68f, 52.98f };
            var gltf = Gltf2Loader.GetGltf(polyhedralsurface, translation);
            File.WriteAllBytes($"building.bin", gltf.Body);
            gltf.Gltf.SaveModel($"building.gltf");
            gltf.Gltf.SaveBinaryModel(gltf.Body, @"building.glb");

            var glb = File.ReadAllBytes("building.glb");
            var b3dm = new B3dm.Tile.B3dm();
            b3dm.GlbData = glb;
            B3dmWriter.WriteB3dm("building.b3dm",b3dm);
            Console.ReadKey();

        }
    }

}
