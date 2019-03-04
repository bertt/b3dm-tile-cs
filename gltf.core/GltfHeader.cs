
using System.Collections.Generic;

namespace Gltf.Core
{
    public class GltfHeader
    {
        public GltfAsset GltfAsset { get; set; }
        public int Scene { get; set; }
        public List<GltfScene> Scenes { get; set; }
        public List<GltfNode> Nodes { get; set; }
        public List<GltfMesh> Meshes { get; set; }
        public List<GltfMaterial> Materials { get; set; }
        public List<GltfAccessor> Accessors { get; set; }
        public List<GltfBufferView> BufferViews { get; set; }
        public List<GltfBuffer> Buffers { get; set; }
    }
}
