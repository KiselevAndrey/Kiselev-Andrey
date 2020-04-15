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
    }
}
