using System;
using System.Collections.Generic;
using GalaxyConquest.Tactics;

namespace GalaxyConquest
{
    [Serializable]
    public class Fleet
    {
        public List<Ship> ships;

        public StarSystem s1 = null;
        public StarSystem s2 = null;
        
        public string name;
        
        public double x;
        public double y;
        public double z;
        public double starDistanse;
        public Fleet()
        {
            ships = new List<Ship>();
        }
    }
}
