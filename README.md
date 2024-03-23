
# Curso .NET Performance

Este repo se destina à todos os artefatos da formação de engenheiro .NET com foco em Perfomance.

## [Fundamentos de Performance em .NET](./Fundamentos%20de%20Performance%20em%20.NET/README.md)

1. [Introdução à Otimização de Performance](Fundamentos%20de%20Performance%20em%20.NET/Introdução%20à%20Otimização%20de%20Performance.md)
   - Definição de performance em aplicações .NET
   - Importância da performance para a experiência do usuário

2. [Anatomia de uma Aplicação .NET](./Fundamentos%20de%20Performance%20em%20.NET/Anatomia%20de%20uma%20Aplicação%20.NET.md)
   - Estrutura básica de uma aplicação .NET
   - Ciclo de vida de uma requisição HTTP em aplicações web
   - ThreadPool: funcionamento e otimizações

3. Gerenciamento de Memória em .NET
   - Garbage Collection: funcionamento e otimizações

## Otimização Avançada em .NET

1. Otimização de Algoritmos
   - Complexidade de algoritmos e seu impacto na performance

2. Otimização de E/S (Entrada e Saída)
   - Uso eficiente de operações de leitura/escrita em arquivos e bancos de dados
   - Utilização de buffers e operações assíncronas

3. Análise de desempenho em ciclo de desenvolvimento
   - Uso e importância do benchmark
   - Ferramentas de teste de carga: K6 e JMetter

## Introdução ao Monitoramento e Diagnóstico em Aplicações .NET

1. Importância do Monitoramento e Diagnóstico
   - Significado para a saúde e desempenho da aplicação
   - Como o monitoramento proativo pode evitar problemas futuros

2. Overview do .NET SDK Ferramentas
   - dotnet-counters: Monitoramento em tempo real de métricas do sistema e da aplicação.
   - dotnet-trace: Captura de eventos de diagnóstico para análise detalhada.
   - dotnet-dump: Coleta de dumps de memória para análise de problemas de memória e exceções.

## Uso Avançado das Ferramentas do .NET SDK

1. dotnet-counters: Monitoramento em Tempo Real
   - Demonstração prática do uso do dotnet-counters
   - Monitorando métricas de CPU, memória, e contadores personalizados
   - Configuração de alertas com base em métricas específicas

2. dotnet-trace: Capturando e Analisando Eventos
   - Captura e análise de eventos de diagnóstico usando o dotnet-trace
   - Análise de eventos relacionados a exceções, requisições HTTP, e personalizados
   - Uso de filtros para identificar eventos específicos

3. dotnet-dump: Diagnóstico Profundo com Dumps de Memória
   - Geração de dumps de memória em diferentes cenários (por exemplo, exceções não tratadas)
   - Análise de dumps para identificar vazamentos de memória e problemas de desempenho
   - Utilização de comandos para explorar objetos e pilhas de chamadas

## Monitoramento Contínuo com Application Insights

1. Introdução ao Application Insights
   - Visão geral das funcionalidades do Application Insights
   - Configuração em aplicações .NET

2. Coleta e Visualização de Dados
   - Rastreamento de solicitações HTTP, exceções e dependências
   - Personalização de eventos para coleta de dados específicos da aplicação

3. Análise e Alertas em Tempo Real
   - Criação de painéis personalizados para monitorar métricas importantes
   - Configuração de alertas para notificações em tempo real sobre problemas críticos
