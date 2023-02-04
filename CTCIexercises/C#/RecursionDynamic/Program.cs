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

        /* Robot in a Grid: Imagine a robot sitting on the upper left corner of grid with r rows and c columns. 
        The robot can only move in two directions, right and down, but certain cells are "off limits" such that 
        the robot cannot step on them. Design an algorithm to find a path for the robot from the top left to 
        the bottom right. */
        static int robotGrid(int[][] obstacleGrid) {
            int rows = obstacleGrid.Length;
            int cols = obstacleGrid[0].Length;
            int[,] grid = new int[rows+1,cols+1];
            grid[0,1] = 1;
            for (int i = 1; i < rows+1; i++) {
                for (int j = 1; j < cols+1; j++) {
                    if (obstacleGrid[i-1][j-1] == 0) {
                        grid[i,j] = grid[i-1,j] + grid[i,j-1];
                    }
                }
            }
            return grid[rows,cols];
        }

        /* Magic Index: A magic index in an array A[ 1 .•. n-1] is defined to be an index such that A[ i] 
        i. Given a sorted array of distinct integers, write a method to find a magic index, if one exists, in
        array A. */
        



    }
}