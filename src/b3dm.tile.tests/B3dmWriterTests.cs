using Gltf.Core;
using NUnit.Framework;
using System.IO;
using System.Numerics;

namespace B3dm.Tile.Tests
{
    public class B3dmWriterTests
    {
        [Test]
        public void WriteB3dmTest()
        {
            // arrange
            var tempPath = Path.GetTempPath();
            var buildingWkb = File.OpenRead(@"testfixtures/building.wkb");

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
            B3dmWriter.WriteB3dm(tempPath + "7.b3dm", b3dm);
            Assert.IsTrue(true);
        }
    }
}
