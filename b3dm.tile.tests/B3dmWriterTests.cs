using NUnit.Framework;
using System.Numerics;
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

            // whats this for matrix??? maybe location of building?
            var m = new Matrix4x4(1, 0, 0, 1842015.125f,
                0, 1, 0, 5177109.25f,
                0, 0, 1, 247.87364196777344f,
                0, 0, 0, 1
                );
            var transform = m.Flatten();

            // todo: make gltf
            // in pyhton    glTF = GlTF.from_binary_arrays(arrays, transform)

            // todo: make b3dm
            // in python: B3dm.from_glTF(glTF)
        }
    }
}
