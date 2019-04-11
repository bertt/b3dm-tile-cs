using Wkx;

namespace B3dm.Tile.Extensions
{
    public static class PointExtension
    {
        public static double[] ToVector(this Point p)
        {
            var vector = new double[] { (double)p.X, (double)p.Y, (double)p.Z };
            return vector;
        }

    }
}
