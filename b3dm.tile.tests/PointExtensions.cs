using System.Collections.Generic;
using System.Numerics;
using Wkx;

namespace B3dm.Tile.Tests
{
    public static class PointExtensions
    {
        public static Vector3 Minus(this Point p, Point other)
        {
            var x = p.X - other.X;
            var y = p.Y - other.Y;
            var z = p.Z - other.Z;
            return new Vector3((float)x,(float)y,(float)z);
        }

        public static Point Cross(this Point p, Point other)
        {
            var x = p.Y * other.Z - other.Y * p.Z;
            var y = (p.X * other.Z - other.X * p.Z) * -1;
            var z = p.X * other.Y - other.X * p.Y;

            return new Point((double)x, (double)y, z);
        }

        public static List<float> ToXYZFloatArray(this Point p)
        {
            var floats = new List<float>();
            floats.Add((float)p.X);
            floats.Add((float)p.Y);
            floats.Add((float)p.Z);
            return floats;
        }
    }
}
