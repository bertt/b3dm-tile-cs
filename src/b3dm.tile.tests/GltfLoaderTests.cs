using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Gltf.Core;
using glTFLoader;
using glTFLoader.Schema;
using NUnit.Framework;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class GltfLoaderTests
    {
        [Test]
        public void LoadSampleGltf()
        {
            var gltf = Interface.LoadModel(@"testfixtures/2CylinderEngine.gltf");
            var stream = File.OpenRead(@"testfixtures/2CylinderEngine0.bin");
            var reader = new BinaryReader(stream);
            var bytes = reader.ReadBytes((int)reader.BaseStream.Length);
            foreach (var buf in gltf.Buffers) {
                buf.Uri = null;
            }
            gltf.SaveBinaryModel(bytes, "test.glb");
        }

        [Test]
        public void LoadB3dmWithGltf2LoaderTest()
        {
            var b3dmfile = File.OpenRead(@"testfixtures/py3dtiles_test_build_1.b3dm");
            var b3dm = B3dmReader.ReadB3dm(b3dmfile);
            var stream1 = new MemoryStream(b3dm.GlbData);
            var gltf = Interface.LoadModel(stream1);
            Assert.IsTrue(gltf.Buffers[0].ByteLength == 1848);
            var stream2 = new MemoryStream(b3dm.GlbData);
            var bytes = Interface.LoadBinaryBuffer(stream2);
            Assert.IsTrue(bytes.Length == 1848);

            //Assert.IsTrue(gltf.Asset.Generator == "py3dtiles");
        }

        [Test]
        public void GenerateGltfTest()
        {
            // arrange
            var tempPath = Path.GetTempPath();
            var buildingWkb = File.OpenRead(@"testfixtures/building.wkb");
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            var polyhedralsurface = ((PolyhedralSurface)g);
            var bb = polyhedralsurface.GetBoundingBox3D();
            var triangles = Triangulator.Triangulate(polyhedralsurface);

            var bytesPositions = triangles.PositionsToBinary();
            var bytesNormals = triangles.NormalsToBinary();
            var n = (int)Math.Round((double)bytesPositions.Length / 12, 0);
            var binIds = BinaryConvertor.ToBinary(new float[n]);

            var gltfArray = new Body {
                Vertices = bytesPositions,
                Normals = bytesNormals,
                BBox = bb,
                Ids = binIds
            };

            var body = gltfArray.AsBinary();
            var gltf = GetGltfHeader(n, gltfArray);

            gltf.SaveBinaryModel(body, @"d:\aaa\hoho.glb");
            //var ms = new MemoryStream();
            //gltf.SaveBinaryModel(body, ms);
            //var s = ms.ToArray();
            //Assert.IsTrue(ms != null);
        }

        private glTFLoader.Schema.Gltf GetGltfHeader(int n, Body gltfArray)
        {
            var gltf = new glTFLoader.Schema.Gltf();
            gltf.Asset = GetAsset();
            gltf.Scene = 0;
            gltf.Materials = GetMaterials();
            //var transform = Transformer.GetTransform(1842015.125f, 5177109.25f, 247.87364196777344f);
            gltf.Nodes = GetNodes(new float[] { 1842015.125f, 5177109.25f, 247.87364196777344f });
            gltf.Buffers = GetBuffers(gltfArray.Vertices.Length);
            gltf.Meshes = GetMeshes();
            gltf.BufferViews = GetBufferViews(gltfArray.Vertices.Length);
            gltf.Accessors = GetAccessors(gltfArray.BBox, n);
            gltf.Scenes = GetScenes(gltf.Nodes.Length);
            return gltf;
        }

        private glTFLoader.Schema.Scene[] GetScenes(int nodes)
        {
            var scene = new glTFLoader.Schema.Scene();
            scene.Nodes = new int[nodes];
            return new glTFLoader.Schema.Scene[] { scene };
        }

        private glTFLoader.Schema.Accessor[] GetAccessors(BoundingBox3D bb, int n)
        {
            // q: max and min are reversed in next py code?
            var max = new float[3] { (float)bb.YMin, (float)bb.ZMin, (float)bb.XMin };
            var min = new float[3] { (float)bb.YMax, (float)bb.ZMax, (float)bb.XMax };
            var accessor = GetAccessor(0, n,min,max, glTFLoader.Schema.Accessor.TypeEnum.VEC3);
            max = new float[3] { 1, 1, 1 };
            min = new float[3] { -1, -1, -1 };
            var accessorNormals = GetAccessor(1, n, min, max, glTFLoader.Schema.Accessor.TypeEnum.VEC3);
            var batchLength = 1;
            max = new float[1] { batchLength };
            min = new float[1] { 0 };
            var accessorBatched = GetAccessor(2, n, min, max, glTFLoader.Schema.Accessor.TypeEnum.SCALAR);
            return new glTFLoader.Schema.Accessor[] { accessor, accessorNormals, accessorBatched };
        }

        private static glTFLoader.Schema.Accessor GetAccessor(int bufferView, int n, float[] min, float[] max, glTFLoader.Schema.Accessor.TypeEnum type)
        {
            var accessor = new glTFLoader.Schema.Accessor();
            accessor.BufferView = bufferView;
            accessor.ByteOffset = 0;
            accessor.ComponentType = glTFLoader.Schema.Accessor.ComponentTypeEnum.FLOAT;
            accessor.Count = n;
            accessor.Min = min;
            accessor.Max = max;
            accessor.Type = type;
            return accessor;
        }

        private BufferView[] GetBufferViews(int verticesLength)
        {
            // q: whats the logic here?
            var bv1 = GetBufferView(verticesLength, 0);
            var bv2 = GetBufferView(verticesLength, verticesLength);
            var bv3 = GetBufferView(verticesLength/3, 2*verticesLength);
            return new BufferView[] { bv1, bv2, bv3 };
        }

        private static BufferView GetBufferView(int byteLength, int byteOffset)
        {
            var bufferView1 = new BufferView();
            bufferView1.Buffer = 0;
            bufferView1.ByteLength = byteLength;
            bufferView1.ByteOffset = byteOffset;
            bufferView1.Target = BufferView.TargetEnum.ARRAY_BUFFER;
            return bufferView1;
        }

        private glTFLoader.Schema.Mesh[] GetMeshes()
        {
            var mesh = new glTFLoader.Schema.Mesh();

            var attributes = new Dictionary<string, int>();
            attributes.Add("POSITION", 0);
            attributes.Add("NORMAL", 1);
            attributes.Add("_BATCHID", 2);

            var primitive = new MeshPrimitive();
            primitive.Attributes = attributes;
            primitive.Material = 0;
            primitive.Mode = MeshPrimitive.ModeEnum.TRIANGLES;
            mesh.Primitives = new MeshPrimitive[] { primitive };
            return new glTFLoader.Schema.Mesh[] { mesh };
        }

        private glTFLoader.Schema.Buffer[] GetBuffers(int verticesLength)
        {
            var byteLength = verticesLength * 2;
            byteLength += verticesLength / 3;

            var buffer = new glTFLoader.Schema.Buffer() {
                ByteLength = byteLength
            };
            return new glTFLoader.Schema.Buffer[] { buffer };
        }

        private glTFLoader.Schema.Node[] GetNodes(float[] transform)
        {
            var node = new glTFLoader.Schema.Node() {
                Translation = transform,
                Mesh = 0
            };
            return new glTFLoader.Schema.Node[] { node };
        }

        private glTFLoader.Schema.Material[] GetMaterials()
        {
            var material = new glTFLoader.Schema.Material() {
                Name = "Material",
                PbrMetallicRoughness = new MaterialPbrMetallicRoughness() { MetallicFactor = 0 }
            };
            return new glTFLoader.Schema.Material[] { material };
        }

        private static glTFLoader.Schema.Asset GetAsset()
        {
            var asset = new glTFLoader.Schema.Asset();
            asset.Generator = "Glt.Core";
            asset.Version = "2.0";
            return asset;
        }


        /**
        public static Header GetHeader(Body gltfArray, float[] transform, int n)
        {

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
    */

    }
}
