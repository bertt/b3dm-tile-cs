using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class B3dmParserTests
    {
        Stream b3dmfile;

        [SetUp]
        public void Setup()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.29.b3dm";
            b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            Assert.IsTrue(b3dmfile != null);
        }

        [Test]
        public void ParseB3dmTest()
        {
            // arrange
            var expectedMagicHeader = "b3dm";
            var expectedVersionHeader = 1;

            // act
            var b3dm = B3dmParser.ParseB3dm(b3dmfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == b3dm.Magic);
            Assert.IsTrue(expectedVersionHeader == b3dm.Version);
            // batchtable json contains string like {\"id\":[22,22,27,27,179,179,171,171,143,143,27,27,171,171,58,58,143,143,179,179]}
            Assert.IsTrue(b3dm.BatchTableJson.Length > 0); 
            Assert.IsTrue(b3dm.GlbData.Length > 0);
            Assert.IsTrue(GltfVersionChecker.GetGlbVersion(b3dm.GlbData) == 2);
        }
    }
}