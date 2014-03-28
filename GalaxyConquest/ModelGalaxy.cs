using System;
using System.Collections.Generic;

namespace GalaxyConquest
{
    [Serializable]
    public class ModelGalaxy
    {
        public string name; //название галактики
        public List<StarSystem> stars; //звездные системы
        public List<StarWarp> lanes;   //гиперпереходы

        public List<Fleet> neutrals;

        public Player player;

        public ModelGalaxy()
        {
            stars = new List<StarSystem>();
            lanes = new List<StarWarp>();
            neutrals = new List<Fleet>();
        }
    }
}
