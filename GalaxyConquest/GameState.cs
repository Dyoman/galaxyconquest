using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest
{
    /// <summary>
    /// "Семя" для создания игры. Включает в себя все основные параметры
    /// </summary>
    [Serializable]
    public struct GameSeed
    {
        public string pName;
        public string gName;
        public GalaxyType gType;
        public int gSize;
        public int gStarsCount;
        public bool gGenerateRandomEvent;
    }

    [Serializable]
    public class GameState
    {
        Player player;
        ModelGalaxy galaxy;

        public void New(GameSeed seed)
        {
            galaxy = new ModelGalaxy();
            player = new Player(seed.pName);
            Random rand = new Random((int)DateTime.Now.Ticks);

            galaxy.GenerateNew(seed.gName, seed.gType, seed.gSize, seed.gStarsCount, seed.gGenerateRandomEvent);
            StarSystem s = galaxy.stars[rand.Next(galaxy.stars.Count - 1)];

            player.stars.Add(s);
            for (int i = 0; i < s.PLN.Count; i++)
                player.player_planets.Add(s.PLN[i]);

            player.fleets.Add(new Fleet(player, rand.Next(2, 5), s));

            int count = rand.Next(1, 3);
            for (int i = 0; i < count; i++)
            {
                StarSystem ns = galaxy.stars[rand.Next(galaxy.stars.Count - 1)];
                while (player.stars.Contains(ns))
                    ns = galaxy.stars[rand.Next(galaxy.stars.Count - 1)];

                galaxy.neutrals.Add(new Fleet(null, rand.Next(1, 4), ns));
            }
        }

        public Player Player
        {
            get
            {
                return player;
            }
        }

        public ModelGalaxy Galaxy
        {
            get
            {
                return galaxy;
            }
        }
    
    }
}
