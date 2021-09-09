using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day9
    {
        private static List<double> Input => InputHelper.GetInputString(2019, 9).Split(',').Select(double.Parse).ToList();

        //public static int ProcessOps(List<int> programInput, int phaseSetting, int previousOutput)
        public static void ProcessOps(List<double> programInput)
        {
            int ptr = 0;
            //bool phaseSet = false;
            //int output = -1;
            int relativeBase = 0;

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
                        Console.WriteLine(programInput[indexParameter1]);
                        //output = programInput[indexParameter1];
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

            //return output;
            return;
        }

        public static void Part1and2(List<double> intCodeProgram)
        {
            ProcessOps(intCodeProgram);
        }

        public static void Start()
        {
            //List<string> input = new List<string> { "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99" };
            //List<string> input = new List<string> { "1102,34915192,34915192,7,4,7,99,0" };
            //List<string> input = new List<string> { "104,1125899906842624,99" };

            List<double> intCodeProgram = Input;
            List<double> memory = Enumerable.Repeat<double>(0, 1000).ToList();
            intCodeProgram.AddRange(memory);
            Part1and2(intCodeProgram);
        }

    }
}
