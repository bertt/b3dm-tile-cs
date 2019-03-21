using System.IO;
using glTFLoader;
using NUnit.Framework;
using Wkx;

namespace Wkb2Gltf.Core.Tests
{
    public class GltfLoaderTests
    {
        [Test]
        public void LoadSampleGltf()
        {
            var gltf = Interface.LoadModel(@"testfixtures/2CylinderEngine.gltf");
            var stream = File.OpenRead(@"testfixtures/2CylinderEngine0.bin");
            var reader = new BinaryReader(stream);
            var bytes = reader.ReadBytes((int)reader.BaseStream.Length);
            foreach (var buf in gltf.Buffers) {
                buf.Uri = null;
            }
            gltf.SaveBinaryModel(bytes, "test.glb");
        }

        [Test]
        public void GenerateGltfTest()
        {
            // arrange
            var tempPath = Path.GetTempPath();
            var buildingWkb = File.OpenRead(@"testfixtures/building.wkb");
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            var polyhedralsurface = ((PolyhedralSurface)g);
            var translation = new float[] { 1842015.125f, 5177109.25f, 247.87364196777344f };
            var gltf = Gltf2Loader.GetGltf(polyhedralsurface, translation);
            gltf.Gltf.SaveBinaryModel(gltf.Body, @"d:\aaa\hihi.glb");
            //var ms = new MemoryStream();
            //gltf.SaveBinaryModel(body, ms);
            //var s = ms.ToArray();
            //Assert.IsTrue(ms != null);
        }

    }
}
