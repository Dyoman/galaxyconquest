using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GalaxyConquest.StarSystems
{
   
    public class PLANET : SpaceObject
    {

        public PointF CENTER = new PointF(0, 0);
        public float SPEED = 0.1f;
        public float DISTANCE = 100f;
        public float ROT = 0f;
        public float POPULATIONMAX = 0;
        public double POPULATION = 0;
        public float MINERALS = 10;
        public float SIZE = 10;
        public string OWNERNAME = "None";
        public double PROFIT = 0;
        public Color CLR = Color.FromArgb(255, 255, 255);

        public String NAME = "DEFAULT";

        public PointF GetPoint()
        {
            return new PointF((float)x, (float)y);
        }

        public override void Move(double time)
        {
            x = (float)Math.Sin(time * SPEED) * DISTANCE + CENTER.X;
            y = (float)Math.Cos(time * SPEED) * DISTANCE + CENTER.Y;
        }

        public double Inc(double p, double f)
        {
            p = p + (p / 2.75) - (p / (f * 200));
            return p;
        }
    }

}
