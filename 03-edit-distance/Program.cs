using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EditDistance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Edit Distance (Levenshtein) ===\n");

            // Test case from README
            string source = "kitten";
            string target = "sitting";
            
            Console.WriteLine($"Source: \"{source}\"");
            Console.WriteLine($"Target: \"{target}\"");
            Console.WriteLine();

            // Test 2D DP approach
            EditDistanceSolver2D solver2D = new EditDistanceSolver2D();
            int distance2D = solver2D.CalculateDistance(source, target);
            List<EditOperation> operations = solver2D.ReconstructOperations(source, target);
            
            Console.WriteLine("--- 2D Dynamic Programming ---");
            Console.WriteLine($"Edit Distance: {distance2D}");
            Console.WriteLine("Operations:");
            foreach (EditOperation op in operations)
            {
                Console.WriteLine($"  {op}");
            }
            Console.WriteLine();

            // Test 1D optimized approach
            EditDistanceSolver1D solver1D = new EditDistanceSolver1D();
            int distance1D = solver1D.CalculateDistance(source, target);
            
            Console.WriteLine("--- 1D Space-Optimized ---");
            Console.WriteLine($"Edit Distance: {distance1D}");
            Console.WriteLine();

            // Additional test cases
            TestCase("horse", "ros");
            TestCase("intention", "execution");
            TestCase("", "abc");
            TestCase("abc", "");
            
            // Benchmark
            Console.WriteLine("\n=== Benchmark ===");
            BenchmarkEditDistance();
        }

        static void TestCase(string source, string target)
        {
            EditDistanceSolver2D solver = new EditDistanceSolver2D();
            int distance = solver.CalculateDistance(source, target);
            
            Console.WriteLine($"Source: \"{source}\" -> Target: \"{target}\"");
            Console.WriteLine($"Edit Distance: {distance}");
            Console.WriteLine();
        }

        static void BenchmarkEditDistance()
        {
            int[] sizes = { 100, 500, 1000, 2000 };
            
            foreach (int size in sizes)
            {
                string source = GenerateRandomString(size);
                string target = GenerateRandomString(size);
                
                // 2D approach
                Stopwatch sw1 = Stopwatch.StartNew();
                EditDistanceSolver2D solver2D = new EditDistanceSolver2D();
                int distance1 = solver2D.CalculateDistance(source, target);
                sw1.Stop();
                
                // 1D optimized approach
                Stopwatch sw2 = Stopwatch.StartNew();
                EditDistanceSolver1D solver1D = new EditDistanceSolver1D();
                int distance2 = solver1D.CalculateDistance(source, target);
                sw2.Stop();
                
                Console.WriteLine($"Strings of length {size}:");
                Console.WriteLine($"  2D Array: {sw1.ElapsedMilliseconds}ms (distance={distance1})");
                Console.WriteLine($"  1D Optimized: {sw2.ElapsedMilliseconds}ms (distance={distance2})");
            }
        }

        static string GenerateRandomString(int length)
        {
            Random random = new Random(42);
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)('a' + random.Next(0, 26));
            }
            return new string(chars);
        }
    }

    enum OperationType
    {
        Insert,
        Delete,
        Replace,
        Keep
    }

    class EditOperation
    {
        public OperationType Type { get; set; }
        public int SourcePosition { get; set; }
        public int TargetPosition { get; set; }
        public char SourceChar { get; set; }
        public char TargetChar { get; set; }

        public override string ToString()
        {
            return Type switch
            {
                OperationType.Insert => $"Insert '{TargetChar}' at position {SourcePosition}",
                OperationType.Delete => $"Delete '{SourceChar}' at position {SourcePosition}",
                OperationType.Replace => $"Replace '{SourceChar}' with '{TargetChar}' at position {SourcePosition}",
                OperationType.Keep => $"Keep '{SourceChar}' at position {SourcePosition}",
                _ => "Unknown operation"
            };
        }
    }

    /// <summary>
    /// 2D Dynamic Programming solution for Edit Distance
    /// dp[i][j] = minimum edit distance to transform source[0..i) to target[0..j)
    /// Space: O(|source| * |target|), Time: O(|source| * |target|)
    /// </summary>
    class EditDistanceSolver2D
    {
        public int CalculateDistance(string source, string target)
        {
            int m = source.Length;
            int n = target.Length;
            
            // dp[i][j] = edit distance for source[0..i) and target[0..j)
            int[,] dp = new int[m + 1, n + 1];

            // Base cases: transforming from/to empty string
            for (int i = 0; i <= m; i++)
            {
                dp[i, 0] = i; // Delete all characters from source
            }
            for (int j = 0; j <= n; j++)
            {
                dp[0, j] = j; // Insert all characters to reach target
            }

            // Fill the DP table
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (source[i - 1] == target[j - 1])
                    {
                        // Characters match, no operation needed
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        // Take minimum of three operations (each costs 1)
                        int deleteCost = dp[i - 1, j] + 1;     // Delete from source
                        int insertCost = dp[i, j - 1] + 1;     // Insert into source
                        int replaceCost = dp[i - 1, j - 1] + 1; // Replace in source
                        
                        dp[i, j] = Math.Min(deleteCost, Math.Min(insertCost, replaceCost));
                    }
                }
            }

            return dp[m, n];
        }

        public List<EditOperation> ReconstructOperations(string source, string target)
        {
            int m = source.Length;
            int n = target.Length;
            
            int[,] dp = new int[m + 1, n + 1];

            // Build DP table (same as CalculateDistance)
            for (int i = 0; i <= m; i++)
            {
                dp[i, 0] = i;
            }
            for (int j = 0; j <= n; j++)
            {
                dp[0, j] = j;
            }

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (source[i - 1] == target[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        dp[i, j] = Math.Min(dp[i - 1, j] + 1, 
                                   Math.Min(dp[i, j - 1] + 1, 
                                           dp[i - 1, j - 1] + 1));
                    }
                }
            }

            // Backtrack to reconstruct the operations
            List<EditOperation> operations = new List<EditOperation>();
            int currentI = m;
            int currentJ = n;

            while (currentI > 0 || currentJ > 0)
            {
                if (currentI == 0)
                {
                    // Only insertions left
                    operations.Add(new EditOperation
                    {
                        Type = OperationType.Insert,
                        SourcePosition = currentI,
                        TargetPosition = currentJ - 1,
                        TargetChar = target[currentJ - 1]
                    });
                    currentJ--;
                }
                else if (currentJ == 0)
                {
                    // Only deletions left
                    operations.Add(new EditOperation
                    {
                        Type = OperationType.Delete,
                        SourcePosition = currentI - 1,
                        SourceChar = source[currentI - 1]
                    });
                    currentI--;
                }
                else if (source[currentI - 1] == target[currentJ - 1])
                {
                    // Characters match
                    operations.Add(new EditOperation
                    {
                        Type = OperationType.Keep,
                        SourcePosition = currentI - 1,
                        TargetPosition = currentJ - 1,
                        SourceChar = source[currentI - 1],
                        TargetChar = target[currentJ - 1]
                    });
                    currentI--;
                    currentJ--;
                }
                else
                {
                    // Find which operation was used
                    int deleteCost = dp[currentI - 1, currentJ];
                    int insertCost = dp[currentI, currentJ - 1];
                    int replaceCost = dp[currentI - 1, currentJ - 1];
                    
                    int minCost = Math.Min(deleteCost, Math.Min(insertCost, replaceCost));

                    if (minCost == replaceCost)
                    {
                        operations.Add(new EditOperation
                        {
                            Type = OperationType.Replace,
                            SourcePosition = currentI - 1,
                            TargetPosition = currentJ - 1,
                            SourceChar = source[currentI - 1],
                            TargetChar = target[currentJ - 1]
                        });
                        currentI--;
                        currentJ--;
                    }
                    else if (minCost == deleteCost)
                    {
                        operations.Add(new EditOperation
                        {
                            Type = OperationType.Delete,
                            SourcePosition = currentI - 1,
                            SourceChar = source[currentI - 1]
                        });
                        currentI--;
                    }
                    else // insertCost
                    {
                        operations.Add(new EditOperation
                        {
                            Type = OperationType.Insert,
                            SourcePosition = currentI,
                            TargetPosition = currentJ - 1,
                            TargetChar = target[currentJ - 1]
                        });
                        currentJ--;
                    }
                }
            }

            operations.Reverse(); // We built it backwards
            return operations;
        }
    }

    /// <summary>
    /// 1D Space-Optimized solution for Edit Distance
    /// Space: O(min(|source|, |target|)), Time: O(|source| * |target|)
    /// </summary>
    class EditDistanceSolver1D
    {
        public int CalculateDistance(string source, string target)
        {
            // Ensure source is the shorter string for space optimization
            if (source.Length > target.Length)
            {
                string temp = source;
                source = target;
                target = temp;
            }

            int m = source.Length;
            int n = target.Length;
            
            // We only need two rows: previous and current
            int[] previousRow = new int[m + 1];
            int[] currentRow = new int[m + 1];

            // Initialize first row
            for (int i = 0; i <= m; i++)
            {
                previousRow[i] = i;
            }

            // Fill row by row
            for (int j = 1; j <= n; j++)
            {
                currentRow[0] = j; // First column (insertions)
                
                for (int i = 1; i <= m; i++)
                {
                    if (source[i - 1] == target[j - 1])
                    {
                        currentRow[i] = previousRow[i - 1];
                    }
                    else
                    {
                        int deleteCost = previousRow[i] + 1;
                        int insertCost = currentRow[i - 1] + 1;
                        int replaceCost = previousRow[i - 1] + 1;
                        
                        currentRow[i] = Math.Min(deleteCost, Math.Min(insertCost, replaceCost));
                    }
                }

                // Swap rows for next iteration
                int[] temp = previousRow;
                previousRow = currentRow;
                currentRow = temp;
            }

            return previousRow[m];
        }
    }
}
