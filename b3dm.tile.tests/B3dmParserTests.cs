using NUnit.Framework;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class B3dmParserTests
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
        public void ParseB3dmTest()
        {
            // arrange
            var expectedMagicHeader = "b3dm";
            var expectedVersionHeader = 1;

            // act
            var b3dm = B3dmParser.ParseB3dm(b3dmfile, false);

            // assert
            Assert.IsTrue(expectedMagicHeader == b3dm.Magic);
            Assert.IsTrue(expectedVersionHeader == b3dm.Version);
            Assert.IsTrue(b3dm.GlbData.Length > 0);
        }
    }
}