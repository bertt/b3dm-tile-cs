using System;
using System.IO;
using System.Reflection;
using B3dm.Tile;
using glTFLoader;

namespace b3dm.tooling
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

                Console.WriteLine($"b3dm v{versionString}");
                Console.WriteLine("-------------");
                Console.WriteLine("\nUsage:");
                Console.WriteLine("  b3dm unpack <b3dm-file>");
                Console.WriteLine("  b3dm pack <glb-file>");
                Console.WriteLine("  b3dm info <b3dm-file>");
                return;
            }

            if (args[0] == "unpack") {
                Unpack(args[1]);
            }
            else if (args[0] == "pack") {
                Pack(args[1]);
            }
            else if (args[0] == "info") {
                Info(args[1]);
            }
        }

        static void Pack(string file)
        {
            var f = File.ReadAllBytes(file);
            var b3dm = new B3dm.Tile.B3dm(f);
            var b3dmfile = Path.GetFileNameWithoutExtension(file) + ".b3dm";
            B3dmWriter.WriteB3dm(b3dmfile, b3dm);
            Console.WriteLine("B3dm created " + b3dmfile);
        }

        static void Unpack(string file)
        {
            var f = File.OpenRead(file);
            var b3dm = B3dmReader.ReadB3dm(f);
            Console.WriteLine("b3dm version: " + b3dm.B3dmHeader.Version);
            var stream = new MemoryStream(b3dm.GlbData);
            try {
                var gltf = Interface.LoadModel(stream);
                Console.WriteLine("glTF asset generator: " + gltf.Asset.Generator);
                Console.WriteLine("glTF version: " + gltf.Asset.Version);
                var bufferBytes = gltf.Buffers[0].ByteLength;
                Console.WriteLine("Buffer bytes: " + bufferBytes);
                var glbfile = Path.GetFileNameWithoutExtension(file) + ".glb";
                File.WriteAllBytes(glbfile, b3dm.GlbData);
                Console.WriteLine("Glb created " + glbfile);
            }
            catch (InvalidDataException ex) {
                Console.WriteLine("B3dm version not supported.");
                Console.WriteLine(ex.Message);
            }
        }

        static void Info(string file)
        {
            var f = File.OpenRead(file);
            var b3dm = B3dmReader.ReadB3dm(f);
            Console.WriteLine("b3dm version: " + b3dm.B3dmHeader.Version);
            var stream = new MemoryStream(b3dm.GlbData);
            var gltf = Interface.LoadModel(stream);
            Console.WriteLine(gltf.SerializeModel());
        }
    }
}
