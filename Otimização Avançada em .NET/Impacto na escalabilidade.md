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

### Bloqueio de Thread

Imagine que um cliente entra na loja e faz um pedido especial que levará algum tempo para ser preparado. Em uma situação de bloqueio de thread, você, como gerente, fica parado esperando que o pedido seja concluído antes de atender outros clientes ou realizar outras tarefas. Isso pode causar atrasos e ineficiências, pois você está bloqueado em uma única atividade até que ela seja concluída.

### Abordagem Assíncrona

Agora, considere a mesma situação, mas com uma abordagem assíncrona. Quando o cliente faz o pedido especial, você anota o pedido e o coloca em andamento, mas em vez de esperar que o pedido seja preparado, você continua atendendo outros clientes e realizando outras tarefas na loja. Quando o pedido especial estiver pronto, você é notificado e pode então atender o cliente sem atrasos significativos. Essa abordagem assíncrona permite que você seja mais eficiente ao lidar com várias tarefas ao mesmo tempo, sem ficar bloqueado em uma única atividade.

Assim como o gerenciamento eficaz de uma loja movimentada requer a capacidade de lidar com várias tarefas simultaneamente sem ficar parado em uma única atividade, a programação assíncrona no desenvolvimento de software permite que o sistema execute várias operações sem bloquear threads desnecessariamente, melhorando a eficiência e a capacidade de resposta do programa.

## Demo

[Demo para demonstrar um Thread.Sleep vs um await Task.Delay](../demos/demo_async_vs_sync/)

A Demo contém uma pasta com três `k6 scripts`, definindo testes de carga para compararmos as abordagens:

- `script-sync.js` - Teste em `endpoint` contendo o segmento de código `Thread.Sleep` (síncrono).
- `script-async.js` - Teste em `endpoint` contendo segmento de código `await Task.Delay` (assíncrono).
- `script-hibrido.js` - Teste dividindo a carga em 50% para ambos `endpoints`.

### `script-sync.js`

```sh
          /\      |‾‾| /‾‾/   /‾‾/
     /\  /  \     |  |/  /   /  /
    /  \/    \    |     (   /   ‾‾\
   /          \   |  |\  \ |  (‾)  |
  / __________ \  |__| \__\ \_____/ .io

     execution: local
        script: .\script-sync.js
        output: -

     scenarios: (100.00%) 1 scenario, 500 max VUs, 1m50s max duration (incl. graceful stop):
              * default: Up to 500 looping VUs for 1m20s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


     ✓ status is 200

     checks.........................: 100.00% ✓ 6414      ✗ 0
     data_received..................: 3.5 MB  43 kB/s
     data_sent......................: 654 kB  8.0 kB/s
     http_req_blocked...............: avg=93.02µs min=0s       med=0s    max=5.56ms p(90)=0s    p(95)=1.05ms   
     http_req_connecting............: avg=73.83µs min=0s       med=0s    max=5.56ms p(90)=0s    p(95)=725.29µs 
   ✗ http_req_duration..............: avg=3.05s   min=302.19ms med=3.02s max=8.24s  p(90)=4.84s p(95)=5.17s    
       { expected_response:true }...: avg=3.05s   min=302.19ms med=3.02s max=8.24s  p(90)=4.84s p(95)=5.17s    
     http_req_failed................: 0.00%   ✓ 0         ✗ 6414
     http_req_receiving.............: avg=48.85µs min=0s       med=0s    max=5.68ms p(90)=0s    p(95)=0s       
     http_req_sending...............: avg=17.02µs min=0s       med=0s    max=3.67ms p(90)=0s    p(95)=0s       
     http_req_tls_handshaking.......: avg=0s      min=0s       med=0s    max=0s     p(90)=0s    p(95)=0s       
     http_req_waiting...............: avg=3.05s   min=302.14ms med=3.02s max=8.24s  p(90)=4.84s p(95)=5.17s    
     http_reqs......................: 6414    78.060406/s
     iteration_duration.............: avg=4.06s   min=1.3s     med=4.03s max=9.26s  p(90)=5.86s p(95)=6.17s    
     iterations.....................: 6414    78.060406/s
     vus............................: 8       min=8       max=500
     vus_max........................: 500     min=500     max=500


running (1m22.2s), 000/500 VUs, 6414 complete and 0 interrupted iterations
default ✓ [======================================] 000/500 VUs  1m20s
ERRO[0083] thresholds on metrics 'http_req_duration' have been crossed
```

Aqui estão algumas análises e interpretações dos resultados:

- **Check de status (status is 200):** 100% das requisições receberam uma resposta com status 200, o que indica que todas as solicitações foram bem-sucedidas.

- **Check de tempo de resposta (http_req_duration):** O tempo médio de resposta das requisições foi de aproximadamente 3.05 segundos, com um mínimo de 302.19 milissegundos e um máximo de 8.24 segundos. Os valores de 90% e 95% das requisições ficaram abaixo de 4.84 segundos e 5.17 segundos, respectivamente. No entanto, é importante observar que as métricas de tempo de resposta cruzaram os limites definidos nos thresholds, indicando um problema nessa métrica durante o teste.

- **Taxa de requisições:** Foram realizadas 6414 requisições durante o teste, com uma média de 78.06 requisições por segundo.

- **VUs (Usuários Virtuais):** O número de usuários virtuais variou de 8 a 500 durante o teste. O número máximo de VUs foi alcançado e mantido por um período de 1 minuto e 20 segundos.

Em resumo, o teste foi bem-sucedido em termos de status das requisições, mas houve um problema com o tempo de resposta das requisições ultrapassando os limites definidos nos thresholds. Isso pode indicar possíveis gargalos ou problemas de desempenho que precisam ser investigados e otimizados.

### `scrip-async.js`

```sh
          /\      |‾‾| /‾‾/   /‾‾/
     /\  /  \     |  |/  /   /  /
    /  \/    \    |     (   /   ‾‾\
   /          \   |  |\  \ |  (‾)  |
  / __________ \  |__| \__\ \_____/ .io

     execution: local
        script: .\script-async.js
        output: -

     scenarios: (100.00%) 1 scenario, 500 max VUs, 1m50s max duration (incl. graceful stop):
              * default: Up to 500 looping VUs for 1m20s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


     ✓ status is 200

     checks.........................: 100.00% ✓ 19222      ✗ 0
     data_received..................: 11 MB   129 kB/s
     data_sent......................: 1.9 MB  24 kB/s
     http_req_blocked...............: avg=36µs     min=0s       med=0s       max=22.77ms  p(90)=0s       p(95)=0s
     http_req_connecting............: avg=24.29µs  min=0s       med=0s       max=22.77ms  p(90)=0s       p(95)=0s
   ✓ http_req_duration..............: avg=309.49ms min=299.38ms med=309.25ms max=386.67ms p(90)=316.73ms p(95)=317.86ms
       { expected_response:true }...: avg=309.49ms min=299.38ms med=309.25ms max=386.67ms p(90)=316.73ms p(95)=317.86ms
     http_req_failed................: 0.00%   ✓ 0          ✗ 19222
     http_req_receiving.............: avg=78.21µs  min=0s       med=0s       max=3.29ms   p(90)=172.69µs p(95)=551.12µs
     http_req_sending...............: avg=19.64µs  min=0s       med=0s       max=4.53ms   p(90)=0s       p(95)=0s
     http_req_tls_handshaking.......: avg=0s       min=0s       med=0s       max=0s       p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=309.39ms min=299.38ms med=309.15ms max=386.67ms p(90)=316.63ms p(95)=317.74ms
     http_reqs......................: 19222   236.438947/s
     iteration_duration.............: avg=1.31s    min=1.3s     med=1.31s    max=1.38s    p(90)=1.32s    p(95)=1.32s
     iterations.....................: 19222   236.438947/s
     vus............................: 43      min=19       max=499
     vus_max........................: 500     min=500      max=500

                                                                                                               
running (1m21.3s), 000/500 VUs, 19222 complete and 0 interrupted iterations                                    
default ✓ [======================================] 000/500 VUs  1m20s  
```

- **Check de status (status is 200):** 100% das requisições receberam uma resposta com status 200, indicando que todas as solicitações foram bem-sucedidas.

- **Check de tempo de resposta (http_req_duration):** O tempo médio de resposta das requisições foi de aproximadamente 309.49 milissegundos (ms), com um mínimo de 299.38 ms e um máximo de 386.67 ms. Os valores de 90% e 95% das requisições ficaram abaixo de 316.73 ms e 317.86 ms, respectivamente. Isso sugere uma resposta rápida e consistente do servidor para a maioria das requisições.

- **Taxa de requisições:** Durante o teste, foram realizadas 19222 requisições, com uma média de 236.44 requisições por segundo.

- **VUs (Usuários Virtuais):** O número de usuários virtuais variou de 19 a 499 durante o teste, com um máximo de 500 usuários virtuais alcançado.

- **Outras métricas:** As métricas relacionadas ao tempo de bloqueio, conexão, envio, espera e recebimento das requisições estão dentro de limites aceitáveis, com tempos médios e máximos razoáveis.

No geral, os resultados indicam um desempenho sólido do sistema durante o teste de carga. A taxa de requisições por segundo é satisfatória, e o tempo de resposta das requisições está dentro de limites aceitáveis, mostrando uma boa capacidade de resposta do servidor sob carga.

### `scrip-hibrido.js`

```sh
          /\      |‾‾| /‾‾/   /‾‾/
     /\  /  \     |  |/  /   /  /
    /  \/    \    |     (   /   ‾‾\
   /          \   |  |\  \ |  (‾)  |
  / __________ \  |__| \__\ \_____/ .io

     execution: local
        script: .\script-hibrido.js
        output: -

     scenarios: (100.00%) 1 scenario, 500 max VUs, 1m50s max duration (incl. graceful stop):
              * default: Up to 500 looping VUs for 1m20s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


     ✓ status is 200

     checks.........................: 100.00% ✓ 12364      ✗ 0
     data_received..................: 6.8 MB  84 kB/s
     data_sent......................: 1.3 MB  16 kB/s
     http_req_blocked...............: avg=54.91µs min=0s       med=0s       max=19.26ms p(90)=0s    p(95)=0s   

     http_req_connecting............: avg=41.59µs min=0s       med=0s       max=19.26ms p(90)=0s    p(95)=0s   

   ✗ http_req_duration..............: avg=1.57s   min=299.55ms med=631.43ms max=5.58s   p(90)=4.03s p(95)=4.39s

       { expected_response:true }...: avg=1.57s   min=299.55ms med=631.43ms max=5.58s   p(90)=4.03s p(95)=4.39s

     http_req_failed................: 0.00%   ✓ 0          ✗ 12364
     http_req_receiving.............: avg=65.19µs min=0s       med=0s       max=20.94ms p(90)=0s    p(95)=268.35µs
     http_req_sending...............: avg=25.18µs min=0s       med=0s       max=22.97ms p(90)=0s    p(95)=0s   

     http_req_tls_handshaking.......: avg=0s      min=0s       med=0s       max=0s      p(90)=0s    p(95)=0s   

     http_req_waiting...............: avg=1.57s   min=299.55ms med=631.43ms max=5.58s   p(90)=4.03s p(95)=4.39s

     http_reqs......................: 12364   153.928058/s
     iteration_duration.............: avg=4.15s   min=1.6s     med=4.1s     max=7.2s    p(90)=6.02s p(95)=6.86s

     iterations.....................: 6182    76.964029/s
     vus............................: 226     min=19       max=500
     vus_max........................: 500     min=500      max=500

                                                                                                               
running (1m20.3s), 000/500 VUs, 6182 complete and 0 interrupted iterations                                     
default ✓ [======================================] 000/500 VUs  1m20s                                          
ERRO[0082] thresholds on metrics 'http_req_duration' have been crossed
```

Esses são os resultados de um teste de carga com um script híbrido, onde se misturam chamadas síncronas e assíncronas:

- **Check de status (status is 200):** 100% das requisições receberam uma resposta com status 200, indicando que todas as solicitações foram bem-sucedidas.

- **Check de tempo de resposta (http_req_duration):** O tempo médio de resposta das requisições foi de aproximadamente 1.57 segundos, com um mínimo de 299.55 milissegundos e um máximo de 5.58 segundos. Os valores de 90% e 95% das requisições ficaram abaixo de 4.03 segundos e 4.39 segundos, respectivamente. No entanto, essas métricas cruzaram os thresholds definidos, indicando um problema com o tempo de resposta durante o teste.

- **Taxa de requisições:** Durante o teste, foram realizadas 12364 requisições, com uma média de 153.93 requisições por segundo.

- **VUs (Usuários Virtuais):** O número de usuários virtuais variou de 19 a 500 durante o teste, com um máximo de 500 usuários virtuais alcançado.

- **Outras métricas:** As métricas relacionadas ao tempo de bloqueio, conexão, envio, espera e recebimento das requisições estão dentro de limites aceitáveis, com tempos médios e máximos razoáveis.

O teste teve sucesso em relação ao status das requisições, mas o tempo de resposta ultrapassou os thresholds definidos, indicando um problema nessa métrica durante o teste. Isso pode ser um indicativo de possíveis gargalos ou problemas de desempenho que precisam ser investigados e otimizados.

## Conclusão

Ao analisar o impacto e as diferenças entre as abordagens síncronas e assíncronas no `.NET`, fica claro que a abordagem assíncrona oferece vantagens significativas, especialmente em termos de eficiência e desempenho do `threadpool`. A execução assíncrona permite que o `threadpool` seja aproveitado de forma mais eficaz, evitando bloqueios desnecessários e melhorando a capacidade de resposta do sistema em cenários de carga elevada.

No próximo capítulo, explorarei alguns problemas comuns na implementação de código assíncrono, destacando os desafios que os desenvolvedores enfrentam ao lidar com tarefas assíncronas e como esses problemas podem ser abordados de forma eficiente para garantir um código assíncrono robusto e eficaz.