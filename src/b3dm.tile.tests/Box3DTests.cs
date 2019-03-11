using System;
using Gltf.Core;
using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class Box3DTests
    {
        [Test]
        public void TestParsingBox3d()
        {
            // Arrange
            // sample query: SELECT ST_3DExtent(geom) FROM tmp.tmp
            var box3d = "BOX3D(538813.873486521 6989034.28860435 42.2104432974011, 539356.37087624 6989407.07155631 63.7590549718589)";

            // Act
            var bb3d = GetBoundingBox3D(box3d);
            var center = bb3d.GetCenter();

            // Assert
            Assert.IsTrue(bb3d != null);
            Assert.IsTrue(bb3d.XMin == 538813.873486521);
            Assert.IsTrue(bb3d.YMin == 6989034.28860435);
            Assert.IsTrue(bb3d.ZMin == 42.2104432974011);
            Assert.IsTrue(bb3d.XMax == 539356.37087624);
            Assert.IsTrue(bb3d.YMax == 6989407.07155631);
            Assert.IsTrue(bb3d.ZMax == 63.7590549718589);

            Assert.IsTrue(center != null);
            Assert.IsTrue(center.X == 539085.1221813804);
            Assert.IsTrue(center.Y == 6989220.68008033);
            Assert.IsTrue(center.Z == 52.98474913463);
        }

        private BoundingBox3D GetBoundingBox3D(string sql)
        {
            var start = 6;
            var l = sql.Length - start - 1;
            var coords = sql.Substring(6, l);
            var split = coords.Split(", ");
            var minima = split[0].Split(' ');
            var maxima = split[1].Split(' ');
            var bb3d = new BoundingBox3D() {
                XMin = Double.Parse(minima[0]),
                YMin = Double.Parse(minima[1]),
                ZMin = Double.Parse(minima[2]),
                XMax = Double.Parse(maxima[0]),
                YMax = Double.Parse(maxima[1]),
                ZMax = Double.Parse(maxima[2]),
            };

            return bb3d;
        }
    }
}
