using System.IO;
using System.Text;

namespace Gltf.Core
{
    public static class Packer
    {
        public static byte[] Pack(Gltf1 gltf)
        {
            var ms = new MemoryStream();
            var binaryWriter = new BinaryWriter(ms);
            binaryWriter.Write(gltf.Magic);
            binaryWriter.Write(gltf.Version);
            binaryWriter.Write(gltf.Length);
            var gltfModelJsonBytes = Encoding.UTF8.GetBytes(gltf.GltfModelJson);
            binaryWriter.Write(gltfModelJsonBytes.Length); // chunklength
            binaryWriter.Write(1313821514); // chunkformat
            binaryWriter.Write(gltfModelJsonBytes); // chunkformat
            binaryWriter.Write(gltf.GltfModelBin.Length); // chunklength2
            binaryWriter.Write(5130562); // chunkformat
            binaryWriter.Write(gltf.GltfModelBin); // chunklength2
            binaryWriter.Flush();
            return ms.ToArray();
        }

        public static Gltf1 Unpack(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);

            var magic = binaryReader.ReadUInt32();
            var version = binaryReader.ReadUInt32();
            var length = binaryReader.ReadUInt32();

            var chunkLength = binaryReader.ReadUInt32();
            var chunkFormat = binaryReader.ReadUInt32();

            // read the first chunk (must be format json)
            var data = binaryReader.ReadBytes((int)chunkLength);
            var json = Encoding.UTF8.GetString(data);

            // read the second chunk (must be format binary)
            var chunkLength1 = binaryReader.ReadUInt32();
            var chunkFormat1 = binaryReader.ReadUInt32();
            var bin = binaryReader.ReadBytes((int)chunkLength1);

            return new Gltf1 {
                Magic = magic,
                Version = version,
                GltfModelJson = json,
                GltfModelBin = bin
            };
        }

    }
}
