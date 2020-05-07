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

        /// <summary>
        /// Choice: select old or create new.
        /// Return from -1 to List.Count - 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start_text"></param>
        /// <param name="texts"></param>
        /// <param name="name_add_item"></param>
        /// <returns>Return from -1 to List.Count - 1</returns>
        public static sbyte Choiсe<T>(string start_text, List<T> texts, string name_add_item)
        {
            Console.WriteLine($"\n\t{start_text}\n");
            for (int i = 0; i < texts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {texts[i].ToString()}");
            }
            Console.WriteLine($"0. Create new {name_add_item}");

            byte result = ConsoleRead.Byte("\nYour choise: ", 0, texts.Count);

            Console.Clear();
            return (sbyte)(result - 1);
        }

        public static byte Choiсe<T>(string start_text, Dictionary<byte, T> texts)
        {
            Console.WriteLine($"\n\t{start_text}\n");
            foreach (var text in texts)
            {
                Console.WriteLine($"{text.Key}. {text.Value.ToString()}");
            }
            Console.WriteLine("\n0. Exit");
            byte result = ConsoleRead.Byte("\nYour choise: ");

            Console.Clear();
            return (byte)(result);
        }

        public static byte Choiсe<T>(string start_text, SortedDictionary<byte, T> texts, SortedDictionary<byte, T> hiddenTexts, bool condition)
        {
            Console.WriteLine($"\n\t{start_text}\n");

            if (condition)
                foreach (var text in hiddenTexts)
                    texts[text.Key] = text.Value;

            foreach (var text in texts)
                Console.WriteLine($"{text.Key}. {text.Value.ToString()}");

            Console.WriteLine("\n0. Exit");
            byte result = ConsoleRead.Byte("\nYour choise: ");

            Console.Clear();
            return (byte)(result);
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
