using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using B3dm.Tile;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Wkb2Gltf;
using Wkx;

namespace pg2b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            // testing out reading tileset.json stuff
            // var json = File.ReadAllText("./testfixtures/sample_tileset.json");
            // var tileset = JsonConvert.DeserializeObject<TileSet>(json);

            var tileset = GetTileSetJson();
            var s= JsonConvert.SerializeObject(tileset,Formatting.Indented);
            File.WriteAllText("./testfixtures/sample_tileset_new.json", s);

            Console.WriteLine("tool: pg2b3dm");
            Console.WriteLine("version: alpha alpha alpha");
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration["connectionstring"];
            var geometry_table = configuration["geometry_table"];
            var geometry_column = configuration["geometry_column"];

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var translation = GetTransform(conn, geometry_table, geometry_column);

            var i = 0;
            var sql = $"SELECT ST_AsBinary(ST_RotateX(ST_Translate(geom, {translation[0]}*-1,{translation[1]}*-1 , {translation[2]}*-1), -pi() / 2)),ST_Area(ST_Force2D({geometry_column})) AS weight FROM {geometry_table} ORDER BY weight DESC";
            var cmd = new NpgsqlCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            var zupboxes = new List<BoundingBox3D>();
            while (reader.Read()) {
                Console.Write(".");
                var stream = reader.GetStream(0);
                var g = Geometry.Deserialize<WkbSerializer>(stream);
                if (g.GeometryType == GeometryType.PolyhedralSurface) {
                    var polyhedralsurface = (PolyhedralSurface)g;
                    var center = polyhedralsurface.GetCenter();
                    var bbox = polyhedralsurface.GetBoundingBox3D();
                    var zupBox = bbox.TransformYToZ();
                    zupboxes.Add(zupBox);

                    var glb = GeometryToGlbConvertor.Convert(g, translation);
                    var b3dm = GlbToB3dmConvertor.Convert(glb);
                    B3dmWriter.WriteB3dm($"./glb/texel_{i}.b3dm", b3dm);
                }
                else {
                    Console.WriteLine("Geometry type: " + g.GeometryType.ToString() + " detected");
                }
                i++;
            }

            reader.Close();
            conn.Close();

            // select min and max from zupboxes for x and y

            var xmin = zupboxes.Select(x => x.XMin).Min();
            var ymin = zupboxes.Select(x => x.YMin).Min();
            var xmax = zupboxes.Select(x => x.XMax).Max();
            var ymax = zupboxes.Select(x => x.YMax).Max();

            var extentX = xmax - xmin;
            var extentY = ymax - ymin;

            // todo: create quadtree

            stopWatch.Stop();
            Console.WriteLine("Elapsed: " + stopWatch.ElapsedMilliseconds);
        }

        private static TileSet GetTileSetJson()
        {
            var tileset = new TileSet();
            tileset.geometricError = 500;
            tileset.asset = new Asset() { version = "1.0" };
            var root = new Root();
            root.geometricError = 500;
            root.refine = "add";
            root.transform = new double[]{
              1.0,
              0.0,
              0.0,
              0.0,
              0.0,
              1.0,
              0.0,
              0.0,
              0.0,
              0.0,
              1.0,
              0.0,
              141584.274,
              471164.637,
              15.816,
              1.0
            };
            var boundingvolume = new Boundingvolume();
            boundingvolume.box = new double[]{
                0.0,
                2.205,
                0.0,
                183.872,
                0,
                0,
                0,
                136.383,
                0,
                0,
                0,
                11.731
            };
            root.boundingVolume = boundingvolume;
            tileset.root = root;
            return tileset;
        }

        private static float[] GetTransform(NpgsqlConnection conn, string geometry_table, string geometry_column)
        {
            var bbox3d = GetBoundingBox3D(conn, geometry_table, geometry_column);
            var center = bbox3d.GetCenter();
            var transform = new float[] { (float)center.X, (float)center.Y, (float)center.Z };
            return transform;
        }

        private static BoundingBox3D GetBoundingBox3D(NpgsqlConnection connection, string geometry_table, string geometry_column)
        {
            // read bounding box
            var cmd = new NpgsqlCommand($"SELECT st_xmin(geom1), st_ymin(geom1), st_zmin(geom1), st_xmax(geom1), st_ymax(geom1), st_zmax(geom1) FROM (select ST_3DExtent({geometry_column}) as geom1 from {geometry_table}) as t", connection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var xmin = reader.GetDouble(0);
            var ymin = reader.GetDouble(1);
            var zmin = reader.GetDouble(2);
            var xmax = reader.GetDouble(3);
            var ymax = reader.GetDouble(4);
            var zmax = reader.GetDouble(5);
            reader.Close();
            return new BoundingBox3D() { XMin = xmin, YMin = ymin, ZMin = zmin, XMax = xmax, YMax = ymax, ZMax = zmax };
        }
    }
}
