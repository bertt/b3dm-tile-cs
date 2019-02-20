using System.IO;
using System.Text;

namespace B3dm.Tile
{
    public static class B3dmReader
    {
        public static B3dm ReadB3dm(Stream stream)
        {
            using (var reader = new BinaryReader(stream)) {

                var magic = Encoding.UTF8.GetString(reader.ReadBytes(4));
                var version = (int)reader.ReadUInt32();
                var bytelength = (int)reader.ReadUInt32();
                var featureTableJsonByteLength = (int)reader.ReadUInt32();
                var featureTableBinaryByteLength = (int)reader.ReadUInt32();
                var batchTableJsonByteLength = (int)reader.ReadUInt32();
                var batchTableBinaryByteLength = (int)reader.ReadUInt32();

                var featureTableJson = Encoding.UTF8.GetString(reader.ReadBytes(featureTableJsonByteLength));
                var featureTableBytes = reader.ReadBytes(featureTableBinaryByteLength);
                var batchTableJson = Encoding.UTF8.GetString(reader.ReadBytes(batchTableJsonByteLength));
                var batchTableBytes = reader.ReadBytes(batchTableBinaryByteLength);

                var glbLength = (int)(reader.BaseStream.Length - reader.BaseStream.Position);
                var glbBuffer = reader.ReadBytes(glbLength);

                var b3dm = new B3dm {
                    Magic = magic,
                    Version = version,
                    GlbData = glbBuffer,
                    FeatureTableJson = featureTableJson,
                    FeatureTableBinary = featureTableBytes,
                    BatchTableJson = batchTableJson,
                    BatchTableBinary = batchTableBytes
                };
                return b3dm;
            }
        }
    }
}
