using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiselev_Andrey
{
    public static class Dict
    {
        public static K KeyByValue<K, V>(Dictionary<K, V> dict, V value)
        {
            K key = default;
            foreach (var pair in dict)
            {
                if (Equals(pair.Value, value))
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }

        public static K KeyByValueLinq<K, V>(Dictionary<K, V> dict, V value)
        {
            return dict.FirstOrDefault(x => Equals(x.Value, value)).Key;
        }

        public static K KeyByValue<K, V>(SortedDictionary<K, V> dict, V value)
        {
            return dict.FirstOrDefault(x => Equals(x.Value, value)).Key;
        }

        public static byte ReadConsoleByte<T>(string text, Dictionary<byte, T> dict)
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
                    Console.WriteLine($"Input is mot included in Keys");
                    continue;
                }
                return num;
            }
        }
    }
}
