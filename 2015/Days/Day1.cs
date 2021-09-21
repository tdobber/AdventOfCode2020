using System;
using System.Linq;
using AdventOfCode.Helpers;

namespace AdventOfCode._2015
{
    public static class Day1
    {
        private static string Input => InputHelper.GetInputString(2015, 1);

        private static int Part1() =>
            Input.Count(x => x == '(') - Input.Count(x => x == ')');

        private static int Part2()
        {
            int currentFloor = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                if (Input[i] == '(')
                {
                    currentFloor++;
                }
                else if (Input[i] == ')')
                {
                    currentFloor--;
                }

                if (currentFloor == -1)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        public static void Start()
        {
            Console.WriteLine($"The answer to part 1 is floor {Part1()}.");
            Console.WriteLine($"The answer to part 2 is position {Part2()}.");
        }
    }
}