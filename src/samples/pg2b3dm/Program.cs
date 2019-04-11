using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using B3dm.Tile;
using B3dm.Tile.Extensions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Wkb2Gltf;
using Wkx;

namespace pg2b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("tool: pg2b3dm");
            Console.WriteLine("version: 0.1");
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration["connectionstring"];
            var geometry_table = configuration["geometry_table"];
            var geometry_column = configuration["geometry_column"];

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var (zupboxes,translation) = WriteB3dms(connectionString, geometry_table, geometry_column);
            var tree = TileCutter.ConstructTree(zupboxes);
            var tileset1 = tree.ToTileset(translation);

            stopWatch.Stop();
            Console.WriteLine("Elapsed: " + stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Program finished. Press any key to continue...");
            Console.ReadKey();
        }


        private static (List<BoundingBox3D>, float[] transform) WriteB3dms(string connectionString, string geometry_table, string geometry_column)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var bbox3d = BoundingBoxRepository.GetBoundingBox3D(conn, geometry_table, geometry_column);
            var translation = bbox3d.GetCenter().ToVector(); 

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
            return (zupboxes, translation);
        }
    }
}
