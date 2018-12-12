using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class B3dmTileParserTests
    {
        Stream b3dmfile;

        [SetUp]
        public void Setup()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.1311.b3dm";
            b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            Assert.IsTrue(b3dmfile != null);
        }

        [Test]
        public void FirstParsing()
        {
            // arrange
            var expectedMagicHeader = "b3dm";
            var expectedVersionHeader = 1;

            // act
            var actualHeader = B3dmParser.ParseHeader(b3dmfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == actualHeader.Magic);
            Assert.IsTrue(expectedVersionHeader == actualHeader.Version);
        }
    }
}