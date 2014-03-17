using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.ObjectModel;


namespace PlanetTeam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();



            // //////////////////////

            PLN.Add(new PLANET());
            PLN[0].CENTER = new Point(400, 400);
            PLN[0].DISTANCE = 0;
            PLN[0].SPEED = 0;
            PLN[0].CLR = Color.Yellow;
            PLN[0].SIZE = 50;
            PLN[0].NAME = "SUN";

            PLN.Add(new PLANET());
            PLN[1].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[1].DISTANCE = 100;
            PLN[1].SPEED = 0.001f;
            PLN[1].CLR = Color.Green;
            PLN[1].SIZE = 10;
            PLN[1].NAME = "EARTH";

            PLN.Add(new PLANET());
            PLN[2].CENTER = new Point(PLN[1].GetPoint().X, PLN[1].GetPoint().Y);
            PLN[2].DISTANCE = 10;
            PLN[2].SPEED = -0.01f;
            PLN[2].CLR = Color.White;
            PLN[2].SIZE = 5;
            PLN[2].NAME = "MOON";

            PLN.Add(new PLANET());
            PLN[3].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[3].DISTANCE = 70;
            PLN[3].SPEED = 0.01f;
            PLN[3].CLR = Color.DarkOrange;
            PLN[3].SIZE = 8;
            PLN[3].NAME = "VENUS";

            PLN.Add(new PLANET());
            PLN[4].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[4].DISTANCE = 50;
            PLN[4].SPEED = 0.02f;
            PLN[4].CLR = Color.WhiteSmoke;
            PLN[4].SIZE = 6;
            PLN[4].NAME = "MERCURY";

            PLN.Add(new PLANET());
            PLN[5].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[5].DISTANCE = 150;
            PLN[5].SPEED = 0.002f;
            PLN[5].CLR = Color.OrangeRed;
            PLN[5].SIZE = 9;
            PLN[5].NAME = "MARS";

            PLN.Add(new PLANET());
            PLN[6].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[6].DISTANCE = 200;
            PLN[6].SPEED = 0.001f;
            PLN[6].CLR = Color.Orange;
            PLN[6].SIZE = 20;
            PLN[6].NAME = "JUPITER";

            PLN.Add(new PLANET());
            PLN[7].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[7].DISTANCE = 250;
            PLN[7].SPEED = 0.0005f;
            PLN[7].CLR = Color.DarkOrange;
            PLN[7].SIZE = 15;
            PLN[7].NAME = "SATURN";

            PLN.Add(new PLANET());
            PLN[8].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[8].DISTANCE = 300;
            PLN[8].SPEED = 0.0003f;
            PLN[8].CLR = Color.Aqua;
            PLN[8].SIZE = 11;
            PLN[8].NAME = "URANUS";

            PLN.Add(new PLANET());
            PLN[9].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[9].DISTANCE = 350;
            PLN[9].SPEED = 0.0002f;
            PLN[9].CLR = Color.LightBlue;
            PLN[9].SIZE = 10;
            PLN[9].NAME = "NEPTUNE";

            PLN.Add(new PLANET());
            PLN[10].CENTER = new Point(PLN[0].GetPoint().X, PLN[0].GetPoint().Y);
            PLN[10].DISTANCE = 400;
            PLN[10].SPEED = 0.0001f;
            PLN[10].CLR = Color.WhiteSmoke;
            PLN[10].SIZE = 6;
            PLN[10].NAME = "PLUTO";

            // //////////////////////



        }




        // ///////////////////////
        static Bitmap BIT = new Bitmap(800, 800);
        static Graphics GBIT = Graphics.FromImage(BIT);
        // ///////////////////////
        static Collection<PLANET> PLN = new Collection<PLANET>();




        private void timer1_Tick(object sender, EventArgs e)
        {
            GBIT.Clear(Color.Black);
            // ////////////////////////////////////////
            for (Int32 i = 0; i < PLN.Count; i++)
            {



                PLN[2].CENTER = new Point(PLN[1].GetPoint().X, PLN[1].GetPoint().Y);

                // /////////////////////////


                GBIT.FillEllipse(new SolidBrush(PLN[i].CLR), new Rectangle(PLN[i].GetPoint().X - (int)PLN[i].SIZE / 2, PLN[i].GetPoint().Y - (int)PLN[i].SIZE / 2, (int)PLN[i].SIZE, (int)PLN[i].SIZE));
                GBIT.DrawString(PLN[i].NAME, new Font("arial", 10), new SolidBrush(Color.White), new Point(PLN[i].GetPoint().X, PLN[i].GetPoint().Y));


            }
            // ////////////////////////////////////////
            pictureBox1.Image = BIT;
        }










































    }
}
