using System.Collections;
using System.Text;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
            Stack<int> s = new Stack<int>();
            s.Push(9);
            s.Push(4);
            s.Push(2);
            s.Push(7);
            s.Push(1);
            s.Push(6);
            s.Push(4);
            s.Push(3);
            sortStack(s);
            while (s.Count != 0) {
                Console.WriteLine(s.Pop());
            }
        }

        /* Sort Stack: Write a program to sort a stack such that the smallest items are on the top. You can use 
        an additional temporary stack, but you may not copy the elements into any other data structure 
        (such as an array). The stack supports the following operations: push, pop, peek, and is Empty.*/
        static void sortStack(Stack<int> s) {
            if (s == null) return;
            Stack<int> s1 = new Stack<int>();
            while (s.Count != 0) {
                int temp = s.Pop();
                int count = 0;
                while (s1.Count != 0 && s1.Peek() > temp) {
                    s.Push(s1.Pop());
                    count++;
                }
                s1.Push(temp);
                while (count > 0) {
                    s1.Push(s.Pop());
                    count--;
                }
            }
            while (s1.Count != 0) {
                s.Push(s1.Pop());
            }
        }
    }
}