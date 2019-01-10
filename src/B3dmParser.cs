using System.IO;
using System.Text;

namespace B3dm.Tile
{
    public static class B3dmParser
    {
        public static B3dm ParseB3dm(Stream stream)
        {
            using (var reader = new BinaryReader(stream)) {

                // first 4 bytes must be 'b3dm' otherwise its not a b3dm file
                var magic = Encoding.UTF8.GetString(reader.ReadBytes(4));

                // version should be 1 otherwise its not a b3dm file
                var version = (int)reader.ReadUInt32();
                var headerByteLength = 28;
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
                // var glbBuffer = reader.ReadBytes(bytelength - headerByteLength- featureTableJsonByteLength - featureTableBinaryByteLength - batchTableJsonByteLength - batchTableBinaryByteLength);

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
