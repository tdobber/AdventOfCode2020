using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2018
{
    public static class Day1
    {
        private static List<int> Input => InputHelper.GetInputListInt(2018, 1).ToList();

        public static void Part1()
        {
            Console.WriteLine($"The resulting frequency is: {Input.Sum()}");
        }

        public static void Part2()
        {
            int frequency = 0;
            bool found = false;
            HashSet<int> frequencies = new HashSet<int>();

            while(!found)
            {
                foreach(int i in Input)
                {
                    frequency += i;
                    if (frequencies.Contains(frequency))
                    {
                        found = true;
                        break;
                    }

                    frequencies.Add(frequency);
                }
            }
            Console.WriteLine($"The first frequency that is reached twice is: {frequency}");
        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
