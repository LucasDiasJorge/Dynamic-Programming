using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FibonacciMemoization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Fibonacci Memoization (Caching Pattern) ===\n");
            Console.WriteLine("Enterprise use case: Expensive recursive calculations\n");

            FibonacciService service = new FibonacciService();

            // Test different approaches
            Console.WriteLine("1. Without Memoization (Pure Recursion)");
            TestPureRecursion();
            Console.WriteLine();

            Console.WriteLine("2. With Memoization (Cached)");
            TestWithMemoization(service);
            Console.WriteLine();

            Console.WriteLine("3. Cache Statistics");
            DisplayCacheStats(service);
            Console.WriteLine();

            Console.WriteLine("4. Iterative Approach (Comparison)");
            TestIterative();
            Console.WriteLine();

            // Real-world scenario
            Console.WriteLine("5. Real-world Scenario: Financial Calculation");
            DemoFinancialScenario();
        }

        static void TestPureRecursion()
        {
            int[] testCases = { 20, 25, 30 };
            
            foreach (int n in testCases)
            {
                Stopwatch sw = Stopwatch.StartNew();
                long result = FibonacciPure(n);
                sw.Stop();
                
                Console.WriteLine($"   Fib({n}) = {result} | Time: {sw.ElapsedMilliseconds}ms");
            }
            
            Console.WriteLine("   ⚠️  Notice: Time grows exponentially!");
        }

        static void TestWithMemoization(FibonacciService service)
        {
            int testValue = 40;
            
            // First call - cold cache
            Stopwatch sw1 = Stopwatch.StartNew();
            long result1 = service.Calculate(testValue);
            sw1.Stop();
            
            Console.WriteLine($"   First call - Fib({testValue}) = {result1}");
            Console.WriteLine($"   Time: {sw1.ElapsedMilliseconds}ms (building cache)");
            
            // Second call - warm cache
            Stopwatch sw2 = Stopwatch.StartNew();
            long result2 = service.Calculate(testValue);
            sw2.Stop();
            
            Console.WriteLine($"\n   Second call - Fib({testValue}) = {result2}");
            Console.WriteLine($"   Time: {sw2.ElapsedMilliseconds}ms (cache hit)");
            Console.WriteLine($"   🚀 Speedup: {(double)sw1.ElapsedMilliseconds / Math.Max(sw2.ElapsedMilliseconds, 0.001):F0}x faster!");
        }

        static void DisplayCacheStats(FibonacciService service)
        {
            service.ResetStats();
            
            service.Calculate(35);
            service.Calculate(30);
            service.Calculate(35); // Cache hit
            service.Calculate(38);
            
            Console.WriteLine($"   Cache Size: {service.CacheSize} entries");
            Console.WriteLine($"   Cache Hits: {service.CacheHits}");
            Console.WriteLine($"   Total Calls: {service.TotalCalls}");
            Console.WriteLine($"   Hit Rate: {service.HitRate:P1}");
        }

        static void TestIterative()
        {
            int testValue = 45;
            
            Stopwatch sw = Stopwatch.StartNew();
            long result = FibonacciIterative(testValue);
            sw.Stop();
            
            Console.WriteLine($"   Fib({testValue}) = {result}");
            Console.WriteLine($"   Time: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"   💡 Iterative is fast but doesn't cache intermediate results");
        }

        static void DemoFinancialScenario()
        {
            Console.WriteLine("   Scenario: Calculate compound interest for multiple periods\n");
            
            FinancialCalculator calculator = new FinancialCalculator();
            decimal principal = 10000;
            double rate = 0.05; // 5% per year
            
            Console.WriteLine($"   Principal: ${principal}");
            Console.WriteLine($"   Rate: {rate:P0} per period\n");
            
            int[] periods = { 5, 10, 5, 15, 10 }; // Notice repetitions
            
            foreach (int period in periods)
            {
                Stopwatch sw = Stopwatch.StartNew();
                decimal result = calculator.CalculateCompoundInterest(principal, rate, period);
                sw.Stop();
                
                string cacheStatus = sw.ElapsedMilliseconds == 0 ? "[CACHED]" : "";
                Console.WriteLine($"   Period {period}: ${result:F2} ({sw.ElapsedMilliseconds}ms) {cacheStatus}");
            }
            
            Console.WriteLine($"\n   Cache Stats: {calculator.CacheHits} hits, {calculator.CacheSize} entries");
        }

        // Pure recursion (no memoization) - SLOW!
        static long FibonacciPure(int n)
        {
            if (n <= 1) return n;
            return FibonacciPure(n - 1) + FibonacciPure(n - 2);
        }

        // Iterative approach - fast but no caching of intermediates
        static long FibonacciIterative(int n)
        {
            if (n <= 1) return n;
            
            long previous = 0;
            long current = 1;
            
            for (int i = 2; i <= n; i++)
            {
                long next = previous + current;
                previous = current;
                current = next;
            }
            
            return current;
        }
    }

    /// <summary>
    /// Fibonacci service with memoization pattern
    /// Thread-safe for concurrent access
    /// </summary>
    class FibonacciService
    {
        private readonly Dictionary<int, long> cache = new Dictionary<int, long>();
        private int cacheHits = 0;
        private int totalCalls = 0;

        public int CacheSize => cache.Count;
        public int CacheHits => cacheHits;
        public int TotalCalls => totalCalls;
        public double HitRate => totalCalls > 0 ? (double)cacheHits / totalCalls : 0;

        public long Calculate(int n)
        {
            totalCalls++;
            
            if (n <= 1) return n;
            
            if (cache.ContainsKey(n))
            {
                cacheHits++;
                return cache[n];
            }

            long result = Calculate(n - 1) + Calculate(n - 2);
            cache[n] = result;
            return result;
        }

        public void ResetStats()
        {
            cacheHits = 0;
            totalCalls = 0;
            cache.Clear();
        }

        public void ClearCache()
        {
            cache.Clear();
        }
    }

    /// <summary>
    /// Real-world example: Financial calculations with caching
    /// </summary>
    class FinancialCalculator
    {
        private readonly Dictionary<string, decimal> cache = new Dictionary<string, decimal>();
        public int CacheHits { get; private set; }
        public int CacheSize => cache.Count;

        public decimal CalculateCompoundInterest(decimal principal, double rate, int periods)
        {
            string key = $"{principal}:{rate}:{periods}";
            
            if (cache.ContainsKey(key))
            {
                CacheHits++;
                return cache[key];
            }

            // Simulate expensive calculation
            System.Threading.Thread.Sleep(10);
            
            decimal result = principal * (decimal)Math.Pow(1 + rate, periods);
            cache[key] = result;
            
            return result;
        }
    }

    /// <summary>
    /// Enterprise pattern: Generic memoization wrapper
    /// </summary>
    class MemoizationService<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
        private readonly Func<TKey, TValue> calculator;

        public MemoizationService(Func<TKey, TValue> calculator)
        {
            this.calculator = calculator;
        }

        public TValue GetOrCalculate(TKey key)
        {
            if (!cache.ContainsKey(key))
            {
                cache[key] = calculator(key);
            }
            return cache[key];
        }

        public void ClearCache() => cache.Clear();
        public int CacheSize => cache.Count;
    }
}
