using NUnit.Framework;

namespace Gltf.Core.Tests
{
    public class GltfArrayTests
    {
        [Test]
        public void TestGlftArrays()
        {
            var gltfArray = new GltfArray(new byte[1]);
            gltfArray.Normals = new byte[1];
            gltfArray.BBox = new BoundingBox3D();

            Assert.IsTrue(gltfArray != null);
        }
    }
}
