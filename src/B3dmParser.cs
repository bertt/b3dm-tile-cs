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

            var version = reader.ReadUInt32();

            var header = new B3dmHeader
            {
                Magic = s.ToString(),
                Version = Convert.ToInt32(version)
            };
            return header;
        }
    }
}
