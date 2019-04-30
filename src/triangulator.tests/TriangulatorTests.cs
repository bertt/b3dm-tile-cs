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
            var ground = GetSquare(-1,-1,0,1,1,0);
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

        private static Polygon GetSquare(double minx, double miny,double minz,double maxx,double maxy, double maxz)
        {
            var groundRing = new LinearRing();
            groundRing.Points.Add(new Point(minx, miny, minz));
            groundRing.Points.Add(new Point(minx, maxy, minz));
            groundRing.Points.Add(new Point(maxx, maxy, minz));
            groundRing.Points.Add(new Point(maxx, miny, minz));
            groundRing.Points.Add(new Point(minx, miny, minz));

            var ground = new Polygon(groundRing);
            return ground;
        }

        [Test]
        public void MultiPolygonTriangulationTest()
        {
            // arrange
            var p1 = GetSquare(-1, -1, 0, 1, 1, 0);
            var p2 = GetSquare(2, 2, 0, 3, 3, 0);
            var mp = new MultiPolygon(new List<Polygon>() { p1, p2 });

            // act
            var triangles = Triangulator.GetTriangles(mp);

            // assert
            Assert.IsTrue(triangles.Count == 4);
        }
    }
}
