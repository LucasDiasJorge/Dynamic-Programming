# Enterprise DP Examples — Catálogo Completo

## 📊 Visão Geral

**Total de Casos**: 13 problemas enterprise  
**Implementados**: 2 ✅  
**Documentados**: 11 📚  

## 🎯 Casos por Categoria

### **Otimização de Recursos** (5 casos)
1. **Cache Eviction** — Maximizar valor em cache limitado
2. **Resource Allocation** — Distribuir CPU/memória entre tarefas
3. **Stock Trading** — Estratégias de compra/venda com restrições
4. **Discount Optimization** — Combinar cupons para máximo desconto
5. **Matrix Chain** — Ordem ótima de operações para minimizar custo

### **String & Pattern Matching** (4 casos)
6. **Fuzzy String Matching** ✅ — Busca tolerante a erros
7. **Text Diff** — Comparação de versões (track changes)
8. **Word Break** — Segmentação inteligente de strings
9. **Regex Matching** — Pattern matching avançado

### **Scheduling & Planning** (3 casos)
10. **Task Scheduling** — Critical path em projetos/pipelines
11. **Minimum Path Cost** — Routing e navegação em grids
12. **Change Making** — Otimização de transações financeiras

### **Caching & Performance** (1 caso)
13. **Fibonacci Memoization** ✅ — Padrão fundamental de caching

## 🏆 Ordem de Estudo Recomendada

### **Nível 1: Fundamentos** (Comece aqui!)
1. **Fibonacci Memoization** — Conceito base de DP
2. **Change Making** — Problema clássico e prático
3. **Minimum Path Cost** — Visual e intuitivo

### **Nível 2: Strings & Matching**
4. **Fuzzy String Matching** — Aplicação real de Edit Distance
5. **Text Diff** — LCS em contexto enterprise
6. **Word Break** — Segmentação com DP

### **Nível 3: Otimização Avançada**
7. **Cache Eviction** — Knapsack prático
8. **Resource Allocation** — Multi-dimensional optimization
9. **Stock Trading** — State machine DP

### **Nível 4: Problemas Complexos**
10. **Task Scheduling** — Graph DP com dependências
11. **Matrix Chain** — Interval DP para queries
12. **Regex Matching** — Pattern matching com wildcards
13. **Discount Optimization** — Regras de negócio complexas

## 💼 Casos por Domínio de Negócio

### **E-commerce & Retail**
- Fuzzy String Matching (search)
- Discount Optimization (pricing)
- Stock Trading (inventory)

### **DevOps & Infrastructure**
- Task Scheduling (CI/CD)
- Cache Eviction (memory)
- Resource Allocation (cloud)

### **Data & Analytics**
- Text Diff (versioning)
- Word Break (parsing)
- Matrix Chain (query optimization)

### **Finance & Trading**
- Stock Trading (algorithmic trading)
- Change Making (payments)

### **General Purpose**
- Fibonacci Memoization (caching pattern)
- Minimum Path Cost (routing)
- Regex Matching (validation)

## 🚀 Quick Start por Objetivo

**Quero aprender DP do zero:**
→ Comece por `01-fibonacci-memoization`

**Preciso resolver problema de search:**
→ Vá para `02-fuzzy-string-matching`

**Trabalho com scheduling/pipelines:**
→ Veja `09-task-scheduling`

**Otimização de custos/recursos:**
→ Explore `05-cache-eviction` e `08-resource-allocation`

**Trading/finanças:**
→ Estude `11-stock-trading` e `07-change-making`

**String processing/NLP:**
→ Veja `10-word-break` e `13-regex-matching`

**Query optimization:**
→ Aprenda `12-matrix-chain`

## 📈 Complexidade dos Problemas

| Problema | Tempo | Espaço | Dificuldade |
|----------|-------|--------|-------------|
| Fibonacci | O(n) | O(n) | ⭐ Fácil |
| Change Making | O(n×m) | O(n) | ⭐ Fácil |
| Min Path Cost | O(n×m) | O(n×m) | ⭐ Fácil |
| Fuzzy Matching | O(n×m) | O(min(n,m)) | ⭐⭐ Médio |
| Text Diff | O(n×m) | O(n×m) | ⭐⭐ Médio |
| Word Break | O(n²×m) | O(n) | ⭐⭐ Médio |
| Cache Eviction | O(n×W) | O(n×W) | ⭐⭐ Médio |
| Resource Allocation | O(n×W) | O(n×W) | ⭐⭐ Médio |
| Stock Trading | O(n×k) | O(k) | ⭐⭐⭐ Difícil |
| Task Scheduling | O(V+E) | O(V) | ⭐⭐⭐ Difícil |
| Matrix Chain | O(n³) | O(n²) | ⭐⭐⭐ Difícil |
| Regex Matching | O(n×m) | O(n×m) | ⭐⭐⭐⭐ Muito Difícil |
| Discount Optimization | O(n×W) | O(n×W) | ⭐⭐⭐ Difícil |

## 🎓 Padrões de DP Cobertos

| Padrão | Casos que Usam |
|--------|----------------|
| **Linear DP** | Fibonacci, Change Making, Stock Trading |
| **2D Grid DP** | Min Path Cost, Fuzzy Matching, Text Diff |
| **Knapsack** | Cache Eviction, Resource Allocation, Discount |
| **State Machine** | Stock Trading (múltiplos estados) |
| **Interval DP** | Matrix Chain, Regex Matching |
| **String DP** | Fuzzy Match, Text Diff, Word Break, Regex |
| **Graph DP** | Task Scheduling (DAG) |

## 📚 Recursos de Aprendizado

### Para cada caso, você encontrará:
- ✅ Explicação do problema real
- ✅ Casos de uso enterprise específicos
- ✅ Implementação em C# comentada
- ✅ Análise de complexidade
- ✅ Exemplos práticos
- ✅ Otimizações possíveis
- ✅ Referências para aprofundamento

### Status de Implementação:
- **✅ Implementado**: Código completo testado e funcional
- **📚 Documentado**: README detalhado, código em desenvolvimento
- **🚧 Planejado**: Próximos a serem implementados

---

**Última atualização**: 13 casos documentados, 2 implementados  
**Próximos**: Implementar casos 03-13 com mesma qualidade dos primeiros
