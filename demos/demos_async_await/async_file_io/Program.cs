using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await WriteToFileAsync("async-demo.txt", "This is an async-await example in C#.");
        Console.WriteLine("Data written to file asynchronously.");
    }

    static async Task WriteToFileAsync(string filePath, string data)
    {
        byte[] encodedText = Encoding.Unicode.GetBytes(data);

        using (FileStream sourceStream = new FileStream(filePath,
            FileMode.Append, FileAccess.Write, FileShare.None,
            bufferSize: 4096, useAsync: true))
        {
            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }
    }
}
