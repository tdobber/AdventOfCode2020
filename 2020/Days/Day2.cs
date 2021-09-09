using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{

    public static class Day2
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 2).ToList();

        public class PasswordEntry
        {
            public int LeftNumber { get; set; }
            public int RightNumber { get; set; }
            public char Letter { get; set; }
            public string Password { get; set; }
        }

        private static PasswordEntry MatchPassword(string password)
        {
            Match match = Regex.Match(password, @"^(?<minimum>\d+)-(?<maximum>\d+)\s+(?<letter>\w):\s+(?<password>\w+)");

            if (match.Success)
            {
                return new PasswordEntry
                {
                    LeftNumber = int.Parse(match.Groups["minimum"].Value),
                    RightNumber = int.Parse(match.Groups["maximum"].Value),
                    Letter = char.Parse(match.Groups["letter"].Value),
                    Password = match.Groups["password"].Value,
                };
            }

            return null;
        }

        private static void Part1()
        {
            int valid = 0;
            foreach (string passwordEntry in Input)
            {
                PasswordEntry entry = MatchPassword(passwordEntry);

                if (entry != null)
                {
                    int count = entry.Password.Count(x => x == entry.Letter);
                    if (entry.LeftNumber <= count && count <= entry.RightNumber)
                    {
                        valid++;
                    }
                }
                else
                {
                    Console.WriteLine("Should not happen... Debug please!");
                }
            }

            Console.WriteLine($"The amount of valid passwords for part 1 is {valid}.");
        }

        private static void Part2()
        {
            int valid = 0;
            foreach (string passwordEntry in Input)
            {
                PasswordEntry entry = MatchPassword(passwordEntry);
                if (entry != null)
                {
                    if (entry.Password[entry.LeftNumber - 1] == entry.Letter ^ entry.Password[entry.RightNumber - 1] == entry.Letter)
                    {
                        valid++;
                    }
                }
                else
                {
                    Console.WriteLine("Should not happen... Debug please!");
                }
            }

            Console.WriteLine($"The amount of valid passwords for part 2 is {valid}.");
        }


        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
