using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day18
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 18).ToList();

        private static Tuple<long, int> Calculate(char[] elements)
        {
            long total = 0;
            int steps = 0;
            bool addition = false;
            bool multiply = false;

            for (int i = 0; i < elements.Length; i++)
            {
                steps++;
                switch (elements[i])
                {
                    case '(':
                        Tuple<long, int> result;
                        result = Calculate(elements[(i + 1)..]);

                        if (addition)
                        {
                            total += result.Item1;
                            addition = false;
                        }
                        else if (multiply)
                        {
                            total *= result.Item1;
                            multiply = false;
                        }
                        else
                        {
                            total = result.Item1;
                        }

                        steps += result.Item2;
                        i += result.Item2;
                        break;

                    case ')':
                        return new Tuple<long, int>(total, steps);

                    case '+':
                        addition = true;
                        break;

                    case '*':
                        multiply = true;
                        break;

                    default:
                        if (!addition && !multiply)
                        {
                            total = (int)char.GetNumericValue(elements[i]);
                        }
                        else if (addition)
                        {
                            total += (int)char.GetNumericValue(elements[i]);
                            addition = false;
                        }
                        else if (multiply)
                        {
                            total *= (int)char.GetNumericValue(elements[i]);
                            multiply = false;
                        }
                        break;
                }
            }

            return new Tuple<long, int>(total, 0);
        }

        public static char[] AddParentheses(string sum)
        {
            for (int i = 0; i < sum.Length; i++)
            {
                if (sum[i] == '+')
                {
                    int closing = 0;
                    int opening = 0;
                    for (int j = 1; i - j >= -1; j++)
                    {
                        if (opening == closing && opening != 0)
                        {
                            sum = sum.Insert(i - j + 1, "(");
                            i++;
                            break;
                        }
                        else if (sum[i - j] == ')')
                        {
                            closing++;
                        }
                        else if (sum[i - j] == '(')
                        {
                            opening++;
                        }
                        else if (opening == closing)
                        {
                            if (!char.IsDigit(sum[i - j]))
                            {
                                sum = sum.Insert(i - j + 1, "(");
                            }
                            else
                            {
                                sum = sum.Insert(i - j, "(");
                            }
                            i++;
                            break;
                        }
                    }

                    closing = 0;
                    opening = 0;
                    for (int j = 1; i + j <= sum.Length; j++)
                    {
                        if (opening == closing && opening != 0)
                        {
                            if (i + j == sum.Length)
                            {
                                sum += ")";
                            }
                            else
                            {
                                sum = sum.Insert(i + j, ")");
                            }
                            break;
                        }
                        else if (sum[i + j] == ')')
                        {
                            closing++;
                        }
                        else if (sum[i + j] == '(')
                        {
                            opening++;
                        }
                        else if (opening == closing)
                        {
                            if (char.IsDigit(sum[i + j]))
                            {
                                sum = sum.Insert(i + j + 1, ")");
                            }
                            else {
                                sum = sum.Insert(i + j, ")");
                            }
                            break;
                        }
                    }
                }
            }

            return sum.ToArray();
        }

        private static void Part1()
        {
            long total = 0;
            Tuple<long, int> result;
            foreach (string sum in Input)
            {
                char[] elements = sum.Replace(" ", "").ToArray();
                result = Calculate(elements);
                total += result.Item1;
            }

            Console.WriteLine($"The total of all the sums is {total}");
        }

        private static void Part2()
        {
            long total = 0;
            Tuple<long, int> result;
            foreach (string sum in Input)
            {
                char[] elements = AddParentheses(sum.Replace(" ", ""));
                result = Calculate(elements);
                total += result.Item1;
            }

            Console.WriteLine($"The total of all the sums with weird precedence is {total}");
        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
