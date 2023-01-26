using System.Collections;

namespace CTCI
{
    public class GraphNode {         
        private int _value;
        private List<GraphNode> _adj;

        public GraphNode(int val) {
            _value = val;
            _adj = new List<GraphNode>();
        }

        public int value
        {
            get { return _value; }
            set { _value = value;}
        }

        public List<GraphNode> adjacent
        {
            get { return _adj; }
            set { _adj = value; }
        }

        public void insertAdjacent(GraphNode node) {
            _adj.Add(node);
        } 

        public int numChildren() {
            return _adj.Count;
        }
    }

    public class Graph {
            public List<GraphNode> nodes;
    }

}