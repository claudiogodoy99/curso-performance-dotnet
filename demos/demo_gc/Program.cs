var root = new List<object>();

object gen0Obj = new object();
object gen1Obj = new object();
object gen2Obj = new object();

root.Add(gen1Obj);
root.Add(gen2Obj);

// Verificar a geração de cada objeto
Console.WriteLine($"Objeto gen0Obj está na Geração: {GC.GetGeneration(gen0Obj)}");
Console.WriteLine($"Objeto gen1Obj está na Geração: {GC.GetGeneration(gen1Obj)}");
Console.WriteLine($"Objeto gen2Obj está na Geração: {GC.GetGeneration(gen2Obj)}");

// Forçar uma coleta de lixo para promover os objetos para gerações mais altas
GC.Collect();

// Verificar a geração dos objetos após a coleta de lixo
Console.WriteLine("\nApós a coleta de lixo:");
Console.WriteLine($"Objeto gen1Obj está na Geração: {GC.GetGeneration(gen1Obj)}");
Console.WriteLine($"Objeto gen2Obj está na Geração: {GC.GetGeneration(gen2Obj)}");

root.Remove(gen1Obj);

GC.Collect();

// Verificar a geração dos objetos após a coleta de lixo
Console.WriteLine("\nApós a coleta de lixo:");
Console.WriteLine($"Objeto gen2Obj está na Geração: {GC.GetGeneration(gen2Obj)}");
