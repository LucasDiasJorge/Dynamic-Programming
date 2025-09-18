# 01 — 0/1 Knapsack

Enunciado: Dado um conjunto de `n` itens, cada um com peso `w[i]` e valor `v[i]`, e uma capacidade `W`, selecionar um subconjunto que maximize o valor total sem exceder `W`. Cada item pode ser escolhido no máximo uma vez (0/1).

## Restrições
- `1 <= n <= 2000` (ajuste conforme perfis de teste)
- `1 <= W <= 10^5` (varie para testar otimizações de espaço)
- Pesos e valores inteiros não negativos

## Exemplos
Entrada: `n=4, W=7`, `w=[6, 4, 3, 2]`, `v=[30, 20, 14, 16]`
Saída (máximo valor): `36` (itens de peso 4 e 2)

## Objetivos
- Implementar Top-Down (recursão + memo) com assinatura clara de estado.
- Implementar Bottom-Up com tabulação 2D e versão 1D (otimizada em espaço).
- Reconstruir o conjunto de itens selecionados (não só o valor máximo).

## Dicas (use somente após tentar)
- Estado clássico: `dp[i][cap]` = melhor valor usando itens até `i` com capacidade `cap`.
- Transição: `dp[i][cap] = max(dp[i-1][cap], dp[i-1][cap-w[i]] + v[i])` se `w[i] <= cap`.
- Otimização de espaço para 1D: iterar `cap` decrescendo de `W` para `w[i]`.
- Para reconstrução, guarde decisões (take/skip) ou compare `dp` para retroceder.
- Se `W` é grande e `n` pequeno, considere DP por valor (min peso para atingir valor).

## Referências (C# e teoria)
- Arrays e performance: https://learn.microsoft.com/dotnet/standard/collections/arrays
- `Span<T>`/`Memory<T>`: https://learn.microsoft.com/dotnet/standard/memory-and-spans
- Padrão de tabulação em C#: usar `int[]`/`int[,]` e loops; atenção a bounds
- Background teórico (knapsack): CLRS; MIT 6.006 slides sobre DP
