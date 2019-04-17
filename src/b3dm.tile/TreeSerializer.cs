using System;
using System.Collections.Generic;
using Wkb2Gltf;

namespace B3dm.Tile
{
    public static class TreeSerializer
    {
        public static TileSet ToTileset(Node n,double[] transform)
        {
            Counter.Count = 1;
            var tileset = new TileSet();
            tileset.asset = new Asset() { version = "1.0" };
            var t = new double[] { 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, Math.Round(transform[0], 3), Math.Round(transform[1], 3), Math.Round(transform[2], 3), 1.0 };
            tileset.geometricError = 500;
            tileset.root = new Root() { transform = t };
            tileset.root.geometricError = 500;
            tileset.root.refine = "add";
            tileset.root.boundingVolume = new Boundingvolume();
            var bbox = new BoundingBox3D(); ;
            tileset.root.boundingVolume.box = bbox.GetBox();
            var rootChild = GetChild(bbox, 250);
            rootChild.content = new Content() { uri = $"tiles/1.b3dm" };
            var childs = new List<Child>() { rootChild };
            tileset.root.children = childs;

            foreach (var node in n.Children) {
                AddNode(rootChild, node, 250 / 2);
            }
            return tileset;
        }

        private static void AddNode(Child parent, Node node, double geometricError)
        {
            Counter.Count++;
            var bbox = new BoundingBox3D();
            var newchild = GetChild(bbox, geometricError);
            newchild.content = new Content() { uri = $"tiles/{Counter.Count}.b3dm" };
            if (parent.children == null) {
                parent.children = new List<Child>();
            }

            foreach (var node1 in node.Children) {
                AddNode(newchild, node1, geometricError / 2);
                parent.children.Add(newchild);
            }
        }

        private static Child GetChild(BoundingBox3D bbox, double GeometricError)
        {
            var child = new Child();
            child.refine = "add";
            child.geometricError = Math.Round(GeometricError, 2);
            child.boundingVolume = new Boundingvolume();
            child.boundingVolume.box = bbox.GetBox();
            return child;
        }
    }
}
