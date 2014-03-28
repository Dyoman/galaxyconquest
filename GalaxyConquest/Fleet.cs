using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GalaxyConquest.Tactics;

namespace GalaxyConquest
{
    public class Fleet
    {
        public List<Ship> ships;

        public StarSystem s1 = null;
        public StarSystem s2 = null;

        double x;
        double y;
        double z;

        public Fleet()
        {
            ships = new List<Ship>();
        }
    }
}
