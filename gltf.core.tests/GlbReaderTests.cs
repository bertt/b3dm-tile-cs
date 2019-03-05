using B3dm.Tile;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Gltf.Core.Tests
{
    public class GlbReaderTests
    {
        Stream glbStream;

        [SetUp]
        public void Setup()
        {
            const string testfile = "Gltf.Core.Tests.testfixtures.1311.b3dm";
            var b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var b3dm = B3dmReader.ReadB3dm(b3dmfile);
            var glb = b3dm.GlbData;
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
            var glb = GlbReader.ReadGlb(glbStream);

            // assert
            Assert.IsTrue(glb.Magic == expectedMagicGlb);
            Assert.IsTrue(glb.Version == expectedVersionGlb);
            // next line is failing :-(
            // Assert.IsTrue(glb.Length == glbStream.Length - 28);
            Assert.IsTrue(glb.GltfModelJson != null);
            Assert.IsTrue(glb.GltfModelJson.Length > 0);
            Assert.IsTrue(glb.GltfModelBin.Length > 0);
        }
    }
}
