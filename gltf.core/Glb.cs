namespace Gltf.Core
{
    public class Glb
    {
        public uint Magic { get; set; }
        public uint Version { get; set; }
        public string GltfModelJson { get; set; }
        public byte[] GltfModelBin { get; set; }
        public uint Length { get; set; }
    }
}
