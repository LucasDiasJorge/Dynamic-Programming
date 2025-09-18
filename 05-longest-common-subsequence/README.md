# 05 — Longest Common Subsequence (LCS)

Enunciado: Dadas duas sequências `s` e `t`, encontrar o comprimento da subsequência comum mais longa. Opcional: reconstruir a subsequência.

## Restrições
- `0 <= |s|, |t| <= 5e3` (tabulação `O(|s||t|)`), avaliar otimização de espaço `O(min(|s|,|t|))` quando necessário.

## Exemplos
Entrada: `s="abcde"`, `t="ace"`
Saída (comprimento): `3` (`"ace"`)

## Objetivos
- Implementar tabulação 2D clássica e versão 1D por linhas.
- Reconstruir a subsequência com backtracking ou guardando decisões.
- Discutir relação com Edit Distance (operações sem custo de substituição quando iguais).

## Dicas (use somente após tentar)
- `dp[i][j]` = LCS de `s[0..i)` e `t[0..j)`.
- Se `s[i-1] == t[j-1]`: `dp[i][j] = dp[i-1][j-1] + 1`; senão `max(dp[i-1][j], dp[i][j-1])`.
- Para 1D, itere `j` de trás para frente para preservar a diagonal anterior.
- Para reconstrução, além do valor, guarde `from(i,j)` ou compare vizinhos.

## Referências (C# e teoria)
- Strings e slicing: https://learn.microsoft.com/dotnet/api/system.string
- Matrizes multidimensionais e jagged arrays: https://learn.microsoft.com/dotnet/csharp/programming-guide/arrays/
- LCS em materiais clássicos (CLRS, MIT 6.006)
