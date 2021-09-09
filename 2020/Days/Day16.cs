using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Day16
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 16).ToList();

        public class Tickets
        {
            public Dictionary<string, HashSet<int>> ValidRanges { get; set; } = new Dictionary<string, HashSet<int>>();
            public HashSet<int> AllValidRanges { get; set; } = new HashSet<int>();
            public List<int> YourTicket { get; set; } = new List<int>();
            public List<List<int>> AllValidTickets { get; set; } = new List<List<int>>();
            public Dictionary<string, List<int>> TicketColumn { get; set; } = new Dictionary<string, List<int>>();
            public Dictionary<string, int> OfficialColumn { get; set; } = new Dictionary<string, int>();

            public void LoadValidRanges()
            {
                int nextIndex = 0;
                for (int i = nextIndex; i < Input.Count; i++)
                {
                    if (string.IsNullOrEmpty(Input[i]))
                    {
                        nextIndex = i + 1;
                        break;
                    }

                    Match match = Regex.Match(Input[i], @"^(?<name>\D+):\s(?<lowermin>\d+)-(?<lowermax>\d+) or (?<uppermin>\d+)-(?<uppermax>\d+)$");
                    if (match.Success)
                    {
                        ValidRanges.Add(match.Groups["name"].Value + "Lower", new HashSet<int>(Enumerable.Range(int.Parse(match.Groups["lowermin"].Value), int.Parse(match.Groups["lowermax"].Value) - int.Parse(match.Groups["lowermin"].Value) + 1).ToList()));
                        ValidRanges.Add(match.Groups["name"].Value + "Upper", new HashSet<int>(Enumerable.Range(int.Parse(match.Groups["uppermin"].Value), int.Parse(match.Groups["uppermax"].Value) - int.Parse(match.Groups["uppermin"].Value) + 1).ToList()));
                        TicketColumn.Add(match.Groups["name"].Value, Enumerable.Range(0, 20).ToList());
                        OfficialColumn.Add(match.Groups["name"].Value, -1);
                    }
                }

                if (Input[nextIndex++] == "your ticket:")
                {
                    YourTicket = Input[nextIndex++].Split(",").Select(int.Parse).ToList();
                    AllValidTickets.Add(YourTicket);
                }

                foreach (HashSet<int> set in ValidRanges.Values)
                {
                    AllValidRanges.UnionWith(set);
                }
            }

            public int TicketError()
            {
                int ticketError = 0;

                int startIndex = Input.FindIndex(x => x == "nearby tickets:");

                for (int i = startIndex + 1; i < Input.Count; i++)
                {
                    List<int> ticket = Input[i].Split(",").Select(int.Parse).ToList();
                    bool valid = true;
                    foreach (int number in ticket)
                    {
                        if (!AllValidRanges.Contains(number))
                        {
                            ticketError += number;
                            valid = false;
                        }
                    }

                    if (valid)
                    {
                        AllValidTickets.Add(ticket);
                    }
                }

                return ticketError;
            }
        }

        private static Tickets Part1()
        {
            Tickets tickets = new Tickets();
            tickets.LoadValidRanges(); 

            Console.WriteLine($"The ticket error is {tickets.TicketError()}");
            return tickets;
        }

        private static void Part2(Tickets tickets)
        {
            Dictionary<string, List<int>> tempColumns = new Dictionary<string, List<int>>();
            foreach (List<int> ticket in tickets.AllValidTickets)
            {
                for (int i = 0; i < ticket.Count; i++)
                {
                    foreach (KeyValuePair<string, List<int>> validColumn in tickets.TicketColumn)
                    {
                        List<int> newValidColumns = new List<int>();
                        if (validColumn.Value.Contains(i))
                        {
                            if (tickets.ValidRanges[validColumn.Key + "Lower"].Contains(ticket[i]) || tickets.ValidRanges[validColumn.Key + "Upper"].Contains(ticket[i]))
                            {
                                if (tempColumns.ContainsKey(validColumn.Key))
                                {
                                    tempColumns[validColumn.Key].Add(i);
                                }
                                else
                                {
                                    tempColumns.Add(validColumn.Key, new List<int> { i });
                                }
                            }
                        }
                    }
                }
                
                foreach (KeyValuePair<string, List<int>> column in tempColumns)
                {
                    tickets.TicketColumn[column.Key] = column.Value;
                }

                tempColumns = new Dictionary<string, List<int>>();
            }

            tempColumns = new Dictionary<string, List<int>>();
            while (tickets.OfficialColumn.Values.Contains(-1))
            {
                foreach (KeyValuePair<string, List<int>> pair in tickets.TicketColumn)
                {
                    if (pair.Value.Count == 1)
                    {
                        tickets.OfficialColumn[pair.Key] = pair.Value[0];
                        foreach (KeyValuePair<string, List<int>> remove in tickets.TicketColumn)
                        {
                            if (tempColumns.ContainsKey(remove.Key))
                            {
                                tempColumns[remove.Key] = remove.Value.Where(x => x != pair.Value[0]).ToList();
                            }
                            else
                            {
                                tempColumns.Add(remove.Key, remove.Value.Where(x => x != pair.Value[0]).ToList());
                            }
                        }
                    }
                }

                foreach (KeyValuePair<string, List<int>> column in tempColumns)
                {
                    tickets.TicketColumn[column.Key] = column.Value;
                }
            }

            long answer = 1;
            foreach (KeyValuePair<string, int> official in tickets.OfficialColumn)
            {
                if (official.Key.Contains("departure"))
                {
                    answer *= tickets.YourTicket[official.Value];
                }
            }

            Console.WriteLine($"The multiplication of those six 'departure' values together is {answer}.");
        }

        public static void Start()
        {
            Tickets tickets = Part1();

            Part2(tickets);
        }
    }
}
