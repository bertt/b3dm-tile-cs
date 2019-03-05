using Gltf.Core;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace B3dm.Tile.Tests
{
    public class B3dmReaderTests
    {
        Stream b3dmfile;

        [SetUp]
        public void Setup()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.29.b3dm";
            b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            Assert.IsTrue(b3dmfile != null);
        }

        [Test]
        public void ReadB3dmTest()
        {
            // arrange
            var expectedMagicHeader = "b3dm";
            var expectedVersionHeader = 1;

            // act
            var b3dm = B3dmReader.ReadB3dm(b3dmfile);

            // assert
            Assert.IsTrue(expectedMagicHeader == b3dm.Magic);
            Assert.IsTrue(expectedVersionHeader == b3dm.Version);
            // batchtable json contains string like {\"id\":[22,22,27,27,179,179,171,171,143,143,27,27,171,171,58,58,143,143,179,179]}
            Assert.IsTrue(b3dm.BatchTableJson.Length > 0); 
            Assert.IsTrue(b3dm.GlbData.Length > 0);
            Assert.IsTrue(GltfVersionChecker.GetGlbVersion(b3dm.GlbData) == 2);
        }

        [Test]
        public void ReadPy3DTilesB3dm()
        {
            const string testfile = "B3dm.Tile.Tests.testfixtures.py3dtiles_test_build_1.b3dm";
            b3dmfile = Assembly.GetExecutingAssembly().GetManifestResourceStream(testfile);
            Assert.IsTrue(b3dmfile != null);
            var b3dm = B3dmReader.ReadB3dm(b3dmfile);
            var glbBinary = b3dm.GlbData;
            var glb = GlbReader.ReadGlb(new MemoryStream(glbBinary));
            Assert.IsTrue(glb.GltfModelJson == "{\"asset\":{\"generator\":\"py3dtiles\",\"version\":\"2.0\"},\"scene\":0,\"scenes\":[{\"nodes\":[0]}],\"nodes\":[{\"matrix\":[1.0,0.0,0.0,0.0,0.0,1.0,0.0,0.0,0.0,0.0,1.0,0.0,1842015.125,5177109.25,247.87364196777344,1.0],\"mesh\":0}],\"meshes\":[{\"primitives\":[{\"attributes\":{\"POSITION\":0,\"NORMAL\":1,\"_BATCHID\":2},\"material\":0,\"mode\":4}]}],\"materials\":[{\"pbrMetallicRoughness\":{\"metallicFactor\":0},\"name\":\"Material\"}],\"accessors\":[{\"bufferView\":0,\"byteOffset\":0,\"componentType\":5126,\"count\":66,\"max\":[-7.35523200035095,-2.05385796777344,-8.74748499994166],\"min\":[7.29930999968201,2.05386103222656,8.8036420000717],\"type\":\"VEC3\"},{\"bufferView\":1,\"byteOffset\":0,\"componentType\":5126,\"count\":66,\"max\":[1,1,1],\"min\":[-1,-1,-1],\"type\":\"VEC3\"},{\"bufferView\":2,\"byteOffset\":0,\"componentType\":5126,\"count\":66,\"max\":[1],\"min\":[0],\"type\":\"SCALAR\"}],\"bufferViews\":[{\"buffer\":0,\"byteLength\":792,\"byteOffset\":0,\"target\":34962},{\"buffer\":0,\"byteLength\":792,\"byteOffset\":792,\"target\":34962},{\"buffer\":0,\"byteLength\":264,\"byteOffset\":1584,\"target\":34962}],\"buffers\":[{\"byteLength\":1848}]}");
            Assert.IsTrue(glb.GltfModelBin.Length==1848);

        }
    }
}