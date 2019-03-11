using System.IO;
using Gltf.Core;
using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class Texel77Tests
    {
        [Test]
        public void ReadTexel77Wkt()
        {
            // arrange
            var file = File.ReadAllText(@".\testfixtures\texel77.wkt");
            var gltf = GltfReader.ReadFromWkt(file);
            var glb = GlbWriter.ToGlb(gltf);
            var b3dm = new B3dm();
            b3dm.GlbData = glb;
            B3dmWriter.WriteB3dm(@"d:\aaa\b3dm\texel77.b3dm", b3dm);
            B3dmWriter.WriteGlb(@"d:\aaa\b3dm\texel77.glb", b3dm);
            Assert.IsTrue(true);
        }
    }
}
