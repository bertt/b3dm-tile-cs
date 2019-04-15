using System;
using System.Collections.Generic;
using Wkb2Gltf;

namespace B3dm.Tile
{
    public class Node
    {
        public Node()
        {
            Children = new List<Node>();
            Features = new List<Feature>();
        }
        public List<Node> Children { get; set; }
        public List<Feature> Features { get; set; }

        public TileSet ToTileset(double[] transform)
        {
            var tileset = new TileSet();
            tileset.asset = new Asset() { version = "1.0" };
            var t = new double[] { 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, Math.Round(transform[0],3), Math.Round(transform[1],3), Math.Round(transform[2],3), 1.0 };
            tileset.geometricError = 500;
            tileset.root = new Root() { transform = t };
            tileset.root.geometricError = 500;
            tileset.root.refine = "add";
            tileset.root.boundingVolume = new Boundingvolume();
            var bbox = GetBoundingBox3D();
            tileset.root.boundingVolume.box = bbox.GetBox();
            // todo: add tiles recursive
            return tileset;
        }

        public List<BoundingBox3D> GetBoundingBoxes3D()
        {
            var bboxes = new List<BoundingBox3D>();
            foreach (var f in Features) {
                bboxes.Add(f.BoundingBox3D);
            }

            foreach(var child in Children) {
                var newboxes = child.GetBoundingBoxes3D();
                bboxes.AddRange(newboxes);
            }
            return bboxes;
        }

        // returns the boundingbox including children
        public BoundingBox3D GetBoundingBox3D()
        {
            var bboxes = GetBoundingBoxes3D();
            var bbox = BoundingBoxCalculator.GetBoundingBox(bboxes);
            return bbox;
        }
    }
}
