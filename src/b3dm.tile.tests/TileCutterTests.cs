using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Wkb2Gltf;

namespace B3dm.Tile.Tests
{
    public class TileCutterTests
    {
        [Test]
        public void TileCutterFirstTests()
        {
            // arrange
            var zUpBoxes = GetTestData();

            // act
            var tree= TileCutter.ConstructTree(zUpBoxes);
            var bbox0 = tree.Children[0].GetBoundingBox3D();
            // assert
            Assert.IsTrue(zUpBoxes.Count == 1580);
            Assert.IsTrue(tree.Children[0].Features.Count == 20);
            Assert.IsTrue(tree.Children[0].Features[0].Id == 0);
            Assert.IsTrue(tree.Children[0].Children[0].Features[0].Id == 20);
            Assert.IsTrue(bbox0.XMin == -183.897999999986);
            Assert.IsTrue(bbox0.XMax == 183.847000000009);
            Assert.IsTrue(bbox0.YMin == -134.141648505291);
            Assert.IsTrue(bbox0.YMax == 138.625);
            Assert.IsTrue(bbox0.ZMin == -11.7305252024974);
            Assert.IsTrue(bbox0.ZMax == 11.7305220562124);
            var c = bbox0.GetCenter();
        }


        private static List<BoundingBox3D> GetTestData()
        {
            var fileStream = new FileStream("testfixtures/bboxes.txt", FileMode.Open);
            var bboxes = new List<BoundingBox3D>();
            var reader = new StreamReader(fileStream);
            string line;
            while ((line = reader.ReadLine()) != null) {
                var splitted = line.Split(",");
                var bbox = new BoundingBox3D();
                bbox.XMin = Double.Parse(splitted[0]);
                bbox.YMin = Double.Parse(splitted[1]);
                bbox.ZMin = Double.Parse(splitted[2]);
                bbox.XMax = Double.Parse(splitted[3]);
                bbox.YMax = Double.Parse(splitted[4]);
                bbox.ZMax = Double.Parse(splitted[5]);
                bboxes.Add(bbox);
            }
            fileStream.Close();
            return bboxes;
        }
    }
}
