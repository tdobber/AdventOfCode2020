using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day17
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 17).ToList();

        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int W { get; set; }
            public bool Active { get; set; } = false;
        }

        private static List<Point> LoadPoints()
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < Input.Count; i++)
            {
                for (int j = 0; j < Input[0].Length; j++)
                {
                    Point point = new Point
                    {
                        X = j,
                        Y = i,
                        Z = 0,
                        W = 0,
                    };

                    if (Input[i][j] == '#')
                    {
                        point.Active = true;
                    }

                    points.Add(point);
                }
            }

            return points;
        }

        private static Point CheckNeighbours(int x, int y, int z, List<Point> currentPoints)
        {
            int activeNeighbours = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (i == 0 && j == 0 && k == 0)
                        {
                            continue;
                        }

                        Point neighbour = currentPoints.FirstOrDefault(nb => nb.X == x + i && nb.Y == y + j && nb.Z == z + k);

                        if (neighbour != null && neighbour.Active)
                        {
                            activeNeighbours++;
                        }
                    }
                }
            }

            Point pointToCheck = currentPoints.FirstOrDefault(nb => nb.X == x && nb.Y == y && nb.Z == z);
            Point pointToReturn = null;

            // Initialize pointToReturn only when there is a change, this way the list will not be flooded with inactive points.
            if (pointToCheck != null)
            {
                if (pointToCheck.Active && (activeNeighbours != 2 && activeNeighbours != 3))
                {
                    pointToReturn = new Point()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Active = false
                    };
                }
                else if (pointToCheck.Active && (activeNeighbours == 2 || activeNeighbours == 3))
                {
                    pointToReturn = new Point()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Active = true
                    };
                }
                else if (!pointToCheck.Active && activeNeighbours == 3)
                {
                    pointToReturn = new Point()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Active = true
                    };
                }
            }
            else if (pointToCheck == null && activeNeighbours == 3)
            {
                pointToReturn = new Point()
                {
                    X = x,
                    Y = y,
                    Z = z,
                    Active = true
                };
            }

            return pointToReturn;
        }

        private static Point CheckNeighbours4D(int x, int y, int z, int w, List<Point> currentPoints)
        {
            int activeNeighbours = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            if (i == 0 && j == 0 && k == 0 && l == 0)
                            {
                                continue;
                            }

                            Point neighbour = currentPoints.FirstOrDefault(nb => nb.X == x + i && nb.Y == y + j && nb.Z == z + k && nb.W == w + l);

                            if (neighbour != null && neighbour.Active)
                            {
                                activeNeighbours++;
                            }
                        }
                    }
                }
            }

            Point pointToCheck = currentPoints.FirstOrDefault(nb => nb.X == x && nb.Y == y && nb.Z == z && nb.W == w);
            Point pointToReturn = null;

            // Initialize pointToReturn only when there is a change, this way the list will not be flooded with inactive points.
            if (pointToCheck != null)
            {
                if (pointToCheck.Active && (activeNeighbours != 2 && activeNeighbours != 3))
                {
                    pointToReturn = new Point()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        W = w,
                        Active = false
                    };
                }
                else if (pointToCheck.Active && (activeNeighbours == 2 || activeNeighbours == 3))
                {
                    pointToReturn = new Point()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        W = w,
                        Active = true
                    };
                }
                else if (!pointToCheck.Active && activeNeighbours == 3)
                {
                    pointToReturn = new Point()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        W = w,
                        Active = true
                    };
                }
            }
            else if (pointToCheck == null && activeNeighbours == 3)
            {
                pointToReturn = new Point()
                {
                    X = x,
                    Y = y,
                    Z = z,
                    W = w,
                    Active = true
                };
            }

            return pointToReturn;
        }

        private static void RunCycles(int cycles, List<Point> points)
        {
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                List<Point> newPoints = new List<Point>();

                for (int i = points.Min(p => p.X) - 1; i <= points.Max(p => p.X) + 1; i++)
                {
                    for (int j = points.Min(p => p.Y) - 1; j <= points.Max(p => p.Y) + 1; j++)
                    {
                        for (int k = points.Min(p => p.Z) - 1; k <= points.Max(p => p.Z) + 1; k++)
                        {
                            Point point = CheckNeighbours(i, j, k, points);

                            if (point != null)
                            {
                                newPoints.Add(point);
                            }
                        }
                    }
                }

                points = newPoints;
            }

            Console.WriteLine($"Done -> active = {points.Count(p => p.Active)}");
        }

        private static void RunCycles4D(int cycles, List<Point> points)
        {
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                List<Point> newPoints = new List<Point>();

                for (int i = points.Min(p => p.X) - 1; i <= points.Max(p => p.X) + 1; i++)
                {
                    for (int j = points.Min(p => p.Y) - 1; j <= points.Max(p => p.Y) + 1; j++)
                    {
                        for (int k = points.Min(p => p.Z) - 1; k <= points.Max(p => p.Z) + 1; k++)
                        {
                            for (int l = points.Min(p => p.W) - 1; l <= points.Max(p => p.W) + 1; l++)
                            {
                                Point point = CheckNeighbours4D(i, j, k, l, points);

                                if (point != null)
                                {
                                    newPoints.Add(point);
                                }
                            }
                        }
                    }
                }

                points = newPoints;
            }

            Console.WriteLine($"Done -> active = {points.Count(p => p.Active)}");
        }

        private static void Part1()
        {
            List<Point> points = LoadPoints();
            RunCycles(6, points);
        }

        private static void Part2()
        {
            List<Point> points = LoadPoints();
            RunCycles4D(6, points);
        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
