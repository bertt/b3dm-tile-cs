using NUnit.Framework;
using Gltf.Core;

namespace Gltf.Core.Tests
{
    public class BinaryConvertorTests
    {
        [Test]
        public void FloatArrayBinaryConvertorTest()
        {
            var array = new float[] { 1.0f, 2.0f };
            var bytes = BinaryConvertor.ToBinary(array);
            Assert.IsTrue(bytes.Length == 8);
        }
    }
}
