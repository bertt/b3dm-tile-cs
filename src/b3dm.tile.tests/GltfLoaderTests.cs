using System.IO;
using glTFLoader;
using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class GltfLoaderTests
    {
        [Test]
        public void LoadGltf()
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
    }
}
