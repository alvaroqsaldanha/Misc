using System.Collections;
using System.Text;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
            Console.WriteLine(urlify("Mr John Smith    ",13));
        }

        // Is Unique: Implement an algorithm to determine if a string has all unique characters.
        static bool isUnique(string str) {
            HashSet<int> check = new HashSet<int>();
            for (int i = 0; i < str.Length; i++) {
                if (check.Contains(str[i])) {
                    return false;
                }
                else {
                    check.Add(str[i]);
                }
            }
            return true;
        }

        // Given two strings, write a method to decide if one is a permutation of the other. 
        static bool checkPermutation(string str1, string str2) {
            if (str1.Length != str2.Length) return false;
            int[] check = new int[128];
            for (int i = 0; i < str1.Length; i++) {
                check[(int)str1[i]]++;
            }
            for (int i = 0; i < str2.Length; i++ ){
                if (check[(int)str2[i]] == 0) return false;
                check[(int)str2[i]]--;
            }
            return true;
        }

        /* RLify: Write a method to replace all spaces in a string with '%20'. You may assume that the string 
        has sufficient space at the end to hold the additional characters, and that you are given the "true" 
        length of the string. */
        static string urlify(string str,int length) {
            StringBuilder sb = new StringBuilder(str);
            int start = length - 1;
            int end = sb.Length - 1;
            while (start >= 0) {
                if (sb[start] != ' ') {
                    sb[end] = sb[start];
                    end--;
                }
                else {
                    sb[end--] = '0';
                    sb[end--] = '2';
                    sb[end--] = '%';
                }
                start--;
            }
            return sb.ToString();
        }


    }


}