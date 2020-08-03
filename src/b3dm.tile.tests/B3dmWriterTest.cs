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
            var buildingGlb = File.ReadAllBytes(@"testfixtures/1.glb");
            var b3dm = new B3dm(buildingGlb);

            // act
            var bytes = b3dm.ToBytes();

            // Assert
            Assert.IsTrue(bytes.Length == 94732);
        }

        [Test]
        public void WriteB3dmWithBatchTest()
        {
            // arrange
            var buildingGlb = File.ReadAllBytes(@"testfixtures/with_batch.glb");
            var batchTableJson = File.ReadAllText(@"testfixtures/BatchTableJsonExpected.json");

            var b3dmBytesExpected = File.OpenRead(@"testfixtures/with_batch.b3dm");
            var b3dmExpected = B3dmReader.ReadB3dm(b3dmBytesExpected);
            var errors = b3dmExpected.B3dmHeader.Validate();
            Assert.IsTrue(errors.Count > 0);

            var b3dm = new B3dm(buildingGlb);
            b3dm.FeatureTableJson = b3dmExpected.FeatureTableJson;
            b3dm.BatchTableJson = b3dmExpected.BatchTableJson;
            b3dm.FeatureTableBinary = b3dmExpected.FeatureTableBinary;
            b3dm.BatchTableBinary = b3dmExpected.BatchTableBinary;

            // act
            var result = "with_batch.b3dm";
            var newB3dm = b3dm.ToBytes();
            var fs = new FileStream(result, FileMode.Create, FileAccess.Write);
            fs.Write(newB3dm, 0, newB3dm.Length);
            fs.Close();

            var b3dmActual = B3dmReader.ReadB3dm(File.OpenRead(result));

            // Assert
            var errorsActual = b3dmActual.B3dmHeader.Validate();
            Assert.IsTrue(errorsActual.Count == 0);

            Assert.IsTrue(b3dmActual.B3dmHeader.Magic == b3dmExpected.B3dmHeader.Magic);
            Assert.IsTrue(b3dmActual.B3dmHeader.Version== b3dmExpected.B3dmHeader.Version);
            Assert.IsTrue(b3dmActual.B3dmHeader.FeatureTableJsonByteLength== b3dmExpected.B3dmHeader.FeatureTableJsonByteLength);
        }
    }
}
