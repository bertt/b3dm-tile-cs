using System.IO;
using NUnit.Framework;

namespace Gltf.Core.Tests
{
    public class GlbWriterTests
    {
        [Test]
        public void WriteGlbFromGltfTests()
        {
            // arrange
            const string testfile = @".\testfixtures\building.glb";
            var fs = File.Open(testfile, FileMode.Open);
            var gltf = Packer.Unpack(fs);

            // act
            var glb = Packer.Pack(gltf);

            // assert 
            Assert.IsTrue(glb.Length == 20 + gltf.GltfModelJson.Length + 8 + gltf.GltfModelBin.Length);


            // as test write to file
            BinaryFileWriter.WriteGlb(@"d:\aaa\b3dm\1.glb", glb);
        }
    }
}
