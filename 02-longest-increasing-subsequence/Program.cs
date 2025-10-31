using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LongestIncreasingSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Longest Increasing Subsequence (LIS) ===\n");

            // Test case from README
            int[] sequence = { 10, 9, 2, 5, 3, 7, 101, 18 };
            
            Console.WriteLine("Input sequence: [" + string.Join(", ", sequence) + "]");
            Console.WriteLine();

            // Test O(n^2) DP approach
            LISSolverQuadratic quadraticSolver = new LISSolverQuadratic();
            int lengthQuadratic = quadraticSolver.FindLISLength(sequence);
            List<int> lisQuadratic = quadraticSolver.ReconstructLIS(sequence);
            
            Console.WriteLine("--- O(n^2) Dynamic Programming ---");
            Console.WriteLine($"LIS Length: {lengthQuadratic}");
            Console.WriteLine("LIS: [" + string.Join(", ", lisQuadratic) + "]");
            Console.WriteLine();

            // Test O(n log n) approach
            LISSolverOptimal optimalSolver = new LISSolverOptimal();
            int lengthOptimal = optimalSolver.FindLISLength(sequence);
            List<int> lisOptimal = optimalSolver.ReconstructLIS(sequence);
            
            Console.WriteLine("--- O(n log n) Patience Sorting ---");
            Console.WriteLine($"LIS Length: {lengthOptimal}");
            Console.WriteLine("LIS: [" + string.Join(", ", lisOptimal) + "]");
            Console.WriteLine();

            // Additional test cases
            TestCase(new int[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 });
            TestCase(new int[] { 7, 7, 7, 7, 7 }); // All equal
            TestCase(new int[] { 5, 4, 3, 2, 1 }); // Decreasing
            
            // Benchmark
            Console.WriteLine("\n=== Benchmark ===");
            BenchmarkLIS();
        }

        static void TestCase(int[] sequence)
        {
            LISSolverOptimal solver = new LISSolverOptimal();
            int length = solver.FindLISLength(sequence);
            List<int> lis = solver.ReconstructLIS(sequence);
            
            Console.WriteLine($"Input: [{string.Join(", ", sequence)}]");
            Console.WriteLine($"LIS Length: {length}, LIS: [{string.Join(", ", lis)}]");
            Console.WriteLine();
        }

        static void BenchmarkLIS()
        {
            int[] sizes = { 100, 500, 1000, 2000 };
            
            foreach (int size in sizes)
            {
                int[] sequence = GenerateRandomSequence(size, 1000);
                
                // O(n^2) approach
                Stopwatch sw1 = Stopwatch.StartNew();
                LISSolverQuadratic quadraticSolver = new LISSolverQuadratic();
                int length1 = quadraticSolver.FindLISLength(sequence);
                sw1.Stop();
                
                // O(n log n) approach
                Stopwatch sw2 = Stopwatch.StartNew();
                LISSolverOptimal optimalSolver = new LISSolverOptimal();
                int length2 = optimalSolver.FindLISLength(sequence);
                sw2.Stop();
                
                Console.WriteLine($"n={size}:");
                Console.WriteLine($"  O(n^2): {sw1.ElapsedMilliseconds}ms (length={length1})");
                Console.WriteLine($"  O(n log n): {sw2.ElapsedMilliseconds}ms (length={length2})");
            }
        }

        static int[] GenerateRandomSequence(int size, int maxValue)
        {
            Random random = new Random(42);
            int[] sequence = new int[size];
            for (int i = 0; i < size; i++)
            {
                sequence[i] = random.Next(1, maxValue + 1);
            }
            return sequence;
        }
    }

    /// <summary>
    /// O(n^2) Dynamic Programming solution for LIS
    /// dp[i] = length of LIS ending at index i
    /// </summary>
    class LISSolverQuadratic
    {
        public int FindLISLength(int[] sequence)
        {
            if (sequence == null || sequence.Length == 0)
            {
                return 0;
            }

            int n = sequence.Length;
            int[] dp = new int[n];
            
            // Each element forms a LIS of length 1 by itself
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
            }

            // For each position i, check all previous positions j
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    // If sequence[j] < sequence[i], we can extend the LIS ending at j
                    if (sequence[j] < sequence[i])
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                    }
                }
            }

            // Find maximum length among all positions
            int maxLength = 0;
            for (int i = 0; i < n; i++)
            {
                maxLength = Math.Max(maxLength, dp[i]);
            }

            return maxLength;
        }

        public List<int> ReconstructLIS(int[] sequence)
        {
            if (sequence == null || sequence.Length == 0)
            {
                return new List<int>();
            }

            int n = sequence.Length;
            int[] dp = new int[n];
            int[] predecessor = new int[n];
            
            // Initialize
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                predecessor[i] = -1; // No predecessor
            }

            // Build DP table and track predecessors
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (sequence[j] < sequence[i] && dp[j] + 1 > dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        predecessor[i] = j; // Track where we came from
                    }
                }
            }

            // Find the position with maximum LIS length
            int maxLength = 0;
            int maxIndex = 0;
            for (int i = 0; i < n; i++)
            {
                if (dp[i] > maxLength)
                {
                    maxLength = dp[i];
                    maxIndex = i;
                }
            }

            // Reconstruct the LIS by following predecessors
            List<int> lis = new List<int>();
            int current = maxIndex;
            while (current != -1)
            {
                lis.Add(sequence[current]);
                current = predecessor[current];
            }

            lis.Reverse(); // We built it backwards
            return lis;
        }
    }

    /// <summary>
    /// O(n log n) Patience Sorting solution for LIS
    /// tails[k] = smallest tail element of all increasing subsequences of length k+1
    /// </summary>
    class LISSolverOptimal
    {
        public int FindLISLength(int[] sequence)
        {
            if (sequence == null || sequence.Length == 0)
            {
                return 0;
            }

            int n = sequence.Length;
            List<int> tails = new List<int>();

            foreach (int value in sequence)
            {
                // Binary search for the position to insert/replace
                int position = BinarySearchLowerBound(tails, value);
                
                if (position == tails.Count)
                {
                    // value is larger than all tails, extend the sequence
                    tails.Add(value);
                }
                else
                {
                    // Replace the tail at position with smaller value
                    tails[position] = value;
                }
            }

            return tails.Count;
        }

        public List<int> ReconstructLIS(int[] sequence)
        {
            if (sequence == null || sequence.Length == 0)
            {
                return new List<int>();
            }

            int n = sequence.Length;
            List<int> tails = new List<int>();
            int[] lisLength = new int[n]; // Length of LIS ending at each index
            int[] predecessor = new int[n]; // Previous index in LIS
            List<int>[] tailsAtLength = new List<int>[n]; // Indices that can be tails for each length

            for (int i = 0; i < n; i++)
            {
                tailsAtLength[i] = new List<int>();
                predecessor[i] = -1;
            }

            for (int i = 0; i < n; i++)
            {
                int value = sequence[i];
                int position = BinarySearchLowerBound(tails, value);
                
                if (position == tails.Count)
                {
                    tails.Add(value);
                }
                else
                {
                    tails[position] = value;
                }

                lisLength[i] = position + 1;
                
                // Find predecessor from previous length
                if (position > 0 && tailsAtLength[position - 1].Count > 0)
                {
                    // Find the best predecessor (last index with smaller value)
                    for (int j = tailsAtLength[position - 1].Count - 1; j >= 0; j--)
                    {
                        int predIndex = tailsAtLength[position - 1][j];
                        if (sequence[predIndex] < value)
                        {
                            predecessor[i] = predIndex;
                            break;
                        }
                    }
                }
                
                tailsAtLength[position].Add(i);
            }

            // Find the last index with maximum LIS length
            int maxLength = tails.Count;
            int lastIndex = -1;
            
            for (int i = n - 1; i >= 0; i--)
            {
                if (lisLength[i] == maxLength)
                {
                    lastIndex = i;
                    break;
                }
            }

            // Reconstruct LIS
            List<int> lis = new List<int>();
            int current = lastIndex;
            while (current != -1)
            {
                lis.Add(sequence[current]);
                current = predecessor[current];
            }

            lis.Reverse();
            return lis;
        }

        /// <summary>
        /// Binary search to find the position where value should be inserted
        /// Returns the index of the first element >= value
        /// </summary>
        private int BinarySearchLowerBound(List<int> list, int value)
        {
            int left = 0;
            int right = list.Count;

            while (left < right)
            {
                int mid = left + (right - left) / 2;
                
                if (list[mid] < value)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            return left;
        }
    }
}
