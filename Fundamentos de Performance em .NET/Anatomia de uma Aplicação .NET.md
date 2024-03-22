# Anatomia de uma Aplicação .NET

No ecossistema do .NET, que abrange tanto o .NET Framework quanto o .NET Core (agora unificado como .NET), existem vários tipos de aplicações que podem ser desenvolvidas, cada uma com sua própria estrutura e finalidade. O .NET é uma plataforma de desenvolvimento de software desenvolvida pela Microsoft, que inclui um vasto conjunto de ferramentas, bibliotecas e frameworks para a criação de aplicações desktop, web, mobile, serviços e muito mais.

O .NET SDK (Software Development Kit) é o conjunto de ferramentas que permite criar, compilar, depurar e publicar aplicações .NET. Ele inclui o compilador C# (ou VB.NET), as bibliotecas de classes .NET (como ASP.NET Core, Entity Framework Core, etc.) e outras utilidades como o MSBuild, responsável por compilar e construir projetos .NET.

O ambiente de execução .NET (também conhecido como Common Language Runtime ou CLR) é a máquina virtual que gerencia a execução de programas .NET. Ele oferece serviços como gerenciamento de memória, coleta de lixo, controle de exceções e execução de código gerenciado, garantindo a interoperabilidade entre diferentes linguagens suportadas pelo .NET.

Durante o processo de build do .NET, o código fonte é compilado para a Linguagem Intermediária (IL), também conhecida como Common Intermediate Language (CIL). Essa IL é então convertida em código nativo pela CLR durante a execução da aplicação. Os binários resultantes incluem o código do projeto em arquivos de Linguagem Intermediária (IL) com extensão .dll.

Em resumo, o ecossistema .NET oferece uma ampla gama de ferramentas e tecnologias para o desenvolvimento de aplicações, sendo o SDK, o CLR e o processo de geração de IL (Linguagem Intermediária) componentes fundamentais no processo de criação, execução e construção de projetos .NET.

## Tipos de Aplicações .NET mais comuns

1. **WebAPI:** Uma aplicação voltada para fornecer serviços e endpoints RESTful para interação com clientes, como navegadores web, aplicativos móveis e outros sistemas. Ela utiliza o protocolo HTTP para comunicação.

2. **Web Application com SSR (Server-Side Rendering):** Aplicações web que renderizam parte do conteúdo no servidor antes de enviá-lo para o cliente. É comumente usado em aplicações que precisam de SEO e em casos onde a renderização no servidor é mais eficiente.

3. **Console Application:** Aplicações de console que são executadas em um prompt de comando, úteis para tarefas automatizadas, scripts e interações diretas com o usuário através da linha de comando.

4. **MAUI (Multi-platform App UI):** Uma evolução do Xamarin.Forms, permite o desenvolvimento de aplicativos multiplataforma para iOS, Android e Windows usando um único código-fonte.

6. **Libraries (.NET Standard/Core Libraries):** As bibliotecas (.NET Standard/Core Libraries) são conjuntos de código reutilizável que fornecem funcionalidades específicas para o desenvolvimento de aplicações .NET.

### WebAPI

Uma WebAPI é um tipo de aplicação que segue o estilo arquitetural REST (Representational State Transfer), baseado no protocolo HTTP. Ela é projetada para ser uma interface entre sistemas e permite que diferentes aplicações se comuniquem de forma eficiente e escalável.

- **Protocolo HTTP:** É um protocolo de comunicação utilizado para transferência de dados na World Wide Web. Ele define métodos como GET, POST, PUT, DELETE, entre outros, para manipulação de recursos em um servidor.

- **REST API (API Restful):** É uma API que segue os princípios do estilo arquitetural REST. Isso inclui o uso de URIs (Uniform Resource Identifiers) para identificar recursos, métodos HTTP para operações CRUD (Create, Read, Update, Delete) e um conjunto de status de resposta padronizado.

- **[Teorema de Richardson](https://martinfowler.com/articles/richardsonMaturityModel.html):** Proposto por Leonard Richardson, esse teorema estabelece níveis de maturidade em APIs REST, divididos em níveis de Richardson, que vão do nível 0 (sem uso adequado de REST) ao nível 3 (uso completo dos princípios RESTful).

O foco principal do curso está na WebAPI, devido à sua relevância na criação de serviços modernos e na integração de sistemas distribuídos de forma eficiente e escalável.

Essa é uma visão geral da anatomia de uma aplicação .NET, abordando os tipos comuns de aplicações e dando ênfase especial na WebAPI, protocolo HTTP, REST API e o Teorema de Richardson como referência para o desenvolvimento de APIs RESTful.

## Demo

Para criar uma `webapi`, com o [dotnet sdk](https://dotnet.microsoft.com/download) instalado, rode o comando:

```sh
dotnet new webapi -n MinhaApi
```