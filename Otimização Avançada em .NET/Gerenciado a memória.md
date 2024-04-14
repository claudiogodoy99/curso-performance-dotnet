# Gerenciando a memória

Gerenciamento de recursos já fez parte do cerne da engenharia de software, entretanto, essa preocupação com consumo de CPU e memória, decaiu consideravelmente na ultima década, devido à evolução dos frameworks, abundância/redução do custo de recursos computacionais.

Em contraste com esse declínio, o mercado começou a exigir que aplicações sejam extremamente eficientes em termos de recursos computacionais novamente, com o avanço das ferramentas de orquestração de containers, cloud computing, microsservices, etc.

O trabalho de otimização de desempenho no .NET frequentemente significa remover alocações do seu código. Cada bloco de memória que você aloca eventualmente deve ser liberado. Menos alocações reduzem o tempo gasto na coleta de lixo. Isso permite um tempo de execução mais previsível ao remover as coletas de lixo de caminhos de código específicos.

- [Utilização de structs e refs](./Utilização%20de%20structs%20e%20refs.md)
- [Como manipular `Strings`](./Como%20manipular%20strings.md) 
- [Como otimizar expressões regulares `Regex`](./Como%20otimizar%20expressões%20regulares%20Regex.md)
- [Expressões `LINQ`](./Expressões%20LINQ.md)
- [Como identificar `MemoryLeaks`](./Como%20identificar%20MemoryLeaks.md) utilizando Contadores de performance e as ferramentas de diagnóstico do Visual Studio