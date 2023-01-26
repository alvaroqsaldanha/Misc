using System.Collections;

namespace CTCI
{
    public class TreeNode {         
        private int _value;
        private List<TreeNode> _children;

        public TreeNode(int val) {
            _value = val;
            _children = new List<TreeNode>();
            _children.Add(null);
            _children.Add(null);
        }

        public int value
        {
            get { return _value; }
            set { _value = value;}
        }

        public List<TreeNode> children
        {
            get { return _children; }
            set { _children = value; }
        }

        public void insertChild(TreeNode node) {
            _children.Add(node);
        } 
    }

    public class Tree {
        public TreeNode root;
        public Tree(TreeNode node) {
            root = node;
        }
    }

}