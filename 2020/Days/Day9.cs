using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day9
    {
        private static List<long> Input => InputHelper.GetInputListLong(2020, 9).ToList();

        private static int Part1()
        {
            List<long> preamble = Input.ToArray()[0..25].ToList();

            for (int i = 25; i < Input.Count(); i++)
            {
                if (preamble.Any(num => preamble.Contains(Input[i] - num) && (Input[i] / 2 != num)))
                {
                    preamble.RemoveAt(0);
                    preamble.Add(Input[i]);
                }
                else
                {
                    Console.WriteLine($"The following number is not correct {Input[i]}");
                    return i;
                }
            }

            return 0;
        }

        private static void Part2(int index)
        {
            long errorNumber = Input[index];
            List<long> validRange = Input.ToArray()[0..index].ToList();

            for (int i = 0; i < index - 1; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    List<long> range = validRange.ToArray()[i..j].ToList();
                    if (range.Sum() == errorNumber)
                    {
                        Console.WriteLine($"The smallest and largest numbers of the range are {range.Min()} and {range.Max()} which add up to {range.Min() + range.Max()}");
                    }
                    else if(range.Sum() > errorNumber)
                    {
                        break;
                    }
                }
            }
        }

        public static void Start()
        {
            int index = Part1();

            Part2(index);
        }
    }
}
