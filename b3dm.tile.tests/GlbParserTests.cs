using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class GlbParserTests
    {
        Stream glbStream;

        [SetUp]
        public void Setup()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.1311.b3dm";
            var b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var b3dm = B3dmParser.ParseB3dm(b3dmfile);
            var glb = b3dm.Glb;
            glbStream = new MemoryStream(glb);
            Assert.IsTrue(glbStream != null);
        }

        [Test]
        public void ParseGlbTest()
        {
            // arrange
            var expectedMagicGlb = 0x46546C67;
            var expectedVersionGlb = 2;

            // act
            var glb = GlbParser.ParseGlb(glbStream);

            // assert
            Assert.IsTrue(glb.Magic == expectedMagicGlb);

            // folowing tests will fail...
            //  todo fix those tests
            Assert.IsTrue(glb.Version == expectedVersionGlb);
            Assert.IsTrue(glb.GltfModelJson!=null);
        }
    }
}
