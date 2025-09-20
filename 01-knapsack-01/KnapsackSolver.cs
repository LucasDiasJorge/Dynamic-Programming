using System;
using System.Collections.Generic;

namespace Knapsack01
{
    /// <summary>
    /// Result structure containing maximum value and selected items
    /// </summary>
    public class KnapsackResult
    {
        public int MaxValue { get; set; }
        public List<int> SelectedItems { get; set; }

        public KnapsackResult()
        {
            SelectedItems = new List<int>();
        }
    }

    /// <summary>
    /// Multiple implementations of the 0/1 Knapsack problem
    /// </summary>
    public class KnapsackSolver
    {
        private int[,] memoTable = new int[0, 0];

        /// <summary>
        /// Top-Down approach using recursion with memoization
        /// Time Complexity: O(n * capacity)
        /// Space Complexity: O(n * capacity) for memo table + O(n) for recursion stack
        /// </summary>
        public int SolveTopDown(int[] weights, int[] values, int capacity)
        {
            int itemCount = weights.Length;
            memoTable = new int[itemCount + 1, capacity + 1];
            
            // Initialize memo table with -1 (uncomputed state)
            for (int i = 0; i <= itemCount; i++)
            {
                for (int w = 0; w <= capacity; w++)
                {
                    memoTable[i, w] = -1;
                }
            }
            
            return KnapsackRecursive(weights, values, itemCount, capacity);
        }

        /// <summary>
        /// Recursive helper function for top-down approach
        /// </summary>
        private int KnapsackRecursive(int[] weights, int[] values, int itemIndex, int remainingCapacity)
        {
            // Base case: no items left or no capacity left
            if (itemIndex == 0 || remainingCapacity == 0)
            {
                return 0;
            }

            // Check if already computed
            if (memoTable[itemIndex, remainingCapacity] != -1)
            {
                return memoTable[itemIndex, remainingCapacity];
            }

            // If current item weight exceeds remaining capacity, skip it
            if (weights[itemIndex - 1] > remainingCapacity)
            {
                memoTable[itemIndex, remainingCapacity] = KnapsackRecursive(weights, values, itemIndex - 1, remainingCapacity);
            }
            else
            {
                // Choose maximum between including and excluding current item
                int excludeCurrentItem = KnapsackRecursive(weights, values, itemIndex - 1, remainingCapacity);
                int includeCurrentItem = values[itemIndex - 1] + KnapsackRecursive(weights, values, itemIndex - 1, remainingCapacity - weights[itemIndex - 1]);
                
                memoTable[itemIndex, remainingCapacity] = Math.Max(excludeCurrentItem, includeCurrentItem);
            }

            return memoTable[itemIndex, remainingCapacity];
        }

        /// <summary>
        /// Bottom-Up approach using 2D dynamic programming table
        /// Time Complexity: O(n * capacity)
        /// Space Complexity: O(n * capacity)
        /// </summary>
        public int SolveBottomUp2D(int[] weights, int[] values, int capacity)
        {
            int itemCount = weights.Length;
            int[,] dp = new int[itemCount + 1, capacity + 1];

            // Fill the DP table
            for (int i = 1; i <= itemCount; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    // If current item weight exceeds current capacity, skip it
                    if (weights[i - 1] > w)
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                    else
                    {
                        // Choose maximum between including and excluding current item
                        int excludeCurrentItem = dp[i - 1, w];
                        int includeCurrentItem = values[i - 1] + dp[i - 1, w - weights[i - 1]];
                        
                        dp[i, w] = Math.Max(excludeCurrentItem, includeCurrentItem);
                    }
                }
            }

            return dp[itemCount, capacity];
        }

        /// <summary>
        /// Bottom-Up approach using 1D array (space optimized)
        /// Time Complexity: O(n * capacity)
        /// Space Complexity: O(capacity)
        /// </summary>
        public int SolveBottomUp1D(int[] weights, int[] values, int capacity)
        {
            int[] dp = new int[capacity + 1];

            // Process each item
            for (int i = 0; i < weights.Length; i++)
            {
                // Iterate backwards to avoid using updated values in same iteration
                for (int w = capacity; w >= weights[i]; w--)
                {
                    dp[w] = Math.Max(dp[w], dp[w - weights[i]] + values[i]);
                }
            }

            return dp[capacity];
        }

        /// <summary>
        /// Solve knapsack problem and reconstruct which items were selected
        /// Time Complexity: O(n * capacity)
        /// Space Complexity: O(n * capacity)
        /// </summary>
        public KnapsackResult SolveWithItemReconstruction(int[] weights, int[] values, int capacity)
        {
            int itemCount = weights.Length;
            int[,] dp = new int[itemCount + 1, capacity + 1];

            // Fill the DP table (same as Bottom-Up 2D)
            for (int i = 1; i <= itemCount; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    if (weights[i - 1] > w)
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                    else
                    {
                        int excludeCurrentItem = dp[i - 1, w];
                        int includeCurrentItem = values[i - 1] + dp[i - 1, w - weights[i - 1]];
                        
                        dp[i, w] = Math.Max(excludeCurrentItem, includeCurrentItem);
                    }
                }
            }

            // Reconstruct the solution by backtracking
            KnapsackResult result = new KnapsackResult();
            result.MaxValue = dp[itemCount, capacity];
            
            int currentCapacity = capacity;
            for (int i = itemCount; i > 0 && result.MaxValue > 0; i--)
            {
                // If value comes from the top (excluding current item), move up
                if (result.MaxValue == dp[i - 1, currentCapacity])
                {
                    continue;
                }
                else
                {
                    // This item is included in the optimal solution
                    result.SelectedItems.Add(i); // 1-based item number
                    
                    // Reduce the maximum value and capacity
                    result.MaxValue -= values[i - 1];
                    currentCapacity -= weights[i - 1];
                }
            }

            // Restore the actual max value and reverse the list for correct order
            result.MaxValue = dp[itemCount, capacity];
            result.SelectedItems.Reverse();
            
            return result;
        }
    }
}