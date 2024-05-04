using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Task task1 = TaskMethod("Task 1", 3000);
        Task task2 = TaskMethod("Task 2", 2000);
        Task task3 = TaskMethod("Task 3", 1000);

        await Task.WhenAll(task1, task2, task3);

        Console.WriteLine("All tasks completed.");
    }

    static async Task TaskMethod(string name, int delay)
    {
        await Task.Delay(delay);
        Console.WriteLine($"{name} completed after {delay} milliseconds.");
    }
}
