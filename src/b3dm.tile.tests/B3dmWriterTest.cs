using System;
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

            var b3dmExpected = File.ReadAllBytes(@"testfixtures/1_expected.b3dm");

            // act
            var result = @"d:\aaa\1.b3dm";
            B3dmWriter.WriteB3dm(result, b3dm);

            // Assert
            var fiResult = new FileInfo(result);
            var fiExpected = new FileInfo(@"testfixtures/1_expected.b3dm");

            Assert.IsTrue(FilesAreEqual(fiResult, fiExpected));

            Assert.IsTrue(fiResult.Length == b3dmExpected.Length);
        }

        [Test]
        public void WriteB3dmWithBatchTest()
        {
            // arrange
            var buildingGlb = File.ReadAllBytes(@"testfixtures/with_batch.glb");
            var batchTableJson = File.ReadAllText(@"testfixtures/BatchTableJsonExpected.json");

            var b3dmBytesExpected = File.OpenRead(@"testfixtures/with_batch.b3dm");
            var b3dmExpected = B3dmReader.ReadB3dm(b3dmBytesExpected);

            var b3dm = new B3dm(buildingGlb);
            b3dm.FeatureTableJson = b3dmExpected.FeatureTableJson;
            b3dm.BatchTableJson = b3dmExpected.BatchTableJson;
            b3dm.FeatureTableBinary = b3dmExpected.FeatureTableBinary;
            b3dm.BatchTableBinary = b3dmExpected.BatchTableBinary;

            // act
            var result = @"d:\aaa\with_batch.b3dm";
            B3dmWriter.WriteB3dm(result, b3dm);
            var b3dmActual = B3dmReader.ReadB3dm(File.OpenRead(result));

            // Assert
            Assert.IsTrue(b3dmActual.B3dmHeader.Magic == b3dmExpected.B3dmHeader.Magic);
            Assert.IsTrue(b3dmActual.B3dmHeader.Version== b3dmExpected.B3dmHeader.Version);
            Assert.IsTrue(b3dmActual.B3dmHeader.FeatureTableJsonByteLength== b3dmExpected.B3dmHeader.FeatureTableJsonByteLength);
            Assert.IsTrue(b3dmActual.B3dmHeader.BatchTableJsonByteLength== b3dmExpected.B3dmHeader.BatchTableJsonByteLength);
            Assert.IsTrue(b3dmActual.B3dmHeader.ByteLength== b3dmExpected.B3dmHeader.ByteLength);

            var fiResult = new FileInfo(result);
            var fiExpected = new FileInfo(@"testfixtures/with_batch.b3dm");

            Assert.IsTrue(fiResult.Length == fiExpected.Length);
            Assert.IsTrue(FilesAreEqual(fiResult, fiExpected));
        }


        const int BYTES_TO_READ = sizeof(Int64);

        static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            if (string.Equals(first.FullName, second.FullName, StringComparison.OrdinalIgnoreCase))
                return true;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead()) {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++) {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }
    }
}
