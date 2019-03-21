using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class GlbToB3dmConvertorTests
    {
        [Test]
        public void DoGlbToB3dmConvertorTests()
        {
            // arrange
            var buildingGlb = File.ReadAllBytes(@"testfixtures/building.glb");

            // act
            var b3dm = GlbToB3dmConvertor.Convert(buildingGlb);

            // assert
            Assert.IsTrue(b3dm.GlbData.Length == 2924);
        }
    }
}
