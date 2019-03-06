using System.IO;
using System.Text;

namespace Gltf.Core
{
    public class GltfReader
    {
        public static Gltf1 ReadGltf(Stream stream)
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

            return new Gltf1
            {
                Magic = magic,
                Version = version,
                GltfModelJson = json,
                GltfModelBin = bin,
                Length = length
            };
        }
    }
}
