using System;

namespace labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                new Labyrinth(40, 80);
                Console.WriteLine();
                Console.ReadLine();
                Console.Clear();
            }
        }
    }


}
