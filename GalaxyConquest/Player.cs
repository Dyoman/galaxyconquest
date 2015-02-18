using System;
using System.Collections.Generic;
using System.Drawing;
using GalaxyConquest.StarSystems;
using GalaxyConquest.Tactics;

namespace GalaxyConquest
{
    [Serializable]
    public class Player
    {
        public static List<int[]> technologies = new List<int[]>();//format {tech, subtech}
        public static List<int[]> buildings = new List<int[]>();//format {star system, planet, building}

        public string name;
        public Color player_color;
        public List<Ship> player_ship;
        public List<PLANET> player_planets;
        public List<StarSystem> stars; //звездные системы игрока
        public List<Fleet> fleets;

        public int selectedFleet;
        public StarSystem selectedStar;
        public StarSystem warpTarget;

        public double credit;
        public double energy;
        public double minerals;

        public Player()
        {
            name = "New player";
            technologies.Add(new int[] { 0, 1 });
            buildings.Add(new int[] { 0, 0, 0 });
            buildings.Add(new int[] { 0, 0, 1 });

            stars = new List<StarSystem>();
            fleets = new List<Fleet>();
            player_color = Color.Red;
            player_planets = new List<PLANET>();
            player_ship = new List<Ship>();

            credit = 0;
            minerals = 0;
            energy = 0;
        }

        public Player(string name)
        {
            this.name = name;
            technologies.Add(new int[] { 0, 1 });
            buildings.Add(new int[] { 0, 0, 0 });
            buildings.Add(new int[] { 0, 0, 1 });

            stars = new List<StarSystem>();
            fleets = new List<Fleet>();
            player_color = Color.Red;
            player_planets = new List<PLANET>();
            player_ship = new List<Ship>();

            credit = 0;
            minerals = 0;
            energy = 0;
        }
    }
}
