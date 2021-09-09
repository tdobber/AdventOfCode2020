using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Day7
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 7).ToList();

        public class Bag
        {
            public string BagName { get; set; }
            public List<Bag> IsContainedInBag { get; set; } = new List<Bag>();
            public List<Bag> ContainsBags { get; set; } = new List<Bag>();
            public Dictionary<string, int> ContainsBagWithAmount { get; set; } = new Dictionary<string, int>();
        }

        public static List<Bag> LoadBags()
        {
            List<Bag> bags = new List<Bag>();

            foreach(string line in Input)
            {
                Bag bag = new Bag();
                string[] splitted = line.Split(" bags contain");
                bag.BagName = splitted[0];

                if (splitted[1] != " no other bags.")
                {
                    foreach (string otherBagName in splitted[1].Split(","))
                    {
                        Match match = Regex.Match(otherBagName.Trim(), @"^(?<number>\d+)\s(?<containsbagname>\w+\s\w+)\s+bag[s]?[,]?");
                        if (match.Success)
                        {
                            bag.ContainsBagWithAmount.Add(match.Groups["containsbagname"].Value, int.Parse(match.Groups["number"].Value));
                        }
                    }
                }

                bags.Add(bag);
            }

            foreach (Bag bag in bags)
            {
                bag.IsContainedInBag.AddRange(bags.Where(x => x.ContainsBagWithAmount.ContainsKey(bag.BagName)).ToList());
                bag.ContainsBags.AddRange(bags.Where(x => bag.ContainsBagWithAmount.ContainsKey(x.BagName)).ToList());
            }

            return bags;
        }

        public static List<string> CheckBags(List<Bag> bags, List<string> otherBags)
        {
            if (bags.Count != 0)
            {
                foreach(Bag bag in bags)
                {
                    otherBags.AddRange(CheckBags(bag.IsContainedInBag, otherBags));
                }
                otherBags.AddRange(bags.Select(x => x.BagName).Distinct().ToList());
                return otherBags.Distinct().ToList();
            }
            else
            {
                return otherBags.Distinct().ToList();
            }
        }

        public static long CountBags(Bag currentBag, List<Bag> allBags)
        {
            if (currentBag.ContainsBags.Count == 0)
            {
                return 0;
            }
            else
            {
                long count = 0;
                foreach(Bag bag in currentBag.ContainsBags)
                {
                    count += currentBag.ContainsBagWithAmount[bag.BagName] + (currentBag.ContainsBagWithAmount[bag.BagName] * CountBags(bag, allBags));
                }

                return count;
            }
        }

        private static void Part1()
        {
            List<Bag> bags = LoadBags();

            List<string> otherBags = CheckBags(bags.Where(x => x.BagName == "shiny gold").FirstOrDefault().IsContainedInBag, new List<string>());

            Console.WriteLine($"The amount of bag colors that can eventually contain at least one shiny gold bag is {otherBags.Distinct().Count()}");
        }

        private static void Part2()
        {
            List<Bag> bags = LoadBags();

            long amountOfBags = CountBags(bags.Where(x => x.BagName == "shiny gold").FirstOrDefault(), bags);

            Console.WriteLine($"The amount of individual bags that are required inside your single shiny gold bag is {amountOfBags}");
        }


        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
