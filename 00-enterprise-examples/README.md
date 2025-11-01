# Enterprise DP Examples — Casos Práticos do Mundo Real

Esta pasta contém exemplos simples e práticos de Dynamic Programming que você encontra em projetos enterprise. São problemas cotidianos que podem ser resolvidos eficientemente com DP.

## 📁 Estrutura Organizada

Cada problema está em sua própria pasta com:
- `README.md` — Explicação detalhada do caso de uso
- `Program.cs` — Implementação completa em C#
- `.csproj` — Projeto executável independente

### Problemas Disponíveis

#### ✅ **Implementados e Testados**

**01-fibonacci-memoization** (Caching Pattern)
- **Caso real**: Cálculos recursivos repetitivos, séries temporais
- **Aplicação**: Cache de resultados caros, análise financeira
- **Execute**: `cd 01-fibonacci-memoization ; dotnet run`
- 🎯 Demonstra o padrão de memoization mais básico e fundamental

**02-fuzzy-string-matching** (Search & Autocomplete)
- **Caso real**: Busca tolerante a erros, autocomplete, duplicatas
- **Aplicação**: Search engines internos, validação de dados
- **Execute**: `cd 02-fuzzy-string-matching ; dotnet run`
- 🎯 Edit Distance aplicado a problemas reais de string matching

#### 📚 **Documentados (Implementação em progresso)**

**03-discount-optimization** (E-commerce Pricing)
- **Caso real**: Maximizar desconto em compras com regras complexas
- **Aplicação**: E-commerce, sistemas de pricing, cupons
- 🎯 Knapsack variante para otimização de preços

**04-text-diff** (Version Control)
- **Caso real**: Comparar versões de documentos, track changes
- **Aplicação**: CMS, document management, audit logs
- 🎯 LCS aplicado a comparação de documentos

**05-cache-eviction** (Memory Management)
- **Caso real**: Decidir quais dados manter em cache limitado
- **Aplicação**: In-memory caches, CDN, database query cache
- 🎯 Knapsack para otimização de cache

**06-minimum-path-cost** (Logistics & Routing)
- **Caso real**: Menor custo em grid (warehouse, logistics)
- **Aplicação**: Pathfinding em grids, custo de operações
- 🎯 Grid DP para problemas de navegação

**07-change-making** (Payment Systems)
- **Caso real**: Calcular troco com menor número de notas/moedas
- **Aplicação**: Sistemas de pagamento, caixas eletrônicos
- 🎯 Coin Change para transações financeiras

**08-resource-allocation** (Cloud Planning)
- **Caso real**: Distribuir recursos limitados entre tarefas
- **Aplicação**: Scheduling, cloud resource allocation, budget
- 🎯 Knapsack para alocação de recursos

#### 🆕 **Novos Casos Adicionados**

**09-task-scheduling** (CI/CD & Project Management)
- **Caso real**: Agendar tarefas com dependências, Critical Path Method
- **Aplicação**: CI/CD pipelines, project planning, ETL jobs
- 🎯 Topological Sort + DP para encontrar gargalos

**10-word-break** (NLP & Search Optimization)
- **Caso real**: Segmentar strings sem espaços, parsing de queries
- **Aplicação**: Search engines, tokenization, URL parsing
- 🎯 DP para segmentação ótima de texto

**11-stock-trading** (Algorithmic Trading)
- **Caso real**: Estratégias de compra/venda com restrições
- **Aplicação**: Trading automatizado, portfolio optimization
- 🎯 State Machine DP para múltiplas variações

**12-matrix-chain** (Query Optimization)
- **Caso real**: Ordem ótima de operações encadeadas
- **Aplicação**: SQL joins, data pipelines, expression evaluation
- 🎯 Interval DP para minimizar custo computacional

**13-regex-matching** (Pattern Matching & Validation)
- **Caso real**: Matching avançado com wildcards
- **Aplicação**: API routing, data validation, log parsing
- 🎯 2D DP para pattern matching customizado

## 🚀 Como Executar

### Executar um projeto específico:
```powershell
cd 01-fibonacci-memoization
dotnet run
```

### Executar todos em sequência:
```powershell
cd 01-fibonacci-memoization ; dotnet run ; cd ..
cd 02-fuzzy-string-matching ; dotnet run ; cd ..
cd 03-discount-optimization ; dotnet run ; cd ..
cd 04-text-diff ; dotnet run ; cd ..
cd 05-cache-eviction ; dotnet run ; cd ..
cd 06-minimum-path-cost ; dotnet run ; cd ..
cd 07-change-making ; dotnet run ; cd ..
cd 08-resource-allocation ; dotnet run ; cd ..
```

## Por que esses exemplos?

1. **Simples de entender**: Problemas familiares do dia a dia
2. **Rápido ROI**: Implementação direta, impacto imediato
3. **Escaláveis**: Funcionam bem em produção com dados reais
4. **Manuteníveis**: Código claro, fácil de debugar
5. **Performance**: Melhoria significativa vs força bruta
6. **Organizados**: Cada projeto isolado, fácil de estudar individualmente

## 📊 Comparação: Enterprise vs Academic

| Aspecto | Enterprise (00) | Academic (01-05) |
|---------|----------------|------------------|
| **Foco** | Resolver problema real | Dominar técnica |
| **Complexidade** | Simples, prática | Alta, completa |
| **Objetivo** | Entregar valor | Aprender padrões |
| **Contexto** | Business domain | Abstrato |
| **Estrutura** | Um problema/pasta | Variações múltiplas |
| **Documentação** | Caso de uso claro | Teoria profunda |

## 🎓 Ordem de Estudo Sugerida

### Iniciante em DP:
1. **01-fibonacci-memoization** — Entender memoization básico
2. **07-change-making** — Coin Change simples e direto
3. **06-minimum-path-cost** — Grid DP visual e intuitivo

### Já conhece DP:
4. **02-fuzzy-string-matching** — Edit Distance aplicado
5. **04-text-diff** — LCS em contexto real
6. **05-cache-eviction** — Knapsack prático

### Avançado:
7. **03-discount-optimization** — Knapsack com regras de negócio
8. **08-resource-allocation** — Otimização multi-dimensional

## Padrão de uso em enterprise

```csharp
// 1. Cache/Memoization (universal)
private readonly Dictionary<string, Result> cache = new();

// 2. Configuration-driven (flexível)
public class DPConfig
{
    public int MaxCacheSize { get; set; }
    public TimeSpan CacheTTL { get; set; }
}

// 3. Dependency Injection (testável)
public class DPService : IDPService
{
    private readonly ILogger logger;
    private readonly IMemoryCache cache;
}

// 4. Async when I/O bound (escalável)
public async Task<Result> CalculateAsync(Input input)
{
    // Check cache, compute, store
}
```

## Referências Enterprise

- **Caching patterns**: https://learn.microsoft.com/azure/architecture/patterns/cache-aside
- **IMemoryCache**: https://learn.microsoft.com/aspnet/core/performance/caching/memory
- **Performance**: https://learn.microsoft.com/dotnet/core/diagnostics/
- **Logging**: https://learn.microsoft.com/dotnet/core/extensions/logging

---

Execute: `dotnet run` para ver todos os exemplos em ação.
