using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Helpers
{
    public static class InputHelper
    {
        public static List<string> GetInputListString(int year, int day) =>
            File.ReadAllLines(ReturnExistngFile(year, day)).ToList();

        public static List<int> GetInputListInt(int year, int day) =>
            File.ReadAllLines(ReturnExistngFile(year, day)).Select(int.Parse).ToList();
        
        public static List<long> GetInputListLong(int year, int day) =>
            File.ReadAllLines(ReturnExistngFile(year, day)).Select(long.Parse).ToList();

        public static string GetInputString(int year, int day) =>
            File.ReadAllText(ReturnExistngFile(year, day));

        private static string ReturnExistngFile(int year, int day)
        {
            if (File.Exists($"..\\..\\..\\{year}\\Inputs\\Day{day}.txt"))
            {
                return $"..\\..\\..\\{year}\\Inputs\\Day{day}.txt";
            }
            else
            {
                return $"../../../{year}/Inputs/Day{day}.txt";
            }
        }
    }
}
