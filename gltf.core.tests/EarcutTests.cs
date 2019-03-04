using NUnit.Framework;
using System.Collections.Generic;
using Wkx;

namespace Gltf.Core.Tests
{
    public class EarcutTests
    {
        [Test]
        public void EartBuildingFirstPolygonTest()
        {
            // arrange
            var poly = GetFirstBuildingPolygon();

            var points = new List<double>();
            foreach(var p in poly.ExteriorRing.Points)
            {
                points.Add((double)p.X);
                points.Add((double)p.Y);
            }

            // act
            var res = Earcut.Tessellate(points,new List<int>());

            // assert
            Assert.IsTrue(res.Count == 6);
            Assert.IsTrue(res[0] == 1);
            Assert.IsTrue(res[1] == 4);
            Assert.IsTrue(res[2] == 3);
            Assert.IsTrue(res[3] == 3);
            Assert.IsTrue(res[4] == 2);
            Assert.IsTrue(res[5] == 1);
        }


        private Polygon GetFirstBuildingPolygon()
        {
            // arrange (this is first polygon of building.wkb)
            var p0 = new Point(-7.3552320003509521, 1.9086260322265503);
            var p1 = new Point(1.4992249999195337, 1.9086260322265503);
            var p2 = new Point(1.4992249999195337, -2.053857967773439);
            var p3 = new Point(-7.3552320003509521, -2.053857967773439);
            var p4 = new Point(-7.3552320003509521, 1.9086260322265503);

            return GetPoly(p0, p1, p2, p3, p4);
        }

        private static Polygon GetPoly(Point p0, Point p1, Point p2, Point p3, Point p4)
        {
            var polygon = new Polygon();
            polygon.ExteriorRing.Points.Add(p0);
            polygon.ExteriorRing.Points.Add(p1);
            polygon.ExteriorRing.Points.Add(p2);
            polygon.ExteriorRing.Points.Add(p3);
            polygon.ExteriorRing.Points.Add(p4);
            return polygon;
        }
    }
}
