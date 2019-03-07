using System.IO;

namespace Gltf.Core
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
            ms.Close();
            return bytes;
        }

        public static byte[] ToBinary(int[] array1)
        {
            var ms = new MemoryStream();
            var binaryWriter = new BinaryWriter(ms);
            foreach (var p in array1) {
                binaryWriter.Write(p);
            }
            var bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }
    }
}
