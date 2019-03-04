using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using Wkx;

namespace Gltf.Core.Tests
{
    public class B3dmWriterTests
    {
        [Test]
        public void WriteB3dmTest()
        {
            const string testfile = "Gltf.Core.Tests.testfixtures.building.wkb";
            var buildingWkb = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            var g = Geometry.Deserialize<WkbSerializer>(buildingWkb);
            var polyhedralsurface = ((PolyhedralSurface)g);
            var bb = polyhedralsurface.GetBoundingBox3D();

            var triangles = Triangulator.Triangulate(polyhedralsurface);
            Assert.IsTrue(triangles.Count == 22);

            var bytesPositions = triangles.PositionsToBinary();
            Assert.IsTrue(bytesPositions.Length == 792);

            var bytesNormals = triangles.NormalsToBinary();
            Assert.IsTrue(bytesNormals.Length == 792);

            // what's this for matrix??? 
            // translation : 1842015.125, 5177109.25, 247.87364196777344
            // Later comment: World coordinates transformation flattend matrix
            var m = new Matrix4x4(1, 0, 0, 1842015.125f,
                0, 1, 0, 5177109.25f,
                0, 0, 1, 247.87364196777344f,
                0, 0, 0, 1
                );
            var transform = m.Flatten();

            var gltfArray = new GltfArray
            {
                Positions = bytesPositions,
                Normals = bytesNormals,
                BBox = bb
            };

            var gltfHeader = GetGltfHeader(gltfArray, transform);

            // todo: make b3dm from gltf
            // in python: B3dm.from_glTF(glTF)
        }

        public GltfHeader GetGltfHeader(GltfArray  gltfArray, float[] transform)
        {
            // q: whats the 12?
            var n= (int)Math.Round((double)gltfArray.Positions.Length / 12, 0);
            Assert.IsTrue(gltfArray.Positions.Length == 792);
            Assert.IsTrue(n == 66);
            Assert.IsTrue(gltfArray.Positions[0] == 184);
            var binIds = BinaryConvertor.ToBinary(new float[n]);
            Assert.IsTrue(binIds[0] == 0);
            var batchLength = 1;
            var byteLength = gltfArray.Positions.Length * 2;
            Assert.IsTrue(byteLength == 1584);
            byteLength += gltfArray.Positions.Length / 3;
            Assert.IsTrue(byteLength == 1848);
            var buffers = new List<GltfBuffer>();
            var buffer = new GltfBuffer() { ByteLength = byteLength };
            buffers.Add(buffer);

            var bufferViews = new List<GltfBufferView>();
            // q: whats 34962? ## vertices
            bufferViews.Add(new GltfBufferView() { Buffer = 0, ByteLength = gltfArray.Positions.Length, ByteOffset = 0, Target = 34962 });
            bufferViews.Add(new GltfBufferView() { Buffer = 0, ByteLength = gltfArray.Positions.Length, ByteOffset = gltfArray.Positions.Length, Target = 34962 });
            bufferViews.Add(new GltfBufferView() { Buffer = 0, ByteLength = gltfArray.Positions.Length/3, ByteOffset = 2*gltfArray.Positions.Length, Target = 34962 });

            var accessors = new List<GltfAccessor>();
            var bb = gltfArray.BBox;
            // q: max and min are reversed in next py code?
            // # vertices
            accessors.Add(new GltfAccessor()
            {
                BufferView = 0,
                ByteOffset = gltfArray.Positions.Length,
                ComponentType = 5126,
                Count = n,
                Max = new double[3] { bb.YMin, bb.ZMin, bb.XMin },
                Min = new double[3] { bb.YMax, bb.ZMax, bb.XMax },
                Type = "VEC3"
            });

            // # normals
            accessors.Add(new GltfAccessor()
            {
                BufferView = 1,
                ByteOffset = 0,
                ComponentType = 5126,
                Count = n,
                Max = new double[3] { 1,1,1 },
                Min = new double[3] { -1,-1,-1 },
                Type = "VEC3"
            });

            // # batched
            accessors.Add(new GltfAccessor()
            {
                BufferView = 2,
                ByteOffset = 0,
                ComponentType = 5126,
                Count = n,
                Max = new double[1] { batchLength},
                Min = new double[1] { 0 },
                Type = "SCALAR"
            });

            // # meshes
            var meshes = new List<GltfMesh>();
            var mesh = new GltfMesh() {};
            var primitive = new GltfPrimitive() { Attributes = new GltfAttribute() { Position = 2, Normal = 2 + 1, BatchID = 2 }, Material = 0, Mode = 4 };
            mesh.Primitives.Add(primitive);
            meshes.Add(mesh);

            // # nodes
            var nodes = new List<GltfNode>();
            var node = new GltfNode() { Matrix = transform, Mesh = 0 };
            nodes.Add(node);

            // # materials
            var materials = new List<GltfMaterial>();
            var material = new GltfMaterial() { Name = "Material", GltfPbrMetallicRoughness = new GltfPbrMetallicRoughness() { MetallicFactor = 0 } };
            materials.Add(material);

            // # GltfHeader
            var gltfHeader = new GltfHeader();
            gltfHeader.GltfAsset = new GltfAsset() { Generator = "Glt.Core", Version = "2.0" };
            gltfHeader.Scene = 0;
            var gltfScenes = new List<GltfScene>();
            gltfScenes.Add(new GltfScene() { Nodes = new int[nodes.Count] });
            gltfHeader.Scenes = gltfScenes;
            gltfHeader.Nodes = nodes;
            gltfHeader.Meshes = meshes;
            gltfHeader.Materials = materials;
            gltfHeader.Accessors = accessors;
            gltfHeader.BufferViews = bufferViews;
            gltfHeader.Buffers = buffers;
            return gltfHeader;
        }
    }
}
