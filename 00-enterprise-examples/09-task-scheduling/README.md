# Task Scheduling with Dependencies — Critical Path Method

## Cenário Enterprise
Agendar tarefas com dependências minimizando o tempo total de execução. Encontrar o caminho crítico (Critical Path Method - CPM) em projetos.

## Casos de Uso Reais

### 1. **CI/CD Pipeline Optimization**
- Determinar quais jobs podem rodar em paralelo
- Identificar gargalos no pipeline (critical path)
- Minimizar tempo total de build + deploy

### 2. **Project Management**
- Planejar sprints e milestones
- Identificar tarefas críticas que atrasam o projeto
- Estimar duração mínima do projeto

### 3. **ETL Job Scheduling**
- Ordenar jobs de data pipeline com dependências
- Maximizar paralelização de transformações
- Detectar ciclos (dependências circulares)

### 4. **Build System Optimization**
- Determinar ordem de compilação de módulos
- Identificar dependências transitivas
- Paralelizar builds independentes

## Problema
Dado um conjunto de tarefas com durações e dependências, encontrar:
1. Tempo mínimo para completar todas as tarefas
2. Caminho crítico (sequência de tarefas que determina o tempo total)
3. Quais tarefas podem atrasar sem impactar o prazo final (slack time)

## Implementação (Topological Sort + DP)
```csharp
public class TaskScheduler
{
    public ScheduleResult FindCriticalPath(List<Task> tasks)
    {
        // 1. Topological sort para ordenar tarefas
        List<Task> sorted = TopologicalSort(tasks);
        
        // 2. DP para calcular earliest start time
        Dictionary<string, int> earliestStart = new();
        
        foreach (Task task in sorted)
        {
            int maxPredecessorEnd = 0;
            foreach (string dep in task.Dependencies)
            {
                int depEnd = earliestStart[dep] + GetTask(dep).Duration;
                maxPredecessorEnd = Math.Max(maxPredecessorEnd, depEnd);
            }
            earliestStart[task.Id] = maxPredecessorEnd;
        }
        
        // 3. Calcular latest start time (backward pass)
        // 4. Identificar critical path (slack = 0)
    }
}
```

## Algoritmo: Longest Path in DAG
- **DP Formula**: `dp[v] = max(dp[u] + weight(u,v))` para todas as arestas `u -> v`
- **Tempo**: O(V + E) com topological sort
- **Espaço**: O(V)

## Exemplo Real: CI/CD Pipeline

```
Tasks:
- Checkout Code (1min)
- Install Dependencies (3min) [depends: Checkout]
- Lint (2min) [depends: Install]
- Unit Tests (5min) [depends: Install]
- Build (4min) [depends: Lint, Unit Tests]
- Deploy (2min) [depends: Build]

Critical Path: Checkout → Install → Unit Tests → Build → Deploy
Total Time: 1 + 3 + 5 + 4 + 2 = 15min

Lint tem slack de 3min (pode atrasar sem impactar prazo)
```

## Performance
- **Detecção de ciclos**: O(V + E) com DFS
- **Critical path**: O(V + E) single pass
- **Practical**: <1ms para 1000 tarefas

## Aplicações Avançadas
- **Resource-constrained scheduling**: Limitar tarefas paralelas
- **Crash analysis**: Quanto custa acelerar o projeto?
- **Monte Carlo simulation**: Estimar probabilidade de cumprir prazo
- **Multi-project scheduling**: Recursos compartilhados entre projetos

## Referências
- CPM: https://en.wikipedia.org/wiki/Critical_path_method
- Topological Sort: https://en.wikipedia.org/wiki/Topological_sorting
- PERT: https://en.wikipedia.org/wiki/Program_evaluation_and_review_technique
