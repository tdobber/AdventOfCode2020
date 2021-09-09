using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class HullPaintingRobot
    {
        public Dictionary<Point, int> PaintedPanels { get; set; } = new Dictionary<Point, int>();
        public Point CurrentPoint { get; set; } = new Point(0, 0);
        public Direction Direction = Direction.North;
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static class Day11
    {
        private static List<double> Input => InputHelper.GetInputString(2019, 11).Split(',').Select(double.Parse).ToList();

        public static void ProcessOps(List<double> programInput, HullPaintingRobot robot, bool firstTileIsWhite = false)
        {
            int ptr = 0;
            int relativeBase = 0;
            bool firstOutput = true;

            while (true)
            {
                string instruction = programInput[ptr].ToString();
                int opcode;

                if (instruction.Length >= 2)
                    opcode = int.Parse(instruction.Substring(instruction.Length - 2, 2));
                else
                    opcode = int.Parse(instruction);

                if (opcode == 99)
                {
                    break;
                }

                int firstParameterMode = instruction.Length > 2 ? int.Parse(instruction.Substring(instruction.Length - 3, 1)) : 0;
                int secondParameterMode = instruction.Length > 3 ? int.Parse(instruction.Substring(instruction.Length - 4, 1)) : 0;
                int thirdParameterMode = instruction.Length > 4 ? int.Parse(instruction.Substring(instruction.Length - 5, 1)) : 0;

                int indexParameter1 = 0;
                int indexParameter2 = 0;
                int indexParameter3 = 0;

                if (firstParameterMode == 0)
                    indexParameter1 = Convert.ToInt32(programInput[ptr + 1]);
                else if (firstParameterMode == 1)
                    indexParameter1 = ptr + 1;
                else if (firstParameterMode == 2)
                    indexParameter1 = Convert.ToInt32(programInput[ptr + 1]) + relativeBase;

                if (opcode == 1 || opcode == 2 || opcode == 5 || opcode == 6 || opcode == 7 || opcode == 8)
                {
                    if (secondParameterMode == 0)
                        indexParameter2 = Convert.ToInt32(programInput[ptr + 2]);
                    else if (secondParameterMode == 1)
                        indexParameter2 = ptr + 2;
                    else if (secondParameterMode == 2)
                        indexParameter2 = Convert.ToInt32(programInput[ptr + 2]) + relativeBase;
                }

                if (opcode == 1 || opcode == 2 || opcode == 7 || opcode == 8)
                {
                    if (thirdParameterMode == 0)
                        indexParameter3 = Convert.ToInt32(programInput[ptr + 3]);
                    else if (thirdParameterMode == 1)
                        indexParameter3 = ptr + 3;
                    else if (thirdParameterMode == 2)
                        indexParameter3 = Convert.ToInt32(programInput[ptr + 3]) + relativeBase;
                }

                switch (opcode)
                {
                    case 1:
                        if (thirdParameterMode == 1)
                            Console.WriteLine("This should never happen!");

                        programInput[indexParameter3] = programInput[indexParameter1] + programInput[indexParameter2];
                        ptr += 4;
                        break;

                    case 2:
                        if (thirdParameterMode == 1)
                            Console.WriteLine("This should never happen!");

                        programInput[indexParameter3] = programInput[indexParameter1] * programInput[indexParameter2];
                        ptr += 4;
                        break;

                    case 3:
                        if (firstParameterMode == 1)
                            Console.WriteLine("This should never happen!");

                        //Console.Write("Reached opcode 3, please enter value: ");
                        //programInput[indexParameter1] = int.Parse(Console.ReadLine());
                        if (robot.PaintedPanels.Keys.Any(point => point.X == robot.CurrentPoint.X && point.Y == robot.CurrentPoint.Y))
                        {
                            programInput[indexParameter1] = robot.PaintedPanels[robot.PaintedPanels.Keys.First(point => point.X == robot.CurrentPoint.X && point.Y == robot.CurrentPoint.Y)];
                        }
                        else if (firstTileIsWhite)
                        {
                            programInput[indexParameter1] = 1;
                            firstTileIsWhite = false;
                        }
                        else
                        {
                            programInput[indexParameter1] = 0;
                        }

                        //Console.WriteLine($"input: {programInput[indexParameter1]}");

                        ptr += 2;
                        break;

                    case 4:
                        //Console.WriteLine($"output: {programInput[indexParameter1]}");

                        // Paint the panel
                        if (firstOutput)
                        {
                            if (robot.PaintedPanels.Keys.Any(point => point.X == robot.CurrentPoint.X && point.Y == robot.CurrentPoint.Y))
                            {
                                robot.PaintedPanels[robot.PaintedPanels.Keys.First(point => point.X == robot.CurrentPoint.X && point.Y == robot.CurrentPoint.Y)] = (int)programInput[indexParameter1];
                            }
                            else
                            {
                                robot.PaintedPanels[robot.CurrentPoint] = (int)programInput[indexParameter1];
                            }

                            firstOutput = false;
                        }
                        // Change direction and make a step.
                        else
                        {
                            if (programInput[indexParameter1] == 0)
                            {
                                robot.Direction = (Direction)(((int)robot.Direction + 3) % 4);
                            }
                            else if (programInput[indexParameter1] == 1)
                            {
                                robot.Direction = (Direction)(((int)robot.Direction + 1) % 4);
                            }

                            switch (robot.Direction)
                            {
                                case Direction.North:
                                    robot.CurrentPoint = new Point(robot.CurrentPoint.X, robot.CurrentPoint.Y + 1);
                                    break;

                                case Direction.East:
                                    robot.CurrentPoint = new Point(robot.CurrentPoint.X + 1, robot.CurrentPoint.Y);
                                    break;

                                case Direction.South:
                                    robot.CurrentPoint = new Point(robot.CurrentPoint.X, robot.CurrentPoint.Y - 1);
                                    break;

                                case Direction.West:
                                    robot.CurrentPoint = new Point(robot.CurrentPoint.X - 1, robot.CurrentPoint.Y);
                                    break;
                            }

                            firstOutput = true;
                        }

                        ptr += 2;
                        break;

                    case 5:
                        if (programInput[indexParameter1] != 0)
                            ptr = Convert.ToInt32(programInput[indexParameter2]);
                        else
                            ptr += 3;
                        break;

                    case 6:
                        if (programInput[indexParameter1] == 0)
                            ptr = Convert.ToInt32(programInput[indexParameter2]);
                        else
                            ptr += 3;
                        break;

                    case 7:
                        if (thirdParameterMode == 1)
                            Console.WriteLine("This should never happen!");

                        if (programInput[indexParameter1] < programInput[indexParameter2])
                            programInput[indexParameter3] = 1;
                        else
                            programInput[indexParameter3] = 0;

                        ptr += 4;

                        break;

                    case 8:
                        if (thirdParameterMode == 1)
                            Console.WriteLine("This should never happen!");

                        if (programInput[indexParameter1] == programInput[indexParameter2])
                            programInput[indexParameter3] = 1;
                        else
                            programInput[indexParameter3] = 0;

                        ptr += 4;

                        break;

                    case 9:
                        relativeBase += Convert.ToInt32(programInput[indexParameter1]);
                        ptr += 2;
                        break;

                    default:
                        Console.WriteLine("Something went wrong...");
                        break;
                }
            }

            return;
        }

        public static void Part1(List<double> intCodeProgram)
        {
            HullPaintingRobot robot = new HullPaintingRobot();
            ProcessOps(intCodeProgram, robot);
            Console.WriteLine($"The number of painted panels is {robot.PaintedPanels.Count}.");
        }

        public static void Part2(List<double> intCodeProgram)
        {
            HullPaintingRobot robot = new HullPaintingRobot();
            ProcessOps(intCodeProgram, robot, true);
            int minX = robot.PaintedPanels.Keys.Min(point => point.X);
            int maxX = robot.PaintedPanels.Keys.Max(point => point.X);
            int minY = robot.PaintedPanels.Keys.Min(point => point.Y);
            int maxY = robot.PaintedPanels.Keys.Max(point => point.Y);

            int maxRangeX = maxX + 1;
            int maxRangeY = maxY + 1;
            int shiftX = 0;
            int shiftY = 0;

            if (minX < 0)
            {
                shiftX = Math.Abs(minX);
                maxRangeX += shiftX;
            }
            if (minY < 0)
            {
                shiftY = Math.Abs(minY);
                maxRangeY += shiftY;
            }

            char[,] identifier = new char[maxRangeY, maxRangeX];
            foreach (KeyValuePair<Point, int> panel in robot.PaintedPanels)
            {
                if (panel.Value == 0)
                {
                    identifier[panel.Key.Y + shiftY, panel.Key.X + shiftX] = ' ';
                }
                else if (panel.Value == 1)
                {
                    identifier[panel.Key.Y + shiftY, panel.Key.X + shiftX] = '#';
                }
            }

            for (int i = 0; i < maxRangeY; i ++)
            {
                for (int j = 0; j < maxRangeX; j++)
                {
                    Console.Write(identifier[i, j].ToString());
                }
                Console.WriteLine();
            }
        }

        public static void Start()
        {
            List<double> intCodeProgram = Input;
            List<double> memory = Enumerable.Repeat<double>(0, 1000).ToList();
            intCodeProgram.AddRange(memory);

            //Part1(intCodeProgram);
            Part2(intCodeProgram);
        }
    }
}
