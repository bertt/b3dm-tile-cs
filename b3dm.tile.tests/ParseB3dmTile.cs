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
        }

        [Test]
        public void FirstParsing()
        {
            Assert.IsTrue(b3dmfile != null);
            B3dmParser.Parse(b3dmfile);
            Assert.IsTrue(true);
        }
    }
}