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

            // arrange
            var triangles = Triangulator.Triangulate(polyhedralsurface);

            // assert
            Assert.IsTrue(triangles.Count == 22);
        }
    }
}
