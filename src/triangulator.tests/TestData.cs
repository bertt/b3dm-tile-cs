using Wkx;

namespace Triangulator.Tests
{
    public static class TestData
    {
        public static Polygon SimplePolygon(double minx, double miny, double minz, double maxx, double maxy, double maxz)
        {
            var groundRing = new LinearRing();
            groundRing.Points.Add(new Point(minx, miny, minz));
            groundRing.Points.Add(new Point(minx, maxy, minz));
            groundRing.Points.Add(new Point(maxx, maxy, minz));
            groundRing.Points.Add(new Point(maxx, miny, minz));
            groundRing.Points.Add(new Point(minx, miny, minz));

            var ground = new Polygon(groundRing);
            return ground;
        }

    }
}
