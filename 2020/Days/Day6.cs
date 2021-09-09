using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day6
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 6).ToList();

        public class QuestionGroup
        {
            public int PeopleInGroup { get; set; } = 0;
            public Dictionary<char, int> Answers { get; set; } = new Dictionary<char, int>();
        }

        public static List<QuestionGroup> LoadQuestions()
        {
            List<QuestionGroup> groups = new List<QuestionGroup>();
            foreach(string group in Input)
            {
                QuestionGroup newGroup = new QuestionGroup();
                newGroup.PeopleInGroup = group.Split("\n").Count();

                string answer = group.Replace("\n", "");

                answer.ToList().ForEach(c =>
                {
                    if (newGroup.Answers.ContainsKey(c))
                    {
                        newGroup.Answers[c]++;
                    }
                    else
                    {
                        newGroup.Answers.Add(c, 1);
                    }
                });

                groups.Add(newGroup);
            }

            return groups;
        }
 
        private static void Part1()
        {
            List<QuestionGroup> groups = LoadQuestions();
            Console.WriteLine($"The sum of all questions answered with yes is {groups.Sum(group => group.Answers.Count)}");
        }

        private static void Part2()
        {
            List<QuestionGroup> groups = LoadQuestions();
            Console.WriteLine($"The sum of all questions that everyone in the group answered with yes is {groups.Sum(group => group.Answers.Where(answer => answer.Value == group.PeopleInGroup).Count())}");
        }


        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
