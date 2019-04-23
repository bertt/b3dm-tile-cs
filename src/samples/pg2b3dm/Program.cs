using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using B3dm.Tile;
using B3dm.Tile.Extensions;
using CommandLine;
using glTFLoader;
using Newtonsoft.Json;
using Wkb2Gltf;
using Wkx;

namespace pg2b3dm
{

    public class Options
    {
        [Option('H', "host", Required = true, HelpText = "Database host")]
        public string Host { get; set; }
        [Option('D', "database", Required = true, HelpText = "Database name")]
        public string Database { get; set; }
        [Option('c', "column", Required = true, HelpText = "Geometry column")]
        public string GeometryColumn { get; set; }
        [Option('t', "table", Required = true, HelpText = "Database table")]
        public string GeometryTable { get; set; }
        [Option('u', "user", Required = true, HelpText = "Database user")]
        public string User { get; set; }
        [Option('p', "password", Required = true, HelpText = "Database password")]
        public string Password { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("tool: pg2b3dm");

            Parser.Default.ParseArguments<Options>(args).WithParsed(o => {
                var connectionString = $"Host={o.Host};Username={o.User};Password={o.Password};Database={o.Database}";
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                Directory.CreateDirectory("./tiles");
                var geometryTable = o.GeometryTable;
                var geometryColumn = o.GeometryColumn;

                var bbox3d = BoundingBoxRepository.GetBoundingBox3D(connectionString, geometryTable, geometryColumn);
                var translation = bbox3d.GetCenter().ToVector();
                var zupBoxes = GetZupBoxes(connectionString, geometryTable, geometryColumn, translation);
                var tree = TileCutter.ConstructTree(zupBoxes);

                WiteTilesetJson(translation, tree);

                // get first batch of id's
                var node = tree.Children[0];
                WriteTile(connectionString, geometryTable, geometryColumn, translation, node);

                stopWatch.Stop();
                Console.WriteLine("Elapsed: " + stopWatch.ElapsedMilliseconds / 1000);
                Console.WriteLine("Program finished. Press any key to continue...");
                Console.ReadKey();
            });
        }

        private static void WriteTile(string connectionString, string geometryTable, string geometryColumn, double[] translation, Node node)
        {
            var subset = (from f in node.Features select (f.Id)).ToArray();
            var tile_id = node.Id;
            var geometries = BoundingBoxRepository.GetGeometrySubset(connectionString, geometryTable, geometryColumn, translation, subset);
            WriteB3dm(geometries, tile_id, translation);

            // and write children too
            foreach(var subnode in node.Children) {
                WriteTile(connectionString, geometryTable, geometryColumn, translation, subnode);
            }
        }

        private static void WiteTilesetJson(double[] translation, Node tree)
        {
            var tileset = TreeSerializer.ToTileset(tree, translation);
            var s = JsonConvert.SerializeObject(tileset, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText("./tiles/tileset.json", s);
        }

        private static List<BoundingBox3D> GetZupBoxes(string connectionString, string GeometryTable, string GeometryColumn, double[] translation)
        {
            var bboxes = BoundingBoxRepository.GetAllBoundingBoxes(connectionString, GeometryTable, GeometryColumn, translation);
            var zupBoxes = new List<BoundingBox3D>();
            foreach (var bbox in bboxes) {
                var zupBox = bbox.TransformYToZ();
                zupBoxes.Add(zupBox);
            }

            return zupBoxes;
        }

        private static void WriteB3dm(List<GeometryRecord> geomrecords, int tile_id, double[] translation)
        {
            var triangleCollection = new TriangleCollection();
            foreach(var g in geomrecords) {
                var surface = (PolyhedralSurface)g.Geometry;
                var triangles = Triangulator.Triangulate(surface);
                triangleCollection.AddRange(triangles);
            }

            var bb = GetBoundingBox3D(geomrecords);
            var gltfArray = Gltf2Loader.GetGltfArray(triangleCollection, bb);
            var gltfall = Gltf2Loader.ToGltf(gltfArray, translation);
            var ms = new MemoryStream();
            gltfall.Gltf.SaveBinaryModel(gltfall.Body, ms);
            var glb = ms.ToArray();
            var b3dm = GlbToB3dmConvertor.Convert(glb);
            B3dmWriter.WriteB3dm($"./tiles/{tile_id}.b3dm", b3dm);
        }

        private static BoundingBox3D GetBoundingBox3D(List<GeometryRecord> records)
        {
            var bboxes = new List<BoundingBox3D>();
            foreach (var record in records) {
                var surface = (PolyhedralSurface)record.Geometry;
                var bbox = surface.GetBoundingBox3D();
                bboxes.Add(bbox);
            }
            var combinedBoundingBox = BoundingBoxCalculator.GetBoundingBox(bboxes);

            return combinedBoundingBox;
        }
    }
}
