# Dynamic Programming — Guia de Prática

Este repositório é um campo de treino focado em Dynamic Programming (DP) com 5 problemas clássicos, cada um com um README contendo: enunciado, restrições, exemplos, objetivos, dicas (para consultar após tentar sozinho) e referências de documentação em C#.

## O que é Dynamic Programming?
Dynamic Programming é uma técnica para resolver problemas complexos quebrando-os em subproblemas que se repetem e armazenando os resultados intermediários (memoização/tabulação). A essência é identificar subestrutura ótima e sobreposição de subproblemas. Em termos práticos, DP transforma soluções exponenciais em soluções polinomiais ao evitar recomputações.

Características típicas:
- Subestrutura ótima: a solução ótima pode ser composta por soluções ótimas de subproblemas.
- Sobreposição de subproblemas: os subproblemas se repetem muitas vezes.
- Estados e transições: definimos um estado (parâmetros) e como ele transita para outros estados.
- Estratégias: Top-Down (recursão + memo) ou Bottom-Up (tabulação iterativa).

## Por que praticar?
- Consolidar raciocínio de modelagem de estados e recursões.
- Aprender a desenhar invariantes, dimensões e limites de memória/tempo.
- Ganhar repertório de padrões (knapsack, subsequências, caminhos mínimos, decomposição de strings, etc.).
- Aplicar em entrevistas e em problemas reais (alocação, scheduling, parsing, bioinformática, otimização combinatória).

## Como usar este repositório
1. Acesse uma pasta de problema e leia o enunciado e as restrições.
2. Tente resolver sozinho (idealmente em 30–60 min) antes de abrir a seção de dicas.
3. Compare com as abordagens sugeridas e estude as referências.
4. Implemente múltiplas variações (Top-Down e Bottom-Up, otimizações de espaço, reconstrução da resposta).

## Estrutura dos problemas

### 🌟 Enterprise Examples (Casos Práticos) — **13 CASOS**
- [`00-enterprise-examples/`](./00-enterprise-examples/) — **Pasta principal com subprojetos organizados**
  
  **✅ Implementados e Testados:**
  - [`01-fibonacci-memoization/`](./00-enterprise-examples/01-fibonacci-memoization/) — Caching pattern básico
  - [`02-fuzzy-string-matching/`](./00-enterprise-examples/02-fuzzy-string-matching/) — Search tolerante a erros
  
  **� Documentados (READMEs completos):**
  - `03-discount-optimization/` — E-commerce pricing otimizado
  - `04-text-diff/` — Version control e track changes
  - `05-cache-eviction/` — Memory management inteligente
  - `06-minimum-path-cost/` — Logistics e routing
  - `07-change-making/` — Payment systems
  - `08-resource-allocation/` — Cloud/budget planning
  
  **🆕 Novos Casos Adicionados:**
  - `09-task-scheduling/` — CI/CD pipelines, Critical Path Method
  - `10-word-break/` — NLP, tokenization, search optimization
  - `11-stock-trading/` — Algorithmic trading, portfolio optimization
  - `12-matrix-chain/` — SQL query optimization, data pipelines
  - `13-regex-matching/` — Pattern matching, API routing, validation
  
  - **👉 13 casos reais de DP em produção!**
  - **📁 Estrutura organizada: Um problema por pasta**
  - **📖 Documentação detalhada: Cada caso com README completo**
  - **🎯 [Ver catálogo completo](./00-enterprise-examples/CATALOG.md)**

### 📚 Classic Problems (Estudo Profundo)
- [`01-knapsack-01/`](./01-knapsack-01/) — 0/1 Knapsack (maximização de valor com limite de peso)
  - ✅ Implementação completa em C# com Top-Down, Bottom-Up 2D/1D, reconstrução
- [`02-longest-increasing-subsequence/`](./02-longest-increasing-subsequence/) — LIS (subsequência crescente mais longa)
  - ✅ Implementação completa com O(n²) DP e O(n log n) Patience Sorting
- [`03-edit-distance/`](./03-edit-distance/) — Distância de Edição (Levenshtein)
  - ✅ Implementação 2D e 1D otimizada com reconstrução de operações
- [`04-coin-change/`](./04-coin-change/) — Troco (contagem e/ou mínimo de moedas)
  - ✅ Implementação de mínimo de moedas e contagem de combinações
- [`05-longest-common-subsequence/`](./05-longest-common-subsequence/) — LCS (subsequência comum mais longa)
  - ✅ Implementação 2D e 1D com reconstrução e diff utility

Cada pasta tem:
- Enunciado e exemplos bem definidos
- Restrições e metas de complexidade
- Dicas para consultar só após tentar
- Referências curadas, com foco em C# e implementação prática
- **Projeto C# completo e testado** com múltiplas abordagens, reconstrução de soluções e benchmarks

## Como executar os projetos

### Início Rápido (Enterprise Examples)
```powershell
# Fibonacci Memoization (Caching Pattern)
cd 00-enterprise-examples/01-fibonacci-memoization
dotnet run

# Fuzzy String Matching (Search & Autocomplete)
cd ../02-fuzzy-string-matching
dotnet run

# ... mais projetos em desenvolvimento
```

### Problemas Clássicos
```powershell
# Navegue até a pasta do projeto
cd 01-knapsack-01

# Execute o projeto
dotnet run

# Ou execute todos os projetos em sequência
cd 00-enterprise-examples ; dotnet run
cd ../01-knapsack-01 ; dotnet run
cd ../02-longest-increasing-subsequence ; dotnet run
cd ../03-edit-distance ; dotnet run
cd ../04-coin-change ; dotnet run
cd ../05-longest-common-subsequence ; dotnet run
```

## Referências gerais (C# e algoritmos)
- Documentação C# e .NET:
	- MSDN Docs: https://learn.microsoft.com/dotnet/csharp/
	- Coleções e desempenho: https://learn.microsoft.com/dotnet/standard/collections/
	- Span/Memory e performance: https://learn.microsoft.com/dotnet/standard/memory-and-spans
- Análise de complexidade e técnicas:
	- MIT OCW 6.006 (Algoritmos): https://ocw.mit.edu/courses/6-006-introduction-to-algorithms-fall-2011/
	- CLRS (Introduction to Algorithms) — capítulos de DP
- Patterns de DP (visão prática):
	- Top-Down vs Bottom-Up em C#: escolha conforme stack depth, clareza e perfil do problema.
	- Otimização de espaço: reduzir dimensões quando a transição depende apenas de linhas/colunas anteriores.
	- Reconstrução de resposta: manter predecessor/decisão para extrair solução, não apenas o valor.

Boa prática e bons estudos!