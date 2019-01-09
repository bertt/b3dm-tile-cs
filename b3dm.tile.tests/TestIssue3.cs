using NUnit.Framework;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class TestIssue3
    {
        [Test]
        public void ParseB3dmTestIssue3()
        {
            // issue https://github.com/bertt/b3dm-tile-cs/issues/3
            // arrange
            // texel tiles does not have correct gltf version
            const string texeltestfile = "B3dm.Tile.Tests.testfixtures.2.b3dm";
            var streamtexel = Assembly.GetExecutingAssembly().GetManifestResourceStream(texeltestfile);
            var b3dmtextel = B3dmParser.ParseB3dm(streamtexel);
            // eurgh what is 576404002? it's not 2...
            Assert.IsTrue(GltfVersionChecker.GetGlbVersion(b3dmtextel.GlbData) == 576404002);

            // amsterdam tile does have correct gltf version (2)
            const string amstestfile = "B3dm.Tile.Tests.testfixtures.1311.b3dm";
            var streamams= Assembly.GetExecutingAssembly().GetManifestResourceStream(amstestfile);
            var b3dmams = B3dmParser.ParseB3dm(streamams);
            Assert.IsTrue(GltfVersionChecker.GetGlbVersion(b3dmams.GlbData) == 2);
        }
    }
}
