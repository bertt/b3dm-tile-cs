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

            // assert
            Assert.IsTrue(zUpBoxes.Count == 1580);
            Assert.IsTrue(tree.Children[0].Features.Count == 20);
            Assert.IsTrue(tree.Children[0].Features[0].Id == 0);
            Assert.IsTrue(tree.Children[0].Children[0].Features[0].Id == 20);
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
