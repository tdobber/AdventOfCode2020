using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day7
    {
        private static List<int> Input => InputHelper.GetInputString(2019, 7).Split(',').Select(int.Parse).ToList();

        public class IntCodeComputer
        {
            public bool HasHalted { get; set; } = false;
            public bool PhaseSet { get; set; } = false;
            public int Ptr { get; set; } = 0;
            public List<int> ProgramInput { get; set; }
            public int Output { get; set; } = -1;
            public bool OutputSet { get; set; } = false;
            public int Input { get; set; } = -1;
            public bool InputSet { get; set; } = false;
            public int PhaseSetting { get; set; }

            public IntCodeComputer(List<int> program, int phaseSetting)
            {
                ProgramInput = program;
                PhaseSetting = phaseSetting;
            }

            public void ProcessOpsWithin()
            {
                OutputSet = false;
                while (true)
                {
                    string instruction = ProgramInput[Ptr].ToString();
                    int opcode;

                    if (instruction.Length >= 2)
                        opcode = int.Parse(instruction.Substring(instruction.Length - 2, 2));
                    else
                        opcode = int.Parse(instruction);

                    if (opcode == 99)
                    {
                        OutputSet = true;
                        break;
                    }

                    int firstParameterMode = instruction.Length > 2 ? int.Parse(instruction.Substring(instruction.Length - 3, 1)) : 0;
                    int secondParameterMode = instruction.Length > 3 ? int.Parse(instruction.Substring(instruction.Length - 4, 1)) : 0;
                    int thirdParameterMode = instruction.Length > 4 ? int.Parse(instruction.Substring(instruction.Length - 5, 1)) : 0;

                    int indexParameter1;
                    int indexParameter2 = 0;
                    int indexParameter3 = 0;

                    if (firstParameterMode == 0)
                        indexParameter1 = ProgramInput[Ptr + 1];
                    else
                        indexParameter1 = Ptr + 1;

                    if (opcode == 1 || opcode == 2 || opcode == 5 || opcode == 6 || opcode == 7 || opcode == 8)
                    {
                        if (secondParameterMode == 0)
                            indexParameter2 = ProgramInput[Ptr + 2];
                        else
                            indexParameter2 = Ptr + 2;
                    }

                    if (opcode == 1 || opcode == 2 || opcode == 7 || opcode == 8)
                    {
                        if (thirdParameterMode == 0)
                            indexParameter3 = ProgramInput[Ptr + 3];
                        else
                            indexParameter3 = Ptr + 3;
                    }

                    switch (opcode)
                    {
                        case 1:
                            if (thirdParameterMode == 1)
                                Console.WriteLine("This should never happen!");

                            ProgramInput[indexParameter3] = ProgramInput[indexParameter1] + ProgramInput[indexParameter2];
                            Ptr += 4;
                            break;

                        case 2:
                            if (thirdParameterMode == 1)
                                Console.WriteLine("This should never happen!");

                            ProgramInput[indexParameter3] = ProgramInput[indexParameter1] * ProgramInput[indexParameter2];
                            Ptr += 4;
                            break;

                        case 3:
                            if (firstParameterMode == 1)
                                Console.WriteLine("This should never happen!");

                            if (!PhaseSet)
                            {
                                ProgramInput[indexParameter1] = PhaseSetting;
                                PhaseSet = true;
                            }
                            else
                            {
                                if (InputSet)
                                {
                                    ProgramInput[indexParameter1] = Input;
                                    InputSet = false;
                                    Input = -1;
                                }
                                else
                                {
                                    Console.WriteLine("Where is my input?");
                                    return;
                                }
                            }

                            Ptr += 2;

                            break;

                        case 4:
                            Output = ProgramInput[indexParameter1];
                            OutputSet = true;
                            Ptr += 2;
                            return;

                        case 5:
                            if (ProgramInput[indexParameter1] != 0)
                                Ptr = ProgramInput[indexParameter2];
                            else
                                Ptr += 3;
                            break;

                        case 6:
                            if (ProgramInput[indexParameter1] == 0)
                                Ptr = ProgramInput[indexParameter2];
                            else
                                Ptr += 3;
                            break;

                        case 7:
                            if (thirdParameterMode == 1)
                                Console.WriteLine("This should never happen!");

                            if (ProgramInput[indexParameter1] < ProgramInput[indexParameter2])
                                ProgramInput[indexParameter3] = 1;
                            else
                                ProgramInput[indexParameter3] = 0;

                            Ptr += 4;

                            break;

                        case 8:
                            if (thirdParameterMode == 1)
                                Console.WriteLine("This should never happen!");

                            if (ProgramInput[indexParameter1] == ProgramInput[indexParameter2])
                                ProgramInput[indexParameter3] = 1;
                            else
                                ProgramInput[indexParameter3] = 0;

                            Ptr += 4;

                            break;

                        default:
                            Console.WriteLine("Something went wrong...");
                            break;
                    }
                }

                HasHalted = true;
                return;
            }
        }

        public static int ProcessOps(List<int> programInput, int phaseSetting, int previousOutput)
        {
            int ptr = 0;
            bool phaseSet = false;
            int output = -1;

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

                        //Console.Write("Reached opcode 3, please enter value: ");
                        //programInput[indexParameter1] = int.Parse(Console.ReadLine());
                        if (!phaseSet)
                        {
                            programInput[indexParameter1] = phaseSetting;
                            phaseSet = true;
                        }

                        else
                        {
                            programInput[indexParameter1] = previousOutput;

                        }

                        ptr += 2;
                        break;

                    case 4:
                        //Console.WriteLine(programInput[indexParameter1]);
                        output = programInput[indexParameter1];
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

            return output;
        }

        private static IEnumerable<string> Permutate(string source)
        {
            if (source.Length == 1) return new List<string> { source };

            var permutations = from c in source
                               from p in Permutate(new string(source.Where(x => x != c).ToArray()))
                               select c + p;

            return permutations;
        }


        public static void Part1(List<int> intCodeProgram)
        {
            //List<int> phaseSetting = new List<int> { 4, 3, 2, 1, 0 };
            //List<int> phaseSetting = new List<int> { 0, 1, 2, 3, 4 };
            //List<int> phaseSetting = new List<int> { 1, 0, 4, 3, 2 };

            List<string> phaseSettings = new List<string>();

            foreach (string phase in Permutate("01234"))
            {
                phaseSettings.Add(phase);
            }

            Dictionary<string, int> thrusterSignals = new Dictionary<string, int>();

            foreach (string phase in phaseSettings)
            {
                int totalOutput = 0;

                foreach (char code in phase)
                {
                    List<int> copyInput = new List<int>();
                    copyInput.InsertRange(0, intCodeProgram);
                    totalOutput = ProcessOps(copyInput, int.Parse(code.ToString()), totalOutput);

                    if (totalOutput == -1)
                        Console.WriteLine("Somethig went extremely wrong....");
                }

                thrusterSignals[phase] = totalOutput;
            }

            Console.WriteLine($"The highest thruster signal output is {thrusterSignals.Values.Max()} with phase {thrusterSignals.Keys.First(x => thrusterSignals[x] == thrusterSignals.Values.Max())}.");
        }

        public static void Part2(List<int> intCodeProgram)
        {
            //List<int> phaseSetting = new List<int> { 9, 8, 7, 6, 5 };
            //List<int> phaseSetting = new List<int> { 9, 7, 8, 5, 6 };
            List<string> phaseSettings = new List<string>();

            foreach (string phase in Permutate("56789"))
            {
                phaseSettings.Add(phase);
            }

            Dictionary<string, int> thrusterSignals = new Dictionary<string, int>();

            foreach (string phase in phaseSettings)
            {
                List<int> copyInputA = new List<int>();
                List<int> copyInputB = new List<int>();
                List<int> copyInputC = new List<int>();
                List<int> copyInputD = new List<int>();
                List<int> copyInputE = new List<int>();

                copyInputA.InsertRange(0, intCodeProgram);
                copyInputB.InsertRange(0, intCodeProgram);
                copyInputC.InsertRange(0, intCodeProgram);
                copyInputD.InsertRange(0, intCodeProgram);
                copyInputE.InsertRange(0, intCodeProgram);

                IntCodeComputer AmpA = new IntCodeComputer(copyInputA, int.Parse(phase.Substring(0, 1)));
                IntCodeComputer AmpB = new IntCodeComputer(copyInputB, int.Parse(phase.Substring(1, 1)));
                IntCodeComputer AmpC = new IntCodeComputer(copyInputC, int.Parse(phase.Substring(2, 1)));
                IntCodeComputer AmpD = new IntCodeComputer(copyInputD, int.Parse(phase.Substring(3, 1)));
                IntCodeComputer AmpE = new IntCodeComputer(copyInputE, int.Parse(phase.Substring(4, 1)));

                int output = 0;

                while (!AmpA.HasHalted || !AmpB.HasHalted || !AmpC.HasHalted || !AmpD.HasHalted || !AmpE.HasHalted)
                {
                    if (AmpE.OutputSet)
                    {
                        AmpA.Input = AmpE.Output;
                        AmpA.InputSet = true;
                    }
                    else
                    {
                        AmpA.Input = output;
                        AmpA.InputSet = true;
                    }
                    AmpA.ProcessOpsWithin();

                    if (!AmpA.OutputSet || AmpA.Output == -1)
                        Console.WriteLine("Something went extremely wrong....");

                    AmpB.Input = AmpA.Output;
                    AmpB.InputSet = true;
                    AmpB.ProcessOpsWithin();

                    if (!AmpB.OutputSet || AmpB.Output == -1)
                        Console.WriteLine("Something went extremely wrong....");

                    AmpC.Input = AmpB.Output;
                    AmpC.InputSet = true;
                    AmpC.ProcessOpsWithin();

                    if (!AmpC.OutputSet || AmpC.Output == -1)
                        Console.WriteLine("Something went extremely wrong....");

                    AmpD.Input = AmpC.Output;
                    AmpD.InputSet = true;
                    AmpD.ProcessOpsWithin();

                    if (!AmpD.OutputSet || AmpD.Output == -1)
                        Console.WriteLine("Something went extremely wrong....");

                    AmpE.Input = AmpD.Output;
                    AmpE.InputSet = true;
                    AmpE.ProcessOpsWithin();

                    if (!AmpE.OutputSet || AmpE.Output == -1)
                        Console.WriteLine("Something went extremely wrong....");
                }

                thrusterSignals[phase] = AmpE.Output;
            }

            Console.WriteLine($"The highest thruster signal output is {thrusterSignals.Values.Max()} with phase {thrusterSignals.Keys.First(x => thrusterSignals[x] == thrusterSignals.Values.Max())}.");

        }

        public static void Start()
        {
            //List<string> input = new List<string> { "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0" };
            //List<string> input = new List<string> { "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0" };
            //List<string> input = new List<string> { "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0" };

            //List<string> input = new List<string> { "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5" };
            //List<string> input = new List<string> { "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10" };

            //Part1(intCodeProgram);
            Part2(Input);
        }
    }
}
