namespace Gltf.Core
{
    public class GltfAccessor
    {
        public int BufferView { get; set; }
        public int ByteOffset { get; set; }
        public int ComponentType { get; set; }
        public int Count { get; set; }
        public double[] Max { get; set; }
        public double[] Min { get; set; }
        public string Type { get; set; }
    }
}
