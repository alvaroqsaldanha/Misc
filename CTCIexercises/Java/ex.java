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

}  