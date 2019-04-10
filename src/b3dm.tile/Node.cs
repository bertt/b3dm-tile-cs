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
    }
}
