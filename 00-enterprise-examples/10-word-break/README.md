# String Tokenization & Word Break — Search Optimization

## Cenário Enterprise
Determinar se uma string pode ser segmentada em palavras válidas de um dicionário. Útil em NLP, URLs, search queries e data parsing.

## Casos de Uso Reais

### 1. **Search Query Parsing**
- Separar queries sem espaços: "laptopasus" → "laptop asus"
- Autocorrect em buscas
- Sugestões de termos de busca

### 2. **URL Slug Generation**
- Converter títulos em URLs amigáveis
- Parsing de domínios compostos
- SEO optimization

### 3. **Data Cleaning & ETL**
- Parsing de dados concatenados sem delimitadores
- Limpeza de imports com formatação incorreta
- Normalização de nomes compostos

### 4. **Natural Language Processing**
- Tokenização de texto sem espaços (ex: chinês, japonês)
- Compound word splitting (alemão)
- Domain-specific terminology parsing

### 5. **Log Analysis**
- Parsing de logs sem delimitação clara
- Extração de estruturas de mensagens de erro
- Pattern matching em traces

## Problema
Dado uma string `s` e um dicionário de palavras válidas, determinar se `s` pode ser segmentado em uma sequência de palavras do dicionário.

**Exemplo:**
```
s = "leetcode"
dict = ["leet", "code"]
Output: true (pode ser "leet code")

s = "applepenapple"
dict = ["apple", "pen"]
Output: true (pode ser "apple pen apple")

s = "catsandog"
dict = ["cats", "dog", "sand", "and", "cat"]
Output: false (não há segmentação válida)
```

## Implementação
```csharp
public bool CanSegment(string text, HashSet<string> dictionary)
{
    int n = text.Length;
    bool[] dp = new bool[n + 1];
    dp[0] = true; // Empty string
    
    for (int i = 1; i <= n; i++)
    {
        for (int j = 0; j < i; j++)
        {
            // Se dp[j] é true E text[j..i) está no dicionário
            if (dp[j] && dictionary.Contains(text.Substring(j, i - j)))
            {
                dp[i] = true;
                break;
            }
        }
    }
    
    return dp[n];
}
```

## Algoritmo: Word Break DP
- **DP Formula**: `dp[i] = true` se existe `j < i` onde `dp[j] = true` e `s[j..i)` está no dict
- **Tempo**: O(n² × m) onde n = tamanho da string, m = tamanho médio das palavras
- **Espaço**: O(n)

## Otimização: Trie para Dicionário
```csharp
// Usar Trie para busca eficiente no dicionário
// Reduz complexidade de O(n² × m) para O(n² × k)
// onde k = profundidade máxima do Trie
```

## Variações Comuns

### 1. **Reconstrói a Segmentação**
```csharp
public List<string> SegmentWithWords(string text, HashSet<string> dict)
{
    // Retorna as palavras usadas: ["apple", "pen", "apple"]
}
```

### 2. **Todas as Segmentações Possíveis**
```csharp
public List<List<string>> AllSegmentations(string text, HashSet<string> dict)
{
    // Retorna todas as formas válidas de segmentar
}
```

### 3. **Segmentação com Custos**
```csharp
public int MinimumCostSegmentation(string text, Dictionary<string, int> dictWithCosts)
{
    // Minimizar custo total da segmentação
}
```

## Exemplo Real: E-commerce Search

```csharp
// Query sem espaços
string query = "samsunggalaxy";
HashSet<string> productTerms = new HashSet<string>
{
    "samsung", "galaxy", "iphone", "apple", "notebook"
};

// Resultado: "samsung galaxy"
// Melhora busca e sugestões
```

## Performance
- **Small dict (< 1000 palavras)**: ~1ms para strings de 100 chars
- **Large dict (> 100k palavras)**: Usar Trie, ~10ms
- **Very long strings**: Considerar sliding window approach

## Aplicações Práticas

### 1. **SEO URL Generation**
```
Input: "Best Laptop for Programming 2024"
Output: "best-laptop-programming-2024"
```

### 2. **Product Name Parsing**
```
Input: "iPhone15ProMax"
Output: ["iPhone", "15", "Pro", "Max"]
```

### 3. **Log Message Extraction**
```
Input: "ErrorDatabase ConnectionTimeout"
Output: ["Error", "Database", "Connection", "Timeout"]
```

## Referências
- Word Break Problem: https://leetcode.com/problems/word-break/
- Trie Data Structure: https://en.wikipedia.org/wiki/Trie
- NLP Tokenization: https://nlp.stanford.edu/IR-book/html/htmledition/tokenization-1.html
