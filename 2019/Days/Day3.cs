using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Point
    {
        public int X;
        public int Y;
        public int DistanceCovered;

        public Point(int x, int y, int distanceCovered = 0)
        {
            X = x;
            Y = y;
            DistanceCovered = distanceCovered;
        }

        public override string ToString()
        {
            return $"({X},{Y},{DistanceCovered})";
        }
    }

    public class Wire
    {
        public List<Point> Points { get; set; } = new List<Point>();
        public Point CurrentPoint { get; set; }

        public Wire()
        {
            CurrentPoint = new Point(0, 0);
            Points.Add(CurrentPoint);
        }

        public void AddPoint(string step)
        {
            char direction = step.First();
            step = step.Remove(0, 1);
            int distance = int.Parse(step);
            Point nextPoint = new Point(CurrentPoint.X, CurrentPoint.Y, CurrentPoint.DistanceCovered + distance);

            switch (direction)
            {
                case 'R':
                    nextPoint.X += distance;
                    break;

                case 'L':
                    nextPoint.X -= distance; 
                    break;

                case 'U':
                    nextPoint.Y += distance;
                    break;

                case 'D':
                    nextPoint.Y -= distance;
                    break;
            }

            Points.Add(nextPoint);
            CurrentPoint = nextPoint;
        }

        public override string ToString()
        {
            string rv = "[";

            foreach (Point point in Points)
            {
                rv += point.ToString();
            }
            rv += "]";

            return rv;
        }
    }

    public static class Day3
    {
        private static List<string> Input => InputHelper.GetInputListString(2019, 3).ToList();

        public static void Part1(string wire1, string wire2)
        {
            Wire firstWire = new Wire();
            List<string> inputWire1 = wire1.Split(',').ToList();

            foreach (string step in inputWire1)
            {
                firstWire.AddPoint(step);
            }

            List<Point> crossPoints = new List<Point>();
            Wire secondWire = new Wire();
            List<string> inputWire2 = wire2.Split(',').ToList();
            int totalDistanceSecondWire = 0;

            foreach (string step in inputWire2)
            {
                Point first = secondWire.Points.Last();
                secondWire.AddPoint(step);
                Point second = secondWire.Points.Last();

                totalDistanceSecondWire = first.DistanceCovered;

                for (int i = 0; i < firstWire.Points.Count - 1; i ++)
                {
                    int distance = 0;
                    if (first.Y == second.Y && firstWire.Points[i].X == firstWire.Points[i + 1].X)
                    {
                        if (Math.Min(first.X, second.X) <= firstWire.Points[i].X && firstWire.Points[i].X <= Math.Max(first.X, second.X) &&
                            Math.Min(firstWire.Points[i].Y, firstWire.Points[i + 1].Y) <= first.Y && first.Y <= Math.Max(firstWire.Points[i].Y, firstWire.Points[i + 1].Y))
                        {
                            distance = firstWire.Points[i].DistanceCovered + Math.Abs(firstWire.Points[i].Y - first.Y) + totalDistanceSecondWire + Math.Abs(firstWire.Points[i].X - first.X);
                            Point cross = new Point(firstWire.Points[i].X, first.Y, distance);
                            crossPoints.Add(cross);
                        }
                    }
                    else if (first.X == second.X && firstWire.Points[i].Y == firstWire.Points[i + 1].Y)
                    {
                        if (Math.Min(first.Y, second.Y) <= firstWire.Points[i].Y && firstWire.Points[i].Y <= Math.Max(first.Y, second.Y) &&
                            Math.Min(firstWire.Points[i].X, firstWire.Points[i + 1].X) <= first.X && first.X <= Math.Max(firstWire.Points[i].X, firstWire.Points[i + 1].X))
                        {
                            distance = firstWire.Points[i].DistanceCovered + Math.Abs(firstWire.Points[i].X - first.X) + totalDistanceSecondWire + Math.Abs(firstWire.Points[i].Y - first.Y);
                            Point cross = new Point(first.X, firstWire.Points[i].Y, distance);
                            crossPoints.Add(cross);
                        }
                    }
                }
            }

            List<int> manhattanDistances = new List<int>();
            List<int> travelDistances = new List<int>();
            foreach (Point point in crossPoints)
            {
                manhattanDistances.Add(Math.Abs(point.X) + Math.Abs(point.Y));
                travelDistances.Add(point.DistanceCovered);
            }

            manhattanDistances.Sort();
            travelDistances.Sort();

            Console.WriteLine($"lowest manhattan distance {manhattanDistances[0]} and then {manhattanDistances[1]}");
            Console.WriteLine($"lowest travel distance {travelDistances[0]} and then {travelDistances[1]}");

            return;
        }

        public static void Start()
        {
            string wire1 = Input[0];
            string wire2 = Input[1];

            //string wire1 = "R8,U5,L5,D3";
            //string wire2 = "U7,R6,D4,L4";
            //string wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
            //string wire2 = "U62,R66,U55,R34,D71,R55,D58,R83";

            Part1(wire1, wire2);
        }
    }
}
