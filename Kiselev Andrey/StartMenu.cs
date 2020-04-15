using System;
using System.Collections.Generic;
using System.Text;

namespace Kiselev_Andrey
{
    public static class StartMenu
    {
        public static byte Choiсe(string start_text, params string[] texts)
        {
            Console.WriteLine($"\n\t{start_text}\n");
            for (int i = 0; i < texts.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {texts[i]}");
            }
            Console.WriteLine("\n0. Exit");
            byte result = ConsoleRead.Byte("\nYour choise: ");

            Console.Clear();
            return result;
        }

        public static byte Choiсe<T>(string start_text, List<T> texts)
        {
            if (texts.Count == 0) throw new ArgumentOutOfRangeException("List<T>.Count == 0");
            Console.WriteLine($"\n\t{start_text}\n");
            for (int i = 0; i < texts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {texts[i].ToString()}");
            }
            byte result = ConsoleRead.Byte("\nYour choise: ", 1, texts.Count);

            Console.Clear();
            return (byte)(result - 1);
        }

        public static void EnterClearConsole()
        {
            Console.WriteLine("\n\nPress Enter.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void EnterClearConsole(string text)
        {
            Console.WriteLine(text);
            Console.WriteLine("\n\nPress Enter.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void Enter()
        {
            Console.WriteLine("\n\nPress Enter.");
            Console.ReadLine();
        }

        public static void Line(int num = 10)
        {
            Console.WriteLine();
            for (int i = 0; i < num; i++)
                Console.Write("-");
            Console.WriteLine("\n");
        }
    }
}
