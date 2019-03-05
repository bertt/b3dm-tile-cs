namespace Gltf.Core
{
    public class Header
    {
        public Asset asset { get; set; }
        public int scene { get; set; }
        public Scene[] scenes { get; set; }
        public Node[] nodes { get; set; }
        public Mesh[] meshes { get; set; }
        public Material[] materials { get; set; }
        public Accessor[] accessors { get; set; }
        public Bufferview[] bufferViews { get; set; }
        public Buffer[] buffers { get; set; }
    }
}
