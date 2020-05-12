using System;
using System.Collections.Generic;
using System.Linq;
using Kiselev_Andrey;

namespace HW_15
{
    class Program
    {
        static void Main(string[] args)
        {
            Fibonachy();
            Obratny();
            InvertedArray();
            PrintIfMore();
            Progressions();
        }

        static void Fibonachy()
        {
            Console.WriteLine("\n\tFibonachy\n\n");
            int num = ConsoleRead.Int("Imput count elements: ");
            int[] fibonachi = new int[num];
            for (int i = 0; i < num; i++)
            {
                int k = i - 2;
                if (k < 0) fibonachi[i] = i;
                else
                {
                    fibonachi[i] = fibonachi[i - 1] + fibonachi[k];
                }
            }
            Console.WriteLine($"\nFibonachi array:\n");
            for (int i = 0; i < num; i++)
            {
                Console.Write(fibonachi[i] + " ");
            }
            StartMenu.EnterClearConsole();
        }

        static void Obratny()
        {
            Console.WriteLine("\n\tInverted num\n\n");
            int num = ConsoleRead.Int("Imput number: ");
            int res = 0, temp = num;
            while (temp != 0)
            {
                res *= 10;
                res += temp % 10;
                temp /= 10;
            }
            Console.WriteLine($"Inverted number: {res}");
            StartMenu.EnterClearConsole();
        }

        static void InvertedArray()
        {
            Console.WriteLine("\n\tInverted array\n\n");
            Random rand = new Random();
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(-10, 10);
            }
            Kiselev_Andrey.Array.Print(arr, "Array:");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] *= -1;
            }
            Kiselev_Andrey.Array.Print(arr, "Inverted array:");
            StartMenu.EnterClearConsole();
        }

        static void PrintIfMore()
        {
            Console.WriteLine("\n\tPrint num if more by previor\n\n");
            Random rand = new Random();
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(-10, 10);
            }
            Kiselev_Andrey.Array.Print(arr, "Array:");

            List<int> res = new List<int>();
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > arr[i-1])
                {
                    res.Add(arr[i]);
                }
            }

            Console.WriteLine("\nResult:");
            foreach(var item in res)
            {
                Matrix.IntPrintBeautiful(item);
            }
            StartMenu.EnterClearConsole();
        }

        static void Progressions()
        {
            Console.WriteLine("\n\tArithmetic and Geometric progression\n\n");

            int startNum = ConsoleRead.Int("Input start num: ");
            int increment = ConsoleRead.Int("Input increment: ");
            int countNum = ConsoleRead.Int("Input number of elements: ");

            int[] arrA = new int[countNum];
            int[] arrG = new int[countNum];

            arrA[0] = startNum;
            arrG[0] = startNum;

            for (int i = 1; i < countNum; i++)
            {
                arrA[i] = arrA[i - 1] + increment;
                arrG[i] = arrG[i - 1] * increment;
            }

            Kiselev_Andrey.Array.Print(arrA, "Arithmetic progression:");
            Kiselev_Andrey.Array.Print(arrG, "Geometric progression:");

            StartMenu.EnterClearConsole();
        }
    }
}
