using Gltf.Core;
using NUnit.Framework;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class B3dmWriterTests
    {
        [Test]
        public void WriteB3dmTest()
        {
            // arrange
            const string testfile = "B3dm.Tile.Tests.testfixtures.building.wkb";
            var buildingWkb = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var gltfFile = GltfReader.ReadFromWkb(buildingWkb);
            var glb = GlbWriter.ToGlb(gltfFile);

            // act
            BinaryFileWriter.WriteGlb(@"d:\aaa\b3dm\2.glb", glb);

            // assert
            Assert.IsTrue(true);
        }
    }
}
