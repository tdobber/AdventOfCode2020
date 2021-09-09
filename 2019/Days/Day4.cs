using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day4
    {
        private static List<string> Input => InputHelper.GetInputListString(2019, 4).ToList();

        public static bool NeverDecreasing(int password)
        {
            var passwordString = password.ToString();

            for (int i = 0; i < passwordString.Length - 1; i++)
            {
                if (passwordString[i] > passwordString[i + 1])
                    return false;
            }

            return true;
        }

        public static bool SameAdjacentDigits(int password)
        {
            var passwordString = password.ToString();
            bool sameAdjacentNumbers = false;

            for (int i = 0; i < passwordString.Length - 1; i++)
            {
                if (passwordString[i] == passwordString[i + 1])
                    sameAdjacentNumbers = true;
            }

            return sameAdjacentNumbers;
        }
        
        public static bool SpecialSameAdjacentDigits(int password)
        {
            var passwordString = password.ToString();

            for (int i = 0; i < passwordString.Length; i++)
            {
                int count = passwordString.Count(x => x == passwordString[i]);
                if (count == 2)
                {
                    return true;
                }
            }

            return false;
        }

        public static int Part2(int lowerRange, int upperRange)
        {
            int amountOfPasswords = 0;
            List<int> correctPasswords = new List<int>();

            for (int password = lowerRange; password <= upperRange; password++)
            {
                if (NeverDecreasing(password) && SpecialSameAdjacentDigits(password))
                {
                    amountOfPasswords++;
                    correctPasswords.Add(password);
                }
            }

            return amountOfPasswords;
        }

        public static int Part1(int lowerRange, int upperRange)
        {
            int amountOfPasswords = 0;

            for (int password = lowerRange; password <= upperRange; password++)
            {
                if (NeverDecreasing(password) && SameAdjacentDigits(password))
                {
                    amountOfPasswords++;
                }
            }

            return amountOfPasswords;
        }

        public static void Start()
        {
            int lowerRange = int.Parse(Input[0].Split('-')[0]);
            int upperRange = int.Parse(Input[0].Split('-')[1]);

            int amountOfPasswords = Part1(lowerRange, upperRange);
            Console.WriteLine($"The amount of passwords possible for part 1 is: {amountOfPasswords}.");

            amountOfPasswords = Part2(lowerRange, upperRange);
            Console.WriteLine($"The amount of passwords possible for part 2 is: {amountOfPasswords}.");

            //Console.WriteLine($"Try password 112233 should be True but is {SpecialSameAdjacentDigits(112233)}");
            //Console.WriteLine($"Try password 123444 should be False but is {SpecialSameAdjacentDigits(123444)}");
            //Console.WriteLine($"Try password 111122 should be True but is {SpecialSameAdjacentDigits(111122)}");

        }
    }
}
