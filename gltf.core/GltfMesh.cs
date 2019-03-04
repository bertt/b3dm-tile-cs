using System.Collections.Generic;

namespace Gltf.Core
{
    public class GltfMesh
    {
        List<GltfPrimitive> primitives;

        public GltfMesh()
        {
            primitives = new List<GltfPrimitive>();
        }
        public List<GltfPrimitive> Primitives {
            get { return primitives; }
            set { primitives = value; }
        }
    }
}
