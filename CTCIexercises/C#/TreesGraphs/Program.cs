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
            return validateBSTHelper(root,Int32.MinValue,Int32.MaxValue);
        }

        static bool validateBSTHelper(TreeNode node, int l, int r) {
            if (node == null) return true;
            if (node.value < l || node.value > r)  {
                return false;
            }
            return validateBSTHelper(node.children[0],l,node.value) && validateBSTHelper(node.children[1],node.value,r);
        }

        /* Successor: Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a 
        binary search tree. You may assume that each node has a link to its parent. */
        static TreeNode successor(TreeNode node) {
            if (node == null) return null;
            if (node.children[1] != null) {
                node = node.children[1];
                while(node.children[0] != null) {
                    node = node.children[0];
                }
                return node;
            }
            while (node.parent != null && node != node.parent.children[0]) {
                node = node.parent;
            }
            return node;
        }

        /* Build Order: You are given a list of projects and a list of dependencies (which is a list of pairs of 
        projects, where the second project is dependent on the first project). All of a project's dependencies 
        must be built before the project is. Find a build order that will allow the projects to be built. If there 
        is no valid build order, return an error. */
        

        /*First Common Ancestor: Design an algorithm and write code to find the first common ancestor 
        of two nodes in a binary tree. Avoid storing additional nodes in a data structure. NOTE: This is not 
        necessarily a binary search tree. */
        static TreeNode firstCommonAncestor(TreeNode node1, TreeNode node2) {
            if(node1 == null || node2 == null) return null;
            TreeNode temp1 = node1;
            TreeNode temp2 = node2;
            int l1 = 0;
            int l2 = 0;
            while (temp1.parent != null) {
                temp1 = temp1.parent;
                l1++;
            }
            while (temp2.parent != null) {
                temp2 = temp2.parent;
                l2++;
            }
            int diff = Math.Abs(l1-l2);
            temp1 = l1 > l2 ? node1 : node2;
            temp2 = l1 > l2 ? node2: node1;
            for (int i = 0; i < diff; i++) {
                temp2 = temp2.parent;
            }
            while (temp1 != temp2 && temp1 != null && temp2 != null) {
                temp1 = temp1.parent;
                temp2 = temp2.parent;
            }
            return temp1;
        }

        /* Check Subtree: Tl and T2 are two very large binary trees, with Tl much bigger than T2. Create an 
        algorithm to determine ifT2 is a subtree of Tl. 
        A tree T2 is a subtree of Tl if there exists a node n in Tl such that the subtree of n is identical to T2. 
        That is, if you cut off the tree at node n, the two trees would be identical. */
        static bool checkSubtree(TreeNode node1, TreeNode node2) {
            if (node2 == null) return true;
            return checkSubtreeHelper(node1,node2);
        }

        static bool checkSubtreeHelper(TreeNode node1, TreeNode node2) {
            if (node1 == null) return false;
            if (node1.value == node2.value && matchTree(node1,node2)) {
                return true;
            }
            return (checkSubtreeHelper(node1.children[0],node2) || checkSubtreeHelper(node1.children[1],node2));
        }

        static bool matchTree(TreeNode node1, TreeNode node2) {
            if (node1 == null && node2 == null) return true;
            if (node1 == null || node2 != null) return false;
            if (node1.value != node2.value) return false;
            return (matchTree(node1.children[0],node2.children[0]) && matchTree(node1.children[1],node2.children[1]));
        }


        

        
    }
}