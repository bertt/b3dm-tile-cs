using NUnit.Framework;
using System.IO;

namespace B3dm.Tile.Tests
{
    public class B3dmReaderTests
    {
        Stream b3dmfile;

        [SetUp]
        public void Setup()
        {
            b3dmfile = File.OpenRead(@"testfixtures/py3dtiles_test_build_1.b3dm");
            Assert.IsTrue(b3dmfile != null);
        }

        [Test]
        public void ReadB3dmTest()
        {
            // arrange
            var expectedMagicHeader = "b3dm";
            var expectedVersionHeader = 1;

            // act
            var b3dm = B3dmReader.ReadB3dm(b3dmfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == b3dm.B3dmHeader.Magic);
            Assert.IsTrue(expectedVersionHeader == b3dm.B3dmHeader.Version);
            Assert.IsTrue(b3dm.BatchTableJson.Length >= 0);
            Assert.IsTrue(b3dm.GlbData.Length > 0);
            var ms = new MemoryStream(b3dm.GlbData);
        }
    }
}