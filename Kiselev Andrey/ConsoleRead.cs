using System;
using System.Collections.Generic;
using System.Text;

namespace Kiselev_Andrey
{
    public static class ConsoleRead
    {
        public static byte Byte(string text)
        {
            while (true)
            {
                Console.Write(text);
                if (!byte.TryParse(Console.ReadLine(), out byte num))
                {
                    Console.WriteLine("Неправильный ввод");
                    //Console.ReadLine();
                    continue;
                }
                return num;
            }
        }

        public static byte Byte(string text, int min, int max)
        {
            while (true)
            {
                Console.Write(text);
                if (!byte.TryParse(Console.ReadLine(), out byte num))
                {
                    Console.WriteLine("Error input");
                    continue;
                }
                if (num > max || num < min)
                {
                    Console.WriteLine($"Input out of range ({min} - {max})");
                    continue;
                }
                return num;
            }
        }

        public static byte Byte<T>(string text, Dictionary<byte, T> dict)
        {
            while (true)
            {
                Console.Write(text);
                if (!byte.TryParse(Console.ReadLine(), out byte num))
                {
                    Console.WriteLine("Error input");
                    continue;
                }
                if (!dict.ContainsKey(num))
                {
                    Console.WriteLine($"Input is not included in Keys");
                    continue;
                }
                return num;
            }
        }

        public static int Int(string text)
        {
            while (true)
            {
                Console.Write(text);
                if (!int.TryParse(Console.ReadLine(), out int num))
                {
                    Console.WriteLine("Error Input! Integer is required.");
                    continue;
                }
                return num;
            }
        }

        /// <summary>
        /// Return int > 0
        /// </summary>
        /// <param name="text"> text befor input Console.Write() </param>
        /// <returns>int > 0</returns>
        public static int IntPositive(string text)
        {
            return IntMore(text, 1);
        }

        /// <summary>
        /// return int > less_num
        /// </summary>
        /// <param name="text"> text befor input Console.Write() </param>
        /// <param name="less_num"> num </param>
        /// <returns></returns>
        public static int IntMore(string text, int less_num)
        {
            while (true)
            {
                int num = Int(text);
                if (num <= less_num)
                {
                    Console.WriteLine($"Required > {less_num}");
                    continue;
                }
                return num;
            }
        }

        public static string String(string text)
        {
            while (true)
            {
                Console.Write(text);
                string s = Console.ReadLine();
                if (s.Length > 0) return s;
                Console.WriteLine("Not Empty! Try again.");
            }
        }

        public static char Char(string text)
        {
            while (true)
            {
                Console.Write(text);
                char c = Console.ReadKey().KeyChar;
                if (c == Convert.ToChar(13))
                {
                    Console.WriteLine("Don't press Enter");
                    continue;
                }
                Console.WriteLine();
                return c;
            }
        }
    }
}
