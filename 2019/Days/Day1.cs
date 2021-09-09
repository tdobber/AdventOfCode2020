using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day1
    {
        private static List<int> Input => InputHelper.GetInputListInt(2019, 1).ToList();

        public static List<int> CalculateFuelRequirement(List<int> input)
        {
            List<int> calculatedFuel = new List<int>();
            foreach(int item in input)
            {
                int fuelModule = (item / 3) - 2;
                calculatedFuel.Add(fuelModule);
            }

            return calculatedFuel;
        }

        public static int Part1()
        {
            List<int> totalFuelRequirement = CalculateFuelRequirement(Input);
            return totalFuelRequirement.Sum();
        }

        public static int Part2()
        {
            int totalFuelRequirement = 0;
            List<int> subtotalFuelRequirement = Input;
            while (subtotalFuelRequirement.Count != 0)
            {
                subtotalFuelRequirement = CalculateFuelRequirement(subtotalFuelRequirement);
                subtotalFuelRequirement = subtotalFuelRequirement.Where(x => x > 0).ToList();
                totalFuelRequirement += subtotalFuelRequirement.Sum();
            }

            return totalFuelRequirement;
        }

        public static void Start()
        {
            int totalFuelRequirement = Part1();
            Console.WriteLine($"The sum of the fuel requirements is: {totalFuelRequirement}");

            totalFuelRequirement = Part2();
            Console.WriteLine($"The sum of the fuel requirements for part 2 is: {totalFuelRequirement}");
        }
    }
}
