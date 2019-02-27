using NUnit.Framework;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class TriangleTests
    {
        [Test]
        public void CalculateNormalTests()
        {
            var p0 = new Point(-7.75033199996687, -7.35523200035095, 1.90862603222655);
            var p1 = new Point(-7.81216700002551, -1.49922499991953, 1.90862603222655);
            var p2 = new Point(-7.75033199996687, -7.35523200035095, -2.05385796777344);
            var triangle = new Triangle(p0, p1, p2);
            Assert.IsTrue(triangle.GetNormal() == 23.2056274f);
        }
    }
}
