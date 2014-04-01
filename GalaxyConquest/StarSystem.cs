using System;
using System.Drawing;
using System.Collections.Generic;

using GalaxyConquest.StarSystems;

namespace GalaxyConquest
{
    [Serializable]
    public class StarSystem
    {
        public string name;

        public int type; //тип звезды

        public double x;
        public double y;
        public double z;
        public SolidBrush br; //brush for stars

        public int planets_count;//num of planets

        public List<PLANET> PLN = new List<PLANET>();
    }
}
