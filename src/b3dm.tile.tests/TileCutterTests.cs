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
            var zUpBoxes = GetTestData("testfixtures/bboxes.txt");
            var bboxes_gropuped_expected = GetTestData("testfixtures/bboxes_grouped_expected.txt");

            // act
            var tree = TileCutter.ConstructTree(zUpBoxes);
            var intfeatures = GetNrOfChildren(tree);
            Assert.IsTrue(intfeatures == bboxes_gropuped_expected.Count);
            var bbox0 = tree.Children[0].GetBoundingBox3D();

            // assert
            Assert.IsTrue(zUpBoxes.Count == 1580);
            Assert.IsTrue(tree.Children[0].Features.Count == 20);
            Assert.IsTrue(tree.Children[0].Features[0].Id == 0);
            Assert.IsTrue(tree.Children[0].Children[0].Features[0].Id == 20);
            Assert.IsTrue(Math.Round(bbox0.XMin,1) == Math.Round(bboxes_gropuped_expected[0].XMin,1));
            Assert.IsTrue(Math.Round(bbox0.YMin, 0) == Math.Round(bboxes_gropuped_expected[0].YMin, 0));
            Assert.IsTrue(Math.Round(bbox0.ZMin, 3) == Math.Round(bboxes_gropuped_expected[0].ZMin, 3));
            var c = bbox0.GetCenter();
        }
        public int GetNrOfChildren(Node tree)
        {
            var f = tree.Children.Count;
            foreach(var c in tree.Children) {
                f+= GetNrOfChildren(c);
            }
            return f;
        }


        private static List<BoundingBox3D> GetTestData(string file)
        {
            var fileStream = new FileStream(file, FileMode.Open);
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
