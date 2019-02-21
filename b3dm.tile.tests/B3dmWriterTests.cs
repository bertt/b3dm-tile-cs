using NUnit.Framework;
using System;
using System.Reflection;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class B3dmWriterTests
    {
        [Test]
        public void WriteB3dmTest()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.building.wkb";
            var buildingWkb = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            Assert.IsTrue(g.GeometryType == GeometryType.PolyhedralSurface);
            var poly = ((PolyhedralSurface)g);
            Assert.IsTrue(poly.Geometries.Count==15);  // 15 polygons
            Assert.IsTrue(poly.Geometries[0].ExteriorRing.Points.Count == 5);  // 5 points in exteriorring of first geometry

            var bb = poly.GetBoundingBox();
            Assert.IsTrue(Math.Round(bb.XMin, 2) == -8.75);
            Assert.IsTrue(Math.Round(bb.YMin, 2) == -7.36);
            Assert.IsTrue(Math.Round(bb.XMax, 2) == 8.80);
            Assert.IsTrue(Math.Round(bb.YMax, 2) == 7.30);
            // q: do we need the min-z, max-z?

            //  ts = TriangleSoup.from_wkb_multipolygon(wkb)

            //#define the geometry's world transformation
            //    >>> transform = np.array([
            //    ...             [1, 0, 0, 1842015.125],
            //    ...             [0, 1, 0, 5177109.25],
            //    ...             [0, 0, 1, 247.87364196777344],
            //    ...             [0, 0, 0, 1]], dtype=float)

            // todo: convert to gltf
            // need: positions, normals, uv's, bbox

            // todo: convert to glb

            // todo: convert to b3dm
        }
    }
}
