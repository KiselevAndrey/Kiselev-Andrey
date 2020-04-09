using System;

namespace labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                new Theseus(new Labyrinth(40, 80, 500));
                Console.WriteLine();
                Console.ReadLine();
                Console.Clear();
            }
        }
    }


}
