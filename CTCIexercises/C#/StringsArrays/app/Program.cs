using System.Collections;
using System.Text;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
            Console.WriteLine(oneAway("pale","ply"));
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

        /* Palindrome Permutation: Given a string, write a function to check if it is a permutation of 
        a palindrome. A palindrome is a word or phrase that is the same forwards and backwards. A 
        permutation is a rearrangement of letters. The palindrome does not need to be limited to just 
        dictionary words. */
        static bool palindromePermutation(string str1) {
            int[] charCount = new int[128];
            for (int i = 0; i < str1.Length; i++) {
                charCount[(int)str1[i]]++;
            }
            int oddCheck = 0;
            for (int i = 0; i < charCount.Length; i++) {
                if (charCount[i] % 2 == 1 && oddCheck == 1) {
                    return false;
                }
                else if (charCount[i] % 2 == 1) {
                    oddCheck = 1;
                }
            }
            return true;
        }

        /* One Away: There are three types of edits that can be performed on strings: insert a character, 
        remove a character, or replace a character. Given two strings, write a function to check if they are 
        one edit (or zero edits) away */
        static bool oneAway(string str1, string str2) {
            if (Math.Abs(str1.Length - str2.Length) > 1) return false;
            int[] count = new int[128];
            string lstr = str1.Length >= str2.Length ? str1 : str2;
            string sstr = str1.Length >= str2.Length ? str2 : str1;

            for (int i = 0; i < sstr.Length; i++) {
                count[(int) sstr[i]]++;
            }

            int checkIrregular = 0;
            for (int i = 0; i < lstr.Length; i++) {
                if (count[(int) lstr[i]] == 0 && checkIrregular == 1) {
                    return false;
                }
                else if (count[(int) lstr[i]] == 0) {
                    checkIrregular = 1;
                }
            }            
            return true;
        }


    }


}