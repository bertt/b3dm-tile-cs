using System;
using System.IO;
using System.Text;

namespace B3dm.Tile
{
    public static class B3dmParser
    {
        public static string ParseMagicHeader(Stream stream)
        {
            var reader = new FastBinaryReader(stream);

            // first 4 bytes must be 'b3dm' otherwise its not a b3dm file
            var s = new StringBuilder();
            for (var i = 0; i < 4; i++)
            {
                s.Append(Convert.ToChar(reader.ReadByte()));
            }

            return s.ToString();
        }
    }
}
