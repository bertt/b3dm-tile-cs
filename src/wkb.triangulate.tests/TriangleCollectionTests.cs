using Triangulator;
using NUnit.Framework;
using Wkx;

namespace Triangulator.Tests
{
    public class TriangleCollectionTests
    {

        [Test]
        public void TrianglesNormalsTests()
        {
            var triangles = new TriangleCollection();
            var p0 = new Point(0, 0, 1);
            var p1 = new Point(1, 0, 2);
            var p2 = new Point(1, 0, 3);
            var t = new Triangle(p0, p1, p2);
            triangles.Add(t);

            var normals = triangles.GetNormals();
            Assert.IsTrue(normals.Count == 1);
        }
    }
}
