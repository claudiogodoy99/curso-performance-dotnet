# Como identificar MemoryLeaks utilizando Contadores de performance e as ferramentas de diagnóstico do Visual Studio

Identificar MemoryLeaks pode ser crucial para o desempenho e estabilidade de um sistema. Utilizando Contadores de performance e as ferramentas de diagnóstico do Visual Studio, é possível monitorar o uso de memória ao longo do tempo e identificar padrões que indicam vazamentos de memória. Essas ferramentas fornecem insights valiosos, permitindo que os desenvolvedores localizem e corrijam eficientemente problemas relacionados à alocação de memória em seus aplicativos.

## Contadores de Performance

Contadores de performance são métricas que monitoram o desempenho de um sistema em tempo real. Eles fornecem informações detalhadas sobre o uso de recursos, como CPU, memória, disco e rede.

Para analisar o uso de memória de um aplicativo, você pode usar contadores de performance específicos do `source System.Runtime`:

- `working-set`
  - **Limiar:** Sem valor específico.
  - **Significado:** Este contador indica o número atual de bytes alocados para esse processo.

- `time-in-gc`
  - **Limiar:** Este contador deve ter uma média de cerca de 5% para a maioria dos aplicativos quando a CPU estiver 70% ocupada, com picos ocasionais. À medida que a carga da CPU aumenta, também aumenta o percentual de tempo gasto realizando coleta de lixo. Tenha isso em mente ao medir a CPU.
  - **Significado:** Este contador indica a porcentagem de tempo decorrido gasto realizando uma coleta de lixo desde a última ciclo de coleta de lixo. A causa mais comum de um valor alto é fazer muitas alocações, o que pode ser o caso se você estiver alocando por solicitação para aplicativos ASP.NET. Você precisa estudar o perfil de alocação para o seu aplicativo se este contador mostrar um valor mais alto.

- `gc-heap-size`
  - **Limiar:** Sem valor específico.
  - **Significado:** Este contador é a soma de outros quatro contadores — Tamanho do Heap Gen 0, Tamanho do Heap Gen 1, Tamanho do Heap Gen 2 e Tamanho do Heap de Objetos Grandes. O valor deste contador será sempre menor que o valor de `working-set`, que também inclui a memória nativa alocada para o processo pelo sistema operacional. `working-set` é o número de bytes alocados incluindo objetos não gerenciados.

- `gen-0-gc-count`
  - **Limiar:** Sem valor específico.
  - **Significado:** Este contador indica o número de vezes que os objetos da geração 0 são coletados pelo coletor de lixo desde o início do aplicativo. Objetos que sobrevivem à coleta são promovidos para a Geração 1. Você pode observar o padrão de alocação de memória do seu aplicativo plotando os valores deste contador ao longo do tempo.

- `gen-1-gc-count`
  - **Limiar:** Um décimo do valor de `gen-0-gc-count`.
  - **Significado:** Este contador indica o número de vezes que os objetos da geração 1 são coletados pelo coletor de lixo desde o início do aplicativo.

- `gen-2-gc-count`
  - **Limiar:** Um décimo do valor de `gen-1-gc-count`.
  - **Significado:** Este contador indica o número de vezes que os objetos da geração 2 são coletados pelo coletor de lixo desde o início do aplicativo. O heap da geração 2 é o mais custoso de manter para um aplicativo. Sempre que há uma coleta da geração 2, tas as `threads` são suspensas. Você deve perfilar o padrão de alocação para o seu aplicativo e minimizar os objetos no heap da geração 2.

- `alloc-rate`: Indica a quantidade de bytes alocados no heap gerenciado entre os intervalos de atualização. Isso representa a taxa de criação de novos objetos e o uso de memória ao longo do tempo.

### Demo profiler Visual Studio

Seguir passo a passo: [Visualize dotnet counters from the Visual Studio profiler](https://learn.microsoft.com/visualstudio/profiling/dotnet-counters-tool?view=vs-2022)