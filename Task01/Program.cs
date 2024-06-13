using System;
using System.Threading;

class Program
{
    static readonly Random random = new Random();

    static void Main()
    {
        // Установите размеры консоли (можно подстроить под ваш экран)
        Console.SetWindowSize(100, 30);
        Console.CursorVisible = false;

        while (true)
        {
            // Запускаем поток для каждого нового ланцюжка
            Thread thread = new Thread(DropChain);
            thread.Start();

            // Задержка перед созданием нового ланцюжка
            Thread.Sleep(random.Next(100, 500));
        }
    }

    static void DropChain()
    {
        int column = random.Next(0, Console.WindowWidth);
        int chainLength = random.Next(5, 15);

        for (int row = 0; row < Console.WindowHeight + chainLength; row++)
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

            // Изменение символов при падении цепочки
            if (row < Console.WindowHeight)
            {
                Thread.Sleep(100);
            }
        }
    }
}
