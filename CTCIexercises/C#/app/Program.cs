using System.Collections.Generic;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
            LinkedListNode head = new LinkedListNode(1);
            head.insert(2);
            head.insert(3);
            head.insert(4);
            head.insert(2);
            head.insert(3);
            head.insert(3);
            deleteDups(head);
            head.printList();
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