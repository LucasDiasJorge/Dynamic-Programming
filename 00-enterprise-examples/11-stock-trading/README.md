# Stock Trading Optimization — Maximum Profit

## Cenário Enterprise
Determinar a estratégia ótima de compra/venda de ativos para maximizar lucro, respeitando restrições de transações e cooldown periods.

## Casos de Uso Reais

### 1. **Automated Trading Systems**
- Algoritmos de trading com limites de transações
- Backtest de estratégias
- Risk management com cooldown obrigatório

### 2. **Portfolio Optimization**
- Rebalanceamento de carteira
- Timing de entrada/saída de posições
- Tax loss harvesting (considerar custos)

### 3. **Resource Purchasing**
- Compra de recursos na nuvem (spot instances)
- Hedge de commodities
- Contratos futuros de energia

### 4. **Pricing Strategy**
- Quando ajustar preços para maximizar receita
- Timing de promoções
- Dynamic pricing em marketplaces

## Problema - Variações Comuns

### **Variação 1: Máximo 1 transação (compra + venda)**
```
Input: prices = [7,1,5,3,6,4]
Output: 5 (compra em 1, vende em 6)
```

### **Variação 2: Transações ilimitadas**
```
Input: prices = [7,1,5,3,6,4]
Output: 7 (compra em 1, vende em 5 = +4; compra em 3, vende em 6 = +3)
```

### **Variação 3: Máximo k transações**
```
Input: prices = [3,2,6,5,0,3], k=2
Output: 7 (transação 1: compra em 2, vende em 6 = +4; transação 2: compra em 0, vende em 3 = +3)
```

### **Variação 4: Com cooldown (obrigatório esperar 1 dia após vender)**
```
Input: prices = [1,2,3,0,2]
Output: 3 (compra em 1, vende em 3 = +2; cooldown; compra em 0, vende em 2 = +2)
```

### **Variação 5: Com transaction fee**
```
Input: prices = [1,3,2,8,4,9], fee=2
Output: 8 (compra em 1, vende em 8 pagando fee 2 = +5; compra em 4, vende em 9 pagando fee 2 = +3)
```

## Implementação - State Machine DP

```csharp
// Estados possíveis em cada dia:
// - hold: tenho ações
// - sold: acabei de vender (cooldown se aplicável)
// - rest: sem ações, sem cooldown

public int MaxProfitWithCooldown(int[] prices)
{
    int n = prices.Length;
    if (n <= 1) return 0;
    
    int[] hold = new int[n];    // Máximo lucro segurando ação no dia i
    int[] sold = new int[n];    // Máximo lucro vendendo no dia i
    int[] rest = new int[n];    // Máximo lucro em repouso no dia i
    
    hold[0] = -prices[0];
    sold[0] = 0;
    rest[0] = 0;
    
    for (int i = 1; i < n; i++)
    {
        hold[i] = Math.Max(hold[i-1], rest[i-1] - prices[i]);
        sold[i] = hold[i-1] + prices[i];
        rest[i] = Math.Max(rest[i-1], sold[i-1]);
    }
    
    return Math.Max(sold[n-1], rest[n-1]);
}
```

## Algoritmo: State Machine DP
- **Estados**: Modelar cada estado possível (hold, sold, rest, etc.)
- **Transições**: Definir como mudar de um estado para outro
- **DP Formula**: `dp[i][state] = max(todas as transições válidas)`
- **Tempo**: O(n × k) onde k é número de transações permitidas
- **Espaço**: O(n × k), otimizável para O(k)

## Variação com K Transações
```csharp
public int MaxProfitKTransactions(int[] prices, int k)
{
    int n = prices.Length;
    if (k >= n / 2)
    {
        // k grande o suficiente = transações ilimitadas
        return MaxProfitUnlimited(prices);
    }
    
    int[,] buy = new int[n, k + 1];
    int[,] sell = new int[n, k + 1];
    
    for (int i = 0; i < n; i++)
    {
        for (int j = 1; j <= k; j++)
        {
            if (i == 0)
            {
                buy[i, j] = -prices[0];
                sell[i, j] = 0;
            }
            else
            {
                buy[i, j] = Math.Max(buy[i-1, j], sell[i-1, j-1] - prices[i]);
                sell[i, j] = Math.Max(sell[i-1, j], buy[i-1, j] + prices[i]);
            }
        }
    }
    
    return sell[n-1, k];
}
```

## Otimização: Space-Optimized
```csharp
// Reduzir espaço de O(n) para O(1)
// Manter apenas o estado anterior
public int MaxProfitOptimized(int[] prices)
{
    int hold = -prices[0];
    int sold = 0;
    int rest = 0;
    
    for (int i = 1; i < prices.Length; i++)
    {
        int prevSold = sold;
        sold = hold + prices[i];
        hold = Math.Max(hold, rest - prices[i]);
        rest = Math.Max(rest, prevSold);
    }
    
    return Math.Max(sold, rest);
}
```

## Casos Enterprise Reais

### 1. **Cloud Resource Optimization**
```csharp
// Preços de spot instances variam ao longo do dia
// Decidir quando comprar/liberar recursos
int[] spotPrices = GetHourlySpotPrices();
int maxProfit = OptimizeResourceUsage(spotPrices, maxTransactions: 3);
```

### 2. **Energy Trading**
```csharp
// Comprar energia em horários de baixa demanda
// Vender em picos de demanda
decimal[] energyPrices = GetDailyEnergyPrices();
decimal profit = OptimizeEnergyTrading(energyPrices, storageFee: 0.05m);
```

### 3. **Inventory Management**
```csharp
// Decidir quando comprar/vender estoque
// Considerar custos de armazenagem
int[] productPrices = GetHistoricalPrices();
int optimalProfit = OptimizeInventory(productPrices, holdingCost: 2);
```

## Performance
- **Simple (1 transaction)**: O(n) tempo, O(1) espaço
- **Unlimited transactions**: O(n) tempo, O(1) espaço
- **K transactions**: O(n × k) tempo, O(k) espaço com otimização
- **With cooldown**: O(n) tempo, O(1) espaço
- **Practical**: <1ms para 10k days, <10ms para 100k days

## Estratégias Avançadas

### 1. **Multi-Asset Portfolio**
```csharp
// Otimizar compra/venda de múltiplos ativos
// Correlação entre ativos
// Limites de exposição por ativo
```

### 2. **Risk-Adjusted Returns**
```csharp
// Não apenas maximizar lucro, mas considerar risco
// Sharpe ratio, volatility constraints
```

### 3. **Market Impact**
```csharp
// Grandes ordens movem o mercado
// Custo de slippage
// Optimal execution (VWAP, TWAP)
```

## Referências
- Stock Trading Problems: https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
- State Machine DP: https://en.wikipedia.org/wiki/Finite-state_machine
- Algorithmic Trading: https://www.quantstart.com/articles/
- Portfolio Optimization: https://en.wikipedia.org/wiki/Portfolio_optimization
