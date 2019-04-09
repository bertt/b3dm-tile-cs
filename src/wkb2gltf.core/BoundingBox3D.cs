using Wkx;

namespace Wkb2Gltf
{
    public class BoundingBox3D
    {
        public BoundingBox3D()
        {

        }
        public BoundingBox3D(double XMin, double YMin, double ZMin, double XMax, double YMax, double ZMax)
        {
            this.XMin = XMin;
            this.YMin = YMin;
            this.ZMin = ZMin;
            this.XMax = XMax;
            this.YMax = YMax;
            this.ZMax = ZMax;

        }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public double ZMin { get; set; }
        public double ZMax { get; set; }

        public Point GetCenter()
        {
            var x = (XMax + XMin) / 2;
            var y = (YMax + YMin) / 2;
            var z = (ZMax + ZMin) / 2;
            return new Point(x,y,z);
        }

        public BoundingBox3D TransformYToZ()
        {
            var res = new BoundingBox3D();
            res.XMin = XMin;
            res.YMin = ZMin * -1;
            res.ZMin = YMin;
            res.XMax = XMax;
            res.YMax = ZMin * -1; // heuh?
            res.ZMax = YMax;
            return res;
        }
    }
}
