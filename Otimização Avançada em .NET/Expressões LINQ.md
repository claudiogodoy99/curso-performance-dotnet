# Expressões LINQ

O LINQ (Language-Integrated Query) é uma tecnologia da Microsoft que permite realizar consultas em fontes de dados usando sintaxe semelhante à linguagem de programação C#. Ele integra consultas diretamente na linguagem, permitindo que os desenvolvedores escrevam consultas de maneira mais intuitiva e eficiente, especialmente em coleções de dados, bancos de dados, serviços da web e outros tipos de fontes de dados. Isso simplifica o processo de consulta e manipulação de dados, tornando o código mais legível e fácil de manter.

## Exemplo de Expressões LINQ

```csharp

using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // Consulta LINQ para selecionar números pares
        var numerosPares = from numero in numeros
                           where numero % 2 == 0
                           select numero;

        // Exibindo os números pares
        foreach (var numero in numerosPares)
        {
            Console.WriteLine(numero);
        }
    }
}
```

## Comparando LINQ com Loops Simples em Termos de Alocação de Memória

Ao comparar o LINQ (Consulta Integrada à Linguagem) com loops simples em termos de alocação de memória, existem algumas considerações importantes. Vamos nos aprofundar nesse tópico:

### Execução Diferida e Alocação de Memória

Os métodos LINQ, quando aplicados a coleções (como LINQ para objetos), frequentemente usam execução diferida. Isso significa que o cálculo real é adiado até que o resultado seja iterado ou materializado.

Por exemplo, o método `Select` com uma expressão lambda (`Select(x => x)`) aloca memória para a função lambda e uma referência à coleção na qual opera. No entanto, essa alocação geralmente é negligenciável em termos de complexidade e não depende diretamente do tamanho da coleção.

A memória é alocada principalmente quando o resultado da consulta é iterado (por exemplo, usando `ToList()` ou `ToArray()`).

### Exemplos de Alocação de Memória

- `Select (x => x)`: Este método não cria uma nova coleção (excluindo as informações acessórias para iteração) e geralmente é eficiente em termos de memória.
- `Select (x => new SomeType(x))`: Da mesma forma, isso não aloca uma nova coleção.

No entanto, se você usar `Select(x => x).ToList()`, ele alocará memória para a lista resultante, pois materializa a consulta em uma nova coleção.

### Regra Geral

Se um método LINQ retorna algo diferente de `IEnumerable`, pode alocar memória (por exemplo, `ToList()`, `ToArray()`). Métodos como `Count()` não necessariamente alocam memória.

Em resumo, embora o LINQ possa alocar memória durante a materialização, entender o comportamento de cada método e considerar o tipo de coleção resultante é crucial para a eficiência de memória. Tenha em mente que os benefícios do LINQ frequentemente superam as pequenas alocações de memória, especialmente ao lidar com código expressivo e conciso.

## Benchmark

[demo](../demos/demo_linq_benchmark/)

resultado:

```powershell
| Method             | Mean     | Error    | StdDev   | Median   | Gen0     | Allocated  |
|------------------- |---------:|---------:|---------:|---------:|---------:|-----------:|
| LinqExpression     | 18.58 ms | 0.363 ms | 0.459 ms | 18.48 ms | 218.7500 | 1015.64 KB |
| LoopUsandoContains | 14.07 ms | 0.312 ms | 0.900 ms | 13.77 ms |  62.5000 |  312.51 KB |
```

### LinqExpression

- Tempo Médio de Execução: 18.58 ms
- Erro Associado: 0.363 ms
- Desvio Padrão (StdDev): 0.459 ms
- Tempo Mediano: 18.48 ms
- Gen0 (Coleções de Lixo de 1ª Geração): 218.75
- Memória Alocada Média: 1015.64 KB

### LoopUsandoContains

- Tempo Médio de Execução: 14.07 ms
- Erro Associado: 0.312 ms
- Desvio Padrão (StdDev): 0.900 ms
- Tempo Mediano: 13.77 ms
- Gen0 (Coleções de Lixo de 1ª Geração): 62.5
- Memória Alocada Média: 312.51 KB

Com base nos resultados, podemos ver que o método LoopUsandoContains é mais eficiente em termos de tempo de execução e consumo de memória do que o método LinqExpression. Ele executa mais rapidamente e requer menos alocação de memória. No entanto, é importante considerar outros fatores, como a legibilidade e a manutenibilidade do código, ao decidir entre esses métodos.
