using System.Collections;
using System.Text;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
            
        }

        // Remove Dups: Write code to remove duplicates from an unsorted linked list.
        static void removeDuplicates(ListNode head) {
            if (head == null) return;
            HashSet<int> check = new HashSet<int>();
            ListNode temp = head;
            check.Add(temp.val);
            while (temp.next != null) {
                if (check.Contains(temp.next.val)) {
                    temp.next = temp.next.next;
                }
                else {
                    check.Add(temp.next.val);
                }
                temp = temp.next;
            }
        }

        // Return Kth to Last: Implement an algorithm to find the kth to last element of a singly linked list. 
        static int[] kthToLast(ListNode head, int k) {
            if (head == null) return new int[2] {0,0};
            int[] wrapper = kthToLast(head.next, k);
            wrapper[0] += 1;
            if (wrapper[0] + 1 == k) {
                wrapper[1] = head.val;
                return wrapper; 
            }
            return wrapper;
        }

    }

}