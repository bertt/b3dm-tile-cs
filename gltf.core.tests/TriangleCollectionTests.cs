﻿using NUnit.Framework;
using Wkx;

namespace Gltf.Core.Tests
{
    public class TriangleCollectionTests
    {
        [Test]
        public void TrianglesBinaryConvertorTest()
        {
            var triangles = new TriangleCollection();
            var p0 = new Point(0, 0, 1);
            var p1 = new Point(1, 0, 2);
            var p2 = new Point(1, 0, 3);
            var t = new Gltf.Core.Triangle(p0, p1, p2);
            triangles.Add(t);

            var bytes = triangles.PositionsToBinary();
            Assert.IsTrue(bytes.Length > 0);
        }

        [Test]
        public void TrianglesNormalsConvertorTest()
        {
            var triangles = new TriangleCollection();
            var p0 = new Point(0, 0, 1);
            var p1 = new Point(1, 0, 2);
            var p2 = new Point(1, 0, 3);
            var t = new Gltf.Core.Triangle(p0, p1, p2);
            triangles.Add(t);

            var bytes = triangles.NormalsToBinary();
            Assert.IsTrue(bytes.Length > 0);
        }


        [Test]
        public void TrianglesNormalsTests()
        {
            var triangles = new TriangleCollection();
            var p0 = new Point(0, 0, 1);
            var p1 = new Point(1, 0, 2);
            var p2 = new Point(1, 0, 3);
            var t = new Gltf.Core.Triangle(p0, p1, p2);
            triangles.Add(t);

            var normals = triangles.GetNormals();
            Assert.IsTrue(normals.Count == 1);
        }
    }
}
