using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CoinChange
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Coin Change Problem ===\n");

            int[] coins = { 1, 2, 5 };
            int amount = 11;
            
            Console.WriteLine($"Coins: [{string.Join(", ", coins)}]");
            Console.WriteLine($"Amount: {amount}");
            Console.WriteLine();

            // Test minimum coins approach
            CoinChangeMinimum minSolver = new CoinChangeMinimum();
            int minCoins = minSolver.CalculateMinimumCoins(coins, amount);
            List<int> usedCoins = minSolver.ReconstructCoins(coins, amount);
            
            Console.WriteLine("--- Minimum Number of Coins ---");
            if (minCoins == -1)
            {
                Console.WriteLine("Impossible to make the amount");
            }
            else
            {
                Console.WriteLine($"Minimum coins needed: {minCoins}");
                Console.WriteLine($"Coins used: [{string.Join(", ", usedCoins)}]");
            }
            Console.WriteLine();

            // Test count ways approach
            CoinChangeCountWays countSolver = new CoinChangeCountWays();
            long ways = countSolver.CountWays(coins, amount);
            
            Console.WriteLine("--- Number of Ways ---");
            Console.WriteLine($"Number of ways to make {amount}: {ways}");
            Console.WriteLine();

            // Additional test cases
            TestMinimumCoins(new int[] { 1, 2, 5 }, 11);
            TestMinimumCoins(new int[] { 2 }, 3);
            TestMinimumCoins(new int[] { 1 }, 0);
            TestMinimumCoins(new int[] { 2, 5, 10, 1 }, 27);
            
            Console.WriteLine();
            TestCountWays(new int[] { 1, 2, 5 }, 5);
            TestCountWays(new int[] { 2, 3, 5 }, 10);
            TestCountWays(new int[] { 10 }, 10);
            
            // Benchmark
            Console.WriteLine("\n=== Benchmark ===");
            BenchmarkCoinChange();
        }

        static void TestMinimumCoins(int[] coins, int amount)
        {
            CoinChangeMinimum solver = new CoinChangeMinimum();
            int result = solver.CalculateMinimumCoins(coins, amount);
            
            Console.Write($"Coins: [{string.Join(", ", coins)}], Amount: {amount} -> ");
            if (result == -1)
            {
                Console.WriteLine("Impossible");
            }
            else
            {
                Console.WriteLine($"Min coins: {result}");
            }
        }

        static void TestCountWays(int[] coins, int amount)
        {
            CoinChangeCountWays solver = new CoinChangeCountWays();
            long ways = solver.CountWays(coins, amount);
            
            Console.WriteLine($"Coins: [{string.Join(", ", coins)}], Amount: {amount} -> Ways: {ways}");
        }

        static void BenchmarkCoinChange()
        {
            int[] coins = { 1, 2, 5, 10, 25, 50 };
            int[] amounts = { 100, 500, 1000, 5000 };
            
            foreach (int amount in amounts)
            {
                // Minimum coins
                Stopwatch sw1 = Stopwatch.StartNew();
                CoinChangeMinimum minSolver = new CoinChangeMinimum();
                int minCoins = minSolver.CalculateMinimumCoins(coins, amount);
                sw1.Stop();
                
                // Count ways
                Stopwatch sw2 = Stopwatch.StartNew();
                CoinChangeCountWays countSolver = new CoinChangeCountWays();
                long ways = countSolver.CountWays(coins, amount);
                sw2.Stop();
                
                Console.WriteLine($"Amount={amount}:");
                Console.WriteLine($"  Minimum Coins: {sw1.ElapsedMilliseconds}ms (result={minCoins})");
                Console.WriteLine($"  Count Ways: {sw2.ElapsedMilliseconds}ms (ways={ways})");
            }
        }
    }

    /// <summary>
    /// Finds the minimum number of coins needed to make an amount
    /// dp[i] = minimum coins needed to make amount i
    /// Time: O(n * amount), Space: O(amount)
    /// </summary>
    class CoinChangeMinimum
    {
        public int CalculateMinimumCoins(int[] coins, int amount)
        {
            if (amount == 0)
            {
                return 0;
            }

            // dp[i] represents minimum coins needed for amount i
            int[] dp = new int[amount + 1];
            
            // Initialize with "infinity" (impossible to reach)
            // Use int.MaxValue / 2 to avoid overflow when adding 1
            for (int i = 1; i <= amount; i++)
            {
                dp[i] = int.MaxValue / 2;
            }
            
            dp[0] = 0; // Base case: 0 coins for amount 0

            // For each amount from 1 to target
            for (int currentAmount = 1; currentAmount <= amount; currentAmount++)
            {
                // Try each coin
                foreach (int coin in coins)
                {
                    if (coin <= currentAmount)
                    {
                        // If we can use this coin, update minimum
                        dp[currentAmount] = Math.Min(dp[currentAmount], 
                                                     dp[currentAmount - coin] + 1);
                    }
                }
            }

            // If dp[amount] is still infinity, it's impossible
            return dp[amount] >= int.MaxValue / 2 ? -1 : dp[amount];
        }

        public List<int> ReconstructCoins(int[] coins, int amount)
        {
            List<int> usedCoins = new List<int>();
            
            if (amount == 0)
            {
                return usedCoins;
            }

            // Build DP table and track which coin was used
            int[] dp = new int[amount + 1];
            int[] coinUsed = new int[amount + 1];
            
            for (int i = 1; i <= amount; i++)
            {
                dp[i] = int.MaxValue / 2;
                coinUsed[i] = -1;
            }
            
            dp[0] = 0;

            for (int currentAmount = 1; currentAmount <= amount; currentAmount++)
            {
                foreach (int coin in coins)
                {
                    if (coin <= currentAmount && dp[currentAmount - coin] + 1 < dp[currentAmount])
                    {
                        dp[currentAmount] = dp[currentAmount - coin] + 1;
                        coinUsed[currentAmount] = coin; // Track which coin led to this minimum
                    }
                }
            }

            // Check if solution exists
            if (dp[amount] >= int.MaxValue / 2)
            {
                return usedCoins; // Empty list (impossible)
            }

            // Backtrack to reconstruct the coins
            int remaining = amount;
            while (remaining > 0)
            {
                int coin = coinUsed[remaining];
                usedCoins.Add(coin);
                remaining -= coin;
            }

            usedCoins.Sort(); // Optional: sort for cleaner output
            return usedCoins;
        }
    }

    /// <summary>
    /// Counts the number of ways to make an amount using coins
    /// Order doesn't matter (combinations, not permutations)
    /// dp[i] = number of ways to make amount i
    /// Time: O(n * amount), Space: O(amount)
    /// </summary>
    class CoinChangeCountWays
    {
        public long CountWays(int[] coins, int amount)
        {
            // dp[i] represents number of ways to make amount i
            long[] dp = new long[amount + 1];
            
            dp[0] = 1; // One way to make 0: use no coins

            // Iterate through each coin type first (important for combinations)
            // This ensures we don't count permutations
            foreach (int coin in coins)
            {
                // For each amount that can be formed with current and previous coins
                for (int currentAmount = coin; currentAmount <= amount; currentAmount++)
                {
                    // Add the number of ways to make (currentAmount - coin)
                    dp[currentAmount] += dp[currentAmount - coin];
                }
            }

            return dp[amount];
        }

        /// <summary>
        /// Alternative: Count permutations (order matters)
        /// This is less common but included for educational purposes
        /// </summary>
        public long CountPermutations(int[] coins, int amount)
        {
            long[] dp = new long[amount + 1];
            dp[0] = 1;

            // Iterate through amounts first (not coins)
            for (int currentAmount = 1; currentAmount <= amount; currentAmount++)
            {
                foreach (int coin in coins)
                {
                    if (coin <= currentAmount)
                    {
                        dp[currentAmount] += dp[currentAmount - coin];
                    }
                }
            }

            return dp[amount];
        }
    }
}
