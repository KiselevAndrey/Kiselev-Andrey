using System;
using System.Collections.Generic;
using System.Text;

namespace HW_10
{
    public class Car : ICar
    {
        public int Odometr { get; set; }
        public int Fuel { get; set; }

        public Car(int odometr, int fuel)
        {
            Odometr = odometr;
            Fuel = fuel;
        }

        public static int operator -(Car car, int minus)
        {
            return car.Fuel - minus;
        }

        public static Car operator ++(Car car)
        {
            car.Odometr++;
            return car;
        }

        public override string ToString()
        {
            return $"I'm a car. My odometr - {Odometr}. My fuel - {Fuel}";
        }
    }
}
