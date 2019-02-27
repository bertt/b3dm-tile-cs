using System.Collections.Generic;

namespace B3dm.Tile.Tests
{
    public class TriangleCollection: List<Triangle>
    {
        public byte[] PositionsToBinary()
        {
            var floats = new List<float>();
            foreach (var triangle in this)
            {
                floats.AddRange(triangle.ToArray());
            }
            var bytes = BinaryConvertor.ToBinary(floats.ToArray());
            return bytes;
        }
    }
}
