using System.Collections.Generic;

namespace CTCI
{

    class TreeNode {
        int value;
        TreeNode[] children;

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

        public override string ToString(){
            string rtrn = this.value.ToString() + "\n";
            foreach (TreeNode child in this.children) {
                if (child != null)
                    rtrn += child.ToString();
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
    }
}