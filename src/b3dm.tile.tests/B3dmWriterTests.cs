using Gltf.Core;
using NUnit.Framework;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class B3dmWriterTests
    {
        [Test]
        public void WriteB3dmTest()
        {
            // arrange
            const string testfile = "B3dm.Tile.Tests.testfixtures.building.wkb";
            var buildingWkb = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var gltf = GltfReader.ReadFromWkb(buildingWkb);
            var glb = GlbWriter.ToGlb(gltf);
            var b3dm = new B3dm();
            b3dm.GlbData = glb;
            B3dmWriter.WriteB3dm(@"d:\aaa\b3dm\7.b3dm", b3dm);
            Assert.IsTrue(true);
        }
    }
}
