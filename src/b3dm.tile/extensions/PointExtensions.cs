using Wkx;

namespace B3dm.Tile.Extensions
{
    public static class PointExtension
    {
        public static float[] ToVector(this Point p)
        {
            var vector = new float[] { (float)p.X, (float)p.Y, (float)p.Z };
            return vector;
        }

    }
}
