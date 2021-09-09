using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Day19
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 19).ToList();

        private static Dictionary<int, List<string>> LoadRules()
        {
            Dictionary<int, List<string>> rules = new Dictionary<int, List<string>>();
            //HashSet<string> possibleValues = new HashSet<string>();

            foreach (string rule in Input)
            {
                if (string.IsNullOrEmpty(rule))
                {
                    break;
                }
                else
                {
                    Match match = Regex.Match(rule, "^(?<ruleNumber>\\d*):\\s(((?<ruleOne>.*){1,2}|\\s(?<ruleTwo>.*){1,2})|((?<ruleThree>\"[a-b]\")))$");
                    if (match.Success)
                    {
                        int ruleNumber = int.Parse(match.Groups["ruleNumber"].Value);
                        rules.Add(ruleNumber, new List<string> { match.Groups["ruleOne"].Value });
                        if (!string.IsNullOrEmpty(match.Groups["ruleTwo"].Value))
                        {
                            rules[ruleNumber].Add(match.Groups["ruleTwo"].Value);
                        }
                    }
                }
            }

            return rules;
        }

        private static HashSet<string> LoadPossibleValues(Dictionary<int, List<string>> rules)
        {
            HashSet<string> possibleValues = new HashSet<string>();

            return possibleValues;
        }

        private static void Part1()
        {
            Dictionary<int, List<string>> rules = LoadRules();
            HashSet<string> possibleValues = LoadPossibleValues(rules);
        }

        private static void Part2()
        {

        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
