using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day5
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 5).ToList();

        public class BoardingPass
        {
            public byte Row { get; set; }
            public byte Column { get; set; }
            public int SeatId
            {
                get
                {
                    return (Row * 8) + Column;
                }
            }
        }

        public static List<BoardingPass> ReadBoardingPasses()
        {
            List<BoardingPass> boardingPasses = new List<BoardingPass>();

            foreach(string line in Input)
            {
                string binaryLine = line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');

                boardingPasses.Add(new BoardingPass
                {
                    Row = Convert.ToByte(binaryLine[0..7], 2),
                    Column = Convert.ToByte(binaryLine[7..10], 2)
                });
            }

            return boardingPasses;
        }
 
        private static void Part1()
        {
            List<BoardingPass> boardingPasses = ReadBoardingPasses();

            Console.WriteLine($"The highest seat id is {boardingPasses.Max(pass => pass.SeatId)}.");
        }

        private static void Part2()
        {
            List<BoardingPass> boardingPasses = ReadBoardingPasses();

            BoardingPass boardingPass = boardingPasses.Where(pass => !boardingPasses.Any(plusOne => plusOne.SeatId == pass.SeatId + 1) &&
                                                                boardingPasses.Any(plusTwo => plusTwo.SeatId == pass.SeatId + 2)).FirstOrDefault();
            Console.WriteLine($"The id of your seat is {boardingPass.SeatId + 1}.");
        }


        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
