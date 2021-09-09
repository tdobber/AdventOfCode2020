using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class AsteroidMap
    {
        public List<string> Map { get; set; } = new List<string>();
        public List<Point> AsteroidPoints { get; set; } = new List<Point>();
        public int MostSightings { get; set; } = 0;
        public Point MostSightingPoint { get; set; } = new Point(0, 0);
        public HashSet<Point> SightingPoints { get; set; } = new HashSet<Point>();
        public SortedDictionary<double, Point> SlopePointDict = new SortedDictionary<double, Point>();

        public AsteroidMap(List<string> input)
        {
            Map.InsertRange(0, input);
        }

        public override string ToString()
        {
            string returnValue = "";
            foreach (string line in Map)
            {
                returnValue += line + "\n";
            }
            return returnValue;
        }
    }

    public static class Day10
    {
        private static List<string> Input => InputHelper.GetInputListString(2019, 10).ToList();

        public static double? CalculateAngle(Point point1, Point point2)
        {
            if (point1 == point2)
            {
                return null;
            }

            return Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
        }

        public static AsteroidMap Part1()
        {
            AsteroidMap asteroidMap = new AsteroidMap(Input);

            for (int i = 0; i < asteroidMap.Map.Count; i++)
            {
                for (int j = 0; j < asteroidMap.Map[0].Length; j++)
                {
                    if (asteroidMap.Map[i][j] == '#')
                    {
                        Point point = new Point(j , i);
                        asteroidMap.AsteroidPoints.Add(point);
                    }
                    
                }
            }

            foreach (Point point1 in asteroidMap.AsteroidPoints)
            {
                HashSet<double> sightingSlopes = new HashSet<double>();
                HashSet<Point> sightedPoints = new HashSet<Point>();
                SortedDictionary<double, Point> slopePoints = new SortedDictionary<double, Point>();

                foreach (Point point2 in asteroidMap.AsteroidPoints)
                {
                    double? angle = CalculateAngle(point1, point2);
                    if (angle != null)
                    {
                        if (!sightingSlopes.Contains((double)angle))
                        {
                            sightingSlopes.Add((double)angle);
                            sightedPoints.Add(point2);
                            slopePoints[(double)angle] = point2;
                        }
                        else
                        {
                            int manhattan1 = Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
                            int manhattan2 = Math.Abs(point1.X - slopePoints[(double)angle].X) + Math.Abs(point1.Y - slopePoints[(double)angle].Y);
                            if (manhattan2 > manhattan1)
                            {
                                slopePoints[(double)angle] = point2;
                            }
                        }
                    }
                }

                if (asteroidMap.MostSightings < sightingSlopes.Count)
                {
                    asteroidMap.MostSightings = sightingSlopes.Count;
                    asteroidMap.MostSightingPoint = point1;
                    asteroidMap.SightingPoints = sightedPoints;
                    asteroidMap.SlopePointDict = slopePoints;
                }
            }

            Console.WriteLine($"The highest possible sightings is: {asteroidMap.MostSightings} from point {asteroidMap.MostSightingPoint}.");

            return asteroidMap;
        }

        public static void Part2(AsteroidMap asteroidMap)
        {
            asteroidMap.Map[asteroidMap.MostSightingPoint.Y] = asteroidMap.Map[asteroidMap.MostSightingPoint.Y].Substring(0, asteroidMap.MostSightingPoint.X) + "O" + asteroidMap.Map[asteroidMap.MostSightingPoint.Y].Substring(asteroidMap.MostSightingPoint.X + 1);
            Console.WriteLine(asteroidMap);
            int vaporizedAsteroids = 0;
            List<Point> vaporized = new List<Point>();
            List<double> keys = asteroidMap.SlopePointDict.Keys.ToList();
            int firstAsteroidIndex = keys.FindIndex(x => x >= -Math.PI / 2);
            List<double> keysToVaporize = keys.GetRange(firstAsteroidIndex, keys.Count - firstAsteroidIndex);
            keysToVaporize.AddRange(keys.GetRange(0, keys.Count - keysToVaporize.Count));
            foreach (double key in keysToVaporize)
            {
                Point point = asteroidMap.SlopePointDict[key];
                asteroidMap.Map[point.Y] = asteroidMap.Map[point.Y].Substring(0, point.X) + "." + asteroidMap.Map[point.Y].Substring(point.X + 1);
                vaporizedAsteroids++;
                vaporized.Add(point);
                if (vaporized.Count == 200)
                {
                    Console.WriteLine($"The 200th asteroid destroyed is: {point}.");
                    break;
                }
            }

            return;
        }

        public static void Start()
        {
            //List<string> input = new List<string> { ".#..#", ".....", "#####", "....#", "...##" };
            //List<string> input = new List<string> { "......#.#.", "#..#.#....", "..#######.", ".#.#.###..", ".#..#.....", "..#....#.#", "#..#....#.", ".##.#..###", "##...#..#.", ".#....####" };
            //List<string> input = new List<string> { "#.#...#.#.", ".###....#.", ".#....#...", "##.#.#.#.#", "....#.#.#.", ".##..###.#", "..#...##..", "..##....##", "......#...", ".####.###." };

            AsteroidMap asteroidMap = Part1();
            Part2(asteroidMap);
        }
    }
}
