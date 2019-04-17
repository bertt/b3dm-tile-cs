using System.Collections.Generic;
using Npgsql;
using Wkb2Gltf;
using Wkx;

namespace pg2b3dm
{
    public static class BoundingBoxRepository
    {
        public static BoundingBox3D GetBoundingBox3D(string connectionString, string geometry_table, string geometry_column)
        {
            // read bounding box
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand($"SELECT st_xmin(geom1), st_ymin(geom1), st_zmin(geom1), st_xmax(geom1), st_ymax(geom1), st_zmax(geom1) FROM (select ST_3DExtent({geometry_column}) as geom1 from {geometry_table}) as t", conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var xmin = reader.GetDouble(0);
            var ymin = reader.GetDouble(1);
            var zmin = reader.GetDouble(2);
            var xmax = reader.GetDouble(3);
            var ymax = reader.GetDouble(4);
            var zmax = reader.GetDouble(5);
            reader.Close();
            conn.Close();
            return new BoundingBox3D() { XMin = xmin, YMin = ymin, ZMin = zmin, XMax = xmax, YMax = ymax, ZMax = zmax };
        }


        public static List<BoundingBox3D> GetAllBoundingBoxes(string connectionString, string geometry_table, string geometry_column, double[] translation)
        {
            var sql = $"SELECT ST_XMIN(geom1),ST_YMIN(geom1),ST_ZMIN(geom1), ST_XMAX(geom1),ST_YMAX(geom1),ST_ZMAX(geom1) FROM (select ST_RotateX(ST_Translate({geometry_column}, {translation[0]}*-1,{translation[1]}*-1 , {translation[2]}*-1), -pi() / 2) as geom1, ST_Area(ST_Force2D(geom)) AS weight FROM {geometry_table} where ST_GeometryType(geom) =  'ST_PolyhedralSurface' ORDER BY weight DESC) as t";
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(sql, conn);
            var bboxes = new List<BoundingBox3D>();
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var xmin = reader.GetDouble(0);
                var ymin = reader.GetDouble(1);
                var zmin = reader.GetDouble(2);
                var xmax = reader.GetDouble(3);
                var ymax = reader.GetDouble(4);
                var zmax = reader.GetDouble(5);
                var bbox = new BoundingBox3D(xmin,ymin,zmin,xmax,ymax,zmax);
                bboxes.Add(bbox);
            }
            reader.Close();
            conn.Close();
            return bboxes;
        }
    }
}
