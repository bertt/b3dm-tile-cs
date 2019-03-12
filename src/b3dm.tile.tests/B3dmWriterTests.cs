using Gltf.Core;
using NUnit.Framework;
using System.Numerics;
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

            var m = new Matrix4x4(1, 0, 0, 1842015.125f,
                0, 1, 0, 5177109.25f,
                0, 0, 1, 247.87364196777344f,
                0, 0, 0, 1
                );
            var transform = m.Flatten();

            var gltf = GltfReader.ReadFromWkb(buildingWkb, transform);
            var glb = Packer.Pack(gltf);
            var b3dm = new B3dm();
            b3dm.GlbData = glb;
            B3dmWriter.WriteB3dm(@"d:\aaa\b3dm\7.b3dm", b3dm);
            Assert.IsTrue(true);
        }
    }
}
