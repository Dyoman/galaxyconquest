using System;
using System.Collections.Generic;
using System.Drawing;
using GalaxyConquest.Tactics;
using GalaxyConquest.SpaceObjects;

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

        /// <summary>
        /// Свойство возвращает true, если игрок изучает какую-то технологию в данный момент.
        /// </summary>
        public bool Learning
        {
            get
            {
                return !learningTech.isNone();
            }
        }
        /// <summary>
        /// Прогресс изучения.
        /// </summary>
        int learningProgress = 0;
        /// <summary>
        /// Данные об изучаемой технологии.
        /// </summary>
        public TechData learningTech { get; private set; }

        public Player()
        {
            name = "none";
            technologies.Add(new int[] { 0, 0, 0 });
            technologies.Add(new int[] { 0, 1, 0 });
            technologies.Add(new int[] { 0, 2, 0 });
            technologies.Add(new int[] { 0, 3, 0 });
            technologies.Add(new int[] { 0, 4, 0 });
            technologies.Add(new int[] { 0, 5, 0 });// add the names of tech trees
            buildings.Add(new int[] { 0, 0, 0 });
            buildings.Add(new int[] { 0, 0, 1 });

            learningTech = new TechData();
            stars = new List<StarSystem>();
            fleets = new List<Fleet>();
            player_color = Color.Red;

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

            learningTech = new TechData();
            stars = new List<StarSystem>();
            fleets = new List<Fleet>();
            player_color = Color.Red;

            credit = 0;
            minerals = 0;
            energy = 0;
        }
        /// <summary>
        /// Осуществляет все изменения, которые должны происходить с игроком во время шага.
        /// Аналог метода SpaceObject.Process().
        /// </summary>
        public void Process()
        {
            ProcessLearning(); 
            
            for (int i = 0; i <stars.Count; i++)
                for (int j = 0; j < stars[i].planets.Count; j++)
                {
                    credit += stars[i].planets[j].PROFIT;
                    minerals += stars[i].planets[j].MINERALS;
                    skillPoints += stars[i].planets[j].skillPointProduce;
                }
        }

        /// <summary>
        /// Начинает изучение игроком технологии. Возвращает true, если изучение началось и false в противном случае.
        /// </summary>
        /// <param name="tech">Параметры изучаемой технологии.</param>
        public bool Learn(TechData tech)
        {
            if (Learning)
                return false;

            learningTech = tech;
            return true;
        }
        /// <summary>
        /// Получает прогресс изучения технологии игроком.
        /// </summary>
        /// <returns></returns>
        public int getLearningProgress()
        {
            return learningProgress;
        }
        /// <summary>
        /// Получает прогресс изучения технологии игроком в процентах.
        /// </summary>
        /// <returns></returns>
        public float getLearningProgressPercent()
        {
            return (float)learningProgress/(float)Tech.learning_tech_time*(float)100;
        }
        /// <summary>
        /// Получает прогресс изучения технологии игроком за один ход в процентах.
        /// </summary>
        /// <returns></returns>
        public float getStepLearningProgressPercent()
        {
            return (float)1 / (float)Tech.learning_tech_time * (float)100;
        }
        /// <summary>
        /// Процесс изучения технологии.
        /// </summary>
        void ProcessLearning()
        {
            if (!Learning) return;

            learningProgress++;

            if (learningProgress >= Tech.learning_tech_time)
            {
                technologies.Add(new int[] { learningTech.Tier, learningTech.Line, learningTech.Subtech });
                learningProgress = 0;
                learningTech = new TechData();
                Tech.CheckTechInnovaions();
            }
        }
    }
}
