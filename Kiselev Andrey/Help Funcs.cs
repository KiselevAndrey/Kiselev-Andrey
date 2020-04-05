﻿using System;
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

        public static int Int(string text)
        {
            while (true)
            {
                Console.Write(text);
                if (!int.TryParse(Console.ReadLine(), out int num))
                {
                    Console.WriteLine("Неправильный ввод! Нужно целое число");
                    //Console.ReadLine();
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
    }

    public static class Matrix
    {
        public static void Print(string text, int[,] matrix, byte count_symbol = 5)
        {
            Console.WriteLine($"\n{text}");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    IntPrintBeautiful(matrix[i, j], count_symbol);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void Print(string text, int[][] matrix, byte count_symbol = 5)
        {
            Console.WriteLine($"\n{text}");
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    IntPrintBeautiful(matrix[i][j], count_symbol);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void IntPrintBeautiful(int num, byte count_symbol = 5)
        {
            string text = num < 0 ? $"{num}" : $" {num}";
            for (int i = count_symbol; i > text.Length;)
            {
                text = text.Insert(text.Length, " ");
            }
            Console.Write(text);
        }
    }

    public static class StartMenu
    {
        public static byte Choiсe(string start_text, params string[] texts) {

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
        
        public static void EnterClearConsole()
        {
            Console.WriteLine("\n\nPress Enter.");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
