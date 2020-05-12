using System;
using System.Collections.Generic;
using System.Text;

namespace Kiselev_Andrey
{
    public static class Array
    {
        public static void Shaffle<T>(ref T[] arr)
        {
            Random rand = new Random();
            int n = arr.Length;
            while (n > 1)
            {
                int k = rand.Next(n--);
                T temp = arr[n];
                arr[n] = arr[k];
                arr[k] = temp;
            }
        }

        public static T Getting<T>(T[] arr, int index)
        {
            while (index < 0) index += arr.Length;
            while (index >= arr.Length) index -= arr.Length;

            return arr[index];
        }

        public static void Print<T>(T[] arr, string text)
        {
            Console.WriteLine(text);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.WriteLine();
        }

        public static void Print(int[] arr, string text)
        {
            Console.WriteLine($"\n{text}\n");
            for (int i = 0; i < arr.Length; i++)
            {
                Matrix.IntPrintBeautiful(arr[i]);
            }
            Console.WriteLine();
        }
    }
}
