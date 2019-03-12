using System.Numerics;
using Gltf.Core;

namespace B3dm.Tile
{
    public static class Transformer
    {
        public static float[] GetTransform(float x, float y, float z)
        {
            var m = new Matrix4x4(1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1
            );
            var transform = m.Flatten();
            return transform;
        }
    }
}
