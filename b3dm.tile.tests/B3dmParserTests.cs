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
            var b3dm = B3dmParser.ParseB3dm(b3dmfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == b3dm.Magic);
            Assert.IsTrue(expectedVersionHeader == b3dm.Version);
            Assert.IsTrue(b3dm.GlbData.Length > 0);
        }

        [Test]
        public void ParseB3dmTestIssue3WithDownload()
        {
            // issue https://github.com/bertt/b3dm-tile-cs/issues/3
            // arrange
            var url = "https://saturnus.geodan.nl/tomt/data/buildingtiles_texel/tiles/2.b3dm";
            var httpClient = new HttpClient();
            var bytes = httpClient.GetByteArrayAsync(url).Result;
            var stream = new MemoryStream(bytes);

            // act
            var b3dm = B3dmParser.ParseB3dm(stream);

            // assert
            Assert.IsTrue(b3dm != null);
        }


    }
}