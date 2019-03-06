namespace Gltf.Core
{
    public class Body
    {
        public byte[] Vertices { get; set; }
        public byte[] Normals { get; set; }
        public byte[] Ids { get; set; }
        public byte[] Uvs { get; set; }
        public BoundingBox3D BBox { get; set; }

        // body = vertices + normals + ids + uvs
    }
}
