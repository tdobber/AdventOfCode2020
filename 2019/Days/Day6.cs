using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class Day6
    {
        private static List<string> Input => InputHelper.GetInputListString(2019, 6).ToList();

        public class Planet
        {
            public string Name { get; set; }
            public Planet Orbits { get; set; } = null;
            public List<Planet> BeingOrbitedBy { get; set; } = new List<Planet>();

            public Planet(string name)
            {
                Name = name;
            }
        }

        public static List<Planet> CreateUniversalOrbitMap()
        {
            List<Planet> mappedPlanets = new List<Planet>();

            foreach (string orbit in Input)
            {
                string parentPlanetName = orbit.Split(')')[0];
                string childPlanetName = orbit.Split(')')[1];

                Planet parentPlanet = mappedPlanets.FirstOrDefault(planet => planet.Name.Equals(parentPlanetName));
                Planet childPlanet = mappedPlanets.FirstOrDefault(planet => planet.Name.Equals(childPlanetName));

                if (parentPlanet == null)
                {
                    parentPlanet = new Planet(parentPlanetName);
                    mappedPlanets.Add(parentPlanet);
                }

                if (childPlanet == null)
                {
                    childPlanet = new Planet(childPlanetName);
                    mappedPlanets.Add(childPlanet);
                }

                parentPlanet.BeingOrbitedBy.Add(childPlanet);
                childPlanet.Orbits = parentPlanet;
            }

            return mappedPlanets;
        }

        public static int CalculateTotalOrbits(Planet planet, int depth = 0)
        {
            int totalOrbit = depth;
            foreach (Planet childPlanet in planet.BeingOrbitedBy)
            {
                totalOrbit += CalculateTotalOrbits(childPlanet, depth + 1);
            }

            return totalOrbit;
        }

        public static int CalculateOrbitalTransfers(Planet planet, Planet previousPlanet = null, int distance = 0)
        {
            int totalTransfers = 0;

            if (planet.BeingOrbitedBy.Any(child => child.Name.Equals("SAN")))
            {
                totalTransfers = distance;
            }
            else
            {
                List<Planet> neighbourPlanets = new List<Planet>();
                if (planet.Orbits != null)
                    neighbourPlanets.Add(planet.Orbits);
                neighbourPlanets.AddRange(planet.BeingOrbitedBy);

                if (previousPlanet != null)
                    neighbourPlanets.Remove(previousPlanet);

                foreach (Planet neighbour in neighbourPlanets)
                {
                    totalTransfers = CalculateOrbitalTransfers(neighbour, planet, distance + 1);
                    if (totalTransfers != 0)
                        break;
                }
            }

            return totalTransfers;
        }

        public static int Part1()
        {
            List<Planet> mappedPlanets = CreateUniversalOrbitMap();
            Planet rootPlanet = mappedPlanets.First(planet => planet.Orbits == null);

            int totalOrbits = CalculateTotalOrbits(rootPlanet);

            return totalOrbits;
        }

        public static int Part2()
        {
            List<Planet> mappedPlanets = CreateUniversalOrbitMap();
            Planet youPlanet = mappedPlanets.First(planet => planet.Name == "YOU");

            int totalOrbitalTransfers = CalculateOrbitalTransfers(youPlanet.Orbits);

            return totalOrbitalTransfers;
        }

        public static void Start()
        {
            //List<string> input = new List<string> { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" };
            //List<string> input = new List<string> { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN" };

            int checksum = Part1();
            Console.WriteLine($"The total number of orbits in part 1 is: {checksum}");

            int totalOrbitalTransfers = Part2();
            Console.WriteLine($"The total number of orbital transfers in part 2 is: {totalOrbitalTransfers}");
        }
    }
}
