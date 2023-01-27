using System.Collections;
using System.Text;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
          
        }

       /* Route Between Nodes: Given a directed graph, design an algorithm to find out whether there is a 
        route between two nodes. */
        static bool routeBetweenNodes(GraphNode node1, GraphNode node2) {
            if (node1.numChildren() == 0 || node2.numChildren() == 0) return false;
            Queue<GraphNode> nodequeue = new Queue<GraphNode>();
            nodequeue.Enqueue(node1);
            HashSet<GraphNode> visited = new HashSet<GraphNode>();
            visited.Add(node1);
            while (nodequeue.Count != 0) {
                GraphNode node = nodequeue.Dequeue();
                if (node == node2) {
                    return true;
                }
                foreach (GraphNode adj in node.adjacent) {
                    if (!visited.Contains(adj)) {
                        visited.Add(adj);
                        nodequeue.Enqueue(adj);
                    }
                }
            }
            return false;
        }

        /* Minimal Tree: Given a sorted (increasing order) array with unique integer elements, write an 
        algorithm to create a binary search tree with minimal height. */
        static TreeNode minimalTree(int[] arr) {
            if (arr.Length == 0) return null;
            return minimalTreeHelper(arr,0,arr.Length);
        }

        static TreeNode minimalTreeHelper(int[] arr, int l, int r) {
            if (l == r) return null;
            int middle = (l + r) / 2;
            TreeNode node = new TreeNode(arr[middle]);
            TreeNode left = minimalTreeHelper(arr,l,middle);
            if (left != null) node.insertChild(left);
            TreeNode right = minimalTreeHelper(arr,middle,r);
            if (right != null) node.insertChild(right);
            return node;
        }

        /* List of Depths: Given a binary tree, design an algorithm which creates a linked list of all the nodes 
        at each depth (e.g., if you have a tree with depth D, you'll have D linked lists). */
        static List<LinkedList<TreeNode>> listOfDepths(TreeNode root) {
            List<LinkedList<TreeNode>> lists = new List<LinkedList<TreeNode>>();
            listOfDepthsHelper(root,lists,0);
            return lists;
        }

        static void listOfDepthsHelper(TreeNode node, List<LinkedList<TreeNode>> lists,int depth) {
            if (depth == lists.Count) {
                lists.Add(new LinkedList<TreeNode>());
            }
            lists[depth].AddFirst(node);
            foreach (TreeNode child in node.children) {
                listOfDepthsHelper(child,lists,depth+1);
            }
        }

        /* Check Balanced: Implement a function to check if a binary tree is balanced. For the purposes of 
        this question, a balanced tree is defined to be a tree such that the heights of the two subtrees of any 
        node never differ by more than one. */
        static bool checkBalanced(TreeNode root) {
            if (root == null) return false;
            int diff = checkBalancedHelper(root);
            return (!(diff == Int32.MinValue));
        }

        static int checkBalancedHelper(TreeNode node) {
            if (node == null) return -1;

            int left = checkBalancedHelper(node.children[0]);
            if (left == Int32.MinValue) {
                return Int32.MinValue;
            }
            int right = checkBalancedHelper(node.children[1]);
            if (right == Int32.MinValue) {
                return Int32.MinValue;
            }

            if (Math.Abs(left-right) > 1) {
                return Int32.MinValue;
            }
            return Math.Max(left,right) + 1;
        }

        /* Validate BST: Implement a function to check if a binary tree is a binary search tree. */
        static bool validateBST(TreeNode root) {
            return validadeBSTHelper(root,Int32.MinValue,Int32.MaxValue);
        }

        static bool validateBSTHelper(TreeNode node, int l, int r) {
            if (node == null) return true;
            if (node.value < l || node.value > r)  {
                return false;
            }
            return validateBST(node.left,l,node.value) && validateBSTHelper(node.right,node.value,r);
        }

        /* Successor: Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a 
        binary search tree. You may assume that each node has a link to its parent. */
        static TreeNode successor(TreeNode node) {
            if (node == null) return null;
            if (node.children[1] != null) {
                node = node.right;
                while(node.children[0] != null) {
                    node = node.left;
                }
                return node;
            }
            while (node.parent != null && node != node.parent.children[0]) {
                node = node.parent;
            }
            return node;
        }
    }
}