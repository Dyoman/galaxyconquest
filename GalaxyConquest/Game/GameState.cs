using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest.Game
{
    /// <summary>
    /// "Семя" для создания игры. Включает в себя все основные параметры
    /// </summary>
    [Serializable]
    public struct GameSeed
    {
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string pName;
        /// <summary>
        /// Имя галактики
        /// </summary>
        public string gName;
        /// <summary>
        /// Тип галактики
        /// </summary>
        public GalaxyType gType;
        /// <summary>
        /// Размер галактики
        /// </summary>
        public int gSize;
        /// <summary>
        /// Количество звезд в галактике
        /// </summary>
        public int gStarsCount;
        /// <summary>
        /// Флаг генерации случайных событий. Если true, то в галактике будут происходить случайные события
        /// </summary>
        public bool gGenerateRandomEvent;
    }

    /// <summary>
    /// Класс объединяет игрока с галактикой
    /// </summary>
    [Serializable]
    public class GameState
    {
        /// <summary>
        /// Игрок
        /// </summary>
        public Player Player { get; private set; }
        /// <summary>
        /// Галактика, в которой находится игрок
        /// </summary>
        public ModelGalaxy Galaxy { get; private set; }
        /// <summary>
        /// Создание новой игры
        /// </summary>
        /// <param name="seed">Семя для создания игры, содержащее все необходимые параметры</param>
        public void New(GameSeed seed)
        {
            Galaxy = new ModelGalaxy();
            Player = new Player(seed.pName);
            Random rand = new Random((int)DateTime.Now.Ticks);

            Galaxy.GenerateNew(seed.gName, seed.gType, seed.gSize, seed.gStarsCount, seed.gGenerateRandomEvent);
            StarSystem s = Galaxy.stars[rand.Next(Galaxy.stars.Count - 1)];

            Player.stars.Add(s);
            for (int i = 0; i < s.planets.Count; i++)
                Player.player_planets.Add(s.planets[i]);

            Player.fleets.Add(new Fleet(Player, rand.Next(2, 5), s));
            s.Discovered = true;

            int count = rand.Next(1, 3);
            for (int i = 0; i < count; i++)
            {
                StarSystem ns = Galaxy.stars[rand.Next(Galaxy.stars.Count - 1)];
                while (Player.stars.Contains(ns))
                    ns = Galaxy.stars[rand.Next(Galaxy.stars.Count - 1)];

                Galaxy.neutrals.Add(new Fleet(null, rand.Next(1, 4), ns));
            }
        }
    
    }
}
