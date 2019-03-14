using System;
using System.IO;
using B3dm.Tile;
using glTFLoader;

namespace sample_read_b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample code for unpacking a b3dm to glb and glTF/bin file");
            var f = File.OpenRead(@"testfixtures/51.b3dm");
            var b3dm = B3dmReader.ReadB3dm(f);
            Console.WriteLine("b3dm version: " + b3dm.B3dmHeader.Version);
            var stream = new MemoryStream(b3dm.GlbData);
            var gltf = Interface.LoadModel(stream);
            Console.WriteLine("glTF asset generator: " + gltf.Asset.Generator);
            Console.WriteLine("glTF version: " + gltf.Asset.Version);
            var model = gltf.SerializeModel();
            Console.WriteLine("glTF model: " + model);
            var bufferBytes = gltf.Buffers[0].ByteLength;
            Console.WriteLine("Buffer bytes: " + bufferBytes);
            File.WriteAllBytes("testfixtures/51.glb", b3dm.GlbData);
            Interface.Unpack("testfixtures/51.glb", "testfixtures");
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
