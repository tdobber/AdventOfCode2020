using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day1
    {
        private static List<int> Input => InputHelper.GetInputListInt(2020, 1).ToList();

        private static int Part1()
        {
            foreach (int i in Input)
            {
                if (Input.Contains(2020 - i))
                {
                    return i;
                }
            }

            return 0;
        }

        private static void Part2()
        {
            for (int i = 0; i < Input.Count; i++)
            {
                for (int j = i + 1; j < Input.Count; j++)
                {
                    int check = (2020 - Input[i] - Input[j]);
                    if (Input.Contains(check))
                    {
                        Console.WriteLine($"The answer to part 2 is {Input[i]} x {Input[j]} x {check} = {Input[i] * Input[j] * check}");
                    }
                }
            }
        }


        public static void Start()
        {
            int result = Part1();
            Console.WriteLine($"The answer to part 1 is {result} x {(2020 - result)} = {result * (2020 - result)}");

            Part2();
        }
    }
}
