using NUnit.Framework;

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
    }
}
