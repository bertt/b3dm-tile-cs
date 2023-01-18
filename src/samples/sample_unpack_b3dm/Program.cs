using System;
using System.IO;
using B3dmCore;
using SharpGLTF.Schema2;

namespace sample_read_b3dm;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Sample code for unpacking a b3dm to glb and glTF/bin file");
        var inputfile = @"testfixtures/662.b3dm";
        var outputfile = Path.GetFileNameWithoutExtension(inputfile) + ".glb";
        var f = File.OpenRead(inputfile);
        var b3dm = B3dmReader.ReadB3dm(f);
        Console.WriteLine("b3dm version: " + b3dm.B3dmHeader.Version);
        var stream = new MemoryStream(b3dm.GlbData);
        var gltf = ModelRoot.ReadGLB(stream, new ReadSettings());
        Console.WriteLine("glTF asset generator: " + gltf.Asset.Generator);
        Console.WriteLine("glTF version: " + gltf.Asset.Version);
        gltf.SaveGLB(outputfile);
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
    }
}
