using System.IO;
using NUnit.Framework;
using Wkx;

namespace Wkb2Gltf.Tests
{
    public class GeometryToGlbConvertorTests
    {
        [Test]
        public void GeometryToGlbTests()
        {
            // arrange
            var buildingWkb = File.OpenRead(@"testfixtures/building.wkb");
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            var translation = new float[] { 539085.1f, 6989220.68f, 52.98f };

            // act
            var glb = GeometryToGlbConvertor.Convert(g, translation);

            // assert
            Assert.IsTrue(glb.Length == 2764);

        }
    }
}
