using System.Collections.Generic;

namespace B3dm.Tile
{
    public class Node
    {
        public Node()
        {
            Children = new List<Node>();
        }
        public List<Node> Children { get; set; }
        public List<Feature> Features { get; set; }

        public TileSet ToTileset(float[] transform)
        {
            var tileset = new TileSet();
            tileset.asset = new Asset() { version = "1.0" };
            tileset.geometricError = 500;

            // todo: add tiles recursive
            tileset.root = new Root() { transform = transform };
            return tileset;
        }

        // public to_tileset_r
    }
}
