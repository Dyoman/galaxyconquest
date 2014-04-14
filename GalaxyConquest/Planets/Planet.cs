using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GalaxyConquest.StarSystems
{
    [Serializable]
    public class PLANET
    {

        public Point CENTER = new Point(0, 0);
        public float SPEED = 0.01f;
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


        public Point GetPoint()
        {
            Point P2 = new Point(0, 0);
            ROT += SPEED;


            float XX = 0, YY = 0;


            Point PP = new Point(CENTER.X, CENTER.Y + (int)DISTANCE);


            XX = ((float)Math.Cos(ROT) * (PP.X - CENTER.X) + (float)Math.Sin(ROT) * (PP.Y - CENTER.Y)) + CENTER.X;
            YY = ((float)Math.Cos(ROT) * (PP.Y - CENTER.Y) + (float)Math.Sin(ROT) * (PP.X - CENTER.X)) + CENTER.Y;


            P2 = new Point((int)XX, (int)YY);


            return P2;
        }

        public double Inc(double p, double f)
        {
            p = p + (p / 2.75) - (p / (f * 200));
            return p;
        }
    }

}
