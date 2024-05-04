# Otimização de Desempenho com Expressões Regulares em .NET

Para otimizar o desempenho de aplicativos que fazem uso extensivo de expressões regulares, é crucial entender como o mecanismo de expressões regulares compila as expressões e como as expressões regulares são armazenadas em cache. Este tópico aborda tanto a compilação quanto o caching.

## Expressões Regulares Compiladas

Por padrão, o mecanismo de expressões regulares compila uma expressão regular em uma sequência de instruções internas (esses são códigos de alto nível diferentes do Código de Linguagem Intermediária da Microsoft, ou MSIL). Quando o mecanismo executa uma expressão regular, ele interpreta os códigos internos.

Se um objeto Regex for construído com a opção RegexOptions.Compiled, ele compila a expressão regular em código MSIL explícito em vez de instruções internas de expressão regular de alto nível. Isso permite que o compilador just-in-time (JIT) do .NET converta a expressão em código de máquina nativo para um desempenho mais elevado. O custo de construção do objeto Regex pode ser maior, mas o custo de realizar correspondências com ele provavelmente será muito menor.

Uma alternativa é usar expressões regulares pré-compiladas. Você pode compilar todas as suas expressões em um DLL reutilizável usando o método CompileToAssembly. Isso evita a necessidade de compilar em tempo de execução enquanto ainda se beneficia da velocidade das expressões regulares compiladas.

## Cache de Expressões Regulares

Para melhorar o desempenho, o mecanismo de expressões regulares mantém um cache de expressões regulares compiladas em toda a aplicação. O cache armazena padrões de expressões regulares que são usados apenas em chamadas de método estático. (Padrões de expressões regulares fornecidos a métodos de instância não são armazenados em cache.) Isso evita a necessidade de reanalisar uma expressão em byte code de alto nível cada vez que ela é usada.

O número máximo de expressões regulares compiladas em cache é determinado pelo valor da propriedade estática (Shared no Visual Basic) Regex.CacheSize. Por padrão, o mecanismo de expressões regulares armazena em cache até 15 expressões regulares compiladas. Se o número de expressões regulares compiladas exceder o tamanho do cache, a expressão regular menos recentemente usada é descartada e a nova expressão regular é armazenada em cache.

Sua aplicação pode reutilizar expressões regulares de uma das duas maneiras a seguir:

1. Ao usar um método estático do objeto Regex para definir a expressão regular. Se você estiver usando um padrão de expressão regular que já foi definido por outra chamada de método estático, o mecanismo de expressões regulares tentará recuperá-lo do cache. Se não estiver disponível no cache, o mecanismo compilará a expressão regular e a adicionará ao cache.
2. Ao reutilizar um objeto Regex existente enquanto seu padrão de expressão regular for necessário.

Devido ao custo da instanciação de objetos e compilação de expressões regulares, criar e destruir rapidamente numerosos objetos Regex é um processo muito caro. Para aplicativos que usam um grande número de expressões regulares diferentes, você pode otimizar o desempenho usando chamadas a métodos estáticos de Regex e possivelmente aumentando o tamanho do cache de expressões regulares.

## Demo

[benchmark](../demos/demo_regex_benchmark/)

Resultado:

```powershell
| Method       | Mean      | Error     | StdDev     | Median    | Gen0      | Allocated  |
|------------- |----------:|----------:|-----------:|----------:|----------:|-----------:|
| Non_Compiled | 33.921 ms | 3.4565 ms | 10.0828 ms | 28.794 ms | 6468.7500 | 27085428 B |
| Compiled     |  1.600 ms | 0.0317 ms |  0.0824 ms |  1.575 ms |         - |        1 B |
| Cached       |  1.535 ms | 0.0301 ms |  0.0432 ms |  1.520 ms |         - |        1 B |
```

O benchmark compara o desempenho de três métodos distintos de uso de expressões regulares:

### Non_Compiled

- Tempo médio de execução: 33.921 ms
- Erro médio: 3.4565 ms
- Desvio padrão: 10.0828 ms
- Mediana: 28.794 ms
- Geração 0 (Gen0): 6468.7500
- Memória alocada: 27085428 B

Este método não utiliza a compilação de expressões regulares, resultando em tempos de execução mais longos e maior consumo de memória.

### Compiled

- Tempo médio de execução: 1.600 ms
- Erro médio: 0.0317 ms
- Desvio padrão: 0.0824 ms
- Mediana: 1.575 ms
- Geração 0 (Gen0): Não especificado
- Memória alocada: 1 B

Neste método, as expressões regulares são compiladas durante a execução, resultando em tempos de execução significativamente mais curtos e baixo consumo de memória.

### Cached

- Tempo médio de execução: 1.535 ms
- Erro médio: 0.0301 ms
- Desvio padrão: 0.0432 ms
- Mediana: 1.520 ms
- Geração 0 (Gen0): Não especificado
- Memória alocada: 1 B

Este método armazena em cache as expressões regulares compiladas, proporcionando tempos de execução ainda menores e consumo mínimo de memória.

Em resumo, os métodos Compiled e Cached demonstram uma melhoria significativa no desempenho em relação ao método Non_Compiled. A compilação e o armazenamento em cache das expressões regulares resultam em tempos de execução mais curtos e eficiência no uso de recursos.
