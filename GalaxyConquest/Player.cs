using System;
using System.Collections.Generic;

namespace GalaxyConquest
{
    [Serializable]
    public class Player
    {
        public List<StarSystem> player_stars; //звездные системы игрока

        public Player()
        {
            player_stars = new List<StarSystem>();
            if (Form1.SelfRef != null)
            {
                player_stars.Add(Form1.SelfRef.galaxy.stars[390]);
                Form1.SelfRef.galaxy.stars[390].name = "Player";
            }
            
        }
    }
}
