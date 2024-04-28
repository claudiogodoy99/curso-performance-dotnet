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

