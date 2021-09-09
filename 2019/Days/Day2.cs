using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day2
    {
        private static List<int> Input => InputHelper.GetInputString(2019, 2).Split(',').Select(int.Parse).ToList();

        public static List<int> ProcessOps(List<int> programInput)
        {
            bool programHalt = false;
            int ptr = 0;

            while (!programHalt)
            {
                if (programInput[ptr] == 1)
                {
                    programInput[programInput[ptr + 3]] = programInput[programInput[ptr + 1]] + programInput[programInput[ptr + 2]];
                }
                else if (programInput[ptr] == 2)
                {
                    programInput[programInput[ptr + 3]] = programInput[programInput[ptr + 1]] * programInput[programInput[ptr + 2]];
                }
                else if (programInput[ptr] == 99)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Something went wrong...");
                }

                ptr += 4;
            }

            return programInput;
        }

        public static int Part1(List<int> intCodeProgram) {
            intCodeProgram[1] = 12;
            intCodeProgram[2] = 2;

            intCodeProgram = ProcessOps(intCodeProgram);

            return intCodeProgram[0];
        }

        public static Tuple<int, int> Part2(List<int> input)
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    List<int> tryCodeProgram = new List<int>();
                    tryCodeProgram.InsertRange(0, input);
                    tryCodeProgram[1] = noun;
                    tryCodeProgram[2] = verb;
                    tryCodeProgram = ProcessOps(tryCodeProgram);

                    if (tryCodeProgram[0] == 19690720)
                    {
                        return Tuple.Create(noun, verb);
                    }
                }
            }

            return Tuple.Create(0, 0);
        }

        public static void Start()
        {
            //List<string> input = File.ReadAllLines(readFile + "Day2").ToList();
            ////List<string> input = new List<string> { "1,9,10,3,2,3,11,0,99,30,40,50" };
            //List<int> intCodeProgram = input[0].Split(',').Select(int.Parse).ToList();

            int valueAtZeroIndex = Part1(Input);
            Console.WriteLine($"The value that is at index 0 at the end of the program is {valueAtZeroIndex}.");

            //intCodeProgram = input[0].Split(',').Select(int.Parse).ToList();
            Tuple<int, int> nounAndVerb = Part2(Input);
            Console.WriteLine($"The noun and verb returned are {nounAndVerb.Item1} and {nounAndVerb.Item2}, which eventually gives: {(100 * nounAndVerb.Item1) + nounAndVerb.Item2}");
        }
    }
}