using NUnit.Framework;
using System.Collections.Generic;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class BinaryConvertorTests
    {
        [Test]
        public void FloatArrayBinaryConvertorTest()
        {
            // in python:
            // b''.join(np.array([[1.0], [2.0]], np.float32))
            // b'\x00\x00\x80?\x00\x00\x00@'
            var array1 = new float[] { 1.0f, 2.0f };
            var bytes = BinaryConvertor.ToBinary(array1);
            Assert.IsTrue(bytes.Length == 8);
        }

        [Test]
        public void TraingleBinayConvertorTest()
        {
            var triangles = new List<Triangle>();
            var p0 = new Point(0, 0, 1);
            var p1 = new Point(1, 0, 2);
            var p2 = new Point(1, 0, 3);
            var t = new Triangle(p0, p1, p2);
            triangles.Add(t);

            var bytes = BinaryConvertor.ToBinary(triangles);
            Assert.IsTrue(bytes.Length > 0);
        }
    }
}
