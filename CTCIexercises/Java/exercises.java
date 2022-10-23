import java.util.HashMap;
import java.util.Map;
import java.util.Iterator;
import java.util.Set;
import ex.*;


class exercises {
    public static void main(String[] args) {
        Node a = new Node(1);
        Node b = new Node(2);
        Node c = new Node(3);
        Node d = new Node(4);
        Node e = new Node(5);
        a.next = b;
        b.next = c;
        c.next = d;
        d.next = e;
        e.next = c;
        LinkedList ll = new LinkedList(a);
        Node asd = ll.loop();
        System.out.println(asd.num);
    }

    /*Is Unique: Implement an algorithm to determine if a string has all unique characters. What if you
      cannot use additional data structures?*/

    public static boolean checkUniqueChars(String stringcheck) {
        boolean[] charscheck = new boolean[128];
        
        HashMap<Character,Integer> charsmap = new HashMap<Character,Integer>();

        for (int i = 0; i < stringcheck.length(); i++) {
            if (charsmap.containsKey(stringcheck.charAt(i))) {
                return false;
            }
            else {
                charsmap.put(stringcheck.charAt(i),1);
            }
        }
        return true;
    }

    /*URLify: Write a method to replace all spaces in a string with '%20'. You may assume that the string
    has sufficient space at the end to hold the additional characters, and that you are given the "true"
    length of the string. (Note: If implementing in Java, please use a character array so that you can
    perform this operation in place.) */

    public static String urlify(String x, int len) {
        char[] c = x.toCharArray();
        int i = x.length() - 1;
        for (int j = len - 1; j > 0; j--) {
            if (c[j] != ' ') {
                c[i] = c[j];
                i--;
            }
            else {
                c[i] = '0';
                c[--i] = '2';
                c[--i] = '%';
                i--;
            }
        }
        String rtrn = new String(c);
        return rtrn;
    }

    /*One Away: There are three types of edits that can be performed on strings: insert a character,
    remove a character, or replace a character. Given two strings, write a function to check if they are
    one edit (or zero edits) away*/

    public static boolean oneAway(String str1, String str2) {
        int dif = str1.length() - str2.length();
        if ((dif > 1) || (dif < - 1)){
            return false;
        }

        HashMap<Character,Integer> str1count = new HashMap<Character,Integer>();
        HashMap<Character,Integer> str2count = new HashMap<Character,Integer>();

        for (int i = 0; i < str1.length();i++) {
            if (str1count.get(str1.charAt(i)) != null)
                str1count.put(str1.charAt(i),str1count.get(str1.charAt(i)) + 1);
            else
                str1count.put(str1.charAt(i),1);
        }
        for (int i = 0; i < str2.length();i++) {
            if (str2count.get(str2.charAt(i)) != null)
                str2count.put(str2.charAt(i),str2count.get(str2.charAt(i)) + 1);
            else
                str2count.put(str2.charAt(i),1);
        }

        int difcheck = 0;

        for (Map.Entry<Character, Integer> entry : str1count.entrySet()) {
            char x = entry.getKey();
            if (str2count.get(x) == null) {
                return false;
            }
            if (str2count.get(x) != str1count.get(x)){
                difcheck++;
            }
            if (difcheck > 1) {
                return false;
            }
        }

        return true;
    }

    /* # String Compression: Implement a method to perform basic string compression using the counts
     of repeated characters. For example, the string aabcccccaaa would become a2blc5a3. If the
     "compressed" string would not become smaller than the original string, your method should return
     the original string. You can assume the string has only uppercase and lowercase letters (a - z) */

     public static String stringCompression(String original) {
        StringBuilder compressedString = new StringBuilder();

        int counter = 0;

        for (int i = 0; i < original.length(); i++) {
            counter++;

            if ((i + 1 == original.length()) || (original.charAt(i) != original.charAt(i + 1))) {
                compressedString.append(original.charAt(i));
                compressedString.append(counter);
                counter = 0;
            }
        }

        return compressedString.length() < original.length() ? compressedString.toString() : original;
     }

     /* Rotate Matrix: Given an image represented by an NxN matrix, where each pixel in the image is 4
    bytes, write a method to rotate the image by 90 degrees. Can you do this in place? */

    public static boolean rotateMatrix(int[][] matrix) {
        int n = matrix.length;
        if ((n == 0) || (n != matrix[0].length)) return false;

        for (int layer = 0; layer < n/2 ; layer++) { 
            int start = layer;
            int last = n - 1 - start; 
            for (int i = start; i < last; i++) {
                int off = i;
                int save = matrix[layer][i];
                matrix[layer][i] = matrix[last-off][layer];
                matrix[last-off][layer] = matrix[last][last-off];
                matrix[last][last-off] = matrix[i][last];
                matrix[i][last] = save;
            }

        } 

        for (int j = 0; j < n; j++) {
            for (int z = 0; z < n; z++) {
                System.out.print(matrix[j][z]);
            }
        }

        return true;

    }

    /* 9 String Rotation: Assume you have a method isSubstringwhich checks if oneword is a substring
        of another. Given two strings, sl and s2, write code to check if s2 is a rotation of sl using only one
        call to isSubstring (e.g., "waterbottle" is a rotation of"erbottlewat"). */

    public static boolean stringRotation(String s1, String s2) {
        
        if (s1.length() != s2.length()) return false;

        String join = s1 + s1;

        return join.contains(s2);

    } 




}



