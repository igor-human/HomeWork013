using System;
using System.Threading;

class Program
{
    // Максимальная глубина рекурсии
    const int MaxDepth = 10;

    static void Main()
    {
        // Запускаем первый рекурсивный вызов в основном потоке
        StartRecursiveMethod(0);
    }

    static void StartRecursiveMethod(int depth)
    {
        // Проверяем, достигнута ли максимальная глубина рекурсии
        if (depth >= MaxDepth)
        {
            Console.WriteLine($"Reached maximum depth of {MaxDepth}.");
            return;
        }

        Console.WriteLine($"Depth {depth} started on thread {Thread.CurrentThread.ManagedThreadId}");

        // Создаем новый поток для следующего рекурсивного вызова
        Thread newThread = new Thread(() =>
        {
            StartRecursiveMethod(depth + 1);
        });

        // Запускаем новый поток
        newThread.Start();

        // Ждем завершения нового потока
        newThread.Join();

        Console.WriteLine($"Depth {depth} ended on thread {Thread.CurrentThread.ManagedThreadId}");
    }
}
