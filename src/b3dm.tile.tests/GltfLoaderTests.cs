using System.IO;
using glTFLoader;
using NUnit.Framework;

namespace B3dm.Tile.Tests
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
        public void LoadB3dmWithGltf2LoaderTest()
        {
            var b3dmfile = File.OpenRead(@"testfixtures/py3dtiles_test_build_1.b3dm");
            var b3dm = B3dmReader.ReadB3dm(b3dmfile);
            var stream = new MemoryStream(b3dm.GlbData);
            var gltf = Interface.LoadModel(stream);
            Assert.IsTrue(gltf.Asset.Generator == "py3dtiles");
        }

        [Test]
        public void GenerateGltfTest()
        {

        }
    }
}
