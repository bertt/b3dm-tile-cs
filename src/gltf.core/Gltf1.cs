using System.Text;

namespace Gltf.Core
{
    public class Gltf1
    {
        public uint Magic { get; set; }
        public uint Version { get; set; }
        public string GltfModelJson { get; set; }
        public byte[] GltfModelBin { get; set; }
        public uint Length {
            get {
                return (uint)(28 + GltfModelBin.Length + Encoding.UTF8.GetBytes(GltfModelJson).Length);
            }
        }
    }
}
