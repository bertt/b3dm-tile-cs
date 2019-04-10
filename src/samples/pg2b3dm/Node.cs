using System.Collections.Generic;

namespace pg2b3dm
{
    public class Node
    {
        public Node()
        {
            Children = new List<Node>();
        }
        public List<Node> Children { get; set; }
        public List<Feature> FeatureIds { get; set; }
    }
}
