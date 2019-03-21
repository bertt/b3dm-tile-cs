using NUnit.Framework;

namespace Wkb2Gltf.Core.Tests
{
    public class BoundingBox3DTests
    {
        [Test]
        public void BoudingBox3DCenterTest()
        {
            // Arrange
            var bb = new BoundingBox3D() { XMin = 0, YMin = 0, ZMin = 0, XMax = 2, YMax = 2, ZMax = 2 };

            // act
            var center = bb.GetCenter();

            // assert
            Assert.IsTrue(center.X == 1 && center.Y==1 && center.Z == 1);
        }
    }
}
