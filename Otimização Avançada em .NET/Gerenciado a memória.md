# Gerenciando a memória

Gerenciamento de recursos já fez parte do cerne da engenharia de software, entretanto, essa preocupação com consumo de CPU e memória, decaiu consideravelmente na ultima década, devido à evolução dos frameworks, abundância/redução do custo de recursos computacionais.

Em contraste com esse declínio, o mercado começou a exigir que aplicações sejam extremamente eficientes em termos de recursos computacionais novamente, com o avanço das ferramentas de orquestração de containers, cloud computing, microsservices, etc.

O trabalho de otimização de desempenho no .NET frequentemente significa remover alocações do seu código. Cada bloco de memória que você aloca eventualmente deve ser liberado. Menos alocações reduzem o tempo gasto na coleta de lixo. Isso permite um tempo de execução mais previsível ao remover as coletas de lixo de caminhos de código específicos.

## Diminuindo alocação de memória no .NET

## Memória Stack e Memória Heap no .NET

Ao declararmos uma variável em uma aplicação .NET, ela aloca memória na RAM. Esta alocação de memória inclui três componentes essenciais:

- Nome da Variável
- Tipo de Dados da Variável
- Valor da Variável

No entanto, dependendo do tipo de dados (se é um tipo de valor ou um tipo de referência), a memória pode ser alocada tanto na stack quanto no heap.

### Memória Stack

- A memória stack é responsável por acompanhar a memória em execução necessária na sua aplicação.
- Segue o princípio Last In, First Out (LIFO), semelhante a uma pilha de pratos.
- Quando uma variável é declarada, o compilador aloca memória para ela na stack.
- Variáveis locais, argumentos de função e informações de controle (como endereços de retorno) residem na stack.
- A alocação e liberação de memória na stack ocorrem apenas em uma extremidade (o topo).

### Memória Heap

- O heap fornece uma área flexível para armazenar estruturas de dados grandes e objetos com vidas dinâmicas.
- Ao contrário da stack, qualquer item no heap pode ser acessado a qualquer momento.
- Objetos criados usando a palavra-chave new (por exemplo, instâncias de classes) são armazenados no heap.
- A localização da memória do heap não rastreia a memória em execução; é usada para alocação de memória dinâmica.
- Gerenciamento manual de memória (alocação e liberação) é necessário para a memória heap.

### Exemplo Prático

Considere o trecho de código a seguir:

```csharp
class Program
{
    static void Main()
    {
        int x = 10; // Aloca memória para 'x' na stack
        int y = 20; // Memória para 'y' é empilhada em cima de 'x'

        SomeClass obj = new SomeClass(); // Cria um objeto no heap
        // ...
    }
}

class SomeClass
{
    // Definição da classe
}
```

- Afirmação 1: O compilador aloca memória para x na stack.
- Afirmação 2: Memória para y é empilhada em cima de x.
- Afirmação 3: Um objeto de SomeClass é criado. Internamente, um ponteiro é armazenado na stack, enquanto o objeto real reside no heap.

Lembre-se, a memória stack é eficiente para gerenciar variáveis locais e fluxo de controle, enquanto a memória heap oferece flexibilidade para estruturas de dados maiores e objetos dinâmicos. Compreender essas áreas de memória é crucial para escrever aplicações .NET eficientes e robustas.

### Benchmark

Com fins demonstrativos, vamos construir uma demo que vai mostra comparação entre o consumo de memória final entre as duas estratégias

Demo: [Benchmark_stack_vs_heap](../demos/demo_benchmar_stack_heap/)

Resultados:

```powershell
| Method          | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| StackAllocation |  57.55 ns |  1.319 ns |  3.806 ns |  56.95 ns |  1.00 |    0.00 | 0.1013 |     424 B |        1.00 |
| HeapAllocation  | 522.17 ns | 15.237 ns | 44.206 ns | 505.58 ns |  9.11 |    1.05 | 0.7706 |    3224 B |        7.60 |
```

# Resultado da Comparação entre StackAllocation e HeapAllocation

Este resultado é proveniente de uma comparação entre dois métodos de alocação de memória: `StackAllocation` e `HeapAllocation`. Abaixo estão as explicações para cada coluna do resultado:

- **Method:** O nome do método testado.
- **Mean:** A média do tempo de execução do método, expressa em nanossegundos (ns). Para `StackAllocation`, a média é de 57.55 ns, e para `HeapAllocation`, é de 522.17 ns.
- **Error:** O erro associado à medição do tempo médio, também em nanossegundos. Indica a variabilidade das medições.
- **StdDev:** O desvio padrão das medições de tempo, em nanossegundos. Indica a dispersão dos resultados em torno da média.
- **Median:** A mediana do tempo de execução, em nanossegundos. Representa o valor que está no meio da distribuição dos tempos de execução.
- **Ratio:** A relação entre o tempo médio de execução do `HeapAllocation` e o `StackAllocation`. Neste caso, o tempo médio do `HeapAllocation` é 9.11 vezes maior que o do `StackAllocation`.
- **RatioSD:** O desvio padrão da relação de tempo entre os métodos. Indica a variabilidade da relação entre as execuções.
- **Gen0:** A quantidade média de coleções de lixo de primeira geração (`Gen0`) realizadas durante a execução do método.
- **Allocated:** A quantidade média de memória alocada durante a execução do método, expressa em bytes (B).
- **Alloc Ratio:** A relação entre a quantidade de memória alocada pelo `HeapAllocation` e o `StackAllocation`. Neste caso, o `HeapAllocation` alocou 7.60 vezes mais memória que o `StackAllocation`.

Resumindo, o resultado mostra que o método `StackAllocation` tem um desempenho significativamente melhor em termos de tempo de execução e consumo de memória em comparação com o método `HeapAllocation`. O tempo médio de execução do `HeapAllocation` é aproximadamente 9.11 vezes maior que o do `StackAllocation`, e ele aloca 7.60 vezes mais memória. Isso indica que, em termos de eficiência de execução e uso de recursos, o método `StackAllocation` é mais vantajoso.
