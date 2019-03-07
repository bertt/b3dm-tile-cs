using System.IO;
using System.Text;

namespace Gltf.Core
{
    public static class GlbWriter
    {
        public static byte[] ToGlb(Gltf1 gltf)
        {
            var ms = new MemoryStream();
            var binaryWriter = new BinaryWriter(ms);
            binaryWriter.Write(gltf.Magic);
            binaryWriter.Write(gltf.Version);
            binaryWriter.Write(gltf.Length);
            var gltfModelJsonBytes = Encoding.UTF8.GetBytes(gltf.GltfModelJson);
            binaryWriter.Write(gltfModelJsonBytes.Length); // chunklength
            binaryWriter.Write(1313821514); // chunkformat
            binaryWriter.Write(gltfModelJsonBytes); // chunkformat
            binaryWriter.Write(gltf.GltfModelBin.Length); // chunklength2
            binaryWriter.Write(5130562); // chunkformat
            binaryWriter.Write(gltf.GltfModelBin); // chunklength2
            binaryWriter.Flush();
            return ms.ToArray();
        }
    }
}
