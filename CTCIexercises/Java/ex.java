import java.util.HashMap;

class ex {
    
    public static void main(String args[]) {  
      System.out.println(palindromepermutation("abcabc"));
    }
      
    // URLify
    static void urlify(char[] str,int l) {
        int curr_char = str.length - 1;
        for (int i = l-1; i > - 1; i--) {
            if (str[i] != ' ') {
                str[curr_char] = str[i];
                curr_char -= 1;
            }
            else {
                str[curr_char] = '0';
                str[curr_char - 1] = '2';
                str[curr_char - 2] = '%';
                curr_char -= 3;
            }
        }
        System.out.println(new String(str));
    } 

    // Palindrome Permutation
    static boolean palindromepermutation(String str) {
        HashMap<Character,Integer> charsmap = new HashMap<Character,Integer>();
        for (int i = 0; i < str.length(); i++) {
            charsmap.merge(str.charAt(i), 1, Integer::sum);
        }
        int odd_counter = 0;
        for (int value : charsmap.values()) {
            if (value % 2 == 1) {
                odd_counter += 1;
                if (odd_counter > 1)
                    return false;
            }
        }
        return true;
    }

    // One Away 
    static boolean oneditaway(String str1, String str2) {
        if (Math.abs(str1.length() - str2.length()) > 1) {
            return false;
        }

        String longer_str = str1.length() < str2.length() ? str2 : str1;
        String shorter_str = str1.length() < str2.length() ? str1 : str2;

        int index_long = 0;
        int index_short = 0;
        boolean found_diff = false;
        while (index_long < longer_str.length() && index_short < shorter_str.length()) {
            if (longer_str.charAt(index_long) != shorter_str.charAt(index_short)) {
                if (found_diff) return false;
                found_diff = true;
                if (longer_str.length() == shorter_str.length()) {
                    index_short++;
                }
            }
            else {
                index_short++;
            }
            index_long++;
        } 
        return true;
    }

    // String Compression
    static String stringcompression(String str) {
        String compressedString = "";
        int count = 0;
        for (int i = 0; i < str.length(); i++) {
            count++;
            if (i + 1 == str.length() || str.charAt(i) !=  str.charAt(i+1)) {
                compressedString += str.charAt(i) + count;
                count = 0;
            }
        }
        return compressedString.length() > str.length() ? str : compressedString;
    }

}  