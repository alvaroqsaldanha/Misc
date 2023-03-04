using System.Collections;
using System.Text;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
          
        }

        /* Sorted Merge: You are given two sorted arrays, A and B, where A has a large enough buffer at the 
        end to hold B. Write a method to merge B into A in sorted order */
        static int[] sortedMerge(int[] a, int[] b) {
            if (b.Length == 0) return a;
            int i = a.Length - b.Length - 1;
            int j = b.Length - 1;
            while(j >= 0) { 
                if (i > - 1 && a[i] > b[j]) {
                    a[k] = a[i];
                    i--;
                }
                else {
                    a[k] = b[j];
                    j--;
                }
            }
            return a;
        }




    }
}