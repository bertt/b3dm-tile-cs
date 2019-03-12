using Gltf.Core;
using NUnit.Framework;
using System.IO;

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

            var transform = Transformer.GetTransform(1842015.125f, 5177109.25f, 247.87364196777344f);
            var gltf = GltfReader.ReadFromWkb(buildingWkb, transform);
            var glb = Packer.Pack(gltf);
            var b3dm = new B3dm();
            b3dm.GlbData = glb;
            B3dmWriter.WriteB3dm(tempPath + "7.b3dm", b3dm);
            Assert.IsTrue(true);
        }
    }
}
