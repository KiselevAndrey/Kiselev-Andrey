using System;

namespace labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nДля лучшей визуализации надо:"+
                "\n\t- Зайти в свойства консоли (кликнуть правой кнопкой мыши по шапке консоли и выбрать свойства)"+
                "\n\t- Перейти на вкладку шрифты и выбрать точечные шрифты (теоретически самый последний)"+
                "\n\t- Поменять размер шрифта на '8 * 9'");

            Kiselev_Andrey.StartMenu.EnterClearConsole();

            while (true)
            {
                int height = Kiselev_Andrey.ConsoleRead.IntMore("Input labyrinth height: ", 2);
                int width = Kiselev_Andrey.ConsoleRead.IntMore("Input labyrinth width: ", 2);

                Kiselev_Andrey.StartMenu.EnterClearConsole();

                new Theseus(new Labyrinth(height, width));

                Console.WriteLine("\nPress Enter for new Labyrinth");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }


}
