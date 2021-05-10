using System;
using System.Collections.Generic;
using System.Linq;
using static Galaxies.CustomMethods;
namespace Galaxies
{
    public abstract class Object
    {
        public abstract string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var galaxyList = new List<Galaxy>();
            var starList = new List<Star>();
            var planetList = new List<Planet>();
            var moonList = new List<Moon>();
            var starDictionary = new Dictionary<string, List<Star>>();
            var planetDictionary = new Dictionary<string, List<Planet>>();
            var moonDictionary = new Dictionary<string, List<Moon>>();  
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit") break;
                else
                {
                    if (input.Contains("add"))
                    {
                        string name = Extractor(input, '[', ']');
                        string[] separators = { " ", "[", "]", "add", name };
                        var split = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        if (split[0] == "galaxy")
                        {
                            string type = split[1];
                            string age = split[2];
                            var galaxy = new Galaxy(name, type, age);
                            galaxyList.Add(galaxy);
                            starDictionary.Add(galaxy.Name, new List<Star>());
                        }
                        else if (split[0] == "star")
                        {
                            bool isFound = false;
                            foreach (var galaxy in galaxyList)
                            {
                                if (galaxy.Name == name)
                                {
                                    isFound = true;
                                    break;
                                }
                            }
                            if (isFound)
                            {
                                string parent = name;
                                string starName = split[1];
                                double mass = double.Parse(split[2]);
                                double size = double.Parse(split[3]);
                                int temp = int.Parse(split[4]);
                                double lumin = double.Parse(split[5]);
                                var star = new Star(starName, mass, size, temp, lumin);
                                if (star.StarClass == 'X')
                                {
                                    Console.WriteLine("Error: Invalid star class.");
                                }
                                starList.Add(star);
                                starDictionary[parent].Add(star);
                                planetDictionary.Add(star.Name, new List<Planet>());
                            }
                            else
                            {
                                Console.WriteLine("No galaxy found.");
                            }
                        }
                        else if (split[0] == "planet")
                        {
                            bool isFound = false;
                            foreach (var star in starList)
                            {
                                if (star.Name == name)
                                {
                                    isFound = true;
                                    break;
                                }
                            }
                            if (isFound)
                            {
                                string parent = name;
                                string planetName = split[1];
                                string type = split[2];
                                string habit = split[3];
                                var planet = new Planet(planetName, type, habit);
                                planetList.Add(planet);
                                planetDictionary[parent].Add(planet);
                                moonDictionary.Add(planet.Name, new List<Moon>());
                            }
                            else
                            {
                                Console.WriteLine("No star system found.");
                            }
                        }
                        else if (split[0] == "moon")
                        {
                            bool isFound = false;
                            foreach (var planet in planetList)
                            {
                                if (planet.Name == name)
                                {
                                    isFound = true;
                                    break;
                                }
                            }
                            if (isFound)
                            {
                                string parent = name;
                                string moonName = split[1];
                                var moon = new Moon(moonName);
                                moonList.Add(moon);
                                moonDictionary[parent].Add(moon);
                            }
                            else
                            {
                                Console.WriteLine("No planet found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unknown object.");
                        }
                    }
                    else if (input.Contains("print"))
                    {
                        string name = Extractor(input, '[', ']');
                        bool galaxyIsFound = false;
                        foreach (var galaxy in galaxyList)
                        {
                            if (galaxy.Name == name)
                            {
                                galaxyIsFound = true;
                                Console.WriteLine($"*** Data for {galaxy.Name} galaxy ***");
                                Console.WriteLine($"Type: {galaxy.Type}");
                                Console.WriteLine($"Age: {galaxy.Age}");
                                Console.Write("Stars:\n");
                                int starCounter = 0;
                                foreach (var kvpStar in starDictionary)
                                {
                                    if (kvpStar.Key == name)
                                    {
                                        foreach (var star in kvpStar.Value)
                                        {
                                            starCounter += 1;
                                            Console.WriteLine($"      Name: {star.Name}");
                                            Console.WriteLine($"      Class: {star.StarClass} ({star.Mass}, {star.Size}, {star.Temp}, {star.Lumin})");
                                            Console.Write("      Planets:\n");
                                            int planetCounter = 0;
                                            foreach (var kvpPlanet in planetDictionary)
                                            {
                                                if (kvpPlanet.Key == star.Name)
                                                {
                                                    foreach (var planet in kvpPlanet.Value)
                                                    {
                                                        planetCounter += 1;
                                                        Console.WriteLine($"          Name: {planet.Name}");
                                                        Console.WriteLine($"          Type: {planet.PlanetType}");
                                                        Console.WriteLine($"          Support life: {planet.Habit}");
                                                        Console.Write("          Moons:\n");
                                                        int moonCounter = 0;
                                                        foreach (var kvpMoon in moonDictionary)
                                                        {
                                                            if (kvpMoon.Key == planet.Name)
                                                            {
                                                                foreach (var moon in kvpMoon.Value)
                                                                {
                                                                    moonCounter += 1;
                                                                    Console.Write($"              Name: {moon.Name}");
                                                                }
                                                            }
                                                        }
                                                        if (moonCounter == 0)
                                                        {
                                                            Console.Write(" -\n");
                                                        }
                                                    }
                                                }
                                            }
                                            if (planetCounter == 0)
                                            {
                                                Console.Write(" -\n");
                                            }
                                        }
                                    }
                                }
                                if (starCounter == 0)
                                {
                                    Console.Write(" -\n");
                                }
                                Console.WriteLine($"\n*********************************************");
                            }
                        }
                        if (!galaxyIsFound)
                        {
                            Console.WriteLine("No galaxy found.");
                        }
                    }
                    else if (input.Contains("stats"))
                    {
                        Statistic(galaxyList, starList, planetList, moonList);
                    }
                    else if (input.Contains("list"))
                    {
                        if (input.Contains("galaxies"))
                        {
                            List("galaxies", galaxyList, starList, planetList, moonList);
                        }
                        else if (input.Contains("stars"))
                        {
                            List("stars", galaxyList, starList, planetList, moonList);
                        }
                        else if (input.Contains("planets"))
                        {
                            List("planets", galaxyList, starList, planetList, moonList);
                        }
                        else if (input.Contains("moons"))
                        {
                            List("moons", galaxyList, starList, planetList, moonList);
                        }
                        else
                        {
                            Console.WriteLine("Unknown object.");
                        }
                    }
                }
            }
        }
    }
    public static class CustomMethods
    {
        public static void Statistic(List<Galaxy> galaxyList, List<Star> starList, List<Planet> planetList, List<Moon> moonList)
        {
            Console.WriteLine("*** Stats ***");
            if (galaxyList.Count == 0) Console.WriteLine("Galaxies: -");
            else Console.WriteLine($"Galaxies: {galaxyList.Count}");
            if (starList.Count == 0) Console.WriteLine("Stars: -");
            else Console.WriteLine($"Stars: {starList.Count}");
            if (planetList.Count == 0) Console.WriteLine("Planets: -");
            else Console.WriteLine($"Planets: {planetList.Count}");
            if (moonList.Count == 0) Console.WriteLine("Moons: -");
            else Console.WriteLine($"Moons: {moonList.Count}");
            Console.WriteLine("*********************************************");
        }
        public static void List(string command, List<Galaxy> galaxyList, List<Star> starList, List<Planet> planetList, List<Moon> moonList)
        {
            if (command == "galaxies")
            {
                Console.WriteLine("*** Galaxy List ***");
                if (galaxyList.Count == 0)
                {
                    Console.WriteLine("-");
                }
                else
                {
                    for (int counter = 0; counter < galaxyList.Count; counter++)
                    {
                        if (counter == galaxyList.Count - 1)
                        {
                            Console.Write($"{galaxyList[counter].Name}\n");
                        }
                        else
                        {
                            Console.Write($"{galaxyList[counter].Name}, ");
                        }
                    }
                }
                Console.WriteLine("*********************************************");
            }
            else if (command == "stars")
            {
                Console.WriteLine("*** Star List ***");
                if (starList.Count == 0)
                {
                    Console.WriteLine("-");
                }
                else
                {
                    for (int counter = 0; counter < starList.Count; counter++)
                    {
                        if (counter == starList.Count - 1)
                        {
                            Console.Write($"{starList[counter].Name}\n");
                        }
                        else
                        {
                            Console.Write($"{starList[counter].Name}, ");
                        }
                    }
                }
                Console.WriteLine("*********************************************");
            }
            else if (command == "planets")
            {
                Console.WriteLine("*** Planet List ***");
                if (planetList.Count == 0)
                {
                    Console.WriteLine("-");
                }
                else
                {
                    for (int counter = 0; counter < planetList.Count; counter++)
                    {

                        if (counter == planetList.Count - 1)
                        {
                            Console.Write($"{planetList[counter].Name}\n");
                        }
                        else
                        {
                            Console.Write($"{planetList[counter].Name}, ");
                        }
                    }
                }
                Console.WriteLine("*********************************************");
            }
            else if (command == "moons")
            {
                Console.WriteLine("*** Moon List ***");
                if (moonList.Count == 0)
                {
                    Console.WriteLine("-");
                }
                else
                {
                    for (int counter = 0; counter < moonList.Count; counter++)
                    {

                        if (counter == moonList.Count - 1)
                        {
                            Console.Write($"{moonList[counter].Name}\n");
                        }
                        else
                        {
                            Console.Write($"{moonList[counter].Name}, ");
                        }
                    }
                }
                Console.WriteLine("*********************************************");
            }
        }
        public static string Extractor(string input, char charFrom, char charTo)
        {
            int posFrom = input.IndexOf(charFrom);
            if (posFrom != -1)
            {
                int posTo = input.IndexOf(charTo, posFrom + 1);
                if (posTo != -1)
                {
                    return input.Substring(posFrom + 1, posTo - posFrom - 1);
                }
            }
            return string.Empty;
        }
    }
}