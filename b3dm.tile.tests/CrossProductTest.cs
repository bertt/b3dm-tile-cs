using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Wkx;

namespace B3dm.Tile.Tests
{
    public class CrossProductTest
    {
        // (-0.06183529, 5.856007, 0.0) cross(-0.06183529, 5.856007 , -3.9624841) = -23.204334 , -0.24502136, 0
        [Test]
        public void TestCrossProduct()
        {
            var p1 = new Point(-0.06183529, 5.856007, 0.0);
            var p2 = new Point(-0.06183529, 5.856007, -3.9624841);

            var result = p1.Cross(p2);
            Assert.IsTrue(result.X == -23.2043346269887);
            Assert.IsTrue(result.Y ==-0.245021353443889);
            Assert.IsTrue(result.Z == 0);
        }
    }
}
