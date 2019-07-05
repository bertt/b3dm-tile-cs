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

            // filesize must be same as glbToB3dm (missing 20 bytes)... 
            Assert.IsTrue(fiResult.Length == b3dmExpected.Length);
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
