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

#### Resultado da Comparação entre StackAllocation e HeapAllocation

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

### Uso de struct para diminuir as alocações na Heap

Structs podem ajudar a reduzir as alocações de objetos no heap em .NET.

#### Tipos de Valor vs. Tipos de Referência

No .NET, existem duas categorias principais de tipos: tipos de valor e tipos de referência.

- Tipos de valor, incluindo structs, são armazenados diretamente na pilha ou dentro de outros tipos de valor. Eles são leves e não envolvem alocações no heap.
- Tipos de referência, como classes, são alocados no heap e gerenciados pelo coletor de lixo (GC).

#### Structs e Alocação na Pilha

Quando você cria uma instância de uma struct, a memória é alocada na pilha.

- Essa alocação na pilha é mais eficiente do que a alocação no heap porque evita a sobrecarga do gerenciamento pelo GC.
- Structs são úteis para representar estruturas de dados pequenas e leves.

#### Usando Structs para Reduzir Alocações no Heap

Considere usar structs em cenários onde:

- Você precisa armazenar uma pequena quantidade de dados (por exemplo, coordenadas, cores, flags).
- Você quer evitar a sobrecarga da alocação no heap.
- Você deseja melhorar o desempenho minimizando a pressão sobre o GC.

#### Exemplos de quando usar structs

- Coordenadas de ponto (x, y)
- Cor (valores RGB)
- DateTime (se a precisão além de milissegundos não for necessária)

#### Avisos e Considerações

Tenha cuidado ao usar structs:

- Eles são tipos de valor, então copiá-los pode ser caro.
- Evite criar structs grandes, pois elas podem levar a um uso ineficiente da memória.
- Structs são mais adequadas para dados pequenos e imutáveis.

#### Benchmark struct vs class

[benchmark](../demos/demo_benchmark_struct_vs_class/)

Resultado:

```powershell
| Method          | Mean      | Error    | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |----------:|---------:|----------:|------:|--------:|-------:|----------:|------------:|
| StackAllocation |  67.17 ns | 2.290 ns |  6.717 ns |  1.00 |    0.00 | 0.1013 |     424 B |        1.00 |
| HeapAllocation  | 501.16 ns | 9.935 ns | 22.015 ns |  7.92 |    0.73 | 0.7706 |    3224 B |        7.60 |
```

- **Method**: O nome do método testado.
- **Mean**: A média do tempo de execução do método, expressa em nanossegundos (ns). Para `StackAllocation`, a média é de 67.17 ns, e para `HeapAllocation`, é de 501.16 ns.
- **Error**: O erro associado à medição do tempo médio, também em nanossegundos. Indica a variabilidade das medições.
- **StdDev**: O desvio padrão das medições de tempo, em nanossegundos. Indica a dispersão dos resultados em torno da média.
- **Ratio**: A relação entre o tempo médio de execução do `HeapAllocation` e o `StackAllocation`. Neste caso, o tempo médio do `HeapAllocation` é 7.92 vezes maior que o do `StackAllocation`.
- **RatioSD**: O desvio padrão da relação de tempo entre os métodos. Indica a variabilidade da relação entre as execuções.
- **Gen0**: A quantidade média de coleções de lixo de primeira geração (Gen0) realizadas durante a execução do método.
- **Allocated**: A quantidade média de memória alocada durante a execução do método, expressa em bytes (B).
- **Alloc Ratio**: A relação entre a quantidade de memória alocada pelo `HeapAllocation` e o `StackAllocation`. Neste caso, o `HeapAllocation` alocou 7.60 vezes mais memória que o `StackAllocation`.

Resumindo, o resultado indica que o método `StackAllocation` tem um desempenho significativamente melhor em termos de tempo de execução e consumo de memória em comparação com o método `HeapAllocation`. O tempo médio de execução do `HeapAllocation` é aproximadamente 7.92 vezes maior que o do `StackAllocation`, e ele aloca 7.60 vezes mais memória. Isso demonstra a eficiência do `StackAllocation` em relação ao `HeapAllocation`.

### Entendo os values types

Os tipos de valor (value types) são um tipo de dados em C# que armazenam diretamente seus valores, em oposição aos tipos de referência que armazenam referências aos dados. Isso significa que quando você declara uma variável de um tipo de valor e atribui um valor a ela, o valor é armazenado diretamente na memória onde a variável está alocada.

Por que os tipos de valor podem afetar o consumo de memória? Quando você passa uma struct (que é um tipo de valor) como argumento para um método, a estrutura completa é copiada. Isso pode resultar em um consumo maior de memória, especialmente se a struct for grande ou complexa.

Por exemplo, se você tiver uma struct com muitos campos ou membros, como uma representação de dados complexa, e passá-la como argumento para um método, toda a struct será copiada na memória antes de ser passada para o método. Isso pode consumir mais memória em comparação com tipos de referência, onde apenas um ponteiro para os dados é copiado.

Portanto, ao lidar com tipos de valor em C#, é importante considerar o impacto no consumo de memória, especialmente ao passá-los como argumentos para métodos ou ao armazená-los em coleções grandes. Em algumas situações, pode ser mais eficiente usar tipos de referência, como classes, para evitar a cópia completa dos dados na memória.

#### Benchmark class vs estrutura

[benchmark](../demos/demo_benchmark_struct_vs_class_coping_memory/)

Resultado:

```powershell
| Method          | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------- |---------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| StackAllocation | 6.412 us | 0.2172 us | 0.6197 us |  1.00 |    0.00 | 0.0992 |     424 B |        1.00 |
| HeapAllocation  | 3.455 us | 0.0664 us | 0.0864 us |  0.56 |    0.07 | 0.7706 |    3224 B |        7.60 |
```

- **Mean (Média):** O tempo médio de execução para StackAllocation é de 6.412 us, enquanto para HeapAllocation é de 3.455 us. Isso indica que StackAllocation é mais lento em comparação com HeapAllocation.

- **Ratio (Razão):** O tempo médio de execução do HeapAllocation em relação ao StackAllocation é de 0.56, o que sugere que HeapAllocation é aproximadamente 44% mais rápido que StackAllocation.

- **Gen0:** A quantidade média de coleções de lixo de primeira geração (Gen0) para StackAllocation é de 0.0992, e para HeapAllocation é de 0.7706. Isso indica que HeapAllocation gera mais coleções de lixo de primeira geração do que StackAllocation durante a execução dos métodos.

- **Allocated (Alocado):** A quantidade média de memória alocada para StackAllocation é de 424 bytes, enquanto para HeapAllocation é de 3224 bytes. Isso mostra que HeapAllocation alocou significativamente mais memória do que StackAllocation.

- **Alloc Ratio (Razão de Alocação):** A relação entre a quantidade de memória alocada pelo HeapAllocation e o StackAllocation é de 7.60, indicando que HeapAllocation alocou 7.60 vezes mais memória que StackAllocation.

Esses resultados sugerem que, embora HeapAllocation seja mais rápido em termos de tempo de execução, ele consome mais memória do que StackAllocation. Portanto, a escolha entre os métodos deve considerar não apenas o desempenho, mas também o consumo de memória, dependendo dos requisitos específicos da aplicação.

### Como resolver o problema de cópia de objetos

A utilização das `structs`, embora mais eficiente à primeira vista, quando passadas por como parâmetro ou atribuídas à outras variáveis, os dados são copiados, e isso introduz dois problemas:

- O volume de alocações cresce significativamente.
- A complexidade de lidar com esse tipo de alocação é muito alta, dado ao fato que `C#` é uma linguagem orientada a objetos.

Vamos nos aprofundar em como as palavras-chave ref e in funcionam no .NET. Essas palavras-chave desempenham um papel crucial na passagem de parâmetros para métodos, permitindo criar melhores abstrações e aprimorar a legibilidade e manutenção do código.

- A palavra-chave `ref`:
Quando você usa a palavra-chave ref, passa um objeto ou variável por referência. Ao contrário da passagem usual de parâmetros do método (onde o valor é copiado para o método), a palavra-chave ref permite que o método acesse diretamente a variável original.
Qualquer alteração feita na variável dentro do método é refletida na variável original fora do método.
Exemplo:

```csharp
void ModifyValue(ref int number)
{
    number += 10;
}

// Uso
int meuNumero = 5;
ModifyValue(ref meuNumero);
// Agora meuNumero é 15
```

- A palavra-chave `in`:
A palavra-chave in permite passar parâmetros por referência, mas com algumas diferenças:

- Você pode usar in apenas para parâmetros de entrada (somente leitura).
- Ela impede que o método chamado modifique os valores dos parâmetros.
- É útil quando você deseja evitar modificações acidentais.

Exemplo:

```csharp
void DisplayValue(in int number)
{
    Console.WriteLine($"O valor é: {number}");
}

// Uso
int meuNumero = 42;
DisplayValue(in meuNumero);
// O valor é: 42
// Tentar modificar meuNumero dentro de DisplayValue resultaria em um erro de compilação
```