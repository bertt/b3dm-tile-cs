using System;
using System.IO;
using System.Text;

namespace B3dm.Tile
{
    public static class B3dmParser
    {
        public static B3dmHeader ParseHeader(Stream stream)
        {
            var reader = new FastBinaryReader(stream);

            // first 4 bytes must be 'b3dm' otherwise its not a b3dm file
            var s = new StringBuilder();
            for (var i = 0; i < 4; i++)
            {
                s.Append(Convert.ToChar(reader.ReadByte()));
            }

            // version should be 1 otherwise its not a b3dm file
            var version = reader.ReadUInt32();
            var headerByteLength = 28;
            var bytelength = reader.ReadUInt32();
            var featureTableJsonByteLength = reader.ReadUInt32();
            var featureTableBinaryByteLength = reader.ReadUInt32();
            var batchTableJsonByteLength = reader.ReadUInt32();
            var batchTableBinaryByteLength = reader.ReadUInt32();
            var batchLength = 0;

            var featureTableJsonByteOffset = headerByteLength;
            var featureTableBinaryByteOffset = featureTableJsonByteOffset + featureTableJsonByteLength;
            var batchTableJsonByteOffset = featureTableBinaryByteOffset + featureTableBinaryByteLength;
            var batchTableBinaryByteOffset = batchTableJsonByteOffset + batchTableJsonByteLength;
            var glbByteOffset = batchTableBinaryByteOffset + batchTableBinaryByteLength;

            var glbBuffer = reader.ReadBytes(bytelength);
            // todo: do something with glbbuffer

            var header = new B3dmHeader
            {
                Magic = s.ToString(),
                Version = Convert.ToInt32(version)
            };
            return header;
        }
    }
}
