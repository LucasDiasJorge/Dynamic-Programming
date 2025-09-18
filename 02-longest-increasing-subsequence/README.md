# 02 — Longest Increasing Subsequence (LIS)

Enunciado: Dada uma sequência de inteiros `a[0..n-1]`, encontrar o comprimento da subsequência estritamente crescente mais longa. Opcional: reconstruir a subsequência.

## Restrições
- `1 <= n <= 2e5` (para treinar a versão `O(n log n)`)
- Valores podem ser negativos e repetidos

## Exemplos
Entrada: `a = [10, 9, 2, 5, 3, 7, 101, 18]`
Saída (comprimento): `4` (`[2, 3, 7, 18]`)

## Objetivos
- DP `O(n^2)` com `dp[i]` = LIS terminando em `i`.
- Versão `O(n log n)` via patience sorting (array `tails`).
- Reconstrução da subsequência (usando predecessores na `O(n^2)` ou índices na `O(n log n)`).

## Dicas (use somente após tentar)
- `O(n^2)`: `dp[i] = 1 + max(dp[j])` para `j < i` e `a[j] < a[i]`.
- `O(n log n)`: `tails[k]` = menor possível tail de uma LIS de tamanho `k+1`.
- Para reconstrução na `O(n log n)`, mantenha arrays de posição e predecessor.
- Cuidado com duplicatas: estritamente crescente ⇒ usar `LowerBound` apropriado.

## Referências (C# e teoria)
- `Array.BinarySearch`: https://learn.microsoft.com/dotnet/api/system.array.binarysearch
- `List<T>` e busca binária: https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1
- Comparadores e ordenação: https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1
- Explicação patience sorting (LIS): MIT 6.006; CP Handbook
