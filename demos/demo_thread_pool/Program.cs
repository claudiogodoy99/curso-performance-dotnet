using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Criar uma tarefa usando Task.Factory.StartNew
        Task task1 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Tarefa 1 iniciada na thread " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000); // Simula algum processamento
            Console.WriteLine("Tarefa 1 concluída na thread " + Thread.CurrentThread.ManagedThreadId);
        });

        // Criar uma tarefa usando Task.Run
        Task task2 = Task.Run(() =>
        {
            Console.WriteLine("Tarefa 2 iniciada na thread " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000); // Simula algum processamento
            Console.WriteLine("Tarefa 2 concluída na thread " + Thread.CurrentThread.ManagedThreadId);
        });

        // Esperar que as tarefas terminem antes de encerrar o programa
        Task.WaitAll(task1, task2);

        Console.WriteLine("Todas as tarefas concluídas. Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}