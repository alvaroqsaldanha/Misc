﻿using System.Collections;
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
        
        /* Letter Combinations of a Phone Number */ 
        public IList<string> LetterCombinations(string digits) {
            IList<string> results = new List<string>();
            if (digits.Length == 0) return results;
            results.Add("");
            Dictionary<char,string> phone = new Dictionary<char,string>();
            BuildDict(phone);
            return LetterCombinations(digits, 0, phone, results);
        }

        public IList<string> LetterCombinations(string digits, int idx, Dictionary<char,string> phone, IList<string> results) {
            if (idx == digits.Length) return results;
            char curr_digit = digits[idx];
            IList<string> new_results = new List<string>();
            foreach (string result in results) {
                for (int i = 0; i < phone[curr_digit].Length; i++) {
                    string temp = string.Copy(result);
                    temp += phone[curr_digit][i];
                    new_results.Add(temp);
                }
            }
            return LetterCombinations(digits, idx+1, phone, new_results);
        }

        public void BuildDict(Dictionary<char,string> phone) {
            phone.Add('2',"abc");
            phone.Add('3',"def");
            phone.Add('4',"ghi");
            phone.Add('5',"jlk");
            phone.Add('6',"mno");
            phone.Add('7',"pqrs");
            phone.Add('8',"tuv");
            phone.Add('9',"wxyz");
        }
        
        /* Group Anagrams */
        public IList<IList<string>> GroupAnagrams(string[] strs) {
            IList<IList<string>> results = new List<IList<string>>();
            Dictionary<string,List<string>> map = new Dictionary<string,List<string>>();
            if (strs.Length == 0) return results;
            foreach (string str in strs) {
                char[] ch = str.ToCharArray();
                Array.Sort(ch);
                string current = new string(ch);
                if (map.ContainsKey(current)) map[current].Add(str);
                else {
                    List<string> a = new List<string>();
                    a.Add(str);
                    map.Add(current,a);
                }
            }
            foreach (var (key, value) in map) {
                results.Add(value);
            }
            return results;
        }
        
        /* Product of self-array */
        public int[] ProductExceptSelf(int[] nums) {
            if (nums.Length == 0 || nums.Length == 1) return nums;
            int[] results = new int[nums.Length];
            results[0] = 1;
            for (int i = 1; i < nums.Length; i++) {
                results[i] = results[i-1] * nums[i-1];
            }
            int right = nums[nums.Length - 1];
            for (int i = nums.Length - 2; i > -1; i--) {
                results[i] *= right;
                right *= nums[i];
            }
            return results;
        }
        
        /* Evaluate Reverse Polish Notation */
        public int EvalRPN(string[] tokens) {
            if (tokens.Length == 0) return -1;
            Stack<int> nums = new Stack<int>();
            HashSet<string> ops = new HashSet<string> {"+","-","*","/"};
            foreach (string token in tokens) {
                if (ops.Contains(token)) {
                    int val = nums.Pop();
                    switch (token) {
                        case "+":
                            nums.Push(val+nums.Pop());
                            break;
                        case "-":
                            val = nums.Pop() - val;
                            nums.Push(val);
                            break;
                        case "*":
                            nums.Push(val*nums.Pop());
                            break;
                        case "/":
                            val = nums.Pop() / val;
                            nums.Push(val);
                            break;
                    }
                }
                else {
                    int i = token[0] == '-' ? 1 : 0;
                    int num = 0;
                    for (; i < token.Length; i++) {
                        num = num * 10 + (token[i] - '0');
                    }
                    num = token[0] == '-' ? -num : num;
                    nums.Push(num);
                }
            }
            return nums.Pop();
        }
        
        /* Calculate */
        public int Calculate(string s) {
            int currentNumber = 0;
            Stack<int> toSum = new Stack<int>();
            HashSet<char> operators = new HashSet<char>() {'+','-','*','/'};
            char last_op = '+';
            for (int i = 0; i < s.Length; i++) {
                 if (!operators.Contains(s[i]) && s[i] != ' '){
                    currentNumber = currentNumber * 10 + (s[i] - '0');
                }
                if (operators.Contains(s[i]) && s[i] != ' ' || i == s.Length - 1) {
                    if (last_op == '+') {
                        toSum.Push(currentNumber);
                    }
                    else if (last_op == '-') {
                        toSum.Push(-currentNumber);
                    }
                    else if (last_op == '*') {
                        toSum.Push(toSum.Pop()*currentNumber);
                    }
                    else if (last_op == '/') {
                        toSum.Push(toSum.Pop()/currentNumber);
                    }
                    last_op = s[i];
                    currentNumber = 0;
                }
            }
            int result = 0;
            while (toSum.Count != 0) {
                result += toSum.Pop();
            }
            return result;
       }
        
       public int LengthOfLIS(int[] nums) {
            int[] dp = new int[nums.Length];
            int max = 0;
            for (int i = 0; i < nums.Length; i++) {
                for (int j = 0; j < i; j++) {
                    if (nums[i] > nums[j] && dp[j] + 1 > dp[i]) {
                        dp[i] = dp[j] + 1;
                        if (dp[i] > max) max = dp[i];
                    }
                }
            }
            return max + 1;
        }
        
            public enum state {
        UNVISITED, VISITING, VISITED
    }

    public class Node {

        public int val;
        public List<Node> adjs = new List<Node>();
        public state state = state.UNVISITED;

        public Node(int value) {
            this.val = value;
        }

        public int getVal() {
            return this.val;
        }

        public void addAdj(Node node) {
            this.adjs.Add(node);
        }

        public List<Node> getAdjs() {
            return this.adjs;
        }
    }

    public bool CanFinish(int numCourses, int[][] prerequisites) {
        Dictionary<int,Node> graph = new Dictionary<int,Node>();
        for (int i = 0; i < prerequisites.Length; i++) {
            if (!graph.ContainsKey(prerequisites[i][1])) {
                graph.Add(prerequisites[i][1],new Node(prerequisites[i][1]));
            }
            if (!graph.ContainsKey(prerequisites[i][0])) {
                graph.Add(prerequisites[i][0],new Node(prerequisites[i][0]));
            }
            graph[prerequisites[i][1]].addAdj(graph[prerequisites[i][0]]);
        }

        foreach (var (key,node) in graph) {
            if (node.state == state.UNVISITED) {
                if (!dfs(node)) return false;
            }
        }
        return true;

    }

    public bool dfs(Node node) {
        if (node.state == state.VISITING) return false;
        if (node.state == state.UNVISITED){
            node.state = state.VISITING;
            foreach (Node child in node.getAdjs()) {
                if (!dfs(child)) return false;;
            }
            node.state = state.VISITED;
        }
        
        return true;
    }
    }

}
