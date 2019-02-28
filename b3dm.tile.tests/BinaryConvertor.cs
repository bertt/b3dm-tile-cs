using System.IO;
using System.Numerics;

namespace B3dm.Tile.Tests
{
    public static class BinaryConvertor
    {
        public static byte[] ToBinary(float[] array1)
        {
            var ms = new MemoryStream();
            var binaryWriter = new BinaryWriter(ms);
            foreach (var p in array1)
            {
                binaryWriter.Write(p);
            }
            var bytes = ms.ToArray();
            return bytes;
        }
    }
}
