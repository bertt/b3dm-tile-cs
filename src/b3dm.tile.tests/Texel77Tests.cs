using System.IO;
using System.Numerics;
using Gltf.Core;
using NUnit.Framework;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class Texel77Tests
    {
        [Test]
        public void ReadTexel77Wkt()
        {
            // arrange
            var file = File.ReadAllText(@".\testfixtures\texel77.wkt");

            // transform matrix according to py3dtiles tool for 388 records tmp.tmp
            var transform = new float[] { 539085.1221813804f, 6989220.68008033f, 52.98474913463f };
            var gltf = GltfReader.ReadFromWkt(file, transform);
            var g = Geometry.Deserialize<WktSerializer>(file);

            var fs = new FileStream(@"d:\aaa\b3dm\texel77.wkb",FileMode.OpenOrCreate);
            g.Serialize<WkbSerializer>(fs);

            var glb = GlbWriter.ToGlb(gltf);
            var b3dm = new B3dm();
            b3dm.GlbData = glb;
            B3dmWriter.WriteB3dm(@"d:\aaa\b3dm\texel77.b3dm", b3dm);
            B3dmWriter.WriteGlb(@"d:\aaa\b3dm\texel77.glb", b3dm);
            Assert.IsTrue(true);
        }
    }
}
