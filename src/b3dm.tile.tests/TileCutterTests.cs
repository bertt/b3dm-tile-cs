using System;
using System.Collections.Generic;
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
            var zUpBoxes = BBTestDataReader.GetTestData("testfixtures/zupboxes_actual.txt");
            var bboxes_grouped_expected = BBTestDataReader.GetTestData("testfixtures/bboxes_grouped_expected.txt");

            // act
            var tree = TileCutter.ConstructTree(zUpBoxes);

            var intfeatures = GetNrOfChildren(tree);
            Assert.IsTrue(intfeatures == bboxes_grouped_expected.Count);
            Assert.IsTrue(intfeatures == 124);

            // assert
            Assert.IsTrue(tree.CalculateBoundingBox3D().Equals(bboxes_grouped_expected[0]));
            Assert.IsTrue(tree.Children[0].CalculateBoundingBox3D().Equals(bboxes_grouped_expected[1]));
            Assert.IsTrue(tree.Children[0].Children[0].CalculateBoundingBox3D().Equals(bboxes_grouped_expected[2]));
            Assert.IsTrue(tree.Children[0].Children[0].Children[0].CalculateBoundingBox3D().Equals(bboxes_grouped_expected[3]));
            // todo: fix next one...
            // Assert.IsTrue(tree.Children[0].Children[0].Children[0].Children[0].CalculateBoundingBox3D().Equals(bboxes_grouped_expected[4]));


            Assert.IsTrue(zUpBoxes.Count == 1580);
            Assert.IsTrue(tree.Children[0].Features.Count == 20);
            Assert.IsTrue(tree.Children[0].Features[0].Id == 0);
            Assert.IsTrue(tree.Children[0].Children[0].Features[0].Id == 20);
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
