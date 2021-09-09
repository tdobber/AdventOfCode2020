using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day10
    {
        private static List<int> Input => InputHelper.GetInputListInt(2020, 10).ToList();

        private static bool CheckValidity(List<int> newTry)
        {
            for (int i = 0; i < newTry.Count - 1; i++)
            {
                if (newTry[i + 1] - newTry[i] > 3)
                {
                    return false;
                }
            }

            return true;
        }

        private static int GetCombination(List<int> list, List<int> oneDiffRange)
        {
            HashSet<List<int>> triedPossibilities = new HashSet<List<int>>();
            double count = Math.Pow(2, list.Count);

            for (int i = 1; i <= count - 1; i++)
            {
                List<int> newTry = new List<int>();
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        newTry.Add(list[j]);
                    }
                }

                newTry.Sort();

                newTry.Insert(0, oneDiffRange[0]);
                newTry.Add(oneDiffRange.Last());

                if (!triedPossibilities.Contains(newTry) && CheckValidity(newTry))
                {
                    triedPossibilities.Add(newTry);
                }
            }

            if (oneDiffRange.Count == 4)
            {
                triedPossibilities.Add(new List<int> { oneDiffRange[0], oneDiffRange.Last() });
            }

            return triedPossibilities.Count;
        }

        private static int CheckPossibilities(List<int> oneDiffRange)
        {
            if (oneDiffRange.Count == 3)
            {
                return 2;
            }

            List<int> toPermute = oneDiffRange.ToArray()[1..(oneDiffRange.Count - 1)].ToList();

            return GetCombination(toPermute, oneDiffRange);
        }

        private static void Part1()
        {
            List<int> adapters = new List<int> { 0, Input.Max() + 3 };
            adapters.AddRange(Input);
            adapters.Sort();

            int joltDiff1 = 0;
            int joltDiff3 = 0;

            for (int i = 0; i < adapters.Count - 1; i++)
            {
                if (adapters[i + 1] - adapters[i] == 1)
                {
                    joltDiff1++;
                }
                else if (adapters[i + 1] - adapters[i] == 3)
                {
                    joltDiff3++;
                }
                else if (adapters[i + 1] - adapters[i] > 3)
                {
                    break;
                }
            }

            Console.WriteLine($"One jolt differences {joltDiff1} times 3 jolt differences {joltDiff3} is {joltDiff1 * joltDiff3}");
        }

        private static void Part2()
        {
            List<int> adapters = new List<int> { 0, Input.Max() + 3};
            adapters.AddRange(Input);
            adapters.Sort();

            long possibilities = 1;
            int forwardIndex = 1;

            // input count + 1 is the same size (and 1 lower) than the original adapters size plus the 2 new ones
            for (int i = 0; i < adapters.Count - 1; i++)
            {
                while(adapters[i + forwardIndex] - adapters[i] == forwardIndex)
                {
                    forwardIndex++;
                }

                List<int> oneDiffRange = new List<int>();
                if (forwardIndex > 2)
                {
                    oneDiffRange = adapters.ToArray()[i..(i + forwardIndex)].ToList();
                }

                if (oneDiffRange.Count > 2)
                {
                    possibilities *= CheckPossibilities(oneDiffRange);
                }

                i += forwardIndex - 1;
                forwardIndex = 1;
            }

            Console.WriteLine($"The amount of distinct ways to arrange the adapters is {possibilities}");
        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
