using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LongestCommonSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Longest Common Subsequence (LCS) ===\n");

            // Test case from README
            string sequence1 = "abcde";
            string sequence2 = "ace";
            
            Console.WriteLine($"Sequence 1: \"{sequence1}\"");
            Console.WriteLine($"Sequence 2: \"{sequence2}\"");
            Console.WriteLine();

            // Test 2D DP approach
            LCSSolver2D solver2D = new LCSSolver2D();
            int length2D = solver2D.FindLCSLength(sequence1, sequence2);
            string lcs2D = solver2D.ReconstructLCS(sequence1, sequence2);
            
            Console.WriteLine("--- 2D Dynamic Programming ---");
            Console.WriteLine($"LCS Length: {length2D}");
            Console.WriteLine($"LCS: \"{lcs2D}\"");
            Console.WriteLine();

            // Test 1D optimized approach
            LCSSolver1D solver1D = new LCSSolver1D();
            int length1D = solver1D.FindLCSLength(sequence1, sequence2);
            
            Console.WriteLine("--- 1D Space-Optimized ---");
            Console.WriteLine($"LCS Length: {length1D}");
            Console.WriteLine();

            // Additional test cases
            TestCase("AGGTAB", "GXTXAYB");
            TestCase("ABCDGH", "AEDFHR");
            TestCase("ABC", "AC");
            TestCase("ABC", "DEF");
            TestCase("", "ABC");
            
            // Benchmark
            Console.WriteLine("\n=== Benchmark ===");
            BenchmarkLCS();
        }

        static void TestCase(string s1, string s2)
        {
            LCSSolver2D solver = new LCSSolver2D();
            int length = solver.FindLCSLength(s1, s2);
            string lcs = solver.ReconstructLCS(s1, s2);
            
            Console.WriteLine($"S1: \"{s1}\", S2: \"{s2}\"");
            Console.WriteLine($"LCS Length: {length}, LCS: \"{lcs}\"");
            Console.WriteLine();
        }

        static void BenchmarkLCS()
        {
            int[] sizes = { 100, 500, 1000, 2000 };
            
            foreach (int size in sizes)
            {
                string s1 = GenerateRandomString(size);
                string s2 = GenerateRandomString(size);
                
                // 2D approach
                Stopwatch sw1 = Stopwatch.StartNew();
                LCSSolver2D solver2D = new LCSSolver2D();
                int length1 = solver2D.FindLCSLength(s1, s2);
                sw1.Stop();
                
                // 1D optimized approach
                Stopwatch sw2 = Stopwatch.StartNew();
                LCSSolver1D solver1D = new LCSSolver1D();
                int length2 = solver1D.FindLCSLength(s1, s2);
                sw2.Stop();
                
                Console.WriteLine($"Strings of length {size}:");
                Console.WriteLine($"  2D Array: {sw1.ElapsedMilliseconds}ms (length={length1})");
                Console.WriteLine($"  1D Optimized: {sw2.ElapsedMilliseconds}ms (length={length2})");
            }
        }

        static string GenerateRandomString(int length)
        {
            Random random = new Random(42);
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)('A' + random.Next(0, 26));
            }
            return new string(chars);
        }
    }

    /// <summary>
    /// 2D Dynamic Programming solution for LCS
    /// dp[i][j] = LCS length for s1[0..i) and s2[0..j)
    /// Time: O(m*n), Space: O(m*n)
    /// </summary>
    class LCSSolver2D
    {
        public int FindLCSLength(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            
            // dp[i][j] = LCS length for s1[0..i) and s2[0..j)
            int[,] dp = new int[m + 1, n + 1];

            // Base cases: empty strings have LCS length 0
            // Already initialized to 0 by default

            // Fill the DP table
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        // Characters match: extend LCS from previous diagonal
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        // Characters don't match: take max from left or top
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            return dp[m, n];
        }

        public string ReconstructLCS(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            
            // Build the DP table
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            // Backtrack to reconstruct the LCS
            List<char> lcsChars = new List<char>();
            int currentI = m;
            int currentJ = n;

            while (currentI > 0 && currentJ > 0)
            {
                if (s1[currentI - 1] == s2[currentJ - 1])
                {
                    // Characters match: this character is part of LCS
                    lcsChars.Add(s1[currentI - 1]);
                    currentI--;
                    currentJ--;
                }
                else if (dp[currentI - 1, currentJ] > dp[currentI, currentJ - 1])
                {
                    // Move up (skip character from s1)
                    currentI--;
                }
                else
                {
                    // Move left (skip character from s2)
                    currentJ--;
                }
            }

            lcsChars.Reverse(); // We built it backwards
            return new string(lcsChars.ToArray());
        }

        /// <summary>
        /// Alternative reconstruction using decision tracking
        /// </summary>
        public string ReconstructLCSWithDecisions(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            
            int[,] dp = new int[m + 1, n + 1];
            // Track which direction we came from: 0=diagonal, 1=up, 2=left
            int[,] decision = new int[m + 1, n + 1];

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        decision[i, j] = 0; // Came from diagonal
                    }
                    else
                    {
                        if (dp[i - 1, j] >= dp[i, j - 1])
                        {
                            dp[i, j] = dp[i - 1, j];
                            decision[i, j] = 1; // Came from up
                        }
                        else
                        {
                            dp[i, j] = dp[i, j - 1];
                            decision[i, j] = 2; // Came from left
                        }
                    }
                }
            }

            // Reconstruct using decisions
            List<char> lcsChars = new List<char>();
            int currentI = m;
            int currentJ = n;

            while (currentI > 0 && currentJ > 0)
            {
                if (decision[currentI, currentJ] == 0)
                {
                    lcsChars.Add(s1[currentI - 1]);
                    currentI--;
                    currentJ--;
                }
                else if (decision[currentI, currentJ] == 1)
                {
                    currentI--;
                }
                else
                {
                    currentJ--;
                }
            }

            lcsChars.Reverse();
            return new string(lcsChars.ToArray());
        }
    }

    /// <summary>
    /// 1D Space-Optimized solution for LCS
    /// Time: O(m*n), Space: O(min(m,n))
    /// Note: This only computes length, not the actual LCS string
    /// </summary>
    class LCSSolver1D
    {
        public int FindLCSLength(string s1, string s2)
        {
            // Ensure s1 is the shorter string for space optimization
            if (s1.Length > s2.Length)
            {
                string temp = s1;
                s1 = s2;
                s2 = temp;
            }

            int m = s1.Length;
            int n = s2.Length;
            
            // We only need two rows: previous and current
            int[] previousRow = new int[m + 1];
            int[] currentRow = new int[m + 1];

            // Fill row by row
            for (int j = 1; j <= n; j++)
            {
                for (int i = 1; i <= m; i++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        currentRow[i] = previousRow[i - 1] + 1;
                    }
                    else
                    {
                        currentRow[i] = Math.Max(previousRow[i], currentRow[i - 1]);
                    }
                }

                // Swap rows for next iteration
                int[] temp = previousRow;
                previousRow = currentRow;
                currentRow = temp;
                
                // Clear current row for next iteration
                Array.Clear(currentRow, 0, currentRow.Length);
            }

            return previousRow[m];
        }
    }

    /// <summary>
    /// Additional utility: Compute LCS as a diff between two sequences
    /// This shows the relationship between LCS and Edit Distance
    /// </summary>
    class LCSDiff
    {
        public enum DiffType
        {
            Keep,      // In LCS
            DeleteFromS1,  // Only in s1
            DeleteFromS2   // Only in s2
        }

        public class DiffItem
        {
            public DiffType Type { get; set; }
            public char Character { get; set; }
            public int PositionS1 { get; set; }
            public int PositionS2 { get; set; }

            public override string ToString()
            {
                return Type switch
                {
                    DiffType.Keep => $"  {Character}",
                    DiffType.DeleteFromS1 => $"- {Character} (from s1)",
                    DiffType.DeleteFromS2 => $"+ {Character} (from s2)",
                    _ => "?"
                };
            }
        }

        public List<DiffItem> ComputeDiff(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            
            // Build LCS DP table
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            // Generate diff
            List<DiffItem> diff = new List<DiffItem>();
            int currentI = m;
            int currentJ = n;

            while (currentI > 0 || currentJ > 0)
            {
                if (currentI > 0 && currentJ > 0 && s1[currentI - 1] == s2[currentJ - 1])
                {
                    diff.Add(new DiffItem
                    {
                        Type = DiffType.Keep,
                        Character = s1[currentI - 1],
                        PositionS1 = currentI - 1,
                        PositionS2 = currentJ - 1
                    });
                    currentI--;
                    currentJ--;
                }
                else if (currentJ > 0 && (currentI == 0 || dp[currentI, currentJ - 1] >= dp[currentI - 1, currentJ]))
                {
                    diff.Add(new DiffItem
                    {
                        Type = DiffType.DeleteFromS2,
                        Character = s2[currentJ - 1],
                        PositionS2 = currentJ - 1
                    });
                    currentJ--;
                }
                else
                {
                    diff.Add(new DiffItem
                    {
                        Type = DiffType.DeleteFromS1,
                        Character = s1[currentI - 1],
                        PositionS1 = currentI - 1
                    });
                    currentI--;
                }
            }

            diff.Reverse();
            return diff;
        }
    }
}
