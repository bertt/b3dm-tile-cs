using System;
using System.IO;
using B3dm.Tile;
using Gltf.Core;
using glTFLoader;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Wkx;

namespace pg2b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("tool: pg2b3dm");
            Console.WriteLine("version: alpha alpha alpha");
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration["connectionstring"];
            var geometry_table = configuration["geometry_table"];
            var geometry_column = configuration["geometry_column"];

            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var translation = GetTransform(conn, geometry_table, geometry_column);

            var i = 0;
            var cmd = new NpgsqlCommand($"SELECT ST_ASBinary({geometry_column}) from {geometry_table}", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Console.Write(".");
                var stream = reader.GetStream(0);
                var g = Geometry.Deserialize<WkbSerializer>(stream);
                if (g.GeometryType == GeometryType.PolyhedralSurface) {
                    var polyhedralsurface = ((PolyhedralSurface)g);
                    var gltf = Gltf2Loader.GetGltf(polyhedralsurface, translation, $"texel_{i}.bin");
                    File.WriteAllBytes($"./glb/texel_{i}.bin", gltf.Body);
                    gltf.Gltf.SaveModel($"./glb/texel_{i}.gltf");
                }
                else {
                    Console.WriteLine("Geometry type: " + g.GeometryType.ToString() + " detected");
                }
                i++;
            }

            reader.Close();
            conn.Close();
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
