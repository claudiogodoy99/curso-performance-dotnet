
# Curso .NET Performance

Este repo se destina à todos os artefatos da formação de engenheiro .NET com foco em Perfomance.

## [Fundamentos de Performance em .NET]

1. [Introdução à Otimização de Performance](Fundamentos%20de%20Performance%20em%20.NET/Introdução%20à%20Otimização%20de%20Performance.md)
   - Definição de performance em aplicações .NET
   - Importância da performance para a experiência do usuário

2. [Anatomia de uma Aplicação .NET](./Fundamentos%20de%20Performance%20em%20.NET/Anatomia%20de%20uma%20Aplicação%20.NET.md)
   - Estrutura básica de uma aplicação .NET
   - Ciclo de vida de uma requisição HTTP em aplicações web

3. [Recursos do Common Language Runtime](./Fundamentos%20de%20Performance%20em%20.NET/Recursos%20do%20Common%20Language%20Runtime.md)
   - ThreadPool: Overview
   - Managed Heap
   - Garbage Collection: Overview
   - JIT: Overview

## Otimização Avançada em .NET

1. [Análise de desempenho em ciclo de desenvolvimento](Otimização%20Avançada%20em%20.NET/Análise%20de%20desempenho%20em%20ciclo%20de%20desenvolvimento.md)
   - Uso e importância do benchmark
   - Ferramentas de teste de carga: K6

2. Gerenciando a memória
   - Utilização de `structs` e `refs`
   - Otimizando o Desempenho de `Strings` no .NET
   - Como otimizar expressões regulares `Regex`
   - Otimizando expressões `LINQ`
   - Como identificar `MemoryLeaks` utilizando Contadores de performance e as ferramentas de diagnóstico do Visual Studio

3. Modelo de programação assíncrona (TAP)
   - Async Await
   - Impacto na escalabilidade
   - ThreadPool overhead: Problemas de comuns no uso do async await

4. Ciclo de vida de objetos de I/O
   - Gerenciamento dos objetos de conexão com Banco de dados
   - Gerenciamento dos objetos de conexão http
   - Gerenciamento dos objetos de FileStream
   - Como tomar decisão de ciclo de vida de um objeto de I/O

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

1. Importância do Monitoramento e Diagnóstico
   - Significado para a saúde e desempenho da aplicação
   - Como o monitoramento proativo pode evitar problemas futuros

2. Introdução ao Application Insights
   - Visão geral das funcionalidades do Application Insights
   - Configuração em aplicações .NET

3. Coleta e Visualização de Dados
   - Rastreamento de solicitações HTTP, exceções e dependências
   - Personalização de eventos para coleta de dados específicos da aplicação

4. Análise e Alertas em Tempo Real
   - Criação de painéis personalizados para monitorar métricas importantes
   - Configuração de alertas para notificações em tempo real sobre problemas críticos
