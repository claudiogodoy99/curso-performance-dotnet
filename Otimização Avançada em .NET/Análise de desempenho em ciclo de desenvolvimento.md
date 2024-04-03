# Análise de desempenho em ciclo de desenvolvimento

Muitos problemas críticos podem ser evitados ainda no estágio de desenvolvimento da aplicação, a análise de desempenho durante o ciclo de desenvolvimento é essencial para identificar e corrigir possíveis gargalos e otimizar a eficiência do sistema em desenvolvimento.

## Uso e importância do benchmark

Você suspeita que pode fazer alguma melhoria isolada no código, optimizar um `loop` aparentemente pesado, ou até provar um conceito relacionado a desempenho e consumo de recursos? O uso de um `benchmark` vai te ajudar muito no processo, no caso do .net a ferramenta mais famosa é o [BenchmarkDotNet](https://benchmarkdotnet.org/)

O BenchmarkDotNet ajuda você a transformar métodos em benchmarks, acompanhar seu desempenho e compartilhar experimentos de medição reprodutíveis. Não é mais difícil do que escrever testes unitários! Por baixo dos panos, ele realiza muita magia que garante resultados confiáveis e precisos, graças ao motor estatístico perfolizer. O BenchmarkDotNet protege você de erros comuns em benchmarks e avisa se algo está errado com o design do seu benchmark ou com as medições obtidas. Os resultados são apresentados em uma forma fácil de entender que destaca todos os fatos importantes sobre o seu experimento. O BenchmarkDotNet já foi adotado por mais de 19100 projetos no GitHub, incluindo .NET Runtime, .NET Compiler, .NET Performance e muitos outros.

### Demo

[demo-benchmark](../demos/demo-benchmark/)