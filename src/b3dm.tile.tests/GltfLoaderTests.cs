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
            var translation = new float[] { 1842015.125f, 5177109.25f, 247.87364196777344f };
            var gltf = Gltf2Loader.GetGltf(polyhedralsurface, translation);
            gltf.Gltf.SaveBinaryModel(gltf.Body, @"d:\aaa\hihi.glb");
            //var ms = new MemoryStream();
            //gltf.SaveBinaryModel(body, ms);
            //var s = ms.ToArray();
            //Assert.IsTrue(ms != null);
        }

    }
}
