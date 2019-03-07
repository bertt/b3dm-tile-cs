using System.IO;

namespace Gltf.Core.Tests
{
    public class BinaryFileWriter
    {
        public static void WriteGlb(string fileName, byte[] bytes)
        {
            var fs = File.Create(fileName);
            var bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
        }
    }
}
