using System;
using System.IO;
using System.Linq;
using System.Text;

namespace B3dm.Tile
{
    public class B3dmHeader
    {
        public string Magic { get; set; }
        public int Version { get; set; }
        public int ByteLength { get; set; }
        public int FeatureTableJsonByteLength { get; set; }
        public int FeatureTableBinaryByteLength { get; set; }
        public int BatchTableJsonByteLength { get; set; }
        public int BatchTableBinaryByteLength { get; set; }

        public B3dmHeader()
        {
            Magic = "b3dm";
            Version = 1;
            FeatureTableJsonByteLength = 0;
            FeatureTableBinaryByteLength = 0;
            BatchTableJsonByteLength = 0;
            BatchTableBinaryByteLength = 0;
        }

        public B3dmHeader(BinaryReader reader)
        {
            Magic = Encoding.UTF8.GetString(reader.ReadBytes(4));
            Version = (int)reader.ReadUInt32();
            ByteLength = (int)reader.ReadUInt32();
            FeatureTableJsonByteLength = (int)reader.ReadUInt32();
            FeatureTableBinaryByteLength = (int)reader.ReadUInt32();
            BatchTableJsonByteLength = (int)reader.ReadUInt32();
            BatchTableBinaryByteLength = (int)reader.ReadUInt32();
        }

        public byte[] AsBinary()
        {
            var magicBytes = Encoding.UTF8.GetBytes(Magic);
            var versionBytes = BitConverter.GetBytes(Version);
            var byteLengthBytes = BitConverter.GetBytes(ByteLength);
            var featureTableJsonByteLengthBytes = BitConverter.GetBytes(FeatureTableJsonByteLength);
            var featureTableBinaryByteLengthBytes = BitConverter.GetBytes(FeatureTableBinaryByteLength);
            var batchTableJsonByteLength = BitConverter.GetBytes(BatchTableJsonByteLength);
            var batchTableBinaryByteLength = BitConverter.GetBytes(BatchTableBinaryByteLength);

            return magicBytes.
                Concat(versionBytes).
                Concat(byteLengthBytes).
                Concat(featureTableJsonByteLengthBytes).
                Concat(featureTableBinaryByteLengthBytes).
                Concat(batchTableJsonByteLength).
                Concat(batchTableBinaryByteLength).
                ToArray();
        }
    }
}
