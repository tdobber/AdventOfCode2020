using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Helpers;

namespace AdventOfCode._2015
{
    public static class Day2
    {
        private static List<string> Input => InputHelper.GetInputListString(2015, 2).ToList();

        private static int Part1()
        {
            int length, width, height, wrappingPaper = 0;

            foreach(string present in Input)
            {
                string[] dimensions = present.Split('x');

                length = int.Parse(dimensions[0]);
                width = int.Parse(dimensions[1]);
                height = int.Parse(dimensions[2]);

                wrappingPaper += (2 * length * width) + (2 * width * height) + (2 * height * length) +
                    Math.Min(length * width, Math.Min(width * height, height * length));
            }

            return wrappingPaper;
        }

        private static int Part2()
        {
            int length, width, height, ribbon = 0;

            foreach (string present in Input)
            {
                string[] dimensions = present.Split('x');

                length = int.Parse(dimensions[0]);
                width = int.Parse(dimensions[1]);
                height = int.Parse(dimensions[2]);

                ribbon += Math.Min((2 * length) + (2* width), Math.Min((2 * width) + (2 * height),
                    (2 * height) + (2 * length))) + (length * width * height);
            }

            return ribbon;
        }

        public static void Start()
        {
            Console.WriteLine($"The answer to part 1 is {Part1()} square feet of wrapping paper.");
            Console.WriteLine($"The answer to part 2 is {Part2()} square feet of ribbon.");
        }
    }
}