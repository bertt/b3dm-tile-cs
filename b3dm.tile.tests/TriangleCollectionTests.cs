using NUnit.Framework;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class TriangleCollectionTests
    {
        [Test]
        public void TriangleBinaryConvertorTest()
        {
            var triangles = new TriangleCollection();
            var p0 = new Point(0, 0, 1);
            var p1 = new Point(1, 0, 2);
            var p2 = new Point(1, 0, 3);
            var t = new Triangle(p0, p1, p2);
            triangles.Add(t);

            var bytes = triangles.PositionsToBinary();
            Assert.IsTrue(bytes.Length > 0);
        }
    }
}
