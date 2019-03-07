using Gltf.Core;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
            var body = gltfFile.Body.AsBinary();
            Assert.IsTrue(body.Length == 1848);
            var actualHeader = gltfFile.Header;
            var l = gltfFile.Header.nodes.Length;
            var expectedHeader = JsonConvert.DeserializeObject<Header>(header);
            Assert.IsTrue(actualHeader.asset.version.Equals(expectedHeader.asset.version));
            Assert.IsTrue(!actualHeader.asset.generator.Equals(expectedHeader.asset.generator));
            var b3dm = new B3dm();
            b3dm.B3dmHeader.Magic = "b3dm";
            b3dm.B3dmHeader.Version = 1;
            var scene = JsonConvert.SerializeObject(actualHeader);
            var length = 28 + body.Length + scene.Length; // padding seems 0
            Assert.IsTrue(length == 28 + + 1848 + 1063); // 2924
            var binaryHeaderArray = new int[] { 0x46546C67, 2, length };  // # "glTF" magic
            var jsonChunkHeaderArray = new int[] {scene.Length, 0x4E4F534A }; // # "JSON"
            var binChunkHeaderArray = new int[] { body.Length, 0x004E4942 };

            var binaryHeader = BinaryConvertor.ToBinary(binaryHeaderArray);
            var jsonChunkHeader = BinaryConvertor.ToBinary(jsonChunkHeaderArray);
            var binChunkHeader = BinaryConvertor.ToBinary(binChunkHeaderArray);
            var sceneBytes = Encoding.UTF8.GetBytes(scene);
            var gltf_Body = binaryHeader.Concat(jsonChunkHeader).Concat(sceneBytes).Concat(binChunkHeader).Concat(body).ToArray();
            //var gltf_Header = gltfFile.Header


            var tile_byte_length = gltf_Body.Length + 28;
            b3dm.B3dmHeader.ByteLength = tile_byte_length;

        }
    }
}
