using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day8
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 8).ToList();

        public class BootOperation
        {
            public long Accumulator { get; set; } = 0;
            public int Index { get; set; } = 0;
            public HashSet<int> VisitedOperations { get; set; } = new HashSet<int>();

            public bool RunProgram(List<string> input)
            {
                while (Index < input.Count)
                {
                    if (VisitedOperations.Contains(Index))
                    {
                        return false;
                    }
                    else
                    {
                        VisitedOperations.Add(Index);
                    }

                    switch (input[Index].Split(" ")[0])
                    {
                        case "acc":
                            Accumulator += int.Parse(input[Index].Split(" ")[1]);
                            Index++;
                            break;

                        case "jmp":
                            Index += int.Parse(input[Index].Split(" ")[1]);
                            break;

                        case "nop":
                            Index++;
                            break;

                        default:
                            Console.WriteLine("This should not happen, start debugging...");
                            break;
                    }
                }

                return true;
            }

            public void Reset()
            {
                Accumulator = 0;
                Index = 0;
                VisitedOperations = new HashSet<int>();
            }
        }

        private static void Part1()
        {
            BootOperation bootOp = new BootOperation();

            bootOp.RunProgram(Input);
            Console.WriteLine($"The accumulator is {bootOp.Accumulator} right before it's gonna loop.");
        }

        private static void Part2()
        {
            BootOperation bootOp = new BootOperation();

            for (int i = 0; i < Input.Count; i++)
            {
                bootOp.Reset();
                List<string> testInput = Input;
                bool result = false;

                if (testInput[i].Split(" ")[0] == "jmp")
                {
                    testInput[i] = testInput[i].Replace("jmp", "nop");
                    result = bootOp.RunProgram(testInput);
                }
                else if (testInput[i].Split(" ")[0] == "nop")
                {
                    testInput[i] = testInput[i].Replace("nop", "jmp");
                    result = bootOp.RunProgram(testInput);
                }

                if (result)
                {
                    Console.WriteLine($"The accumulator is {bootOp.Accumulator} after the program finishes.");
                    break;
                }
            }
        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
