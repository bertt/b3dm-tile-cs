using System.IO;
using System.Text;

namespace B3dm.Tile
{
    public class B3dm
    {
        public B3dm()
        {
            B3dmHeader = new B3dmHeader();
            FeatureTableJson = string.Empty;
            BatchTableJson = string.Empty;
            FeatureTableJson = "{\"BATCH_LENGTH\":0}  ";
            FeatureTableBinary = new byte[0];
            BatchTableBinary = new byte[0];
        }

        public B3dm(byte[] glb): this()
        {
            GlbData = glb;
        }

        public B3dmHeader B3dmHeader { get; set; }
        public string FeatureTableJson { get; set; }
        public byte[] FeatureTableBinary { get; set; }
        public string BatchTableJson { get; set; }
        public byte[] BatchTableBinary { get; set; }
        public byte[] GlbData { get; set; }

        public byte[] ToBytes()
        {
            var header_length = 28;

            var featureTableJson = BufferPadding.AddPadding(FeatureTableJson, header_length);
            var batchTableJson = BufferPadding.AddPadding(BatchTableJson);
            var featureTableBinary = BufferPadding.AddPadding(FeatureTableBinary);
            var batchTableBinary = BufferPadding.AddPadding(BatchTableBinary);

            B3dmHeader.ByteLength = GlbData.Length + header_length + featureTableJson.Length + batchTableJson.Length + batchTableBinary.Length + FeatureTableBinary.Length;

            B3dmHeader.FeatureTableJsonByteLength = featureTableJson.Length;
            B3dmHeader.BatchTableJsonByteLength = batchTableJson.Length;
            B3dmHeader.FeatureTableBinaryByteLength =featureTableBinary.Length;
            B3dmHeader.BatchTableBinaryByteLength = batchTableBinary.Length;

            var memoryStream = new MemoryStream();
            var binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write(B3dmHeader.AsBinary());
            binaryWriter.Write(Encoding.UTF8.GetBytes(featureTableJson));
            if (featureTableBinary != null) {
                binaryWriter.Write(featureTableBinary);
            }
            binaryWriter.Write(Encoding.UTF8.GetBytes(batchTableJson));
            if (batchTableBinary != null) {
                binaryWriter.Write(batchTableBinary);
            }
            binaryWriter.Write(GlbData);
            binaryWriter.Flush();
            binaryWriter.Close();
            return memoryStream.ToArray();
        }
    }
}
