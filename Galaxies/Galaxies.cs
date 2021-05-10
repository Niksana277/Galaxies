using System.Collections.Generic;
namespace Galaxies
{
    public class Galaxy : Object
    {
        public Galaxy(string name, string type, string age)
        {
            Name = name;
            Type = type;
            Age = age;
        }
        public override string Name { get; set; }
        public string Type { get; set; }
        public string Age { get; set; }
    }
    public class Star : Object
    {
        public Star(string name, double mass, double size, int temp, double lumin)
        {
            Name = name;
            Mass = mass;
            Size = size;
            Temp = temp;
            Lumin = lumin;
            StarClass = ClassFinder();
        }
        public override string Name { get; set; }
        public char StarClass { get; set; }
        public double Mass { get; set; }
        public double Size { get; set; }
        public int Temp { get; set; }
        public double Lumin { get; set; }
        private char ClassFinder()
        {
            if (Temp >= 30000 && Lumin >= 30000 & Mass >= 16 & Size / 2 >= 6.6)
            {
                return 'O';
            }
            else if (Temp >= 10000 && Temp < 30000 && Lumin >= 25 && Lumin < 30000 &&
                     Mass >= 2.1 && Mass < 16 && Size / 2 >= 1.8 && Size / 2 < 6.6)
            {
                return 'B';
            }
            else if (Temp >= 7500 && Temp < 10000 && Lumin >= 5 && Lumin < 25 &&
                     Mass >= 1.4 && Mass < 2.1 && Size / 2 >= 1.4 && Size / 2 < 1.8)
            {
                return 'A';
            }
            else if (Temp >= 6000 && Temp < 7500 && Lumin >= 1.5 && Lumin < 5 &&
                     Mass >= 1.04 && Mass < 1.4 && Size / 2 >= 1.15 && Size / 2 < 1.4)
            {
                return 'F';
            }
            else if (Temp >= 5200 && Temp < 6000 && Lumin >= 0.6 && Lumin < 1.5 &&
                     Mass >= 0.8 && Mass < 1.04 && Size / 2 >= 0.96 && Size / 2 < 1.15)
            {
                return 'G';
            }
            else if (Temp >= 3700 && Temp < 5200 && Lumin >= 0.08 && Lumin < 0.6 &&
                     Mass >= 0.45 && Mass < 0.8 && Size / 2 >= 0.7 && Size / 2 < 0.96)
            {
                return 'K';
            }
            else if (Temp >= 2400 && Temp < 3700 && Lumin <= 0.08 && Mass >= 0.08 && Mass < 0.45 &&
                     Size / 2 <= 0.7)
            {
                return 'M';
            }
            else return 'X';
        }
    }
    public class Planet : Object
    {
        public Planet(string name, string type, string habit)
        {
            Name = name;
            PlanetType = type;
            Habit = habit;
        }
        public override string Name { get; set; }
        public string PlanetType { get; set; }
        public string Habit { get; set; }
    }
    public class Moon : Object
    {
        public Moon(string name)
        {
            Name = name;
        }
        public override string Name { get; set; }
    }
}