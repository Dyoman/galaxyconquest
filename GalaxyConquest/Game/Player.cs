using System;
using System.Collections.Generic;
using System.Drawing;
using GalaxyConquest.StarSystems;
using GalaxyConquest.Tactics;

namespace GalaxyConquest.Game
{
    /// <summary>
    /// Представляет игрока и все его параметры
    /// </summary>
    [Serializable]
    public class Player
    {
        public static List<int[]> technologies = new List<int[]>();//format {tier, techLine, subtech}
        public static List<int[]> buildings = new List<int[]>();//format {star system, planet, building}
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string name;
        public Color player_color;
        public List<Ship> player_ship;
        /// <summary>
        /// Планеты игрока (спорно)
        /// </summary>
        public List<PLANET> player_planets;
        /// <summary>
        /// Звездные системы игрока
        /// </summary>
        public List<StarSystem> stars;
        /// <summary>
        /// Флоты игрока
        /// </summary>
        public List<Fleet> fleets;
        /// <summary>
        /// Индекс выбранного флота
        /// </summary>
        public int selectedFleet;
        /// <summary>
        /// Выбранная звездная система
        /// </summary>
        public StarSystem selectedStar;
        /// <summary>
        /// Цель для перемещения !Не выбранная! Не путать. Изменяется при наведении курсора мыши на звезду
        /// </summary>
        public StarSystem warpTarget;
        /// <summary>
        /// Капитал игрока
        /// </summary>
        public double credit;
        /// <summary>
        /// Энергия
        /// </summary>
        public double energy;
        /// <summary>
        /// Минералы
        /// </summary>
        public double minerals;
        /// <summary>
        /// Очки изучения
        /// </summary>
        public double skillPoints;

        public Player()
        {
            name = "New player";
            technologies.Add(new int[] { 0, 0, 0 });
            technologies.Add(new int[] { 0, 1, 0 });
            technologies.Add(new int[] { 0, 2, 0 });
            technologies.Add(new int[] { 0, 3, 0 });
            technologies.Add(new int[] { 0, 4, 0 });
            technologies.Add(new int[] { 0, 5, 0 });// add the names of tech trees
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
            technologies.Add(new int[] { 0, 0, 0 });
            technologies.Add(new int[] { 0, 1, 0 });
            technologies.Add(new int[] { 0, 2, 0 });
            technologies.Add(new int[] { 0, 3, 0 });
            technologies.Add(new int[] { 0, 4, 0 });
            technologies.Add(new int[] { 0, 5, 0 });// add the names of tech trees
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
