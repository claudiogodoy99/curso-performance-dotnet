# Utilização eficiente de objetos I/O

Entender como utilizar os objetos de I/O de forma correta no .NET é extremamente importante, uma vez que, usualmente o propósito de uma aplicação Web é oferecer algum tipo de integração com outro sistema, exemplo: Uma api de usuários, vai ser responsável por implementar uma camada de negócio, e persistir os dados em um `SGDB`.

Exemplo de objetos de I/O:

- `SqlConnection`
- `HttpConnection`
- `FileStream`

Em resumo, todo objeto responsável por implementar uma camada de integração com algum serviço externo a sua aplicação, utilizando algum protocolo de comunicação, pode ser considerado um objeto de I/O.

## Gerenciamento dos objetos de conexão com Banco de dados

Conectar-se a um servidor de banco de dados envolve vários passos demorados. Para otimizar o processo, o ADO.NET usa o pool de conexão, que reduz a necessidade de abrir novas conexões.

Principais pontos:

- O pool de conexão mantém conexões ativas para cada configuração específica.
- As conexões são agrupadas por string de conexão e identidade do Windows quando a segurança integrada é usada.
- O pool é mantido automaticamente pelo sistema, mas pode ser controlado com modificadores na string de conexão.
- O uso adequado do pool de conexão pode aumentar significativamente o desempenho e a escalabilidade da aplicação.

Exemplo de uso do pool de conexão em C#:

```csharp
using (SqlConnection connection = new SqlConnection(  
  "Integrated Security=SSPI;Initial Catalog=Northwind"))  
{  
    connection.Open();
    // Utilize a conexão aqui
}  
```

Pontos importantes para evitar problemas no pool de conexões:

1. Feche as conexões assim que não forem mais necessárias para liberar recursos.
2. Adie as operações de banco de dados sempre que possível e agrupe várias operações em uma única transação para reduzir o número de idas ao banco de dados.
3. Evite realizar operações desnecessárias no banco de dados para não sobrecarregar o pool de conexões.
4. Sempre chame os métodos `Close` e `Dispose` para liberar corretamente as conexões e evitar vazamentos de conexão.

## Gerenciamento dos objetos de conexão http

Ao utilizar HttpClient em seu código para fazer chamadas HTTP, é importante estar atento para evitar problemas de exaustão de portas TCP, especialmente em situações de alto volume de requisições. O uso do padrão using com HttpClient pode resultar em múltiplas conexões TCP sendo mantidas em estado de TIME_WAIT, levando ao esgotamento de portas disponíveis. Uma solução é criar HttpClient como um objeto singleton ou estático para reutilizar a conexão TCP, evitando assim o problema de exaustão de sockets. Uma alternativa mais robusta é o uso da interface IHttpClientFactory, que implementa um pool de objetos HttpMessageHandler's para reutilização eficiente de conexões. Isso é especialmente útil em aplicações Web, onde a resolução de DNS pode mudar e a recriação do HttpClient pode não ser resiliente o suficiente. O uso de Typed Clients ou Cliente Tipado é uma abordagem recomendada ao trabalhar com IHttpClientFactory, pois permite criar classes de serviços específicos e injetar HttpClient no construtor, garantindo uma gestão eficiente de conexões HTTP.

### Boas Práticas

### Criar HttpClient como Singleton ou Estático

```csharp
public class MyHttpClientService
{
    private static readonly HttpClient _client = new HttpClient();

    public HttpClient GetHttpClient()
    {
        return _client;
    }
}
```

### Usar HttpClient com IHttpClientFactory (ASP.NET Core)

```csharp
public class MyService
{
    private readonly HttpClient _httpClient;

    public MyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetResponseAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
```

### Como Não Fazer (Problemas Potenciais)

#### Criar HttpClient em Cada Requisição

```csharp
public async Task<string> MakeHttpRequestAsync(string url)
{
    using (var client = new HttpClient())
    {
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
```

#### Não Usar IHttpClientFactory (ASP.NET Core)

```csharp
public class MyService
{
    private readonly HttpClient _httpClient;

    public MyService()
    {
        _httpClient = new HttpClient(); // Evite criar HttpClient diretamente
    }

    public async Task<string> GetResponseAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
```

### Gerenciamento dos objetos de FileStream

Cada objeto FileStream encapsula um identificador do sistema operacional, que é um recurso limitado do sistema. Não fechar esses identificadores pode levar a vazamentos de recursos, que eventualmente podem exaurir a capacidade do sistema de abrir novos arquivos ou sockets.

- Integridade dos Dados:
Fechar adequadamente um FileStream garante que todos os dados em buffer sejam escritos no arquivo subjacente, mantendo a integridade dos dados. Se um fluxo não for fechado, há o risco de que os dados em buffer não sejam gravados no arquivo, resultando em perda de dados.

- Acessibilidade do Arquivo:
Um FileStream aberto pode bloquear o arquivo, impedindo que outros processos ou threads acessem. Fechar o fluxo libera esse bloqueio, permitindo que outras operações prossigam sem conflitos.

#### Como Fechar um FileStream

##### Usando a Declaração Using

A declaração using em C# é um açúcar sintático para um bloco try-finally. Isso garante que o método Dispose seja chamado no objeto FileStream, o que internamente chama Close. Isso acontece mesmo se uma exceção for lançada dentro do bloco using, garantindo a limpeza de recursos.

```csharp
using (FileStream fs = new FileStream("arquivo.txt", FileMode.OpenOrCreate))
{
    // Realize operações de arquivo
} // O FileStream é fechado automaticamente aqui, mesmo se ocorrer uma exceção
```

##### Fechamento Explícito ou Dispose

Se você não estiver usando uma declaração using, deve chamar explicitamente Close ou Dispose no objeto FileStream quando terminar de usá-lo.

```csharp
try
{
    fs = new FileStream("arquivo.txt", FileMode.OpenOrCreate);
    // Realize operações de arquivo
}
finally
{
    if (fs != null)
        fs.Dispose(); // Isso fechará o FileStream
}
```

##### Tratamento de Exceções

- Blocos Try-Catch:
Envolver operações de E/S de arquivo em blocos try-catch para lidar com exceções, como IOException para problemas de acesso a arquivos ou UnauthorizedAccessException para problemas de permissão.

- Usando Declaração com Try-Catch

Você pode combinar declarações using com blocos try-catch para um tratamento robusto de exceções, garantindo ao mesmo tempo a limpeza de recursos.

```csharp
try
{
    using (FileStream fs = new FileStream("arquivo.txt", FileMode.OpenOrCreate))
    {
        // Realize operações de arquivo
    }
}
catch (Exception ex)
{
    // Trate as exceções
}
```

#### Melhores Práticas

- Sempre Use Declarações Using:
Sempre que possível, use declarações using para gerenciar objetos FileStream. É uma prática recomendada que simplifica o código e garante o gerenciamento adequado de recursos.
- Flush Antes do Fechamento:
Se você escreveu dados no fluxo e precisa garantir que eles sejam gravados no arquivo antes do fechamento, chame Flush ou FlushAsync para limpar todos os buffers.

### Conclusão

É importante sempre consultar as documentações oficiais da biblioteca que o seu projeto está usando, para garantir que ele segue as corretas diretrizes e boas práticas. Cada tipo de objeto tem suas implementações e especificações, por tanto, esse estudo deve estar incluído como escopo de qualquer integração.