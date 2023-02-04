using System.Collections;
using System.Text;

namespace CTCI
{
    class CTCI {         
        static void Main(string[] args)
        {
          
        }

        /* Triple Step: A child is running up a staircase with n steps and can hop either 1 step, 2 steps, or 3 
        steps at a time. Implement a method to count how many possible ways the child can run up the 
        stairs. */
        static int tripleStep(int n) {
            if (n < 0) return -1;
            if (n <= 2) return n;
            int[] dp = new int[n];
            dp[0] = 1;
            dp[1] = 2;
            dp[2] = 4;
            for (int i = 3; i < n; i++) {
                dp[i] = dp[i-3] + dp[i-2] + dp[i-1]; 
            }
            return dp[n-1];

        }

    }
}