using System;
using System.Collections.Generic;

namespace KeyValue
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", "Kira");
            Console.WriteLine($"Hello, {dict["name"]}");
            Console.ReadLine();
        }
    }
}
