using System;
using System.Threading;

class Program
{
    static readonly Random random = new Random();
    static readonly object locker = new object();

    static void Main()
    {
        // Установите размеры консоли (можно подстроить под ваш экран)
        Console.SetWindowSize(80, 30);
        Console.CursorVisible = true;

        for (int i = 0; i < Console.WindowWidth; i++)
        {
            int column = i;
            // Запуск первого ланцюжка в столбце
            Thread thread1 = new Thread(() => DropChain(column));
            thread1.Start();

            // Задержка перед запуском второго ланцюжка в том же столбце
            Thread.Sleep(random.Next(500, 1000));

            // Запуск второго ланцюжка в столбце
            Thread thread2 = new Thread(() => DropChain(column));
            thread2.Start();

            // Задержка перед созданием нового столбца
            Thread.Sleep(random.Next(100, 500));
        }
    }

    static void DropChain(int column)
    {
        while (true)
        {
            int chainLength = random.Next(5, 15);
            for (int row = 0; row < Console.WindowHeight + chainLength; row++)
            {
                lock (locker)
                {
                    if (row < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(column, row);

                        if (row == 0)
                        {
                            // Белый цвет для первого символа
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (row == 1)
                        {
                            // Светло-зеленый для второго символа
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            // Темно-зеленый для остальных символов
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }

                        // Вывод случайного символа
                        Console.Write((char)random.Next(33, 126));
                    }

                    // Очистка символа после прохождения цепочки
                    if (row >= chainLength)
                    {
                        Console.SetCursorPosition(column, row - chainLength);
                        Console.Write(' ');
                    }
                }

                // Изменение символов при падении цепочки
                if (row < Console.WindowHeight)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
