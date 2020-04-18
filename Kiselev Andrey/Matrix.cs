using System;
using System.Collections.Generic;
using System.Text;

namespace Kiselev_Andrey
{
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

        public static T[] Geting<T>(T[][] matrix, int index)
        {
            while (index >= matrix.Length)
                index -= matrix.Length;
            return matrix[index];
        }

        public static T Geting<T>(T[][] matrix, int i, int j)
        {
            while (i >= matrix.Length)
                i -= matrix.Length;
            while (j >= matrix[i].Length)
                j -= matrix[i].Length;
            return matrix[i][j];
        }

        public static void Enter()
        {
            Console.WriteLine("\n\nPress Enter.");
            Console.ReadLine();
        }
    }
}
