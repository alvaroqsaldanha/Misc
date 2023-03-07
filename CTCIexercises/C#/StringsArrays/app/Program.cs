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

        /* String Compression: Implement a method to perform basic string compression using the counts 
        of repeated characters. For example, the string aabcccccaaa would become a2blc5a3. If the 
        "compressed" string would not become smaller than the original string, your method should return 
        the original string. You can assume the string has only uppercase and lowercase letters (a - z). */
        static string stringCompression(string str) {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            for (int i = 0; i < str.Length; i++) {
                if (i < -1 && str[i] == str[i+1]) {
                    counter++;
                }
                else {
                    sb.Append(str[i]);
                    sb.Append(counter.ToString());
                    counter = 0;
                }
            }
            return str.Length > sb.Length ? sb.ToString() : str;
        }

        /* Rotate Matrix: Given an image represented by an NxN matrix, where each pixel in the image is 4 
        bytes, write a method to rotate the image by 90 degrees. Can you do this in place? */
        static int[][] rotateMatrix(int[][] mat){
            int n = mat.Length;
            for (int layer = 0; layer < n / 2; layer++) {
                for (int i = layer; i < n - layer; i++) {
                    int temp = mat[layer][i];
                    mat[layer][i] = mat[n-i-1][layer];
                    mat[n-i-1][layer] = mat[n-layer-1][n-i-1];
                    mat[n-layer-1][n-i-1] = mat[i][n-layer-1];
                    mat[i][n-layer-1] = temp;
                }
            }
            return mat;
        }

        /* Zero Matrix: Write an algorithm such that if an element in an MxN matrix is 0, its entire row and 
        column are set to 0 */
        static void zeroMatrix(int[][] mat) {
            int[] rows = new int[mat.Length];
            int[] columns = new int[mat[0].Length]; 
            List<Tuple<int,int>> pos = new List<Tuple<int,int>>();
            for (int i = 0; i < mat.Length; i++) {
                for (int j = 0; j < mat[0].Length; j++) {
                    if (mat[i][j] == 0) {
                        pos.Add(new Tuple<int,int>(i,j));
                    }
                }
            }
            foreach (Tuple<int,int> tup in pos) {
                if (rows[tup.Item1] == 0) {
                    for (int i = 0; i < mat[0].Length; i++) {
                        mat[tup.Item1][i] = 0;
                    }
                    rows[tup.Item1] = 1;
                }
                if (columns[tup.Item2] == 0) {
                    for (int i = 0; i < mat.Length; i++) {
                        mat[i][tup.Item2] = 0;
                    }
                    columns[tup.Item2] = 1;
                }
            }
        }

        /* String Rotation: Assume you have a method isSubstring which checks if one word is a substring 
        of another. Given two strings, 51 and 52, write code to check if 52 is a rotation of 51 using only one 
        call to isSubstring (e.g., "waterbottle" is a rotation of"erbottlewat"). */
        static bool stringRotation(string s1, string s2) {
            if (s1.Length != s2.Length || s1.Length == 0 || s2.Length == 0) {
                return false;
            }
            string concat = s1 + s2;
            return concat.Contains(s1);
        }

        /* Search in rotated array */
        public int Search(int[] nums, int target) {
            if (nums.Length == 0) return -1;
            int l = 0;
            int r = nums.Length - 1;
            int minidx = 0;
            int min = Int32.MaxValue;
            while (l <= r) {
                int middle = (l + r) / 2;
                if (nums[middle] < min) {
                    min = nums[middle];
                    minidx = middle;
                }
                if (nums[middle] > nums[r]) {
                    l = middle + 1;
                }
                else {
                    r = middle - 1;
                }
            }
            l = 0;
            r = nums.Length - 1;
            while (l <= r) {
                int middle = (l + r) / 2;
                int realmiddle = (middle + minidx) % nums.Length;
                if (nums[realmiddle] == target) return realmiddle;
                if (nums[realmiddle] < target) {
                    l = middle + 1;
                }
                else {
                    r = middle - 1;
                }
            }       
            return -1;
        }

        /* Container with the most water */
        public int MaxArea(int[] height) {
            if (height.Length < 2) return 0;
            if (height.Length == 2) return Math.Min(height[0],height[1]) * 1;
            int max = Int32.MinValue;
            int i = 0;
            int j = height.Length - 1;
            while (i != j) {
                    max = Math.Max(max,(j-i)*Math.Min(height[i],height[j]));
                    if (height[i] < height[j]) i++;
                    else j--;
            }
            return max;
        }

        /* Word Search */
        public bool Exist(char[][] board, string word) {
            if (board.Length == 0 || word.Length == 0) return false;
            for (int i = 0; i < board.Length; i++) {
                for(int j = 0; j < board[0].Length; j++) {
                    HashSet<string> path = new HashSet<string>();
                    if (dfs(board,i,j,word,0,path)) return true;
                }
            }
            return false;
        }

        public List<string> getNeighbours(int n, int m, int i, int j) {
            List<string> nb = new List<string>();
            if (i < n) nb.Add((i+1).ToString() + j.ToString());
            if (i > 0) nb.Add((i-1).ToString() + j.ToString());
            if (j > 0) nb.Add(i.ToString() + (j-1).ToString());
            if (j < m) nb.Add(i.ToString() + (j+1).ToString());
            return nb;
        }

        public bool dfs(char[][] board, int i, int j, string word, int idx, HashSet<string> path) {
            if (idx == word.Length || (idx == word.Length - 1 && word[idx] == board[i][j])) {
                return true;
            } 
            if (board[i][j] != word[idx]) return false;
            string save = i.ToString() + j.ToString();
            path.Add(save);
            List<string> nb = getNeighbours(board.Length - 1,board[0].Length - 1,i,j);
            foreach (string l in nb) {
                if (!path.Contains(l)) {
                    if (dfs(board,(int)Char.GetNumericValue(l[0]),(int)Char.GetNumericValue(l[1]),word,idx+1,path)) return true;
                }
            }
            path.Remove(save);
            return false;
        }
        
        /* Longest Palindromic Substring */
        public string LongestPalindrome(string s) {
            if (s.Length == 0) return "";
            int start = 0;
            int end = 0;
            for (int i = 0; i < s.Length; i++) {
                int opt1 = getMaxPalindromeSize(s,i,i);
                int opt2 = getMaxPalindromeSize(s,i,i+1);
                int len = Math.Max(opt1, opt2);
                if (len > end - start) {
                    start = i - (len - 1) / 2;
                    end = i + len / 2;
                }        
            }
            return s.Substring(start,end + 1 - start);
        }

        public int getMaxPalindromeSize(string s, int l ,int r) {
            int size = 0;
            while (l >= 0 && r < s.Length && s[l] == s[r]) {
                l--;
                r++;
            }
            return (r - l - 1);
        }
    }

}
