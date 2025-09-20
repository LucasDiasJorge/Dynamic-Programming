using System;

namespace Knapsack01
{
    /// <summary>
    /// 0/1 Knapsack Problem Implementation
    /// Each item can be selected at most once (0 or 1 times)
    /// Goal: Maximize value without exceeding weight capacity
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example test case
            int[] weights = { 6, 4, 3, 2 };
            int[] values = { 30, 20, 14, 16 };
            int capacity = 7;
            
            Console.WriteLine("=== 0/1 Knapsack Problem ===");
            Console.WriteLine($"Items: {weights.Length}");
            Console.WriteLine($"Capacity: {capacity}");
            Console.WriteLine();
            
            // Display items
            Console.WriteLine("Items available:");
            for (int i = 0; i < weights.Length; i++)
            {
                Console.WriteLine($"Item {i + 1}: Weight = {weights[i]}, Value = {values[i]}");
            }
            Console.WriteLine();
            
            // Test Top-Down approach
            KnapsackSolver solver = new KnapsackSolver();
            int maxValueTopDown = solver.SolveTopDown(weights, values, capacity);
            Console.WriteLine($"Maximum Value (Top-Down): {maxValueTopDown}");
            
            // Test Bottom-Up 2D approach
            int maxValueBottomUp2D = solver.SolveBottomUp2D(weights, values, capacity);
            Console.WriteLine($"Maximum Value (Bottom-Up 2D): {maxValueBottomUp2D}");
            
            // Test Bottom-Up 1D (space optimized) approach
            int maxValueBottomUp1D = solver.SolveBottomUp1D(weights, values, capacity);
            Console.WriteLine($"Maximum Value (Bottom-Up 1D): {maxValueBottomUp1D}");
            
            // Test with item reconstruction
            KnapsackResult result = solver.SolveWithItemReconstruction(weights, values, capacity);
            Console.WriteLine($"Maximum Value (with items): {result.MaxValue}");
            Console.WriteLine($"Selected items: [{string.Join(", ", result.SelectedItems)}]");
            Console.WriteLine();
            
            // Run performance benchmarks
            KnapsackBenchmark.RunBenchmarks();
        }
    }
}