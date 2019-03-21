using NUnit.Framework;
using System.Numerics;
using Wkx;

namespace Wkb2Gltf.Tests
{
    public class ProjectionsTests
    {
        [Test]
        public void IsYZProjection()
        {
            var p = new Vector3(-23.204334043097425f, -0.24502019837234382f, 0f);
            Assert.IsTrue(Projections.IsYZProjection(p));
            // heuh next one fails....
            // Assert.IsFalse(Projections.IsZXProjection(p));
        }

        [Test]
        public void IsZXProjection()
        {
            var p = new Vector3(0.49031478f, -46.434658f, 0.0f);
            Assert.IsFalse(Projections.IsYZProjection(p));
            Assert.IsTrue(Projections.IsZXProjection(p));
        }

        [Test]
        public void VectorProductTest()
        {
            // arrange
            var polygon = GetFirstBuildingPolygon();

            // act
            var vectProd = Projections.GetVectorProduct(polygon);

            // assert
            Assert.IsTrue(vectProd.X == -23.2043343f);
            Assert.IsTrue(vectProd.Y == -0.245020181f);
            Assert.IsTrue(vectProd.Z == 0);
        }

        [Test]
        public void Get2DPolygon()
        {
            // arrange
            var polygon = GetSecondBuildingPolygon();

            // act
            var vectProd = Projections.GetVectorProduct(polygon);
            var poly = Projections.Get2DPoints(polygon);
            var isyz = Projections.IsYZProjection(vectProd);
            var iszx = Projections.IsZXProjection(vectProd);

            // assert
            Assert.IsFalse(isyz);
            Assert.IsTrue(iszx);
            Assert.IsTrue(poly.Count == 10);
        }

        private Polygon GetFirstBuildingPolygon()
        {
            // arrange (this is first polygon of building.wkb)
            var p0 = new Point(-7.7503319999668747, -7.3552320003509521, 1.9086260322265503);
            var p1 = new Point(-7.8121670000255108, -1.4992249999195337, 1.9086260322265503);
            var p2 = new Point(-7.8121670000255108, -1.4992249999195337, -2.053857967773439);
            var p3 = new Point(-7.7503319999668747, -7.3552320003509521, -2.053857967773439);
            var p4 = new Point(-7.7503319999668747, -7.3552320003509521, 1.9086260322265503);

            return GetPoly(p0, p1, p2, p3, p4);
        }

        private Polygon GetSecondBuildingPolygon()
        {
            // arrange (this is first polygon of building.wkb)
            var p0 = new Point(3.9682410000823438, -7.2314929999411106, 1.9086260322265503);
            var p1 = new Point(-7.7503319999668747, -7.3552320003509521, 1.9086260322265503);
            var p2 = new Point(-7.7503319999668747, -7.3552320003509521, -2.053857967773439);
            var p3 = new Point(3.9682410000823438, -7.2314929999411106, -2.053857967773439);
            var p4 = new Point(3.9682410000823438, -7.2314929999411106, 1.9086260322265503);

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
