using System;
using System.Collections.Generic;
using Wkx;

namespace Wkb2Gltf
{
    public class BoundingBoxCalculator
    {
        public static BoundingBox GetBoundingBox(List<BoundingBox3D> boxes)
        {
            var xmin = double.MaxValue;
            var ymin = double.MaxValue;
            var xmax = double.MinValue;
            var ymax = double.MinValue;

            foreach (var box in boxes) {
                xmin = box.XMin < xmin ? box.XMin : xmin;
                ymin = box.YMin < ymin ? box.YMin : ymin;
                xmax = box.XMax > xmax ? box.XMax : xmax;
                ymax = box.YMax > ymax ? box.YMax : ymax;
            }

            return new BoundingBox(xmin, ymin, xmax, ymax);
        }
    }
}
