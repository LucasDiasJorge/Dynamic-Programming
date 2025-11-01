# Matrix Chain Multiplication — Query Optimization

## Cenário Enterprise
Determinar a ordem ótima de operações encadeadas para minimizar custo computacional. Comum em otimização de queries, pipelines de dados e renderização.

## Casos de Uso Reais

### 1. **SQL Query Optimization**
- Determinar ordem de JOINs para minimizar linhas intermediárias
- Escolher índices e estratégias de execução
- Otimizar subqueries e CTEs

### 2. **Data Pipeline Optimization**
- Ordenar transformações em ETL para minimizar dados processados
- Decidir quando materializar resultados intermediários
- Otimizar operações de MapReduce

### 3. **Expression Evaluation**
- Compiladores: ordem de avaliação de expressões
- Calcular query plans em databases
- Otimizar operações de álgebra relacional

### 4. **Image/Video Processing**
- Ordem de aplicação de filtros e transformações
- Composição de matrizes de transformação (rotação, escala, etc.)
- Rendering pipelines em graphics

### 5. **Scientific Computing**
- Multiplicação encadeada de matrizes em simulações
- Otimização de operações tensoriais (ML/AI)
- Cálculos numéricos complexos

## Problema
Dado dimensões de matrizes A₁, A₂, ..., Aₙ onde Aᵢ tem dimensão pᵢ₋₁ × pᵢ, encontrar a ordem de multiplicação que minimiza o número de operações escalares.

**Exemplo:**
```
Matrizes: A₁(10×20), A₂(20×30), A₃(30×40)
Dimensões: p = [10, 20, 30, 40]

Opção 1: (A₁ × A₂) × A₃
  - A₁ × A₂: 10×20×30 = 6,000 ops → resultado 10×30
  - Resultado × A₃: 10×30×40 = 12,000 ops
  - Total: 18,000 ops

Opção 2: A₁ × (A₂ × A₃)
  - A₂ × A₃: 20×30×40 = 24,000 ops → resultado 20×40
  - A₁ × Resultado: 10×20×40 = 8,000 ops
  - Total: 32,000 ops

Melhor: Opção 1 com 18,000 operações
```

## Implementação
```csharp
public int MinimumMultiplications(int[] dimensions)
{
    int n = dimensions.Length - 1; // número de matrizes
    int[,] dp = new int[n, n];
    
    // dp[i,j] = custo mínimo para multiplicar matrizes de i até j
    
    // Comprimento da cadeia
    for (int length = 2; length <= n; length++)
    {
        for (int i = 0; i < n - length + 1; i++)
        {
            int j = i + length - 1;
            dp[i, j] = int.MaxValue;
            
            // Tentar cada ponto de divisão
            for (int k = i; k < j; k++)
            {
                int cost = dp[i, k] + dp[k + 1, j] 
                         + dimensions[i] * dimensions[k + 1] * dimensions[j + 1];
                
                dp[i, j] = Math.Min(dp[i, j], cost);
            }
        }
    }
    
    return dp[0, n - 1];
}
```

## Algoritmo: Interval DP
- **DP Formula**: `dp[i][j] = min(dp[i][k] + dp[k+1][j] + cost(i,k,j))` para todo k entre i e j
- **Tempo**: O(n³) onde n é o número de matrizes
- **Espaço**: O(n²)
- **Pattern**: Dividir intervalo em dois subproblemas e combinar

## Reconstrução da Ordem Ótima
```csharp
public string ReconstructOrder(int[] dimensions)
{
    int n = dimensions.Length - 1;
    int[,] dp = new int[n, n];
    int[,] split = new int[n, n]; // Onde dividir para ótimo
    
    // Preencher dp e split...
    
    return BuildExpression(split, 0, n - 1);
}

private string BuildExpression(int[,] split, int i, int j)
{
    if (i == j)
        return $"A{i}";
    
    int k = split[i, j];
    string left = BuildExpression(split, i, k);
    string right = BuildExpression(split, k + 1, j);
    
    return $"({left} × {right})";
}
```

## Exemplo Real: SQL Join Optimization

```csharp
// Tabelas: Users (1M rows), Orders (5M rows), Products (100k rows)
// Query: Users JOIN Orders JOIN Products

// Opção 1: (Users ⋈ Orders) ⋈ Products
// - Users ⋈ Orders: potencialmente 5M rows intermediárias
// - Resultado ⋈ Products: caro

// Opção 2: Users ⋈ (Orders ⋈ Products)
// - Orders ⋈ Products: pré-filtrar com WHERE clauses
// - Menos linhas intermediárias
// - Mais eficiente se bem indexado

class QueryOptimizer
{
    public string OptimizeJoinOrder(Table[] tables)
    {
        // Estimar tamanho de cada join intermediário
        // Usar DP para encontrar ordem ótima
        // Retornar query plan otimizado
    }
}
```

## Variações do Problema

### 1. **Boolean Parenthesization**
```csharp
// Maximizar/minimizar resultado de expressão booleana
// Input: "T|F&T^F"
// Quantas formas de parentizar resultam em True?
```

### 2. **Optimal Binary Search Tree**
```csharp
// Construir BST que minimiza custo de busca
// Considerar frequências de acesso
```

### 3. **Polygon Triangulation**
```csharp
// Dividir polígono convexo em triângulos
// Minimizar soma dos perímetros dos triângulos
```

## Performance Considerations

### 1. **Memoization vs Tabulation**
```csharp
// Top-down com memo: útil se não explorar todo espaço
// Bottom-up: melhor cache locality, mais previsível
```

### 2. **Space Optimization**
```csharp
// Para alguns casos, pode reduzir para O(n) espaço
// Depende do pattern de acesso
```

### 3. **Parallel Computation**
```csharp
// Alguns intervalos podem ser calculados em paralelo
// Útil para n muito grande (> 1000)
```

## Caso Enterprise: Data Pipeline

```csharp
public class DataPipelineOptimizer
{
    // Operações: Filter, Map, Join, Aggregate
    // Cada operação tem custo baseado no volume de dados
    
    public PipelineExecutionPlan Optimize(Pipeline pipeline)
    {
        // 1. Modelar como matriz chain
        //    Cada operação é uma "matriz" com input/output size
        
        // 2. Estimar custo de cada ordenação
        //    Filter antes de Join reduz dados processados
        
        // 3. DP para encontrar ordem ótima
        
        // 4. Gerar execution plan
        
        return optimalPlan;
    }
}

// Exemplo:
// Pipeline: Load → Filter(condition1) → Join(table2) → Filter(condition2) → Aggregate
// Otimizado: Load → Filter(condition1 AND condition2) → Join(table2) → Aggregate
// Benefício: Menos dados no Join caro
```

## Aplicações em ML/AI

```csharp
// Tensor operations em redes neurais
// Otimizar ordem de multiplicações de tensores
// Exemplo: Attention mechanism em Transformers

public class TensorOptimizer
{
    // Query: [batch, seq_len, d_model]
    // Key: [batch, seq_len, d_model]
    // Value: [batch, seq_len, d_model]
    
    // Attention: softmax((Q × K^T) / √d) × V
    // Ordem de operações importa para performance!
}
```

## Performance Metrics
- **Small (n < 10)**: <1ms
- **Medium (n < 100)**: ~100ms
- **Large (n < 1000)**: ~10s
- **Very Large**: Considerar heurísticas (greedy) em vez de ótimo

## Referências
- Matrix Chain Multiplication: https://en.wikipedia.org/wiki/Matrix_chain_multiplication
- Query Optimization: https://www.postgresql.org/docs/current/geqo.html
- Interval DP: https://cp-algorithms.com/dynamic_programming/divide-and-conquer-dp.html
