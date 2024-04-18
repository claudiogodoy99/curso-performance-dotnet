using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await GetDataAsync();
    }

    static async Task GetDataAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            response.EnsureSuccessStatusCode();
            string data = await response.Content.ReadAsStringAsync();
            Console.WriteLine(data);
        }
    }
}
