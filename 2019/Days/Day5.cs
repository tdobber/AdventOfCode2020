using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day5
    {
        private static List<int> Input => InputHelper.GetInputString(2019, 5).Split(',').Select(int.Parse).ToList();

        public static void ProcessOps(List<int> programInput)
        {
            int ptr = 0;

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

                int indexParameter1;
                int indexParameter2 = 0;
                int indexParameter3 = 0;

                if (firstParameterMode == 0)
                    indexParameter1 = programInput[ptr + 1];
                else
                    indexParameter1 = ptr + 1;

                if (opcode == 1 || opcode == 2 || opcode == 5 || opcode == 6 || opcode == 7 || opcode == 8)
                {
                    if (secondParameterMode == 0)
                        indexParameter2 = programInput[ptr + 2];
                    else
                        indexParameter2 = ptr + 2;
                }

                if (opcode == 1 || opcode == 2 || opcode == 7 || opcode == 8)
                {
                    if (thirdParameterMode == 0)
                        indexParameter3 = programInput[ptr + 3];
                    else
                        indexParameter3 = ptr + 3;
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
                        ptr += 2;
                        break;

                    case 4:
                        Console.WriteLine(programInput[indexParameter1]);
                        ptr += 2;
                        break;

                    case 5:
                        if (programInput[indexParameter1] != 0)
                            ptr = programInput[indexParameter2];
                        else
                            ptr += 3;
                        break;

                    case 6:
                        if (programInput[indexParameter1] == 0)
                            ptr = programInput[indexParameter2];
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

                    default:
                        Console.WriteLine("Something went wrong...");
                        break;
                }
            }

            Console.WriteLine("Program finished.");
            return;
        }

        public static void Start()
        {
            //List<string> input = new List<string> { "1002,4,3,4,33" };

            // Equal to and less than 8 examples.
            //List<string> input = new List<string> { "3,9,8,9,10,9,4,9,99,-1,8" };
            //List<string> input = new List<string> { "3,9,7,9,10,9,4,9,99,-1,8" };
            //List<string> input = new List<string> { "3,3,1108,-1,8,3,4,3,99" };
            //List<string> input = new List<string> { "3,3,1107,-1,8,3,4,3,99" };

            // Jump examples
            //List<string> input = new List<string> { "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9" };
            //List<string> input = new List<string> { "3,3,1105,-1,9,1101,0,0,12,4,12,99,1" };

            // Bigger example
            //List<string> input = new List<string> { "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99" };

            ProcessOps(Input);
        }
    }
}