using System;
using System.Collections.Generic;

namespace GalaxyConquest
{
    [Serializable]
    public class Player
    {
        public int x = -200;
        public int y = -200;
        public int z = 0;
        public string name;
        public static List<int> technologies = new List<int>();
        public List<StarSystem> player_stars; //звездные системы игрока

        public Player()
        {
            technologies.Add(0);
            technologies.Add(2);
            player_stars = new List<StarSystem>();
            name = "KosmoPlayer";
            /*if (Form1.SelfRef != null)
            {
                player_stars.Add(Form1.SelfRef.galaxy.stars[3]);
                Form1.SelfRef.galaxy.stars[3].name = "Player";
            }
            */
        }

    }
}
