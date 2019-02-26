using NUnit.Framework;
using System.Reflection;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class TriangulatorTests
    {
        [Test]
        public void TriangulateTest()
        {
            // arrange
            const string testfile = "B3dm.Tile.Tests.testfixtures.building.wkb";
            var buildingWkb = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            Assert.IsTrue(g.GeometryType == GeometryType.PolyhedralSurface);
            var polyhedralsurface = ((PolyhedralSurface)g);

            // arrange
            var triangles = Triangulator.Triangulate(polyhedralsurface);

            // assert
            Assert.IsTrue(triangles.Count == 22);
        }
    }
}
