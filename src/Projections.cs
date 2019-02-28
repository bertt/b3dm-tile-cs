using System;
using System.Collections.Generic;
using System.Numerics;
using Wkx;

namespace B3dm.Tile
{
    public static class Projections
    {
        public static bool InvertTriangle(Vector3 vectProd, Point point0, Point point1, Point point2)
        {
            var v1 = point1.Minus(point0);
            var v2 = point2.Minus(point0);
            var crossproduct = Vector3.Cross(v1,v2);
            var dotproduct = Vector3.Dot(vectProd, crossproduct);
            var invert = (dotproduct < 0);
            return invert;
        }
        public static Vector3 GetVectorProduct(Polygon polygon)
        {
            var points = polygon.ExteriorRing.Points;
            var vect1 = points[1].Minus(points[0]);
            var vect2 = points[2].Minus(points[0]);
            var vectProd = Vector3.Cross(vect1, vect2);
            return vectProd;
        }

        public static List<double> Get2DPoints(Polygon polygon3d)
        {
            var points2d = new List<double>();
            var vectProd = GetVectorProduct(polygon3d);
            var points3d = polygon3d.ExteriorRing.Points;

            foreach (var point3d in points3d)
            {
                var point2d = GetPoint(vectProd, point3d);
                points2d.Add((double)point2d.X);
                points2d.Add((double)point2d.Y);
            }

            return points2d;
        }

        private static Point GetPoint(Vector3 vectProd, Point point3d)
        {
            Point newpoint;
            if (IsYZProjection(vectProd))
            {
                newpoint = new Point((double)point3d.Y, (double)point3d.Z);
            }
            else if (IsZXProjection(vectProd))
            {
                newpoint = new Point((double)point3d.Y, (double)point3d.Z);
            }
            else
            {
                // does not occur in building.wkb?
                newpoint = new Point((double)point3d.X, (double)point3d.Y);
            }

            return newpoint;
        }

        public static bool IsYZProjection(Vector3 vectProd)
        {
            return Math.Abs(vectProd.X) > Math.Abs(vectProd.Y) &&
                Math.Abs(vectProd.X) > Math.Abs(vectProd.Z);
        }

        public static bool IsZXProjection(Vector3 vectProd)
        {
            return Math.Abs(vectProd.Y) > Math.Abs(vectProd.Z);
        }

    }
}
