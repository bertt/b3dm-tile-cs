using Wkx;

namespace B3dm.Tile.Extensions
{
    public static class BoundingBoxExtensions
    {
        public static bool Inside(this BoundingBox bb, Point point)
        {
            var xinside = (bb.XMin <= point.X) && (bb.XMax > point.X);
            var yinside = (bb.YMin <= point.Y) && (bb.YMax > point.Y);
            return (xinside && yinside);
        }
    }
}
