using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;

namespace Gltf.Core.Tests
{
    public class GltfHeaderTests
    {
        [Test]
        public void ReadExpectedGltfHeader()
        {
            // deserialize JSON directly from a file
            using (var file = File.OpenText(@".\testfixtures\building_header.json"))
            {
                var serializer = new JsonSerializer();
                var header = (Header)serializer.Deserialize(file, typeof(Header));
                Assert.IsTrue(header.asset.generator == "py3dtiles");
            }
        }
    }
}
