using System;
using System.Reflection;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What would you like to run? (Example: 3-19 for day 3 of 2019, 8 for the current year day 8)");

            string[] input = Console.ReadLine().Split("-");
            int dayToRun = Convert.ToInt16(input[0]);
            int yearToRun = input.Length > 1 ? Convert.ToInt16(input[1]) : 0;

            switch (yearToRun)
            {
                case 15:
                case 2015:
                    yearToRun = 2015;
                    break;

                case 18:
                case 2018:
                    yearToRun = 2018;
                    break;

                case 19:
                case 2019:
                    yearToRun = 2019;
                    break;

                default:
                    yearToRun = 2020;
                    break;
            }

            Type specificDay = Type.GetType($"AdventOfCode._{yearToRun}.Day{dayToRun}");

            if (specificDay != null)
            {
                MethodInfo methodToInvoke = specificDay.GetMethod("Start");
                methodToInvoke.Invoke(null, null);
            }
            else
            {
                Console.WriteLine($"Day {dayToRun} of year {yearToRun} does not (yet) exist.");
            }

            Console.WriteLine("----End of program, terminate by pressing a key----");
            Console.Read();
        }
    }
}
