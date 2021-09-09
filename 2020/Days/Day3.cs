using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day3
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 3).ToList();

        public class MountainSlope
        {
            public char[,] TreeLocation { get; set; }
            public int TreeHit { get; set; }
            public int SizeX { get; set; }
            public int SizeY { get; set; }
            public int SlopeX { get; set; } = 3;
            public int SlopeY { get; set; } = 1;

            public void CountTrees()
            {
                int locationX = 0;

                for (int i = 0; i < SizeY; i += SlopeY)
                {
                    if (TreeLocation[i, locationX] == '#')
                    {
                        TreeHit++;
                    }

                    locationX += SlopeX;
                    locationX %= SizeX;
                }

                return;
            }
        }

        public static char[,] LoadMountainSlope(char[,] mountainSlope)
        {
            for (int i = 0; i < Input.Count; i++)
            {
                for (int j = 0; j < Input[i].Length; j++)
                {
                    mountainSlope[i, j] = Input[i][j];
                }
            }

            return mountainSlope;
        }

        private static void Part1(char[,] mountainSlope, int sizeY, int sizeX)
        {
            MountainSlope slope = new MountainSlope
            {
                TreeLocation = mountainSlope,
                SizeX = sizeX,
                SizeY = sizeY
            };

            slope.CountTrees();

            Console.WriteLine($"On the ride down you will encounter {slope.TreeHit} trees.");
        }

        private static void Part2(char[,] mountainSlope, int sizeY, int sizeX)
        {
            List<Tuple<int, int>> slopes = new List<Tuple<int, int>> { new Tuple<int, int>(1, 1), new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1), new Tuple<int, int>(7, 1), new Tuple<int, int>(1, 2) };

            MountainSlope slope = new MountainSlope
            {
                TreeLocation = mountainSlope,
                SizeX = sizeX,
                SizeY = sizeY
            };

            List<long> amountTreesHit = new List<long>();

            foreach (Tuple<int, int> otherSlope in slopes)
            {
                slope.SlopeX = otherSlope.Item1;
                slope.SlopeY = otherSlope.Item2;
                slope.TreeHit = 0;

                slope.CountTrees();
                amountTreesHit.Add(slope.TreeHit);
            }

            Console.WriteLine($"The multiplication of all the trees hit is {amountTreesHit.Aggregate((a, x) => a * x)}");
        }


        public static void Start()
        {
            char[,] mountainSlope = new char[Input.Count, Input[0].Length];
            mountainSlope = LoadMountainSlope(mountainSlope);
            Part1(mountainSlope, Input.Count, Input[0].Length);

            Part2(mountainSlope, Input.Count, Input[0].Length);
        }
    }
}
