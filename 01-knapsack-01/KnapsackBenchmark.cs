using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Knapsack01
{
    /// <summary>
    /// Performance benchmarking for different knapsack implementations
    /// </summary>
    public class KnapsackBenchmark
    {
        public static void RunBenchmarks()
        {
            Console.WriteLine("=== Performance Benchmarks ===");
            
            // Test with larger dataset
            int[] weights = GenerateRandomWeights(100, 1, 50);
            int[] values = GenerateRandomValues(100, 1, 100);
            int capacity = 250;

            KnapsackSolver solver = new KnapsackSolver();
            Stopwatch stopwatch = new Stopwatch();

            // Benchmark Top-Down
            stopwatch.Start();
            int resultTopDown = solver.SolveTopDown(weights, values, capacity);
            stopwatch.Stop();
            Console.WriteLine($"Top-Down: {resultTopDown} (Time: {stopwatch.ElapsedMilliseconds}ms)");

            // Benchmark Bottom-Up 2D
            stopwatch.Restart();
            int resultBottomUp2D = solver.SolveBottomUp2D(weights, values, capacity);
            stopwatch.Stop();
            Console.WriteLine($"Bottom-Up 2D: {resultBottomUp2D} (Time: {stopwatch.ElapsedMilliseconds}ms)");

            // Benchmark Bottom-Up 1D
            stopwatch.Restart();
            int resultBottomUp1D = solver.SolveBottomUp1D(weights, values, capacity);
            stopwatch.Stop();
            Console.WriteLine($"Bottom-Up 1D: {resultBottomUp1D} (Time: {stopwatch.ElapsedMilliseconds}ms)");

            // Verify all results are the same
            bool allResultsMatch = (resultTopDown == resultBottomUp2D) && (resultBottomUp2D == resultBottomUp1D);
            Console.WriteLine($"All results match: {allResultsMatch}");
        }

        private static int[] GenerateRandomWeights(int count, int minWeight, int maxWeight)
        {
            Random random = new Random(42); // Fixed seed for reproducible results
            int[] weights = new int[count];
            
            for (int i = 0; i < count; i++)
            {
                weights[i] = random.Next(minWeight, maxWeight + 1);
            }
            
            return weights;
        }

        private static int[] GenerateRandomValues(int count, int minValue, int maxValue)
        {
            Random random = new Random(42); // Fixed seed for reproducible results
            int[] values = new int[count];
            
            for (int i = 0; i < count; i++)
            {
                values[i] = random.Next(minValue, maxValue + 1);
            }
            
            return values;
        }
    }
}