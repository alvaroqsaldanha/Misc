using System.Collections;

namespace CTCI
{

    class TreeNode {
        int value;
        public TreeNode[] children;

        public TreeNode(int value){
            this.value = value;
            this.children = new TreeNode[2];
        }

        public TreeNode(int value, TreeNode[] children){
            this.value = value;
            this.children = children;
        }

        public void addChild(TreeNode child,int index){
            this.children[index] = child;
        }

        public int getValue() {
            return this.value;
        }

        public override string ToString(){
            string rtrn = this.value.ToString() + "\n";
            foreach (TreeNode child in this.children) {
                if (child != null)
                    rtrn += child.ToString();
            }
            return rtrn;
        }
    }

    class TreeLinkedListNode {

        TreeNode node;
        TreeLinkedListNode next;

        public TreeLinkedListNode(TreeNode node) {
            this.node =  node;
            this.next = null;
        }

        public void setNext(TreeNode next) {
            TreeLinkedListNode temp = this;
            while (temp.next != null) {
                temp = temp.next;
            }
            temp.next = new TreeLinkedListNode(next);
        }

        public override string ToString(){
            string rtrn = "";
            TreeLinkedListNode temp = this;
            while (temp != null) {
                rtrn += temp.node.getValue().ToString();
                temp = temp.next;
            }
            return rtrn;
        }
    }

    class Trees {         
        
        static public TreeNode minimalTree(int[] arr,int start, int end) {
            int middle = (start + end) / 2;
            if (end < start) {
                return null;
            }
            TreeNode root = new TreeNode(arr[middle]);
            root.addChild(minimalTree(arr,start,middle-1),0);
            root.addChild(minimalTree(arr,middle+1,end),1);
            return root;
        }

        static public void listOfDepthsAux(TreeNode root, List<TreeLinkedListNode> lists, int level) {
            if (root == null) {
                return;
            }
            if (level == lists.Count) {
                TreeLinkedListNode list = new TreeLinkedListNode(root);
                lists.Add(list);
            }
            else {
                lists[level].setNext(root);
            }
            listOfDepthsAux(root.children[0],lists,level+1);
            listOfDepthsAux(root.children[1],lists,level+1);
        }

        static public List<TreeLinkedListNode> listOfDepths(TreeNode root) {
            List<TreeLinkedListNode> lists = new List<TreeLinkedListNode>();
            listOfDepthsAux(root,lists,0);
            return lists;
        }

        static public bool validateBST(TreeNode root) {
            
        }
    }
}