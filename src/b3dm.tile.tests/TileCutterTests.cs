using System;
using NUnit.Framework;

namespace B3dm.Tile.Tests
{
    public class TileCutterTests
    {
        [Test]
        public void TileCutterFirstTests()
        {
            // arrange
            var zUpBoxes = BBTestDataReader.GetTestData("testfixtures/bboxes.txt");
            var bboxes_gropuped_expected = BBTestDataReader.GetTestData("testfixtures/bboxes_grouped_expected.txt");

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


    }
}
