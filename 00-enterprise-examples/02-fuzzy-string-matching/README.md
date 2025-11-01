# Fuzzy String Matching — Search & Autocomplete

## Cenário Enterprise
Busca tolerante a erros que permite encontrar resultados mesmo com typos, variações de escrita ou dados incompletos.

## Casos de Uso Reais

### 1. **Search Engines Internos**
- Busca de produtos em e-commerce com tolerância a typos
- Pesquisa em bases de conhecimento corporativas
- Busca em catálogos de serviços

### 2. **Autocomplete & Sugestões**
- "Você quis dizer...?" em formulários
- Sugestões de busca em tempo real
- Correção automática de input do usuário

### 3. **Detecção de Duplicatas**
- Identificar registros similares em CRM
- Deduplicação de dados em ETL
- Merge de perfis de usuários similares

### 4. **Validação de Dados**
- Matching de nomes com variações (João/Joao)
- Comparação de endereços com formatação diferente
- Validação fuzzy em importações de dados

## Problema
Dado um texto de busca e uma lista de candidatos, retornar os mais similares mesmo com erros de digitação, usando Edit Distance (Levenshtein).

## Implementação
```csharp
public List<SearchResult> Search(string query, string[] candidates, double threshold = 0.5)
{
    return candidates
        .Select(c => new SearchResult 
        { 
            Text = c, 
            Similarity = CalculateSimilarity(query, c) 
        })
        .Where(r => r.Similarity >= threshold)
        .OrderByDescending(r => r.Similarity)
        .ToList();
}

private double CalculateSimilarity(string s1, string s2)
{
    int distance = EditDistance(s1, s2);
    int maxLength = Math.Max(s1.Length, s2.Length);
    return 1.0 - ((double)distance / maxLength);
}
```

## Algoritmo: Edit Distance (Levenshtein)
Calcula o número mínimo de operações (inserção, deleção, substituição) para transformar uma string em outra.

**DP Formula:**
- Se `s1[i] == s2[j]`: `dp[i][j] = dp[i-1][j-1]`
- Senão: `dp[i][j] = 1 + min(insert, delete, replace)`

## Performance
- **Tempo**: O(m × n) onde m, n são os tamanhos das strings
- **Espaço**: O(min(m, n)) com otimização
- **Prático**: ~1ms para strings de até 100 caracteres

## Métricas de Similaridade
- **0.0 - 0.3**: Muito diferentes (geralmente irrelevante)
- **0.3 - 0.5**: Pouco similares (borderline)
- **0.5 - 0.7**: Moderadamente similares (typos comuns)
- **0.7 - 0.9**: Muito similares (variações pequenas)
- **0.9 - 1.0**: Quase idênticos ou iguais

## Otimizações Enterprise

### 1. **Pré-processamento**
```csharp
// Normalizar antes de comparar
string Normalize(string text)
{
    return text.ToLower()
               .RemoveAccents()
               .RemoveSpecialChars()
               .Trim();
}
```

### 2. **Early Exit**
```csharp
// Se diferença de tamanho é muito grande, skip
if (Math.Abs(s1.Length - s2.Length) > maxDistance)
    return double.MinValue;
```

### 3. **Caching**
```csharp
// Cache pares já calculados
Dictionary<(string, string), double> similarityCache;
```

## Aplicações Avançadas
- **Phonetic Matching**: Soundex, Metaphone (nomes que soam parecido)
- **Token-based**: Comparar palavras individuais, não string inteira
- **Weighted Distance**: Operações têm custos diferentes (teclado QWERTY)
- **Prefix Matching**: Bonus para matches no início da string

## Referências
- Edit Distance: https://en.wikipedia.org/wiki/Levenshtein_distance
- Fuzzy Search: https://www.elastic.co/guide/en/elasticsearch/reference/current/query-dsl-fuzzy-query.html
- String Metrics: https://github.com/markvanderloo/stringdist
