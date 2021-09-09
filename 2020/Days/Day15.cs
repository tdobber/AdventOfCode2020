using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day15
    {
        private static List<int> Input => InputHelper.GetInputListString(2020, 15).ToList()[0].Split(",").Select(int.Parse).ToList();

        public static int Play(List<int> numbers, int iterations)
        {
            Dictionary<int, int> numbersSpoken = new Dictionary<int, int>();
            int lastNumber = -1;

            for (int i = 0; i < iterations; i++)
            {
                if (i < numbers.Count)
                {
                    numbersSpoken.Add(numbers[i], i + 1);
                }
                else
                {
                    int nextNumber;

                    if (lastNumber == -1)
                    {
                        lastNumber = numbers[numbers.Count - 1];
                    }

                    if (numbersSpoken.ContainsKey(lastNumber))
                    {
                        nextNumber = i - numbersSpoken[lastNumber];
                    }
                    else
                    {
                        nextNumber = 0;
                    }

                    numbersSpoken[lastNumber] = i;
                    lastNumber = nextNumber;
                }
            }

            return lastNumber;
        }

        private static void Part1()
        {
            int lastSpoken = Play(Input, 2020);

            Console.WriteLine($"The 2020th number spoken is {lastSpoken}");
        }

        private static void Part2()
        {
            int lastSpoken = Play(Input, 30000000);

            Console.WriteLine($"The 30000000th number spoken is {lastSpoken}");

        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
