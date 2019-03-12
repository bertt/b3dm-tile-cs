using System;
using System.Collections.Generic;
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
            var id_column = configuration["id_column"];

            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var ids = GetIds(conn, geometry_table, id_column);
            var transform = GetTransform(geometry_table, geometry_column, conn);
            //var transform = Transformer.GetTransform(539085.1221813804f, 6989220.68008033f, 52.98474913463f);

            foreach (var id in ids) {
                Console.Write(".");
                var b3dm = GetB3dm(conn, transform, id);
                Directory.CreateDirectory("b3dm");
                B3dmWriter.WriteB3dm($"./b3dm/texel{id}.b3dm", b3dm);
                B3dmWriter.WriteGlb($"./b3dm/texel{id}.glb", b3dm);
            }

            conn.Close();
        }

        private static List<long> GetIds(NpgsqlConnection conn, string geometry_table, string id_column)
        {
            var res = new List<long>();
            var cmd = new NpgsqlCommand($"select {id_column} from {geometry_table}", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var id = reader.GetInt64(0);
                res.Add(id);
            }
            reader.Close();
            return res;
        }

        private static B3dm.Tile.B3dm GetB3dm(NpgsqlConnection conn, float[] transform, long id)
        {
            // todo: research the st_asbinary option...
            // var cmd = new NpgsqlCommand($"SELECT ST_ASBinary(geom) from tmp.tmp where id={id}", conn);
            var cmd = new NpgsqlCommand($"SELECT ST_ASTEXT(geom) from tmp.tmp where id={id}", conn);

            var reader = cmd.ExecuteReader();
            reader.Read();
            // var stream = reader.GetStream(0);
            // var gltf = GltfReader.ReadFromWkb(stream, transform);
            var stream = reader.GetString(0);
            var gltf = GltfReader.ReadFromWkt(stream, transform);
            var glb = Packer.Pack(gltf);
            var b3dm = new B3dm.Tile.B3dm {
                GlbData = glb
            };
            reader.Close();
            return b3dm;
        }

        private static float[] GetTransform(string geometry_table, string geometry_column, NpgsqlConnection conn)
        {
            var bbox3d = GetBoundingBox3D(conn, geometry_table, geometry_column);
            var center = bbox3d.GetCenter();
            var transform = Transformer.GetTransform((float)center.X, (float)center.Y, (float)center.Z);
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
