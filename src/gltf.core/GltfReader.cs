using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Newtonsoft.Json;
using Wkx;

namespace Gltf.Core
{
    public static class GltfReader
    {
        public static Gltf1 ReadFromWkt(string wkt, float[] transform)
        {
            var g = Geometry.Deserialize<WktSerializer>(wkt);
            var polyhedralsurface = (PolyhedralSurface)g;
            var gltf = ReadFromPolyHedralSurface(polyhedralsurface, transform);
            return gltf;
        }

        public static Gltf1 ReadFromWkb(Stream buildingWkb, float[] transform)
        {
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            var polyhedralsurface = ((PolyhedralSurface)g);
            return ReadFromPolyHedralSurface(polyhedralsurface, transform);
        }

        private static Gltf1 ReadFromPolyHedralSurface(PolyhedralSurface polyhedralsurface, float[] transform)
        {
            var bb = polyhedralsurface.GetBoundingBox3D();

            var triangles = Triangulator.Triangulate(polyhedralsurface);
            var bytesPositions = triangles.PositionsToBinary();
            var bytesNormals = triangles.NormalsToBinary();
            var n = (int)Math.Round((double)bytesPositions.Length / 12, 0);
            var binIds = BinaryConvertor.ToBinary(new float[n]);


            var gltfArray = new GltfArray(bytesPositions) {
                Vertices = bytesPositions,
                Normals = bytesNormals,
                BBox = bb,
            };
            var header = GetHeader(gltfArray, transform, gltfArray.Count);

            var gltf = new Gltf1();
            gltf.Magic = 1179937895;
            gltf.Version = 2;
            gltf.GltfModelJson = JsonConvert.SerializeObject(header);
            gltf.GltfModelBin = gltfArray.AsBinary();

            return gltf;
        }

        public static Header GetHeader(GltfArray gltfArray, float[] transform, int n)
        {
            var batchLength = 1;
            var byteLength = gltfArray.Vertices.Length * 2;
            byteLength += gltfArray.Vertices.Length / 3;
            var buffers = new List<Buffer>();
            var buffer = new Buffer() { byteLength = byteLength };
            buffers.Add(buffer);

            var bufferViews = new List<Bufferview>();
            // q: whats 34962? ## vertices
            bufferViews.Add(new Bufferview() { buffer = 0, byteLength = gltfArray.Vertices.Length, byteOffset = 0, target = 34962 });
            bufferViews.Add(new Bufferview() { buffer = 0, byteLength = gltfArray.Vertices.Length, byteOffset = gltfArray.Vertices.Length, target = 34962 });
            bufferViews.Add(new Bufferview() { buffer = 0, byteLength = gltfArray.Vertices.Length / 3, byteOffset = 2 * gltfArray.Vertices.Length, target = 34962 });

            var accessors = new List<Accessor>();
            var bb = gltfArray.BBox;
            // q: max and min are reversed in next py code?
            // # vertices
            accessors.Add(new Accessor() {
                bufferView = 0,
                byteOffset = 0,
                componentType = 5126,
                count = n,
                max = new double[3] { bb.YMin, bb.ZMin, bb.XMin },
                min = new double[3] { bb.YMax, bb.ZMax, bb.XMax },
                type = "VEC3"
            });

            // # normals
            accessors.Add(new Accessor() {
                bufferView = 1,
                byteOffset = 0,
                componentType = 5126,
                count = n,
                max = new double[3] { 1, 1, 1 },
                min = new double[3] { -1, -1, -1 },
                type = "VEC3"
            });

            // # batched
            accessors.Add(new Accessor() {
                bufferView = 2,
                byteOffset = 0,
                componentType = 5126,
                count = n,
                max = new double[1] { batchLength },
                min = new double[1] { 0 },
                type = "SCALAR"
            });

            // # meshes
            var mesh = new Mesh() { };
            var primitives = new List<Primitive>();
            var attributes = new Attributes() { POSITION = 0, NORMAL = 1, _BATCHID = 2 };
            var primitive = new Primitive() { attributes = attributes, material = 0, mode = 4 };
            primitives.Add(primitive);
            mesh.primitives = primitives.ToArray();
            var meshes = new List<Mesh>();
            meshes.Add(mesh);

            // # nodes
            var nodes = new List<Node>();
            var node = new Node() { matrix = transform, mesh = 0 };
            nodes.Add(node);

            // # materials
            var materials = new List<Material>();
            var material = new Material() { name = "Material", pbrMetallicRoughness = new Pbrmetallicroughness() { metallicFactor = 0 } };
            materials.Add(material);

            // # GltfHeader
            var gltfHeader = new Header();
            gltfHeader.asset = new Asset() { generator = "Glt.Core", version = "2.0" };
            gltfHeader.scene = 0;
            var gltfScenes = new List<Scene>();
            gltfScenes.Add(new Scene() { nodes = new int[nodes.Count] });
            gltfHeader.scenes = gltfScenes.ToArray();
            gltfHeader.nodes = nodes.ToArray();
            gltfHeader.meshes = meshes.ToArray();
            gltfHeader.materials = materials.ToArray();
            gltfHeader.accessors = accessors.ToArray();
            gltfHeader.bufferViews = bufferViews.ToArray();
            gltfHeader.buffers = buffers.ToArray();
            return gltfHeader;
        }


    }
}
