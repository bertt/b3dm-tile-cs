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
            var actualHeader = gltfFile.Header;
            var actualHeaderString = JsonConvert.SerializeObject(actualHeader);
            var expectedHeader = JsonConvert.DeserializeObject<Header>(header);
            Assert.IsTrue(actualHeader.asset.version.Equals(expectedHeader.asset.version));
            Assert.IsTrue(!actualHeader.asset.generator.Equals(expectedHeader.asset.generator));

            // todo: create b3dm!
        }

    }
}
