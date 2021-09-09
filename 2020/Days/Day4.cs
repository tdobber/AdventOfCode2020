using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Day4
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 4).ToList();

        public class Passport
        {
            public Dictionary<string, string> PassportData { get; set; } = new Dictionary<string, string>();
            public List<string> EyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            public bool CheckPassportValidity()
            {
                if (PassportData.Count == 8 || (PassportData.Count == 7 && !PassportData.ContainsKey("cid")))
                {
                    return true;
                }

                return false;
            }

            public bool CheckPassportValidityStrict()
            {
                if (!(PassportData.TryGetValue("byr", out string byr) && PassportData.TryGetValue("iyr", out string iyr) && PassportData.TryGetValue("eyr", out string eyr) &&
                    PassportData.TryGetValue("hgt", out string hgt) && PassportData.TryGetValue("hcl", out string hcl) && PassportData.TryGetValue("ecl", out string ecl) &&
                    PassportData.TryGetValue("pid", out string pid)))
                {
                    return false;
                }

                int byrInt = int.Parse(byr);
                if (!(1920 <= byrInt && byrInt <= 2002))
                {
                    return false;
                }

                int iyrInt = int.Parse(iyr);
                if (!(2010 <= iyrInt && iyrInt <= 2020))
                {
                    return false;
                }
                
                int eyrInt = int.Parse(eyr);
                if (!(2020 <= eyrInt && eyrInt <= 2030))
                {
                    return false;
                }

                Match match = Regex.Match(hgt, @"^(?<height>\d+)(?<metric>\w+)$");
                if (match.Success)
                {
                    int height = int.Parse(match.Groups["height"].Value);
                    string metric = match.Groups["metric"].Value;
                    if (!((metric == "cm" && 150 <= height && height <= 193) || (metric == "in" && 59 <= height && height <= 76)))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                match = Regex.Match(hcl, @"^#(?<color>[0-9a-f]{6}$)");
                if (!match.Success)
                {
                    return false;
                }

                if (!EyeColors.Contains(ecl))
                {
                    return false;
                }

                match = Regex.Match(pid, @"^(?<pidcode>\d{9}$)");
                if (!match.Success)
                {
                    return false;
                }

                return true;
            }
        }

        public static List<Passport> LoadPassportData()
        {
            List<Passport> result = new List<Passport>();
            Passport passport = new Passport();

            foreach(string line in Input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    result.Add(passport);
                    passport = new Passport();
                }
                else
                {
                    string[] dataLine = line.Split(" ");
                    foreach(string data in dataLine)
                    {
                        string[] pair = data.Split(":");
                        passport.PassportData.Add(pair[0], pair[1]);
                    }
                }
            }

            result.Add(passport);

            return result;
        }

        private static void Part1()
        {
            List<Passport> passports = LoadPassportData();

            int validPassports = 0;

            foreach(Passport passport in passports)
            {
                if (passport.CheckPassportValidity())
                {
                    validPassports++;
                }
            }

            Console.WriteLine($"The amount of valid passports is {validPassports}");
        }

        private static void Part2()
        {
            List<Passport> passports = LoadPassportData();

            int validPassports = 0;

            foreach (Passport passport in passports)
            {
                if (passport.CheckPassportValidityStrict())
                {
                    validPassports++;
                }
            }

            Console.WriteLine($"The amount of strictly valid passports is {validPassports}");
        }


        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
