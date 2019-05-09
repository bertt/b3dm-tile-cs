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
            var inputfile = @"testfixtures/building.b3dm";
            var outputfile = Path.GetFileNameWithoutExtension(inputfile) + ".glb";
            var f = File.OpenRead(inputfile);
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
            File.WriteAllBytes(outputfile, b3dm.GlbData);
            Interface.Unpack(outputfile, "testfixtures");
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
