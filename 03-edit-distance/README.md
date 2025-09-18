# 03 — Edit Distance (Levenshtein)

Enunciado: Dadas strings `s` e `t`, calcular o custo mínimo para transformar `s` em `t` usando inserção, deleção e substituição (custo 1 cada, a não ser que especificado diferente). Opcional: reconstruir a sequência de operações.

## Restrições
- `0 <= |s|, |t| <= 5e3` (tabulação `O(|s||t|)`), testar otimização de espaço `O(min(|s|,|t|))`.
- Unicode: se necessário, considere `Rune` (System.Text) para graphemes.

## Exemplos
Entrada: `s="kitten"`, `t="sitting"`
Saída (distância): `3` (substituir k→s, substituir e→i, inserir g)

## Objetivos
- Implementar tabulação 2D clássica e versão otimizada em espaço 1D.
- Reconstruir operações (path backtracking) para obter script de edição.
- Explorar variações: custo diferente para operações, Damerau–Levenshtein (transposição).

## Dicas (use somente após tentar)
- `dp[i][j]` = custo para `s[0..i)` → `t[0..j)`.
- Transições: deleção `dp[i-1][j]+1`, inserção `dp[i][j-1]+1`, substituição `dp[i-1][j-1] + (s[i-1]!=t[j-1])`.
- Inicialização: primeira linha/coluna é distância de edição para string vazia.
- Otimização 1D: manter `prev` e `curr`; cuidado com a célula diagonal `dp[i-1][j-1]`.

## Referências (C# e teoria)
- `string`, `Span<char>`: https://learn.microsoft.com/dotnet/api/system.string
- `System.Text.Rune` para Unicode: https://learn.microsoft.com/dotnet/api/system.text.rune
- Algoritmo de Levenshtein clássico; variantes em recursos acadêmicos
