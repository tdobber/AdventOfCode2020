using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class Day11
    {
        private static List<string> Input => InputHelper.GetInputListString(2020, 11).ToList();

        public class SeatMap
        {
            public char[,] Seats { get; set; }
            public char[,] OldSeats { get; set; }
            public int SizeX { get; set; }
            public int SizeY { get; set; }
            public int Rounds { get; set; } = 0;

            public void ProcessRound(int part = 1)
            {
                while (true)
                {
                    if (CopyArraysAndCheck())
                    {
                        break;
                    }

                    for (int i = 0; i < SizeY; i++)
                    {
                        for (int j = 0; j < SizeX; j++)
                        {
                            switch(OldSeats[i, j])
                            {
                                case 'L':
                                    if (part == 1)
                                    {
                                        if (CheckEmptySeats(i, j))
                                        {
                                            Seats[i, j] = '#';
                                        }
                                    }
                                    else
                                    {
                                        if (CheckEmptySeatsStrict(i, j))
                                        {
                                            Seats[i, j] = '#';
                                        }
                                    }
                                    break;

                                case '#':
                                    if (part == 1)
                                    {
                                        if (CheckOccupiedSeats(i, j))
                                        {
                                            Seats[i, j] = 'L';
                                        }
                                    }
                                    else
                                    {
                                        if (CheckOccupiedSeatsStrict(i, j))
                                        {
                                            Seats[i, j] = 'L';
                                        }
                                    }
                                    
                                    break;

                                case '.':
                                    break;

                                default:
                                    Console.WriteLine("Should not happen, debug...");
                                    break;
                            }
                        }
                    }
                }

                int occupiedSeats = 0;
                for (int i = 0; i < SizeY; i++)
                {
                    for (int j = 0; j < SizeX; j++)
                    {
                        if (Seats[i, j] == '#')
                        {
                            occupiedSeats++;
                        }
                    }
                }

                Console.WriteLine($"The amount of occupied seats after stabilization is {occupiedSeats}.");

                return;
            }

            public bool CopyArraysAndCheck()
            {
                if (OldSeats == null)
                {
                    OldSeats = new char[SizeY, SizeX];
                }

                bool same = true;

                for (int i = 0; i < SizeY; i++)
                {
                    for (int j = 0; j < SizeX; j++)
                    {
                        if (OldSeats[i, j] != Seats[i, j])
                        {
                            OldSeats[i, j] = Seats[i, j];
                            same = false;
                        }
                    }
                }

                return same;
            }

            private bool CheckEmptySeats(int originalI, int originalJ)
            {
                for (int i = originalI - 1; i <= originalI + 1; i++)
                {
                    if (i < 0 || i >= SizeY)
                    {
                        continue;
                    }
                    else
                    {
                        for (int j = originalJ - 1; j <= originalJ + 1; j++)
                        {
                            if (j < 0 || j >= SizeX)
                            {
                                continue;
                            }
                            else if (OldSeats[i, j] == '#')
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }

            private bool CheckEmptySeatsStrict(int originalI, int originalJ)
            {
                int takenSeats = 0;

                bool seat1Found = false;
                bool seat2Found = false;
                bool seat3Found = false;
                bool seat4Found = false;
                bool seat5Found = false;
                bool seat6Found = false;
                bool seat7Found = false;
                bool seat8Found = false;

                for (int i = 1; i < SizeX && i < SizeY; i++)
                {
                    if (!seat1Found)
                    {
                        if (originalI - i < 0)
                        {
                            seat1Found = true;
                        }
                        else if (OldSeats[originalI - i, originalJ] != '.')
                        {
                            if (OldSeats[originalI - i, originalJ] == '#')
                            {
                                takenSeats++;
                            }
                            seat1Found = true;
                        }
                    }

                    if (!seat2Found)
                    {
                        if (originalI - i < 0 || originalJ + i >= SizeX)
                        {
                            seat2Found = true;
                        }
                        else if (OldSeats[originalI - i, originalJ + i] != '.')
                        {
                            if (OldSeats[originalI - i, originalJ + i] == '#')
                            {
                                takenSeats++;
                            }
                            seat2Found = true;
                        }
                    }

                    if (!seat3Found)
                    {
                        if (originalJ + i >= SizeX)
                        {
                            seat3Found = true;
                        }
                        else if (OldSeats[originalI, originalJ + i] != '.')
                        {
                            if (OldSeats[originalI, originalJ + i] == '#')
                            {
                                takenSeats++;
                            }
                            seat3Found = true;
                        }
                    }

                    if (!seat4Found)
                    {
                        if (originalI + i >= SizeY || originalJ + i >= SizeX)
                        {
                            seat4Found = true;
                        }
                        else if (OldSeats[originalI + i, originalJ + i] != '.')
                        {
                            if (OldSeats[originalI + i, originalJ + i] == '#')
                            {
                                takenSeats++;
                            }
                            seat4Found = true;
                        }
                    }

                    if (!seat5Found)
                    {
                        if (originalI + i >= SizeY)
                        {
                            seat5Found = true;
                        }
                        else if (OldSeats[originalI + i, originalJ] != '.')
                        {
                            if (OldSeats[originalI + i, originalJ] == '#')
                            {
                                takenSeats++;
                            }
                            seat5Found = true;
                        }
                    }

                    if (!seat6Found)
                    {
                        if (originalI + i >= SizeY || originalJ - i < 0)
                        {
                            seat6Found = true;
                        }
                        else if (OldSeats[originalI + i, originalJ - i] != '.')
                        {
                            if (OldSeats[originalI + i, originalJ - i] == '#')
                            {
                                takenSeats++;
                            }
                            seat6Found = true;
                        }
                    }

                    if (!seat7Found)
                    {
                        if (originalJ - i < 0)
                        {
                            seat7Found = true;
                        }
                        else if (OldSeats[originalI, originalJ - i] != '.')
                        {
                            if (OldSeats[originalI, originalJ - i] == '#')
                            {
                                takenSeats++;
                            }
                            seat7Found = true;
                        }
                    }

                    if (!seat8Found)
                    {
                        if (originalI - i < 0 || originalJ - i < 0)
                        {
                            seat8Found = true;
                        }
                        else if (OldSeats[originalI - i, originalJ - i] != '.')
                        {
                            if (OldSeats[originalI - i, originalJ - i] == '#')
                            {
                                takenSeats++;
                            }
                            seat8Found = true;
                        }
                    }

                    if (seat1Found &&
                        seat2Found &&
                        seat3Found &&
                        seat4Found &&
                        seat5Found &&
                        seat6Found &&
                        seat7Found &&
                        seat8Found)
                    {
                        if (takenSeats > 0)
                        {
                            return false;
                        }

                        return true;
                    }
                }

                return true;
            }

            private bool CheckOccupiedSeats(int originalI, int originalJ)
            {
                int takenSeats = 0;
                for (int i = originalI - 1; i <= originalI + 1; i++)
                {
                    if (i < 0 || i >= SizeY)
                    {
                        continue;
                    }
                    else
                    {
                        for (int j = originalJ - 1; j <= originalJ + 1; j++)
                        {
                            if (j < 0 || j >= SizeX || (i == originalI && j == originalJ))
                            {
                                continue;
                            }
                            else if (OldSeats[i, j] == '#')
                            {
                                takenSeats++;
                            }
                        }
                    }
                }

                if (takenSeats >= 4)
                {
                    return true;
                }

                return false;
            }

            private bool CheckOccupiedSeatsStrict(int originalI, int originalJ)
            {
                int takenSeats = 0;

                bool seat1Found = false;
                bool seat2Found = false;
                bool seat3Found = false;
                bool seat4Found = false;
                bool seat5Found = false;
                bool seat6Found = false;
                bool seat7Found = false;
                bool seat8Found = false;

                for (int i = 1; i < SizeX && i < SizeY; i++)
                {
                    if (!seat1Found)
                    {
                        if (originalI - i < 0)
                        {
                            seat1Found = true;
                        }
                        else if(OldSeats[originalI - i, originalJ] != '.')
                        {
                            if (OldSeats[originalI - i, originalJ] == '#')
                            {
                                takenSeats++;
                            }
                            seat1Found = true;
                        }
                    }

                    if (!seat2Found)
                    {
                        if (originalI - i < 0 || originalJ + i >= SizeX)
                        {
                            seat2Found = true;
                        }
                        else if (OldSeats[originalI - i, originalJ + i] != '.')
                        {
                            if (OldSeats[originalI - i, originalJ + i] == '#')
                            {
                                takenSeats++;
                            }
                            seat2Found = true;
                        }
                    }

                    if (!seat3Found)
                    {
                        if (originalJ + i >= SizeX)
                        {
                            seat3Found = true;
                        }
                        else if (OldSeats[originalI, originalJ + i] != '.')
                        {
                            if (OldSeats[originalI, originalJ + i] == '#')
                            {
                                takenSeats++;
                            }
                            seat3Found = true;
                        }
                    }

                    if (!seat4Found)
                    {
                        if (originalI + i >= SizeY || originalJ + i >= SizeX)
                        {
                            seat4Found = true;
                        }
                        else if (OldSeats[originalI + i, originalJ + i] != '.')
                        {
                            if (OldSeats[originalI + i, originalJ + i] == '#')
                            {
                                takenSeats++;
                            }
                            seat4Found = true;
                        }
                    }

                    if (!seat5Found)
                    {
                        if (originalI + i >= SizeY)
                        {
                            seat5Found = true;
                        }
                        else if (OldSeats[originalI + i, originalJ] != '.')
                        {
                            if (OldSeats[originalI + i, originalJ] == '#')
                            {
                                takenSeats++;
                            }
                            seat5Found = true;
                        }
                    }

                    if (!seat6Found)
                    {
                        if (originalI + i >= SizeY || originalJ - i < 0)
                        {
                            seat6Found = true;
                        }
                        else if (OldSeats[originalI + i, originalJ - i] != '.')
                        {
                            if (OldSeats[originalI + i, originalJ - i] == '#')
                            {
                                takenSeats++;
                            }
                            seat6Found = true;
                        }
                    }

                    if (!seat7Found)
                    {
                        if (originalJ - i < 0)
                        {
                            seat7Found = true;
                        }
                        else if (OldSeats[originalI, originalJ - i] != '.')
                        {
                            if (OldSeats[originalI, originalJ - i] == '#')
                            {
                                takenSeats++;
                            }
                            seat7Found = true;
                        }
                    }

                    if (!seat8Found)
                    {
                        if (originalI - i < 0 || originalJ - i < 0)
                        {
                            seat8Found = true;
                        }
                        else if (OldSeats[originalI - i, originalJ - i] != '.')
                        {
                            if (OldSeats[originalI - i, originalJ - i] == '#')
                            {
                                takenSeats++;
                            }
                            seat8Found = true;
                        }
                    }

                    if (seat1Found &&
                        seat2Found &&
                        seat3Found &&
                        seat4Found &&
                        seat5Found &&
                        seat6Found &&
                        seat7Found &&
                        seat8Found)
                    {
                        if (takenSeats >= 5)
                        {
                            return true;
                        }

                        return false;
                    }
                }

                return false;
            }
        }

        private static SeatMap LoadSeatMap(int sizeY, int sizeX)
        {
            SeatMap map = new SeatMap();
            map.SizeX = sizeX;
            map.SizeY = sizeY;
            map.Seats = new char[sizeY, sizeX];

            for(int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    map.Seats[i, j] = Input[i][j];
                }
            }

            return map;
        }

        private static void Part1()
        {
            SeatMap map = LoadSeatMap(Input.Count, Input[0].Length);

            map.ProcessRound();
            return;
        }

        private static void Part2()
        {
            SeatMap map = LoadSeatMap(Input.Count, Input[0].Length);

            map.ProcessRound(2);
            return;
        }

        public static void Start()
        {
            Part1();

            Part2();
        }
    }
}
