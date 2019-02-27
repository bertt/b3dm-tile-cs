using System.Collections.Generic;
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


        public static byte[] ToBinary(List<Triangle> triangles)
        {
            var floats = new List<float>();
            foreach (var triangle in triangles)
            {
                floats.AddRange(triangle.GetP0().ToXYZFloatArray());
                floats.AddRange(triangle.GetP1().ToXYZFloatArray());
                floats.AddRange(triangle.GetP2().ToXYZFloatArray());
            }
            var bytes = BinaryConvertor.ToBinary(floats.ToArray());
            return bytes;
        }
    }
}
