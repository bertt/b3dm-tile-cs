using System;
using System.IO;
using B3dm.Tile;
using Gltf.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;

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

            var bbox3d = GetBoundingBox3D(conn, geometry_table, geometry_column);
            var center = bbox3d.GetCenter();
            var transform = new float[] { (float)center.X,(float)center.Y,(float)center.Z };

            var cmd = new NpgsqlCommand("SELECT ST_ASBinary(geom) from tmp.tmp where id=77", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var stream = reader.GetStream(0);

            var gltf = GltfReader.ReadFromWkb(stream, transform);
            var glb = Packer.Pack(gltf);
            var b3dm = new B3dm.Tile.B3dm {
                GlbData = glb
            };

            B3dmWriter.WriteB3dm(@"./result/texel77.b3dm", b3dm);
            B3dmWriter.WriteGlb(@"./result/texel77.glb", b3dm);
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
