# Enterprise Examples — Quick Start

## ✅ Projetos Implementados e Testados

### 1. Fibonacci Memoization ✅
**Pasta**: `01-fibonacci-memoization/`  
**Conceito**: Caching pattern básico  
**Casos de Uso**: Cálculos recursivos caros, séries temporais, análise financeira  
**Execução**:
```powershell
cd 01-fibonacci-memoization
dotnet run
```
**Destaque**: Demonstra speedup de 4000x com cache!

### 2. Fuzzy String Matching ✅
**Pasta**: `02-fuzzy-string-matching/`  
**Conceito**: Edit Distance (Levenshtein)  
**Casos de Uso**: Busca tolerante a erros, detecção de duplicatas, autocomplete  
**Execução**:
```powershell
cd 02-fuzzy-string-matching
dotnet run
```
**Destaque**: Detecta "João Silva" vs "Joao Silva" como duplicata (90% similarity)

### 3-8: Em Desenvolvimento 🚧
Os projetos restantes seguirão a mesma estrutura organizada:
- `03-discount-optimization/`
- `04-text-diff/`
- `05-cache-eviction/`
- `06-minimum-path-cost/`
- `07-change-making/`
- `08-resource-allocation/`

## 📂 Estrutura de Cada Projeto

```
01-fibonacci-memoization/
├── README.md              # Explicação detalhada do caso
├── Program.cs             # Implementação completa
└── *.csproj               # Projeto .NET executável
```

## 🎯 Por Onde Começar?

**Se você é novo em DP**:
1. Comece por `01-fibonacci-memoization` — conceito mais básico
2. Depois vá para `07-change-making` (quando implementado)

**Se já conhece DP**:
1. Vá direto para `02-fuzzy-string-matching` — caso real interessante
2. Explore `04-text-diff` para ver LCS aplicado

## 💡 Diferencial dessa Organização

Antes: Um arquivo monolítico com todos os exemplos  
Agora: 
- ✅ Um projeto por problema
- ✅ README dedicado por caso
- ✅ Executável independente
- ✅ Fácil de estudar isoladamente
- ✅ Fácil de expandir e modificar

## 🚀 Próximos Passos

Estou criando os 6 projetos restantes com a mesma qualidade e organização. Cada um terá:
- Cenários reais detalhados
- Múltiplos casos de teste
- Benchmarks de performance
- Comentários explicativos
- Referencias úteis

---

**Status**: 2/8 projetos completos ✅  
**Próximo**: Implementando projetos 03-08
