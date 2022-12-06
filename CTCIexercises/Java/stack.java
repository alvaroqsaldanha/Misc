import java.io.*;
import java.util.*;

class stack {
    
    class StackOfStacks {

        ArrayList<Stack> stacks = new ArrayList<Stack>();
        int capacity;

        public StackOfStacks(int capacity) {
            this.capacity = capacity;
        }

        public int pop() {
            Stack<Integer> st = stacks.get(stacks.size() - 1);
            if (st == null) return 0;
            int v = st.pop();
            if (st.size() == 0) stacks.remove(stacks.size() - 1);
            return v;
        }

        public void push(int v) {
            if (stacks.size() == 0) {
                Stack<Integer> st = new Stack<Integer>();
                st.push(v);
                stacks.add(st);
            }
            else {
                Stack<Integer> st = stacks.get(stacks.size() - 1);
                if (st.size() == capacity) {
                    Stack<Integer> nst = new Stack<Integer>();
                    nst.push(v);
                    stacks.add(nst);
                }
                else {
                    st.push(v);
                }
            }
        }
    }
}  