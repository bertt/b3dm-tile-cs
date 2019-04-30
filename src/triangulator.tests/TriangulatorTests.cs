using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Wkx;

namespace Triangulator.Tests
{
    public class TriangulatorTests
    {
        [Test]
        public void TriangulateTest()
        {
            // arrange
            var buildingWkb = File.OpenRead(@"testfixtures/building.wkb");
            var g = Wkx.Geometry.Deserialize<WkbSerializer>(buildingWkb);
            Assert.IsTrue(g.GeometryType == GeometryType.PolyhedralSurface);
            var polyhedralsurface = ((PolyhedralSurface)g);

            // act
            var triangles = Triangulator.GetTriangles(polyhedralsurface);

            // assert
            Assert.IsTrue(triangles.Count == 22);
        }

        [Test]
        public void CubeTriangulationTest()
        {
            // arrange
            var surface = new PolyhedralSurface();
            var ground = TestData.SimplePolygon(-1,-1,0,1,1,0);
            surface.Geometries.Add(ground);

            // act
            var triangles = Triangulator.GetTriangles(surface);

            // assert
            Assert.IsTrue(triangles.Count == 2);
            Assert.IsTrue(triangles[0].GetP0().Equals(new Point(-1, -1, 0)));
            Assert.IsTrue(triangles[0].GetP1().Equals(new Point(-1, 1, 0)));
            Assert.IsTrue(triangles[0].GetP2().Equals(new Point(1, -1, 0)));

            Assert.IsTrue(triangles[1].GetP0().Equals(new Point(1, 1, 0)));
            Assert.IsTrue(triangles[1].GetP1().Equals(new Point(1, -1, 0)));
            Assert.IsTrue(triangles[1].GetP2().Equals(new Point(-1, 1, 0)));
        }

        [Test]
        public void MultiPolygonTriangulationTest()
        {
            // arrange
            var p1 = TestData.SimplePolygon(-1, -1, 0, 1, 1, 0);
            var p2 = TestData.SimplePolygon(2, 2, 0, 3, 3, 0);
            var mp = new MultiPolygon(new List<Polygon>() { p1, p2 });

            // act
            var triangles = Triangulator.GetTriangles(mp);

            // assert
            Assert.IsTrue(triangles.Count == 4);
        }
    }
}
