using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Tile
    {
        public Point Point { get; set; } = new Point(0, 0);
        public int TileId { get; set; } = 0;
    }

    public class Breakout
    {
        public List<Tile> Tiles { get; set; } = new List<Tile>();
    }

    public static class Day13
    {
        private static List<double> Input => InputHelper.GetInputString(2019, 13).Split(',').Select(double.Parse).ToList();

        public static void ProcessOps(List<double> programInput, Breakout breakout)
        {
            int ptr = 0;
            bool playing = false;
            int outputNumber = 0;
            int relativeBase = 0;

            int ballX = -1;
            int paddleX = -1;

            Tile tile = new Tile();

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

                        if (!playing)
                            playing = true;

                        // Check where the ball is here and move the horizontal paddle to the same x. So should be memorising the previous x of the paddle so we can automatically give a -1, 0 or 1 as input.
                        // Also need to consider that blocks break and become something new instead of adding tiles to the game.
                        Console.Write("Reached opcode 3, please enter value: ");
                        programInput[indexParameter1] = int.Parse(Console.ReadLine());
                        //if (!phaseSet)
                        //{
                        //    programInput[indexParameter1] = phaseSetting;
                        //    phaseSet = true;
                        //}

                        //else
                        //{
                        //    programInput[indexParameter1] = previousOutput;

                        //}

                        ptr += 2;
                        break;

                    case 4:
                        //Console.WriteLine(programInput[indexParameter1]);
                        //output = programInput[indexParameter1];

                        if (outputNumber == 0)
                        {
                            tile = new Tile();
                            tile.Point.X = (int)programInput[indexParameter1];
                        }
                        else if (outputNumber == 1)
                        {
                            tile.Point.Y = -(int)programInput[indexParameter1];
                        }
                        else if (outputNumber == 2 && playing)
                        {
                            if (tile.Point.X == -1 && tile.Point.Y == 0)
                            {
                                Console.WriteLine($"Score is {programInput[indexParameter1]}");
                            }
                            else
                            {
                                tile.TileId = (int)programInput[indexParameter1];
                                breakout.Tiles.Add(tile);
                            }
                        }
                        else if (outputNumber == 2)
                        {
                            tile.TileId = (int)programInput[indexParameter1];
                            breakout.Tiles.Add(tile);
                        }

                        outputNumber++;
                        outputNumber %= 3;

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
            Breakout breakout = new Breakout();
            ProcessOps(intCodeProgram, breakout);

            int totalBlocks = breakout.Tiles.Count(tile => tile.TileId == 2);
            Console.WriteLine($"Amount of block tiles after game ends is {totalBlocks}.");
        }

        public static void Part2(List<double> intCodeProgram)
        {
            Breakout breakout = new Breakout();
            ProcessOps(intCodeProgram, breakout);
        }

        public static void Start()
        {
            List<double> intCodeProgram = Input;
            List<double> memory = Enumerable.Repeat<double>(0, 1000).ToList();
            intCodeProgram.AddRange(memory);

            //Part1(intCodeProgram);
            intCodeProgram[0] = 2;
            Part2(intCodeProgram);
        }
    }
}
