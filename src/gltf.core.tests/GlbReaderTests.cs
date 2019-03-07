using System.IO;
using NUnit.Framework;

namespace Gltf.Core.Tests
{
    public class GlbReaderTests
    {
        [Test]
        public void TestReadGlb()
        {
            // arrange
            const string testfile = @".\testfixtures\building.glb";
            var fs=  File.Open(testfile, FileMode.Open);
            // act
            var gltf = GlbReader.ReadFromGlb(fs);
            // assert
            Assert.IsTrue(gltf.Version == 2);

            fs.Close();
        }
    }
}
