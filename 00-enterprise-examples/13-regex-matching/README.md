# Regex Pattern Matching — Advanced String Validation

## Cenário Enterprise
Implementar pattern matching avançado com wildcards e repetições. Usado em validação de dados, parsing, routing e rule engines.

## Casos de Uso Reais

### 1. **API Route Matching**
- Express-style routes: `/users/:id/posts/*`
- Wildcard patterns em URLs
- Parameter extraction e validation

### 2. **Data Validation & Sanitization**
- Validar formatos complexos (emails, telefones, CPF/CNPJ)
- Pattern-based data masking
- Schema validation com patterns

### 3. **Log Parsing & Analysis**
- Extrair informações estruturadas de logs não estruturados
- Pattern matching em arquivos de configuração
- Security: detectar padrões de ataque em logs

### 4. **Rule Engines & Business Logic**
- Matching de regras complexas
- Conditional routing baseado em patterns
- Feature flags com pattern matching

### 5. **File System Operations**
- Glob patterns: `*.txt`, `**/*.cs`
- .gitignore style patterns
- File filtering e selection

## Problema
Implementar matching de padrões com:
- `.` (dot) = corresponde a qualquer caractere único
- `*` (star) = corresponde a zero ou mais do caractere anterior
- Suporte a escapes e caracteres literais

**Exemplos:**
```
Pattern: "a.b"  String: "acb"  → Match ✓
Pattern: "a*b"  String: "aaab" → Match ✓ (* = zero ou mais 'a')
Pattern: "a*b"  String: "b"    → Match ✓ (zero 'a's)
Pattern: ".*"   String: "abc"  → Match ✓ (qualquer string)
Pattern: "a.c"  String: "abc"  → Match ✓
Pattern: "a.c"  String: "ab"   → No Match ✗
```

## Implementação - DP Approach
```csharp
public bool IsMatch(string text, string pattern)
{
    int m = text.Length;
    int n = pattern.Length;
    
    // dp[i][j] = true se text[0..i) match pattern[0..j)
    bool[,] dp = new bool[m + 1, n + 1];
    dp[0, 0] = true; // Empty matches empty
    
    // Handle patterns like a*, a*b*, etc. that can match empty string
    for (int j = 2; j <= n; j++)
    {
        if (pattern[j - 1] == '*')
        {
            dp[0, j] = dp[0, j - 2];
        }
    }
    
    for (int i = 1; i <= m; i++)
    {
        for (int j = 1; j <= n; j++)
        {
            char currentText = text[i - 1];
            char currentPattern = pattern[j - 1];
            
            if (currentPattern == '*')
            {
                char prevPattern = pattern[j - 2];
                
                // Option 1: * means zero occurrences
                dp[i, j] = dp[i, j - 2];
                
                // Option 2: * means one or more occurrences
                if (prevPattern == '.' || prevPattern == currentText)
                {
                    dp[i, j] = dp[i, j] || dp[i - 1, j];
                }
            }
            else if (currentPattern == '.' || currentPattern == currentText)
            {
                dp[i, j] = dp[i - 1, j - 1];
            }
        }
    }
    
    return dp[m, n];
}
```

## Algoritmo: 2D DP
- **DP Formula**: 
  - Se `pattern[j] == '.'` ou `pattern[j] == text[i]`: `dp[i][j] = dp[i-1][j-1]`
  - Se `pattern[j] == '*'`: considerar zero ou múltiplas ocorrências do char anterior
- **Tempo**: O(m × n) onde m = len(text), n = len(pattern)
- **Espaço**: O(m × n), otimizável para O(n)

## Variação: Glob Patterns
```csharp
public bool GlobMatch(string text, string pattern)
{
    // Suporte a:
    // * = qualquer sequência de caracteres
    // ? = qualquer caractere único
    // [abc] = qualquer caractere do conjunto
    // [!abc] = qualquer caractere que NÃO está no conjunto
    
    // Exemplo: "*.txt" matches "file.txt", "doc.txt"
    // Exemplo: "test?.cs" matches "test1.cs", "testA.cs"
}
```

## Caso Real: API Router

```csharp
public class ApiRouter
{
    private Dictionary<string, RouteHandler> routes = new();
    
    public void AddRoute(string pattern, RouteHandler handler)
    {
        // Pattern: "/api/users/:id/posts/:postId"
        // Converte para regex ou DP pattern
        routes[pattern] = handler;
    }
    
    public RouteHandler? MatchRoute(string path)
    {
        foreach (var (pattern, handler) in routes)
        {
            if (IsMatch(path, pattern))
            {
                return handler;
            }
        }
        return null;
    }
    
    public Dictionary<string, string> ExtractParams(string path, string pattern)
    {
        // Extrai valores dos parâmetros
        // "/api/users/123/posts/456" com pattern "/api/users/:id/posts/:postId"
        // Retorna: { "id": "123", "postId": "456" }
    }
}
```

## Exemplo: Data Validation Service

```csharp
public class ValidationService
{
    private Dictionary<string, string> patterns = new()
    {
        ["email"] = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}",
        ["phone_br"] = @"\(\d{2}\) \d{4,5}-\d{4}",
        ["cpf"] = @"\d{3}\.\d{3}\.\d{3}-\d{2}",
        ["url"] = @"https?://[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(/.*)?",
        ["date_iso"] = @"\d{4}-\d{2}-\d{2}"
    };
    
    public ValidationResult Validate(string input, string patternName)
    {
        if (!patterns.ContainsKey(patternName))
        {
            return ValidationResult.Invalid("Unknown pattern");
        }
        
        string pattern = patterns[patternName];
        bool isValid = IsMatch(input, pattern);
        
        return isValid 
            ? ValidationResult.Valid() 
            : ValidationResult.Invalid($"Does not match {patternName} pattern");
    }
    
    public string Sanitize(string input, string patternName)
    {
        // Remove caracteres que não fazem parte do pattern
        // Útil para limpeza de dados
    }
}
```

## Otimizações Avançadas

### 1. **NFA/DFA Compilation**
```csharp
// Compilar pattern para automaton (Finite State Machine)
// Amortizar custo se pattern é usado muitas vezes
public class CompiledPattern
{
    private StateMachine automaton;
    
    public CompiledPattern(string pattern)
    {
        this.automaton = CompileToNFA(pattern);
    }
    
    public bool Match(string text)
    {
        return automaton.Accept(text);
    }
}
```

### 2. **Caching de Resultados**
```csharp
// Cache (pattern, text) → result
// Útil se mesmos textos são validados repetidamente
private Dictionary<(string, string), bool> cache = new();
```

### 3. **Early Exit Optimization**
```csharp
// Se sabemos que não vai dar match, parar cedo
// Exemplo: pattern mais longo que text e sem wildcards
if (pattern.Length > text.Length && !pattern.Contains('*'))
    return false;
```

## Performance Comparisons

| Approach | Time | Space | Use Case |
|----------|------|-------|----------|
| Naive Recursion | O(2^(m+n)) | O(m+n) | ❌ Muito lento |
| DP (2D) | O(m×n) | O(m×n) | ✓ Padrão geral |
| DP (1D optimized) | O(m×n) | O(n) | ✓ Espaço limitado |
| Compiled NFA | O(m×n) compile + O(m) match | O(n) | ✓✓ Múltiplos matches |
| System.Text.RegularExpressions | Otimizado | Variável | ✓✓✓ Produção .NET |

## Quando NÃO usar DP

Para patterns simples, use bibliotecas nativas:
```csharp
// Em .NET, prefira Regex para produção
using System.Text.RegularExpressions;

bool isMatch = Regex.IsMatch(text, pattern);

// DP é útil para:
// 1. Aprendizado de algoritmos
// 2. Patterns customizados que Regex não suporta
// 3. Quando precisa de controle fino sobre o matching
// 4. Performance crítica com patterns muito específicos
```

## Aplicações Práticas

### 1. **Access Control Lists (ACL)**
```csharp
// Patterns para matching de recursos
// "user:*:read" matches "user:123:read"
// "admin:*:*" matches qualquer operação de admin
```

### 2. **Feature Flags**
```csharp
// Enable features baseado em patterns de user properties
// "user:premium:*" → habilita features premium
```

### 3. **Log Filtering**
```csharp
// Filtrar logs por patterns
// "[ERROR] Database*" matches erros de database
```

### 4. **Message Routing**
```csharp
// Event-driven: rotear mensagens por topic patterns
// "orders.*.created" matches "orders.payment.created"
```

## Referências
- Regular Expression Matching: https://leetcode.com/problems/regular-expression-matching/
- Regex Engines: https://swtch.com/~rsc/regexp/
- Finite Automata: https://en.wikipedia.org/wiki/Finite-state_machine
- Glob Patterns: https://en.wikipedia.org/wiki/Glob_(programming)
