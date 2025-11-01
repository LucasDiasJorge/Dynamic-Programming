# Fibonacci Memoization — Caching Pattern

## Cenário Enterprise
Cálculos recursivos caros que se repetem frequentemente em aplicações de produção. Este é o padrão de caching mais básico e fundamental em DP.

## Casos de Uso Reais

### 1. **Análise Financeira**
- Cálculos de juros compostos em séries temporais
- Modelagem de crescimento exponencial
- Análise de investimentos com períodos recursivos

### 2. **Séries Temporais**
- Previsão de tendências com dependências históricas
- Cálculos de métricas cumulativas
- Agregações recursivas em dashboards

### 3. **Cache de Resultados Caros**
- Qualquer função pura com entrada → saída determinística
- Operações matemáticas complexas repetitivas
- Transformações de dados que se repetem

## Problema
Calcular Fibonacci de forma eficiente. Sem cache, `Fib(40)` faz 331.160.281 chamadas recursivas. Com memoization, apenas 39 cálculos únicos.

## Implementação
```csharp
class FibonacciService
{
    private readonly Dictionary<int, long> cache = new();
    
    public long Calculate(int n)
    {
        if (n <= 1) return n;
        
        if (cache.ContainsKey(n))
            return cache[n]; // Cache hit!
        
        long result = Calculate(n - 1) + Calculate(n - 2);
        cache[n] = result;
        return result;
    }
}
```

## Padrão Enterprise
```csharp
// Com IMemoryCache do ASP.NET Core
public class CachedCalculationService
{
    private readonly IMemoryCache cache;
    
    public async Task<Result> GetOrCalculateAsync(string key, Func<Task<Result>> calculation)
    {
        if (!cache.TryGetValue(key, out Result result))
        {
            result = await calculation();
            cache.Set(key, result, TimeSpan.FromMinutes(10));
        }
        return result;
    }
}
```

## Performance
- **Sem cache**: O(2^n) exponencial
- **Com cache**: O(n) linear
- **Espaço**: O(n)

Para `Fib(40)`:
- Primeira chamada: ~5ms (calcula e popula cache)
- Chamadas subsequentes: ~0ms (cache hit)

## Aplicação Prática
Use este padrão quando:
- ✅ Função pura (mesma entrada → mesma saída)
- ✅ Cálculo caro (> 10ms)
- ✅ Alta taxa de repetição
- ✅ Espaço de entrada limitado (não infinito)

## Referências
- IMemoryCache: https://learn.microsoft.com/aspnet/core/performance/caching/memory
- Cache-Aside Pattern: https://learn.microsoft.com/azure/architecture/patterns/cache-aside
- Distributed Caching: https://learn.microsoft.com/aspnet/core/performance/caching/distributed
