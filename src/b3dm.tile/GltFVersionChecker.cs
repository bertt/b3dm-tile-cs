using System.IO;
using System.Text;

namespace B3dm.Tile
{
    public static class GltfVersionChecker
    {
        public static int GetGlbVersion(byte[] GlbData)
        {
            var glbStream = new MemoryStream(GlbData);
            using (var reader = new BinaryReader(glbStream)) {
                var magic = Encoding.UTF8.GetString(reader.ReadBytes(4));
                var version = (int)reader.ReadUInt32();
                return version;
            }
        }
    }
}
