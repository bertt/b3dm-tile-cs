using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using B3dm.Tile;
using B3dm.Tile.Extensions;
using CommandLine;
using Newtonsoft.Json;
using Wkb2Gltf;

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

                var bbox3d = BoundingBoxRepository.GetBoundingBox3D(connectionString, o.GeometryTable, o.GeometryColumn);
                var translation = bbox3d.GetCenter().ToVector();
                var zupBoxes = GetZupBoxes(connectionString, o.GeometryTable, o.GeometryColumn, translation);
                var tree = TileCutter.ConstructTree(zupBoxes);
                WiteTilesetJson(translation, tree);

                // get first batch of id's
                var subset = (from f in tree.Children[0].Features select (f.Id)).ToArray();
                var tile_id = tree.Children[0].Id;
                var geometries = BoundingBoxRepository.GetGeometrySubset(connectionString, o.GeometryTable, o.GeometryColumn, translation, subset);
                WriteB3dm(geometries, tile_id, translation);
                // todo: write b3dms
                // var zupboxes = WriteB3dms(connectionString, geometry_table, geometry_column, translation);

                stopWatch.Stop();
                Console.WriteLine("Elapsed: " + stopWatch.ElapsedMilliseconds / 1000);
                Console.WriteLine("Program finished. Press any key to continue...");
                Console.ReadKey();
            });
        }

        private static void WiteTilesetJson(double[] translation, Node tree)
        {
            var tileset = TreeSerializer.ToTileset(tree, translation);
            var s = JsonConvert.SerializeObject(tileset, Formatting.Indented);
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
            var g = geomrecords[0].Geometry;
            var glb = GeometryToGlbConvertor.Convert(g, translation);
            var b3dm = GlbToB3dmConvertor.Convert(glb);
            B3dmWriter.WriteB3dm($"./tiles/texel_{tile_id}.b3dm", b3dm);
        }

        /**
        private static List<BoundingBox3D> WriteB3dms(string connectionString, string geometry_table, string geometry_column, double[] translation)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var i = 0;
            var sql = $"SELECT ST_AsBinary(ST_RotateX(ST_Translate(geom, {translation[0]}*-1,{translation[1]}*-1 , {translation[2]}*-1), -pi() / 2)),ST_Area(ST_Force2D({geometry_column})) AS weight FROM {geometry_table} ORDER BY weight DESC";
            var cmd = new NpgsqlCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            var bboxes = new List<BoundingBox3D>();
            var zupboxes = new List<BoundingBox3D>();
            while (reader.Read()) {
                Console.Write(".");
                var stream = reader.GetStream(0);
                var g = Geometry.Deserialize<WkbSerializer>(stream);
                if (g.GeometryType == GeometryType.PolyhedralSurface) {
                    var polyhedralsurface = (PolyhedralSurface)g;
                    var bbox = polyhedralsurface.GetBoundingBox3D();
                    bboxes.Add(bbox);
                    var zupBox = bbox.TransformYToZ();
                    zupboxes.Add(zupBox);

                    var glb = GeometryToGlbConvertor.Convert(g, translation);
                    var b3dm = GlbToB3dmConvertor.Convert(glb);
                    B3dmWriter.WriteB3dm($"texel_{i}.b3dm", b3dm);
                }
                else {
                    Console.WriteLine("Geometry type: " + g.GeometryType.ToString() + " detected");
                }
                i++;
            }

            foreach(var box in bboxes) {
                Debug.WriteLine(box.XMin + "," + box.YMin + "," + box.ZMin + "," + box.XMax + "," + box.YMax + "," + box.ZMax);
            }

            reader.Close();
            conn.Close();
            return zupboxes;
        }*/
    }
}
