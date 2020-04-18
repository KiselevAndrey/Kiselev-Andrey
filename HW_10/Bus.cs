using System;
using System.Collections.Generic;
using System.Text;

namespace HW_10
{
    class Bus : Car
    {
        public int CountPassager { get; set; }

        public Bus(int odometr, int fuel, int countPassager) : base(odometr, fuel)
        {
            CountPassager = countPassager;
        }

        public override string ToString()
        {
            return $"I'm a bus. My odometr - {Odometr}. My fuel - {Fuel}. My passager count - {CountPassager}";
        }

        public static Bus operator ++(Bus bus)
        {
            bus.Odometr++;
            return bus;
        }
    }
}
