using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class TransformerTests
    {
        [Test]
        public void TransformTest()
        {
            var result = Transformer.GetTransform(539085.1221813804f, 6989220.68008033f, 52.98474913463f);
            Assert.IsTrue(result.Length == 16);
        }
    }
}
