# Impacto na escalabilidade

O uso de `async` e `await` para operações de I/O em aplicações web tem um impacto significativamente positivo, especialmente quando se trata de processamento de requisições pelo thread pool. Essas palavras-chave permitem que as aplicações web lidem com múltiplas requisições de forma eficiente, sem bloquear threads enquanto esperam que as operações de I/O sejam concluídas. Isso significa que enquanto uma operação de I/O está pendente, o thread pode ser liberado para atender outras requisições, melhorando assim a capacidade de resposta e a escalabilidade da aplicação.

Por exemplo, em uma aplicação web que recebe um grande volume de requisições, o uso de `async` e `await` permite que cada requisição seja processada de forma assíncrona. Isso evita que as requisições fiquem em fila aguardando a liberação de recursos, o que poderia causar lentidão e uma experiência de usuário insatisfatória. Além disso, ao utilizar o thread pool de forma mais eficaz, há uma redução no consumo de recursos, o que pode levar a uma economia de custos em infraestrutura.

Em resumo, a adoção de `async` e `await` para operações de I/O em aplicações web não só melhora a experiência do usuário final, como também otimiza o uso de recursos e pode contribuir para uma arquitetura de aplicação mais robusta e escalável.

## Diferença de paralelismo e concorrência

Esses conceitos estão relacionados, mas têm diferenças importantes:

**Concorrência** refere-se à capacidade de lidar com várias tarefas ou processos simultaneamente. No contexto do software, a concorrência envolve a execução de múltiplas tarefas de forma sobreposta. Isso significa que várias tarefas são iniciadas, executadas e concluídas ao longo do tempo, mas não necessariamente em paralelo. Por exemplo, um sistema operacional pode alternar rapidamente entre várias tarefas em uma única CPU, dando a ilusão de execução simultânea. A concorrência é útil para melhorar o desempenho, a capacidade de resposta e a eficiência em sistemas que precisam lidar com várias operações concorrentes, como servidores web ou aplicativos que realizam várias tarefas ao mesmo tempo.

**Paralelismo**, por outro lado, refere-se à capacidade de executar várias tarefas simultaneamente em um ambiente de hardware que suporta múltiplos processadores ou núcleos de processamento. No nível do hardware, o paralelismo é alcançado ao dividir as tarefas em unidades menores e executá-las em paralelo em diferentes processadores ou núcleos. O paralelismo permite uma execução mais rápida de tarefas, aproveitando o poder de processamento de vários recursos de hardware. É particularmente útil para operações intensivas em termos de processamento, como cálculos complexos, processamento de grandes volumes de dados e execução de algoritmos simultâneos.

**Em resumo:**

- **Concorrência** refere-se à capacidade de lidar com várias coisas ao mesmo tempo.
- **Paralelismo** refere-se à capacidade de fazer várias coisas ao mesmo tempo, aproveitando os recursos de hardware.

Em um processador com 8 núcleos, é possível realizar paralelismo em até 8 tarefas simultaneamente. Cada núcleo do processador é capaz de executar uma instrução ou tarefa separada ao mesmo tempo, o que significa que se houver 8 núcleos disponíveis, é possível realizar 8 tarefas em paralelo. No entanto, é importante ressaltar que o número máximo de tarefas que podem ser executadas em paralelo está limitado ao número de núcleos físicos do processador.

Se houver mais tarefas do que núcleos físicos disponíveis, o sistema operacional precisa alternar entre as tarefas em execução, dando a impressão de que várias tarefas estão sendo executadas simultaneamente. Isso é conhecido como multitarefa ou multithreading. No entanto, o verdadeiro paralelismo, onde cada tarefa é executada em um núcleo separado ao mesmo tempo, está restrito ao número de núcleos físicos disponíveis no processador.

## Tratamento Concorrente de Requisições no .NET

No .NET, as requisições são tratadas de forma concorrente, o que significa que múltiplas requisições podem ser processadas ao mesmo tempo. Isso é alcançado através do uso de threads e programação assíncrona.

## Threads

Quando uma requisição chega, ela é atribuída a uma thread separada. Essa thread é responsável por executar o código para aquela requisição. Se outra requisição chegar enquanto a primeira ainda está sendo processada, ela é atribuída a uma thread diferente. Isso permite que múltiplas requisições sejam processadas simultaneamente, cada uma em sua própria thread.

## Programação Assíncrona

O .NET também suporta programação assíncrona, que permite que uma thread inicie uma tarefa e então siga para algo diferente sem esperar pela conclusão da tarefa. Isso é particularmente útil para operações de I/O (como leitura de um banco de dados ou arquivo), que podem levar um tempo significativo. Enquanto a operação de I/O está em progresso, a thread pode ser liberada para lidar com outras requisições. Uma vez que a operação de I/O é concluída, a thread pode retomar de onde parou.

Essa combinação de threading e programação assíncrona permite que o .NET lide com múltiplas requisições de forma concorrente, melhorando o desempenho e escalabilidade de aplicações .NET.

## Exemplo de Método Assíncrono

```csharp
public async Task<string> GetUserDataAsync(int userId)
{
    // Operação de I/O: Obter dados do usuário de um banco de dados
    var userData = await _database.GetUserAsync(userId);

    return userData;
}
```
Neste exemplo, o método `GetUserDataAsync` inicia a tarefa `GetUserAsync` (que é uma operação de I/O) e então retorna imediatamente o controle para o chamador. A thread não é bloqueada esperando `GetUserAsync` completar, então pode ser usada para lidar com outras requisições. Uma vez que `GetUserAsync` completa, ele retoma o método `GetUserDataAsync` de onde parou. É assim que o .NET alcança a concorrência e melhora a eficiência da utilização de recursos.

Lembre-se, a chave para lidar com requisições de forma concorrente é evitar bloquear as threads tanto quanto possível, especialmente durante operações de I/O. É aqui que a programação assíncrona se destaca no .NET.

## Analogia

Imagine que você é o gerente de uma loja movimentada durante a temporada de festas. Existem muitos clientes entrando e saindo da loja, e você tem que lidar com várias tarefas ao mesmo tempo.

### Bloqueio de Thread:

Imagine que um cliente entra na loja e faz um pedido especial que levará algum tempo para ser preparado. Em uma situação de bloqueio de thread, você, como gerente, fica parado esperando que o pedido seja concluído antes de atender outros clientes ou realizar outras tarefas. Isso pode causar atrasos e ineficiências, pois você está bloqueado em uma única atividade até que ela seja concluída.

### Programação Assíncrona:

Agora, considere a mesma situação, mas com uma abordagem assíncrona. Quando o cliente faz o pedido especial, você anota o pedido e o coloca em andamento, mas em vez de esperar que o pedido seja preparado, você continua atendendo outros clientes e realizando outras tarefas na loja. Quando o pedido especial estiver pronto, você é notificado e pode então atender o cliente sem atrasos significativos. Essa abordagem assíncrona permite que você seja mais eficiente ao lidar com várias tarefas ao mesmo tempo, sem ficar bloqueado em uma única atividade.

Assim como o gerenciamento eficaz de uma loja movimentada requer a capacidade de lidar com várias tarefas simultaneamente sem ficar parado em uma única atividade, a programação assíncrona no desenvolvimento de software permite que o sistema execute várias operações sem bloquear threads desnecessariamente, melhorando a eficiência e a capacidade de resposta do programa.

## Demo

Demo para demonstrar um Thread.Sleep vs um await Task.Delay 