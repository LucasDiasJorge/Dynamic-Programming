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
- [`01-knapsack-01/`](./01-knapsack-01/) — 0/1 Knapsack (maximização de valor com limite de peso)
- [`02-longest-increasing-subsequence/`](./02-longest-increasing-subsequence/) — LIS (subsequência crescente mais longa)
- [`03-edit-distance/`](./03-edit-distance/) — Distância de Edição (Levenshtein)
- [`04-coin-change/`](./04-coin-change/) — Troco (contagem e/ou mínimo de moedas)
- [`05-longest-common-subsequence/`](./05-longest-common-subsequence/) — LCS (subsequência comum mais longa)

Cada pasta tem:
- Enunciado e exemplos bem definidos
- Restrições e metas de complexidade
- Dicas para consultar só após tentar
- Referências curadas, com foco em C# e implementação prática

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