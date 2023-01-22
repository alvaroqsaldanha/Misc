using System.Collections;
using System.Text;

namespace CTCI
{
    public class ListNode {         
        
        private int _val = 0;
        private ListNode _next = null;

        public ListNode(int val) {
            _val = val;
        }

        public int val
        {
            get { return val; }
            set { val = value;}
        }

        public ListNode next
        {
            get { return _next; }
            set { _next = value; }
        }
    }

}