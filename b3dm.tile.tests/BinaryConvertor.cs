using System.IO;

namespace B3dm.Tile.Tests
{
    public static class BinaryConvertor
    {
        public static byte[] ToBinary(float[] array1)
        {
            var ms = new MemoryStream();
            var binaryWriter = new BinaryWriter(ms);
            foreach (var f in array1)
            {
                binaryWriter.Write(f);
            }
            var bytes = ms.ToArray();
            return bytes;
        }
    }
}
