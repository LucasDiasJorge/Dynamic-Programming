# 04 — Coin Change

Existem duas variações frequentes:
- Mínimo número de moedas para totalizar `amount` (se impossível, retorne -1).
- Número de maneiras de totalizar `amount` usando moedas disponíveis (ordem não importa).

## Restrições
- `1 <= amount <= 1e5`
- `1 <= nMoedas <= 100` com denominações positivas

## Exemplos
- Min moedas: `coins=[1,2,5], amount=11` → `3` (5+5+1)
- Contagem de maneiras: `coins=[1,2,5], amount=5` → `4` ([5], [2,2,1], [2,1,1,1], [1,1,1,1,1])

## Objetivos
- Resolver a versão de mínimo número de moedas com DP 1D (`O(n*amount)`).
- Resolver a versão de contagem de maneiras com DP 1D (`O(n*amount)`), iterando moedas externamente.
- Discutir impossibilidade (∞) e reconstrução de combinação para mínimo.

## Dicas (use somente após tentar)
- Min moedas: `dp[x] = min(dp[x], dp[x-coin] + 1)` iterando `x` crescente.
- Contagem de maneiras: `ways[x] += ways[x-coin]` com loop externo nas moedas para evitar permutações.
- Use `int.MaxValue/2` como sentinela para evitar overflow ao somar 1.
- Para reconstrução, mantenha `prev[x]` com a moeda utilizada.

## Referências (C# e teoria)
- Arrays e inicialização eficiente: https://learn.microsoft.com/dotnet/standard/collections/arrays
- Overflow e limites de int: https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
- Discussão clássica de Coin Change (CP Handbook, MIT 6.006)