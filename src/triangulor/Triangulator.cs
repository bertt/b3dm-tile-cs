using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Wkx;

namespace Triangulator
{
    public class Triangulator
    {
        public static TriangleCollection GetTriangles(PolyhedralSurface polyhedralsurface)
        {
            var geometries = polyhedralsurface.Geometries;
            var allTriangles = GetTriangles(geometries);
            return allTriangles;
        }

        public static TriangleCollection GetTriangles(List<Polygon> geometries)
        {
            var allTriangles = new TriangleCollection();
            foreach (var geometry in geometries) {
                var triangles = GetTriangles1(geometry);
                allTriangles.AddRange(triangles);
            }

            return allTriangles;
        }

        public static TriangleCollection GetTriangles(MultiPolygon multipoly)
        {
            var allTriangles = new TriangleCollection();
            foreach (var poly in multipoly.Geometries) {
                var triangles = GetTriangles1(poly);
                allTriangles.AddRange(triangles);
            }

            return allTriangles;
        }

        // just create triangle and return
        public static TriangleCollection GetTriangles1(Polygon geometry)
        {
            var pnts = geometry.ExteriorRing.Points;
            var triangle = new Triangle(pnts[0],pnts[1], pnts[2]);
            return new TriangleCollection(){ triangle};

            /**var vectProd = Projections.GetVectorProduct(geometry);
            var points2d = Projections.Get2DPoints(geometry, vectProd);
            var triangleidx = Earcut.Earcut.Tessellate(points2d, new List<int>());
            var triangles = GetTrianglesFromPolygon(geometry, triangleidx, vectProd);
            return triangles;
            */
        }


        public static TriangleCollection GetTriangles(Polygon geometry)
        {

            var vectProd = Projections.GetVectorProduct(geometry);
            var points2d = Projections.Get2DPoints(geometry, vectProd);
            var triangleidx = Earcut.Earcut.Tessellate(points2d, new List<int>());
            var triangles = GetTrianglesFromPolygon(geometry, triangleidx, vectProd);
            return triangles;
        }

        public static TriangleCollection GetTrianglesFromPolygon(Polygon polygon, List<int> triangleIndexes, Vector3 vectProd)
        {
            var triangles_count = triangleIndexes.Count / 3;

            var triangles = new TriangleCollection();
            for (var i = 0; i < triangles_count; i++)
            {
                var point0 = polygon.ExteriorRing.Points[triangleIndexes[i * 3]];
                var point1 = polygon.ExteriorRing.Points[triangleIndexes[i * 3 + 1]];
                var point2 = polygon.ExteriorRing.Points[triangleIndexes[i * 3 + 2]];

                // triangle orientation
                var invert = Projections.ShouldInvertTriangle(vectProd, point0, point1, point2);

                var triangle = (invert ? new Triangle(point1, point0, point2) : new Triangle(point0, point1, point2));

                triangles.Add(triangle);
            }
            return triangles;
        }
    }
}
