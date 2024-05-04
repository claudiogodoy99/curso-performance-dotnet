# ThreadPool overhead: Problemas de comuns no uso do async await

Quando a aplicação usa a abordagem síncrona, ou seja, realiza chamadas bloqueantes nas `threads`, o `threadpool` pode encontrar dificuldades para lidar com a distribuição de carga de trabalho entre as `threads`, esse efeito é conhecido como `ThreadPool overhead`.

## Algoritmo de criação de novas threads

O pool de threads fornece novas worker threads ou threads de I/O conforme necessário, até atingir um mínimo especificado para cada categoria. Você pode utilizar o método ThreadPool.GetMinThreads para obter esses valores mínimos.

Quando a demanda é baixa, o número real de threads do pool de threads pode ficar abaixo dos valores mínimos.

Quando um mínimo é alcançado, o pool de threads pode criar threads adicionais ou aguardar até que algumas tarefas sejam concluídas. O pool de threads cria e destrói threads de trabalhadores para otimizar o throughput, que é definido como o número de tarefas que são concluídas por unidade de tempo. Poucas threads podem não utilizar de forma ideal os recursos disponíveis, enquanto muitas threads podem aumentar a contenção de recursos.

É possível utilizar o método `ThreadPool.SetMinThreads` para aumentar o número mínimo de threads inativas. No entanto, aumentar desnecessariamente esses valores pode causar problemas de desempenho. Se muitas tarefas começarem ao mesmo tempo, todas elas podem parecer lentas. Na maioria dos casos, o pool de threads funcionará melhor com seu próprio algoritmo para alocar threads.

## Async await deve ser viral

Em uma pilha de métodos assíncronos, em quase todos os casos, você precisa utilizar o `async await`, em toda cadeia de execução do caminho assíncrono.

Por exemplo:

```csharp
public async Task async1(){
    await async2();
}

public Task async2(){
    await async3();
}

public Task async3(){
    await Task.Delay(100);
}
```

Nessa situação, toda cadeia de chamada assíncrona faz uso do `async await`.

Agora imagine o segundo exemplo:

```csharp
 public voi async1(){
    async2().Result;
}

public Task async2(){
    await async3();
}

public Task async3(){
    await Task.Delay(100);
}
```

A segunda implementação apesar de sutil, possui um grane impacto na performance desta aplicação, uma vez que o primeiro método `async1`, contém um `.Result`. Neste contexto o `.Result`, irá bloquear a `thread`, mesmo que em um cenário onde os métodos subsequentes são assíncronos.

Quando você usa `.Result` ou `.Wait()`, você está bloqueando a thread atual até que a tarefa seja concluída, o que pode desperdiçar recursos. async e await permitem que a thread atual seja usada para outros trabalhos enquanto a tarefa ainda está sendo processada.

### Demo

[Demo async precisa ser viral](../demos/demo_async_vs_dotresult/)

Projeto contendo dois `endpoints`:

- `normal`: `endpoint` cujo a implementação é parcialmente assíncrona, entretanto, algum método utiliza o `.Result`.
- `async`: `endpoint` com implementação assíncrona.

Projeto possui dois arquivos de teste de carga.

#### .Result

```sh
        /\      |‾‾| /‾‾/   /‾‾/
     /\  /  \     |  |/  /   /  /
    /  \/    \    |     (   /   ‾‾\
   /          \   |  |\  \ |  (‾)  |
  / __________ \  |__| \__\ \_____/ .io

     execution: local
        script: .\script-dotresult.js
        output: -

     scenarios: (100.00%) 1 scenario, 500 max VUs, 1m50s max duration (incl. graceful stop):
              * default: Up to 500 looping VUs for 1m20s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


     ✓ status is 200

     checks.........................: 100.00% ✓ 11124      ✗ 0
     data_received..................: 6.1 MB  76 kB/s
     data_sent......................: 1.1 MB  14 kB/s
     http_req_blocked...............: avg=42.4µs  min=0s      med=0s       max=6.3ms  p(90)=0s       p(95)=242.56µs
     http_req_connecting............: avg=34.36µs min=0s      med=0s       max=6.3ms  p(90)=0s       p(95)=0s
   ✗ http_req_duration..............: avg=1.3s    min=299.2ms med=316.55ms max=5.19s  p(90)=4.12s    p(95)=4.92s
       { expected_response:true }...: avg=1.3s    min=299.2ms med=316.55ms max=5.19s  p(90)=4.12s    p(95)=4.92s
     http_req_failed................: 0.00%   ✓ 0          ✗ 11124
     http_req_receiving.............: avg=76.59µs min=0s      med=0s       max=2.33ms p(90)=386.79µs p(95)=527.51µs
     http_req_sending...............: avg=12.13µs min=0s      med=0s       max=2.22ms p(90)=0s       p(95)=0s
     http_req_tls_handshaking.......: avg=0s      min=0s      med=0s       max=0s     p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=1.3s    min=299.2ms med=316.37ms max=5.19s  p(90)=4.12s    p(95)=4.92s
     http_reqs......................: 11124   138.684089/s
     iteration_duration.............: avg=2.3s    min=1.29s   med=1.31s    max=6.19s  p(90)=5.14s    p(95)=5.94s
     iterations.....................: 11124   138.684089/s
     vus............................: 147     min=20       max=499
     vus_max........................: 500     min=500      max=500


running (1m20.2s), 000/500 VUs, 11124 complete and 0 interrupted iterations
default ✓ [======================================] 000/500 VUs  1m20s
ERRO[0081] thresholds on metrics 'http_req_duration' have been crossed
```

- **execução**: local (o teste foi realizado localmente, não em um ambiente de produção)
- **script**: o arquivo script-dotresult.js foi usado para conduzir o teste
- **saída**: nenhuma saída específica mencionada
- **cenários**: um único cenário foi testado, com um máximo de 500 Usuários Virtuais (VUs) por 1 minuto e 50 segundos, incluindo uma fase de redução gradual da carga.
- **status é 200**: confirma que o status HTTP das solicitações foi bem-sucedido.
- **checks**: mostra que todos os 11.124 checks foram bem-sucedidos, indicando que as verificações realizadas durante o teste foram passadas sem falhas.
- **dados recebidos**: mostra que 6.1 MB de dados foram recebidos durante o teste, a uma taxa de 76 kB/s.
- **dados enviados**: indica que 1.1 MB de dados foram enviados durante o teste, a uma taxa de 14 kB/s.
- **duração das requisições HTTP**: indica a média, mínimo, máximo e percentis das durações das requisições HTTP. Parece haver um problema aqui, pois o erro "thresholds on metrics 'http_req_duration' have been crossed" indica que a duração das requisições excedeu algum limite pré-definido.
- **falhas de requisições HTTP**: 0% de falhas de requisições HTTP, o que é bom.
- **requisições HTTP**: 11.124 requisições HTTP foram feitas a uma taxa média de 138.684089/s.
- **VUs (Usuários Virtuais)**: variação no número de Usuários Virtuais ao longo do teste, de 20 a 499 VUs, com um máximo de 500 VUs definido.

No geral, os resultados parecem indicar uma execução bem-sucedida do teste, exceto pelo problema relatado nas métricas de duração das requisições HTTP, que pode indicar um gargalo ou problema de desempenho a ser investigado.

#### Async

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

     checks.........................: 100.00% ✓ 19305      ✗ 0
     data_received..................: 11 MB   130 kB/s
     data_sent......................: 1.9 MB  24 kB/s
     http_req_blocked...............: avg=28.65µs  min=0s       med=0s       max=4.82ms   p(90)=0s       p(95)=0s
     http_req_connecting............: avg=20.64µs  min=0s       med=0s       max=4.82ms   p(90)=0s       p(95)=0s
   ✓ http_req_duration..............: avg=307.1ms  min=299.06ms med=302.4ms  max=321.97ms p(90)=317.06ms p(95)=317.9ms
       { expected_response:true }...: avg=307.1ms  min=299.06ms med=302.4ms  max=321.97ms p(90)=317.06ms p(95)=317.9ms
     http_req_failed................: 0.00%   ✓ 0          ✗ 19305
     http_req_receiving.............: avg=116.39µs min=0s       med=0s       max=1.52ms   p(90)=508.49µs p(95)=740.8µs
     http_req_sending...............: avg=13.31µs  min=0s       med=0s       max=1ms      p(90)=0s       p(95)=0s
     http_req_tls_handshaking.......: avg=0s       min=0s       med=0s       max=0s       p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=306.97ms min=299.06ms med=302.28ms max=321.93ms p(90)=316.93ms p(95)=317.83ms
     http_reqs......................: 19305   237.737227/s
     iteration_duration.............: avg=1.31s    min=1.3s     med=1.3s     max=1.33s    p(90)=1.31s    p(95)=1.32s
     iterations.....................: 19305   237.737227/s
     vus............................: 28      min=20       max=499
     vus_max........................: 500     min=500      max=500


running (1m21.2s), 000/500 VUs, 19305 complete and 0 interrupted iterations
default ✓ [======================================] 000/500 VUs  1m20s
```

- **execução**: o teste foi realizado localmente.
- **script**: o script-async.js foi usado para conduzir o teste.
- **saída**: não há saída específica mencionada.
- **cenários**: um único cenário foi testado, com um máximo de 500 Usuários Virtuais (VUs) por 1 minuto e 50 segundos, incluindo uma fase de redução gradual da carga.
- **status é 200**: confirma que o status HTTP das solicitações foi bem-sucedido.
- **checks**: todos os 19.305 checks foram bem-sucedidos, indicando que as verificações durante o teste foram passadas sem falhas.
- **dados recebidos**: 11 MB de dados foram recebidos durante o teste, a uma taxa de 130 kB/s.
- **dados enviados**: 1.9 MB de dados foram enviados durante o teste, a uma taxa de 24 kB/s.
- **duração das requisições HTTP**: a duração média das requisições HTTP foi de 307.1ms, com um mínimo de 299.06ms e um máximo de 321.97ms. A grande maioria das requisições ficou dentro dos percentis 90 e 95.
- **falhas de requisições HTTP**: 0% de falhas de requisições HTTP.
- **requisições HTTP**: 19.305 requisições HTTP foram feitas a uma taxa média de 237.737227/s.
- **VUs (Usuários Virtuais)**: houve uma variação de 28 a 499 VUs ao longo do teste, com um máximo de 500 VUs definido.
- **duração da iteração**: a duração média das iterações foi de 1.31s, com uma variação mínima entre 1.3s e 1.33s.

Em resumo, os resultados indicam que o teste foi bem-sucedido, com um status HTTP 200 para todas as solicitações, nenhum erro de requisição HTTP, e todas as verificações passaram sem problemas. A duração das requisições HTTP está dentro dos limites aceitáveis, e a quantidade de dados recebidos e enviados durante o teste também é relatada.

### Conclusão

Os resultados dos dois últimos testes fornecem uma comparação significativa entre o uso do modelo assíncrono no .NET e uma implementação assíncrona com o método .Result, que bloqueia as threads do `threadpool`, resultando em um overhead. O primeiro teste, utilizando o modelo assíncrono no .NET, demonstrou uma execução bem-sucedida, com status HTTP 200 para todas as solicitações, nenhum erro de requisição HTTP e todas as verificações passadas sem falhas. A duração das requisições HTTP ficou dentro dos limites aceitáveis, indicando uma eficiência no uso de recursos e uma taxa de requisições por segundo considerável. Em contrapartida, o segundo teste com o método .Result apresentou um problema com a duração das requisições HTTP, indicando um possível gargalo ou problema de desempenho devido ao bloqueio das threads. Isso ressalta a importância de utilizar abordagens assíncronas corretamente para evitar overheads no `threadpool` e garantir um melhor desempenho e utilização de recursos no sistema.
