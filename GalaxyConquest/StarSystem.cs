using System;
using System.Drawing;
using System.Collections.Generic;

using GalaxyConquest.StarSystems;

namespace GalaxyConquest
{
    [Serializable]
    public class StarSystem : SpaceObject
    {
        public double angVel = 0.05;  //Угловая скорость

        public double R = 0, timeOffset = 0, increment = 0;//Радиус движения системы, смещение фазы и смещение координат

        public int type; //тип звезды
        public SolidBrush br; //brush for stars

        public int planets_count;//num of planets

        public bool Discovered = false;

        public List<PLANET> PLN = new List<PLANET>();

        public override void Move(double time)
        {
            for (int i = 0; i < PLN.Count; i++)
                PLN[i].Move(time);
        }
    }
}
