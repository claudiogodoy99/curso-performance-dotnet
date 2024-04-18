# Async await: Promise model no .NET

Future, promise, delay e deferred são conceitos em programação concorrente que ajudam a gerenciar resultados que ainda não estão disponíveis. Eles são como promessas de que um resultado será fornecido no futuro. Aqui está uma explicação simplificada:

- Future: É como um recibo que você recebe quando faz um pedido. Ele promete que, eventualmente, você receberá o produto (ou resultado) que pediu.
- Promise: É o compromisso de que algo será feito. Pense nisso como o cozinheiro que promete que sua comida será preparada.
- Delay: É o tempo de espera antes que sua promessa seja cumprida. Como o tempo que você espera pela comida depois de fazer o pedido.
- Deferred: É um pouco como adiar a entrega do seu pedido. Você sabe que vai chegar, mas pode não ser imediatamente.
Esses termos ajudam os programadores a escrever código que pode esperar por resultados sem bloquear todo o programa. Eles são especialmente úteis em operações que podem levar algum tempo, como carregar dados da internet ou realizar um cálculo complexo.

O conceito de promise foi introduzido por Daniel P. Friedman e David Wise em 1976, e Peter Hibbard chamou-o de “eventual”. O termo future foi introduzido por Henry Baker e Carl Hewitt em 1977. Embora os termos possam ser usados de forma intercambiável, eles têm nuances específicas em diferentes contextos de programação. Por exemplo, um future é geralmente uma visão somente leitura de um resultado, enquanto uma promise é um contêiner que pode ser preenchido uma única vez com um valor que define o resultado do future.

No lado do C#, o compilador transforma seu código em uma máquina de estados que acompanha aspectos como ceder a execução quando um await é alcançado e retomar a execução quando um trabalho em segundo plano é concluído.

Para aqueles inclinados teoricamente, isso é uma implementação do Modelo de Promessa de assincronia. Complementando com a explicação anterior, em C#, quando você usa await, está dizendo ao programa para esperar pelo resultado de uma operação sem bloquear o restante do código. Isso é útil quando você tem tarefas que podem demorar um pouco para serem concluídas, como acessar a web ou ler um arquivo grande.

Assim, enquanto o future é o recibo que promete um resultado futuro, e a promise é o compromisso de que esse resultado será entregue, em C# você tem uma máquina de estados gerenciando essas promessas e futuros de forma eficiente, permitindo que seu programa continue executando outras tarefas enquanto espera por resultados.

## programação assíncrona usando as Tasks

O cerne da programação assíncrona são os objetos `Task` e `Task<T>`, que modelam operações assíncronas. Eles são suportados pelas palavras-chave `async` e `await`. O modelo é bastante simples na maioria dos casos:

- Para código ligado a I/O, você aguarda uma operação que retorna um `Task` ou `Task<T>` dentro de um método `async`.
- Para código ligado a CPU, você aguarda uma operação que é iniciada em uma thread de segundo plano com o método `Task.Run`.

A palavra-chave `await` é onde a mágica acontece. Ela cede o controle ao chamador do método que executou o `await` e, por fim, permite que uma interface de usuário seja responsiva ou um serviço seja elástico. Embora existam maneiras de abordar o código assíncrono além de `async` e `await`, este artigo se concentra nos construtos em nível de linguagem.

## Reconhecer Trabalho Ligado à CPU e Ligado à I/O

Os dois primeiros exemplos deste guia mostraram como você pode usar `async` e `await` para trabalho ligado à I/O e ligado à CPU. É fundamental que você possa identificar quando uma tarefa que você precisa realizar é ligada à I/O ou ligada à CPU, pois isso pode afetar significativamente o desempenho do seu código e potencialmente levar ao uso inadequado de certos construtos.

Aqui estão duas perguntas que você deve fazer antes de escrever qualquer código:

1. Seu código estará "esperando" por algo, como dados de um banco de dados?
   - Se a resposta for "sim", então seu trabalho é ligado à I/O.

2. Seu código estará realizando uma computação cara?
   - Se a resposta for "sim", então seu trabalho é ligado à CPU.

Se o trabalho que você tem é ligado à I/O, use `async` e `await` sem `Task.Run`. Você não deve usar a Biblioteca de Tarefas Paralelas (`Task Parallel Library`).

Se o trabalho que você tem é ligado à CPU e você se importa com a responsividade, use `async` e `await`, mas inicie o trabalho em outra thread com `Task.Run`. Se o trabalho for apropriado para concorrência e paralelismo, também considere usar a Biblioteca de Tarefas Paralelas.

Além disso, você sempre deve medir a execução do seu código. Por exemplo, você pode se encontrar em uma situação em que o trabalho ligado à CPU não é custoso o suficiente em comparação com o overhead das trocas de contexto ao multithreading. Cada escolha tem seu trade-off, e você deve escolher o trade-off correto para a sua situação.

## Demos

[demos](../demos/demos_async_await)