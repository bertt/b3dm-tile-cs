using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    class TileSetJsonTests
    {
        [Test]
        public void FirstTileSetJsonTests()
        {
            // arrange
            var zUpBoxes = BBTestDataReader.GetTestData("testfixtures/zupboxes_actual.txt");
            var tree = TileCutter.ConstructTree(zUpBoxes);
            var translation = new double[] { 141584.2745, 471164.637, 15.81555842685751 };
            var s = File.ReadAllText(@"./testfixtures/tileset_json_expected.json");
            var tileset_json_expected = JsonConvert.DeserializeObject<TileSet>(s);

            // act
            var tileset_json_actual = TreeSerializer.ToTileset(tree,translation);
            var actual_json = JsonConvert.SerializeObject(tileset_json_actual, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText("d:/aaa/sample_tileset_actual.json", actual_json);

            // assert
            Assert.IsTrue(tileset_json_actual.asset.version=="1.0");
            Assert.IsTrue(tileset_json_actual.geometricError==500);
            Assert.IsTrue(tileset_json_actual.root.refine == "add"); // 500
            Assert.IsTrue(tileset_json_actual.root.geometricError == 500); // 500
            Assert.IsTrue(tileset_json_actual.root.transform.Length == 16); // 500
            Assert.IsTrue(tileset_json_actual.root.boundingVolume.box.Length == 12);

        }
    }
}
