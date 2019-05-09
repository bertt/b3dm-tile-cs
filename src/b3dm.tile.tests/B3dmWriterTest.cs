using System.IO;
using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class B3dmWriterTest
    {
        [Test]
        public void WriteB3dmTest()
        {
            // arrange
            var buildingGlb = File.ReadAllBytes(@"testfixtures/building.glb");
            var b3dm = new B3dm(buildingGlb);

            // act
            B3dmWriter.WriteB3dm($"output.b3dm", b3dm);

            // Assert
        }
    }
}
