using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class B3dmHeaderTests
    {
        [Test]
        public void HeaderToBinaryTests()
        {
            var b3dmHeader = new B3dmHeader {
                ByteLength = 2952
            };
            var binary = b3dmHeader.AsBinary();
            Assert.IsTrue(binary.Length == 28);
        }
    }
}
