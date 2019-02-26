using System;
using Wkx;

namespace B3dm.Tile.Tests
{
    public static class Projections
    {
        public static Point GetVectorProduct(Polygon polygon)
        {
            var points = polygon.ExteriorRing.Points;
            var vect1 = points[1].Minus(points[0]);
            var vect2 = points[2].Minus(points[0]);
            var vectProd = vect1.Cross(vect2);
            return vectProd;
        }


        public static Polygon Get2DPolygon(Polygon polygon3d)
        {
            var polygon = new Polygon();

            var vectProd = GetVectorProduct(polygon3d);

            var points = polygon3d.ExteriorRing.Points;

            foreach (var point3d in points)
            {
                var point2d = GetPoint(vectProd, point3d);
                polygon.ExteriorRing.Points.Add(point2d);
            }

            return polygon;
        }

        private static Point GetPoint(Point vectProd, Point point3d)
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

        public static bool IsYZProjection(Point vectProd)
        {
            return Math.Abs((double)vectProd.X) > Math.Abs((double)vectProd.Y) &&
                Math.Abs((double)vectProd.X) > Math.Abs((double)vectProd.Z);
        }

        public static bool IsZXProjection(Point vectProd)
        {
            return Math.Abs((double)vectProd.Y) > Math.Abs((double)vectProd.Z);
        }

    }
}
