using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Moon
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public Velocity Velocity { get; set; } = new Velocity();

        public int PotentialEnergy { 
            get 
            {
                return Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
            }
        }

        public int KineticEnergy { 
            get
            {
                return Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
            }
        }

        public int TotalEnergy
        {
            get
            {
                return PotentialEnergy * KineticEnergy;
            }
        }

        public Moon(Position position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return $"pos=<x={Position.X}, y={Position.Y}, z={Position.Z}>, vel=<x={Velocity.X}, y={Velocity.Y}, z={Velocity.Z}>";
        }
    }

    public class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(Position other)
        {
            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y && Z == other.Z;
        }
    }

    public class Velocity : IEquatable<Velocity>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Velocity()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public bool Equals(Velocity other)
        {
            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y && Z == other.Z;
        }
    }

    public static class Day12
    {
        private static List<string> Input => InputHelper.GetInputListString(2019, 12).ToList();

        public static List<Moon> CreateMoons()
        {
            List<Moon> moons = new List<Moon>();

            foreach (string line in Input)
            {
                string[] location = line.Split(",");
                Position position = new Position(int.Parse(location[0].Substring(3)), int.Parse(location[1].Substring(3)), int.Parse(location[2].Split(">")[0].Substring(3)));
                Moon moon = new Moon(position);
                moons.Add(moon);
            }

            return moons;
        }

        public static List<Moon> CalculateVelocity(List<Moon> moons)
        {
            for (int i = 0; i < moons.Count - 1; i++)
            {
                for (int j = i + 1; j < moons.Count; j++)
                {
                    if (moons[i].Position.X > moons[j].Position.X)
                    {
                        moons[i].Velocity.X--;
                        moons[j].Velocity.X++;
                    }
                    else if (moons[i].Position.X < moons[j].Position.X)
                    {
                        moons[i].Velocity.X++;
                        moons[j].Velocity.X--;
                    }

                    if (moons[i].Position.Y > moons[j].Position.Y)
                    {
                        moons[i].Velocity.Y--;
                        moons[j].Velocity.Y++;
                    }
                    else if (moons[i].Position.Y < moons[j].Position.Y)
                    {
                        moons[i].Velocity.Y++;
                        moons[j].Velocity.Y--;
                    }

                    if (moons[i].Position.Z > moons[j].Position.Z)
                    {
                        moons[i].Velocity.Z--;
                        moons[j].Velocity.Z++;
                    }
                    else if (moons[i].Position.Z < moons[j].Position.Z)
                    {
                        moons[i].Velocity.Z++;
                        moons[j].Velocity.Z--;
                    }
                }
            }

            return moons;
        }

        private static void UpdatePosition(Moon moon)
        {
            moon.Position.X += moon.Velocity.X;
            moon.Position.Y += moon.Velocity.Y;
            moon.Position.Z += moon.Velocity.Z;
        }

        public static long GCD(long a, long b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return GCD(b, a % b);
            }
        }

        public static long LCM(long a, long b, long c)
        {
            if (a == 0 || b == 0 || c ==0)
            {
                return 0;
            }
            else
            {
                return ((a*b*c) / GCD(a*c, GCD(a*b, b*c)));
            }
        }

        public static void Part1(List<Moon> moons, int totalSteps)
        {
            int step = 0;

            while (step < totalSteps)
            {
                moons = CalculateVelocity(moons);
                moons.ForEach(UpdatePosition);
                step++;
            }

            Console.WriteLine($"The total energy after {totalSteps} steps is {moons.Sum(moon => moon.TotalEnergy)}.");
        }

        public static void Part2(List<Moon> moons)
        {
            long step = 0;
            long xVelocityZero = -1;
            long yVelocityZero = -1;
            long zVelocityZero = -1;
            long totalSteps = 0;

            List<Moon> moonStart = new List<Moon>();
            foreach (Moon moon in moons)
            {
                moonStart.Add(new Moon(new Position(moon.Position.X, moon.Position.Y, moon.Position.Z)));
            }

            while (true)
            {
                moons = CalculateVelocity(moons);
                moons.ForEach(UpdatePosition);
                step++;

                if (xVelocityZero == -1 && moons.All(moon => moon.Velocity.X == 0))
                {
                    xVelocityZero = step;
                }

                if (yVelocityZero == -1 && moons.All(moon => moon.Velocity.Y == 0))
                {
                    yVelocityZero = step;
                }

                if (zVelocityZero == -1 && moons.All(moon => moon.Velocity.Z == 0))
                {
                    zVelocityZero = step;
                }

                if (xVelocityZero != -1 && yVelocityZero != -1 && zVelocityZero != -1)
                {
                    Console.WriteLine("All velocities have been zero");
                    totalSteps = (LCM(xVelocityZero, yVelocityZero, zVelocityZero) * 2);
                    break;
                }
            }

            Console.WriteLine($"After {totalSteps} steps the position is again the same!");
            return;
        }

        public static void Start()
        {
            //List<string> input = new List<string> { "<x=-1, y=0, z=2>", "<x=2, y=-10, z=-7>", "<x=4, y=-8, z=8>", "<x=3, y=5, z=-1>" };
            //List<string> input = new List<string> { "<x=-8, y=-10, z=0>", "<x=5, y=5, z=10>", "<x=2, y=-7, z=3>", "<x=9, y=-8, z=-3>" };

            List<Moon> moons = CreateMoons();

            //Part1(moons, 1000);
            Part2(moons);
        }
    }
}
