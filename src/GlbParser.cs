using System.IO;

namespace B3dm.Tile
{
    public class GlbParser
    {
        public static Glb ParseGlb(Stream stream)
        {
            var binaryReader = new BinaryReader(stream);

            var magic = binaryReader.ReadUInt32();

            return new Glb {
                Magic = magic
            };
        }
    }
}
