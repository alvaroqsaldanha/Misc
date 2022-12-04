using System.Collections.Generic;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
            LinkedListNode head = new LinkedListNode(1);
            head.insert(3);
            head.insert(9);
            head.insert(9);
            head.insert(1);
            Console.WriteLine(isPalindrome(head));
            Console.WriteLine(isPalindromeRecursive(head));
        }

        static void deleteDups(LinkedListNode head) {
            HashSet<int> buffer = new HashSet<int>();
            LinkedListNode prev = null;
            LinkedListNode n = head;
            while (n != null) {
                if (buffer.Contains(n.data)) {
                    prev.next = n.next;
                }
                else {
                    buffer.Add(n.data);
                    prev = n;
                }
                n =  n.next;
            }
        }

        // Intersection
        static LinkedListNode intersection(LinkedListNode head1, LinkedListNode head2) {
            LinkedListNode temp1 = head1;
            LinkedListNode temp2 = head2;
            int length1 = 0;
            int length2 = 0;
            while (temp1 != null) {
                length1++;
                temp1 = temp1.next;
            }
            while (temp2 != null) {
                length2++;
                temp2 = temp2.next;
            }
            int diff = Math.Abs(length1 - length2);
            temp1 = length1 > length2 ? head1 : head2;
            temp2 = length1 > length2 ? head2 : head1;
            for (int i = 0; i < diff; i++) {
                temp1 = temp1.next;
            }
            while (temp1 != temp2) {
                temp1 = temp1.next;
                temp2 = temp2.next;
            }
            return temp1;
        }

        // Loop Detection
        static LinkedListNode loopDetection(LinkedListNode head) {
            if (head == null)
                return null;
            LinkedListNode slow = head;
            LinkedListNode runner = head.next;
            while (runner != null && runner.next != null) {
                if (slow == runner) {
                    break;
                }
                slow = slow.next;
                runner = runner.next.next;
            }
            if (runner == null || runner.next == null) {
                return null;
            }
            slow = head;
            while (slow != runner) {
                slow = slow.next;
                runner = runner.next;
            }
            return slow;

        }

        // Delete Middle Node
        static void deleteMiddleNode(LinkedListNode del) {
            LinkedListNode n = del;
            n.data = n.next.data;
            n.next = n.next.next;
        }

        //Sum Lists
        static LinkedListNode sumLists(LinkedListNode head1, LinkedListNode head2) {
            LinkedListNode temp1 = head1;
            LinkedListNode temp2 = head2;
            LinkedListNode sum = null;
            LinkedListNode head = null;
            int carry = 0;
            while (temp1 != null && temp2 != null) {
                if (sum == null) {
                    sum = new LinkedListNode(0);
                    head = sum;
                }
                else {
                    sum.next = new LinkedListNode(0);
                    sum = sum.next;
                }
                int val = (temp1.data + temp2.data + carry) % 10;
                carry = (temp1.data + temp2.data + carry) / 10;
                sum.data = val;
                temp1 = temp1.next;
                temp2 = temp2.next;
            } 
            while (temp1 != null) {
                sum.next = new LinkedListNode(0);
                sum = sum.next;
                int val = (temp1.data + carry) % 10;
                carry = (temp1.data + carry) / 10;
                sum.data = val;
                temp1 = temp1.next;            
            }  
            while (temp2 != null) {
                sum.next = new LinkedListNode(0);
                sum = sum.next;
                int val = (temp2.data + carry) % 10;
                carry = (temp2.data + carry) / 10;
                sum.data = val;
                temp1 = temp2.next;            
            }
            if (carry == 1) {
                sum.next = new LinkedListNode(1);
            }
            return head;
        }

        static LinkedListNode sumListsRecursive(LinkedListNode head1, LinkedListNode head2, int carry) {
            int value = 0;
            if (head1 != null) {
                value += head1.data;
            }
            if (head2 != null) {
                value += head2.data;
            }
            value += carry;
            carry = value / 10;
            LinkedListNode result = null;
            if (value == 0 && head1 == null && head2 == null) {
                return null;
            }
            else {
                result = new LinkedListNode(value % 10);
            }
            result.next = sumListsRecursive(head1 == null ? null : head1.next,head2 == null ? null : head2.next,carry);
            return result; 
        }

        // Is Palindrome
        static bool isPalindrome(LinkedListNode head) {
            LinkedListNode slow = head;
            LinkedListNode runner = head;
            Stack<int> st = new Stack<int>();
            while (runner != null && runner.next != null) {
                st.Push(slow.data);
                slow = slow.next;
                runner = runner.next.next;
            }
            if (runner != null) {
                slow = slow.next;
            }
            while (slow != null) {
                if (st.Pop() != slow.data) {
                    return false;
                }
                slow = slow.next;
            }
            return true;
        }

        // Is Palindrome Recursive Auxiliar Function
        static LinkedListNode isPalindromeRecursiveAux(LinkedListNode node,int length) {
            if (length == 0) {
                return node;
            }
            else if(length == 1) {
                return node.next;
            }
            LinkedListNode tocompare = isPalindromeRecursiveAux(node.next,length - 2);
            if (tocompare == null) {
                return null;
            }
            if (tocompare.data == node.data) {
                if (tocompare.next == null) {
                    return tocompare;
                }
                return tocompare.next;
            }
            else {
                return null;
            }

        }

        // Is Palindrome Recursive
        static bool isPalindromeRecursive(LinkedListNode head) {
            int length = 0;
            LinkedListNode temp = head;
            while (temp != null) {
                length++;
                temp = temp.next;
            }
            LinkedListNode result = isPalindromeRecursiveAux(head,length);
            if (result == null) {
                return false;
            }
            return true;
        }

    }

    class LinkedListNode {

        public int data;
        public LinkedListNode next;

        public LinkedListNode(int d) {
            data = d;
            next = null;
        }

        public void insert(int d) {
            LinkedListNode n = this;
            LinkedListNode add = new LinkedListNode(d);
            while (n.next != null) {
                n = n.next;
            }
            n.next = add;
        }

        public void printList() {
            LinkedListNode n = this;
            while (n != null) {
                Console.WriteLine(n.data);
                n = n.next;
            }
        }
    }


}