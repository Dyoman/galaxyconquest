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

        public Color player_color;
        public static List<Ship> player_ship;
        public static List<PLANET> player_planets;
        public List<StarSystem> player_stars; //звездные системы игрока
        public List<Fleet> player_fleets;

        public Player()
        {
            technologies.Add(new int[] { 0, 1 });
            buildings.Add(new int[] { 0, 0, 0 });
            buildings.Add(new int[] { 0, 0, 1 });

            player_stars = new List<StarSystem>();
            player_fleets = new List<Fleet>();
            player_color = Color.Red;
            player_planets = new List<PLANET>();
            player_ship = new List<Ship>();

        }

    }
}
