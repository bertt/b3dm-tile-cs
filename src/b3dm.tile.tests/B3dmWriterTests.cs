using Gltf.Core;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class B3dmWriterTests
    {
        [Test]
        public void WriteB3dmTest()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.building.wkb";
            var header = File.ReadAllText(@".\testfixtures\building_header.json");
            var buildingWkb = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var gltfFile = GltfReader.ReadFromWkb(buildingWkb);
            Assert.IsTrue(gltfFile != null);
            var body = gltfFile.Body;
            Assert.IsTrue(body.AsBinary().Length == 1848);
            var actualHeader = gltfFile.Header;
            var l = gltfFile.Header.nodes.Length;
            var expectedHeader = JsonConvert.DeserializeObject<Header>(header);
            Assert.IsTrue(actualHeader.asset.version.Equals(expectedHeader.asset.version));
            Assert.IsTrue(!actualHeader.asset.generator.Equals(expectedHeader.asset.generator));
            var b3dm = new B3dm();
            b3dm.Magic = "b3dm";
            b3dm.Version = 1;
            var scene = JsonConvert.SerializeObject(actualHeader);
            var length = 28 + scene.Length; // padding seems 0
            var binaryHeader = new int[] { 0x46546C67, 2, length };  // # "glTF" magic
            var jsonChunkHeader = new int[] {scene.Length, 0x4E4F534A }; // # "JSON"
            // gltfFile.Body.
            // var binChunkHeader = # "BIN"

            // b3dm.GlbData
            // todo: convert GLTF.TOArray method
        }

        [Test]
        public void WriteB3dmHeaderTest()
        {
            var b3dm = new B3dm() { Magic = "b3dm"};
            var bytes = b3dm.GetHeaderArray();
            Assert.IsTrue(bytes.Length == 4);
        }

    }
}
