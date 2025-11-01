using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FuzzyStringMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Fuzzy String Matching (Search & Autocomplete) ===\n");
            Console.WriteLine("Enterprise use case: Tolerant search, duplicate detection\n");

            // 1. E-commerce Product Search
            Console.WriteLine("1. E-COMMERCE: Product Search with Typos");
            DemoProductSearch();
            Console.WriteLine();

            // 2. CRM Duplicate Detection
            Console.WriteLine("\n2. CRM: Duplicate Contact Detection");
            DemoDuplicateDetection();
            Console.WriteLine();

            // 3. Autocomplete Suggestions
            Console.WriteLine("\n3. AUTOCOMPLETE: Suggestion Engine");
            DemoAutocomplete();
            Console.WriteLine();

            // 4. Data Validation
            Console.WriteLine("\n4. DATA VALIDATION: Address Matching");
            DemoAddressMatching();
            Console.WriteLine();

            // 5. Performance Benchmark
            Console.WriteLine("\n5. PERFORMANCE: Benchmark");
            BenchmarkPerformance();
        }

        static void DemoProductSearch()
        {
            FuzzyMatcher matcher = new FuzzyMatcher();
            
            string[] products = 
            {
                "iPhone 15 Pro Max",
                "Samsung Galaxy S24 Ultra",
                "Google Pixel 8 Pro",
                "OnePlus 12 Pro",
                "Xiaomi 14 Ultra",
                "Sony Xperia 1 V",
                "Motorola Edge 40 Pro"
            };

            string[] searchQueries = 
            {
                "iPhon 15",        // Typo
                "Galaxy S24",      // Partial
                "pixle 8",         // Typo
                "One Plus"         // Space variation
            };

            foreach (string query in searchQueries)
            {
                Console.WriteLine($"   Search: \"{query}\"");
                List<SearchResult> results = matcher.Search(query, products, threshold: 0.4);
                
                if (results.Count > 0)
                {
                    Console.WriteLine("   Results:");
                    foreach (SearchResult result in results.Take(3))
                    {
                        Console.WriteLine($"     - {result.Text} (similarity: {result.Similarity:P0})");
                    }
                }
                else
                {
                    Console.WriteLine("   No matches found");
                }
                Console.WriteLine();
            }
        }

        static void DemoDuplicateDetection()
        {
            FuzzyMatcher matcher = new FuzzyMatcher();
            
            Contact[] contacts = 
            {
                new Contact { Name = "João Silva", Email = "joao@email.com" },
                new Contact { Name = "Joao Silva", Email = "joao.silva@email.com" },
                new Contact { Name = "Maria Santos", Email = "maria@email.com" },
                new Contact { Name = "Maria dos Santos", Email = "maria.santos@email.com" },
                new Contact { Name = "Pedro Oliveira", Email = "pedro@email.com" }
            };

            Console.WriteLine("   Detecting potential duplicates (similarity > 70%):\n");
            
            for (int i = 0; i < contacts.Length; i++)
            {
                for (int j = i + 1; j < contacts.Length; j++)
                {
                    double similarity = matcher.CalculateSimilarity(
                        contacts[i].Name, 
                        contacts[j].Name
                    );
                    
                    if (similarity > 0.7)
                    {
                        Console.WriteLine($"   🔍 Potential duplicate:");
                        Console.WriteLine($"      - \"{contacts[i].Name}\" ({contacts[i].Email})");
                        Console.WriteLine($"      - \"{contacts[j].Name}\" ({contacts[j].Email})");
                        Console.WriteLine($"      Similarity: {similarity:P1}\n");
                    }
                }
            }
        }

        static void DemoAutocomplete()
        {
            FuzzyMatcher matcher = new FuzzyMatcher();
            
            string[] cities = 
            {
                "São Paulo",
                "Rio de Janeiro",
                "Belo Horizonte",
                "Brasília",
                "Salvador",
                "Fortaleza",
                "Curitiba",
                "Manaus",
                "Recife",
                "Porto Alegre"
            };

            string[] partialInputs = { "sao", "rio", "belo", "bras" };

            Console.WriteLine("   Autocomplete suggestions:\n");
            
            foreach (string input in partialInputs)
            {
                Console.WriteLine($"   User typed: \"{input}\"");
                List<SearchResult> suggestions = matcher.Search(
                    input, 
                    cities, 
                    threshold: 0.3
                );
                
                Console.WriteLine("   Suggestions:");
                foreach (SearchResult suggestion in suggestions.Take(3))
                {
                    Console.WriteLine($"     → {suggestion.Text} ({suggestion.Similarity:P0})");
                }
                Console.WriteLine();
            }
        }

        static void DemoAddressMatching()
        {
            FuzzyMatcher matcher = new FuzzyMatcher();
            
            string masterAddress = "Av Paulista 1000 Sao Paulo SP";
            
            string[] variations = 
            {
                "Avenida Paulista, 1000 - São Paulo/SP",
                "Av. Paulista 1000, Sao Paulo",
                "Paulista 1000 SP",
                "Av Paulista 1001 Sao Paulo"
            };

            Console.WriteLine($"   Master: \"{masterAddress}\"\n");
            Console.WriteLine("   Checking variations:\n");
            
            foreach (string variation in variations)
            {
                double similarity = matcher.CalculateSimilarity(
                    matcher.Normalize(masterAddress),
                    matcher.Normalize(variation)
                );
                
                string status = similarity > 0.8 ? "✓ MATCH" : "✗ NO MATCH";
                Console.WriteLine($"   {status} ({similarity:P0})");
                Console.WriteLine($"   \"{variation}\"\n");
            }
        }

        static void BenchmarkPerformance()
        {
            FuzzyMatcher matcher = new FuzzyMatcher();
            
            string query = "test query string";
            string[] candidates = new string[1000];
            Random random = new Random(42);
            
            for (int i = 0; i < candidates.Length; i++)
            {
                candidates[i] = GenerateRandomString(random, 15, 30);
            }

            Stopwatch sw = Stopwatch.StartNew();
            List<SearchResult> results = matcher.Search(query, candidates, threshold: 0.3);
            sw.Stop();

            Console.WriteLine($"   Searched {candidates.Length} candidates");
            Console.WriteLine($"   Found {results.Count} matches (threshold: 30%)");
            Console.WriteLine($"   Time: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"   Average: {(double)sw.ElapsedMilliseconds / candidates.Length:F3}ms per comparison");
        }

        static string GenerateRandomString(Random random, int minLength, int maxLength)
        {
            int length = random.Next(minLength, maxLength + 1);
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)('a' + random.Next(0, 26));
            }
            return new string(chars);
        }
    }

    class Contact
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }

    class SearchResult
    {
        public string Text { get; set; } = "";
        public double Similarity { get; set; }
    }

    /// <summary>
    /// Fuzzy string matcher using Edit Distance (Levenshtein)
    /// Optimized for production use with normalization and caching
    /// </summary>
    class FuzzyMatcher
    {
        private readonly Dictionary<(string, string), double> cache = new Dictionary<(string, string), double>();
        private int cacheHits = 0;

        public int CacheHits => cacheHits;
        public int CacheSize => cache.Count;

        public List<SearchResult> Search(string query, string[] candidates, double threshold = 0.5)
        {
            List<SearchResult> results = new List<SearchResult>();
            string normalizedQuery = Normalize(query);
            
            foreach (string candidate in candidates)
            {
                string normalizedCandidate = Normalize(candidate);
                double similarity = CalculateSimilarity(normalizedQuery, normalizedCandidate);
                
                if (similarity >= threshold)
                {
                    results.Add(new SearchResult 
                    { 
                        Text = candidate, 
                        Similarity = similarity 
                    });
                }
            }
            
            return results.OrderByDescending(r => r.Similarity).ToList();
        }

        public double CalculateSimilarity(string s1, string s2)
        {
            // Check cache first
            (string, string) key = (s1, s2);
            if (cache.ContainsKey(key))
            {
                cacheHits++;
                return cache[key];
            }

            // Early exit for very different lengths
            int lengthDiff = Math.Abs(s1.Length - s2.Length);
            if (lengthDiff > Math.Max(s1.Length, s2.Length) * 0.5)
            {
                cache[key] = 0.0;
                return 0.0;
            }

            int distance = EditDistance(s1, s2);
            int maxLength = Math.Max(s1.Length, s2.Length);
            
            double similarity = maxLength > 0 ? 1.0 - ((double)distance / maxLength) : 1.0;
            cache[key] = similarity;
            
            return similarity;
        }

        public string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            // Convert to lowercase
            text = text.ToLower();
            
            // Remove accents (simplified)
            text = text.Replace("á", "a").Replace("é", "e").Replace("í", "i")
                       .Replace("ó", "o").Replace("ú", "u").Replace("ã", "a")
                       .Replace("õ", "o").Replace("ç", "c").Replace("â", "a")
                       .Replace("ê", "e").Replace("ô", "o");
            
            // Remove special characters except spaces
            string result = "";
            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c) || c == ' ')
                {
                    result += c;
                }
            }
            
            // Normalize whitespace
            result = System.Text.RegularExpressions.Regex.Replace(result, @"\s+", " ").Trim();
            
            return result;
        }

        private int EditDistance(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            
            // Optimize for shorter string
            if (m > n)
            {
                string temp = s1;
                s1 = s2;
                s2 = temp;
                int tempLen = m;
                m = n;
                n = tempLen;
            }

            // Use 1D array for space optimization
            int[] previous = new int[m + 1];
            int[] current = new int[m + 1];

            for (int i = 0; i <= m; i++)
            {
                previous[i] = i;
            }

            for (int j = 1; j <= n; j++)
            {
                current[0] = j;
                
                for (int i = 1; i <= m; i++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        current[i] = previous[i - 1];
                    }
                    else
                    {
                        int deleteCost = previous[i] + 1;
                        int insertCost = current[i - 1] + 1;
                        int replaceCost = previous[i - 1] + 1;
                        
                        current[i] = Math.Min(deleteCost, Math.Min(insertCost, replaceCost));
                    }
                }

                // Swap arrays
                int[] temp = previous;
                previous = current;
                current = temp;
            }

            return previous[m];
        }

        public void ClearCache()
        {
            cache.Clear();
            cacheHits = 0;
        }
    }

    /// <summary>
    /// Advanced fuzzy matcher with weighted edit distance
    /// Useful for keyboard-aware matching (QWERTY layout)
    /// </summary>
    class WeightedFuzzyMatcher
    {
        public double CalculateWeightedSimilarity(string s1, string s2)
        {
            // TODO: Implement keyboard-aware distance
            // Keys close on keyboard have lower cost
            // Example: 'e' -> 'r' costs less than 'e' -> 'p'
            throw new NotImplementedException();
        }
    }
}
