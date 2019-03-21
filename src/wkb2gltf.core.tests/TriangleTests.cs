using NUnit.Framework;
using Wkx;

namespace Wkb2Gltf.Tests
{
    public class TriangleTests
    {
        [Test]
        public void CalculateNormalTests()
        {
            // arrange
            var p0 = new Point(-7.75033199996687, -7.35523200035095, 1.90862603222655);
            var p1 = new Point(-7.81216700002551, -1.49922499991953, 1.90862603222655);
            var p2 = new Point(-7.75033199996687, -7.35523200035095, -2.05385796777344);
            var triangle = new Triangle(p0, p1, p2);

            // act
            var normal = triangle.GetNormal();

            // assert
            Assert.IsTrue(normal.X == -0.99994427f);
            Assert.IsTrue(normal.Y == -0.0105586536f);
            Assert.IsTrue(normal.Z == 0);
        }

        [Test]
        public void TriangleToArrayTests()
        {
            var p0 = new Point(-7.75033199996687, -7.35523200035095, 1.90862603222655);
            var p1 = new Point(-7.81216700002551, -1.49922499991953, 1.90862603222655);
            var p2 = new Point(-7.75033199996687, -7.35523200035095, -2.05385796777344);
            var triangle = new Triangle(p0, p1, p2);
            Assert.IsTrue(triangle.Flatten().Length==9);
        }
    }
}
