using System.IO;
using System.Text;

namespace B3dm.Tile
{
    public static class B3dmWriter
    {
        public static void WriteB3dm(string path, B3dm b3dm)
        {
            var fileStream = File.Open(path, FileMode.Create);
            var binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(b3dm.B3dmHeader.AsBinary());
            binaryWriter.Write(Encoding.UTF8.GetBytes(b3dm.FeatureTableJson));
            if (b3dm.FeatureTableBinary != null) {
                binaryWriter.Write(b3dm.FeatureTableBinary);
            }
            binaryWriter.Write(Encoding.UTF8.GetBytes(b3dm.BatchTableJson));
            if (b3dm.BatchTableBinary != null) {
                binaryWriter.Write(b3dm.BatchTableBinary);
            }
            binaryWriter.Write(b3dm.GlbData);
            binaryWriter.Flush();
            binaryWriter.Close();
        }

        public static void WriteGlb(string fileName, B3dm b3dm)
        {
            var fs = File.Create(fileName);
            var bw = new BinaryWriter(fs);
            bw.Write(b3dm.GlbData);
            bw.Close();
        }
    }
}