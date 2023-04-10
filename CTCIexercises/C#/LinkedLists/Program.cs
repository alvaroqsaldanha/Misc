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
                    temp = temp.next;
                }
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

        /* Delete Middle Node: Implement an algorithm to delete a node in the middle (i.e., any node but 
        the first and last node, not necessarily the exact middle) of a singly linked list, given only access to 
        that node. */
        static void deleteMiddleNode(ListNode middle) {
            if (middle == null || middle.next == null) return; 
            middle.val = middle.next.val;
            middle.next = middle.next.next;
        }

        /* Partition: Write code to partition a linked list around a value x, such that all nodes less than x come 
        before all nodes greater than or equal to x. If x is contained within the list the values of x only need 
        to be after the elements less than x (see below). The partition element x can appear anywhere in the 
        "right partition"; it does not need to appear between the left and right partitions. */
        static ListNode partition(ListNode head, int k) {
            if (head == null) return null;
            ListNode smaller = null;
            ListNode larger = null;
            ListNode temp = head;
            ListNode smallerhead = null;
            ListNode largerhead = null;
            while (temp != null) {
                if (temp.val < k) {
                    if (smaller != null) {
                        smaller.next = temp;
                        smaller = smaller.next;
                    } 
                    else {
                        smaller = temp;
                        smallerhead = temp;
                    }
                }
                else {
                    if (larger != null) {
                        larger.next = temp;
                        larger = larger.next;
                    } 
                    else {
                        larger = temp;
                        largerhead = temp;
                    }
                }
            }
            smaller.next = largerhead;
            return smallerhead;
        }

        /* Sum Lists: You have two numbers represented by a linked list, where each node contains a single 
        digit. The digits are stored in reverse order, such that the 1 's digit is at the head of the list. Write a 
        function that adds the two numbers and returns the sum as a linked list.  */
        static ListNode sumLists(ListNode list1, ListNode list2) {
            if (list1 == null || list2 == null) return null;
            int carry = 0;
            ListNode head = null;
            ListNode current = null;
            while (list1 != null || list2 != null || carry != 0) {
                int x = list1 == null ? 0 : list1.val;
                int y = list2 == null ? 0 : list2.val;
                int val = x + y + carry;
                carry = val / 10;
                val = val % 2;
                if (current == null) {
                    current = new ListNode(val);
                    head = current;
                }
                else {
                    current.next = new ListNode(val);
                    current = current.next;
                }
                list1 = list1 == null ? list1 : list1.next;
                list2 = list2 == null ? list2 : list2.next;
            }
            return head;
        }

        // Palindrome: Implement a function to check if a linked list is a palindrome. 
        static bool palindrome(ListNode head) {
            ListNode runner = head;
            ListNode slow = head;
            Stack<int> keep = new Stack<int>();
            while (runner != null && runner.next != null) {
                runner = runner.next.next;
                slow = slow.next;
                keep.Push(slow.val);
            }
            if (runner != null) {
                slow = slow.next;
            }
            while (slow != null) {
                if (slow.val != keep.Pop()) {
                    return false;
                }
                slow = slow.next;
            }
            return true;
        }

        /* Intersection: Given two (singly) linked lists, determine if the two lists intersect. Return the 
        intersecting node. Note that the intersection is defined based on reference, not value. That is, if the 
        kth node of the first linked list is the exact same node (by reference) as the jth node of the second 
        linked list, then they are intersecting. */
        static ListNode intersection(ListNode head1, ListNode head2) {
            ListNode temp1 = head1;
            ListNode temp2 = head2;
            int length1 = 0;
            int length2 = 0;
            while (temp1.next != null || temp2.next != null) {
                if (temp1.next !=  null) {
                    temp1 = temp1.next;
                    length1++;
                }
                if (temp2.next !=  null) {
                    temp2 = temp2.next;
                    length2++;
                }
            }
            if (temp1 != temp2) return null;
            ListNode shortlist = length1 > length2 ? head2 : head1;
            ListNode longlist = length1 > length2 ? head1 : head2;

            int diff = Math.Abs(length2 - length1);
            for (int i = 0; i < diff; i++) {
                longlist = longlist.next;
            }

            while (longlist != shortlist) {
                longlist = longlist.next;
                shortlist = shortlist.next;
            }

            return longlist;
        }

        /* Loop Detection: Given a circular linked list, implement an algorithm that returns the node at the 
        beginning of the loop */
        static ListNode loopDetection(ListNode head) {
            if (head == null) return null;
            ListNode slow = head;
            ListNode runner = head.next;
            while (slow != runner && runner != null && runner.next != null) {
                slow = slow.next;
                runner = runner.next.next;
            }
            if (runner == null || runner.next == null) return null;
            runner = head;
            while (slow != head) {
                slow = slow.next;
                runner = runner.next;
            }
            return slow;
        }
        
        /* OddEven Linked List */
        public ListNode OddEvenList(ListNode head) {
            if (head == null) return null;
            ListNode odd = head;
            ListNode even = head.next;
            ListNode headEven = even;
            while (even != null && even.next != null) {
                odd.next = even.next;
                even.next = odd.next.next;
                odd = odd.next;
                even = even.next;
            }
            odd.next = headEven;
            return head;
        }
        
        public class Count {
             public int counter = 0;
        }

    public int KthSmallest(TreeNode root, int k) {
        if (root == null || k == 0) return 0;
        return inorder(root,k,new Count());
    }

    public int inorder(TreeNode node, int k, Count counter) {
        if (node == null) return 0;
        int left = inorder(node.left,k,counter);
        if (counter.counter == k) {
            return left;
        }
        counter.counter++;
        if (counter.counter == k) {
            return node.val;
        }
        int right = inorder(node.right,k,counter);
        if (counter.counter == k) {
            return right;
        }
        return 0;
    }

    }
    
    public class DLinkedListNode {
    public DLinkedListNode next;
    public DLinkedListNode prev;
    public int val;
    public int key;

    public DLinkedListNode(DLinkedListNode next, DLinkedListNode prev, int val, int key) {
        this.next = next;
        this.prev = prev;
        this.val = val;
        this.key = key;
    }
}

public class LRUCache {

    Dictionary<int,DLinkedListNode> cache = new Dictionary<int,DLinkedListNode>();
    DLinkedListNode head;
    DLinkedListNode tail;
    int capacity = 0;
    int count = 0;

    public LRUCache(int capacity) {
        this.capacity = capacity;
    }

    public void moveToHead(DLinkedListNode temp) {
        if (this.head == null) this.head = temp;
        if (temp == this.head) return;
        temp.prev.next = temp.next;
        if (temp == tail) {
            tail = temp.prev;
        }
        else {
            temp.next.prev = temp.prev;
        }
        temp.next = head;
        head.prev = temp;
        temp.prev = null;
        head = temp;
    }
    
    public int Get(int key) {
        if (cache.Count == 0) return -1;
        if (!cache.ContainsKey(key)) return -1;
        DLinkedListNode temp = cache[key];
        moveToHead(temp);
        return cache[key].val;
    }
    
    public void Put(int key, int value) {
        if (cache.ContainsKey(key)) {
            cache[key].val = value;
            DLinkedListNode temp = cache[key];
            moveToHead(temp);
            return;
        }
        DLinkedListNode node = new DLinkedListNode(this.head,null,value,key);
        if (head != null) {
            head.prev = node;
        }
        if (tail == null) {
            tail = node;
        }
        head = node;
        if (this.count < this.capacity) {
            this.count++;
        }
        else {
            cache.Remove(tail.key);
            tail = tail.prev;
            tail.next = null;
        }
        cache.Add(key,node);
    }
}

}
