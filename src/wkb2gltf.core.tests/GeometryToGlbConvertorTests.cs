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
            var translation = new double[] { 539085.1, 6989220.68, 52.98 };

            // act
            var glb = GeometryToGlbConvertor.Convert(g, translation);

            // assert
            Assert.IsTrue(glb.Length == 2764);

        }
    }
}
