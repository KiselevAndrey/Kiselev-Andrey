using System;

namespace HW_10
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car(167, 43);
            Bus bus = new Bus(19985, 85, 45);

            Console.WriteLine($"{car}\n{bus}");
            
            Console.WriteLine($"\nFuel in car - 6 = {car - 6}");
            Console.WriteLine($"Fuel in bus - 17 = {bus - 17}");

            Console.WriteLine("\nUp odometr ++");
            car++;
            bus++;

            Console.WriteLine($"{car}\n{bus}");

            Console.ReadLine();
        }
    }
}
