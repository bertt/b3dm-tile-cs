using System.Collections.Generic;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class Triangulator
    {
        public static List<Triangle> Triangulate(PolyhedralSurface polyhedralsurface)
        {
            var allTriangles = new List<Triangle>();
            foreach (var geometry in polyhedralsurface.Geometries)
            {
                var points2d = Projections.Get2DPoints(geometry);
                var triangleidx = Earcut.Tessellate(points2d, new List<int>());
                var triangles = GetTriangles(geometry, triangleidx);
                allTriangles.AddRange(triangles);
            }

            return allTriangles;
        }

        public static List<Triangle> GetTriangles(Polygon polygon3d, List<int> triangleIndexes)
        {
            var vectProd = Projections.GetVectorProduct(polygon3d);
            var triangles_count = triangleIndexes.Count / 3;

            var triangles = new List<Triangle>();
            for (var i = 0; i < triangles_count; i++)
            {
                var point0 = polygon3d.ExteriorRing.Points[triangleIndexes[i * 3]];
                var point1 = polygon3d.ExteriorRing.Points[triangleIndexes[i * 3 + 1]];
                var point2 = polygon3d.ExteriorRing.Points[triangleIndexes[i * 3 + 2]];

                // triangle orientation
                var invert = Projections.InvertTriangle(vectProd, point0, point1, point2);

                var triangle = (invert ? new Triangle(point1, point0, point2) : new Triangle(point0, point1, point2));

                triangles.Add(triangle);
            }
            return triangles;
        }
    }
}
