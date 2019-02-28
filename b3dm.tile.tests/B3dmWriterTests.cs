using NUnit.Framework;
using System.Reflection;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class B3dmWriterTests
    {
        [Test]
        public void WriteB3dmTest()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.building.wkb";
            var buildingWkb = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            var polyhedralsurface = ((PolyhedralSurface)g);
            var bb = polyhedralsurface.GetBoundingBox();

            var triangles = Triangulator.Triangulate(polyhedralsurface);
            Assert.IsTrue(triangles.Count == 22);

            var bytesPositions = triangles.PositionsToBinary();
            Assert.IsTrue(bytesPositions.Length == 792);

            var bytesNormals = triangles.NormalsToBinary();
            Assert.IsTrue(bytesNormals.Length == 792);

            // todo: make gltf
            // in pyhton    glTF = GlTF.from_binary_arrays(arrays, transform)

            // todo: make b3dm
            // in python: B3dm.from_glTF(glTF)
        }
    }
}
