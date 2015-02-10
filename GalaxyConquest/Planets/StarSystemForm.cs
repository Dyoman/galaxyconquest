using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;


namespace GalaxyConquest.StarSystems
{
    public partial class StarSystemForm : Form
    {
        public static int pln_selected;
        public static string size1;
        public static string minerals1;
        public int planet_selected;
        Random rand = new Random();
        int maxpln = 4;
        int dist = 100;
        float speed = 0.001f;
       // public PLANET sp;
        public StarSystem s;

        public StarSystemForm(StarSystem system)
        {
            InitializeComponent();
            s = system;
            //sp = PLN;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        // ///////////////////////
        static Bitmap BIT = new Bitmap(800, 800);
        static Graphics GBIT = Graphics.FromImage(BIT);
        // ///////////////////////

        private void timer1_Tick(object sender, EventArgs e)
        {
            GBIT.Clear(Color.Black);
            Image newImage = Image.FromFile("Sprites/PlanetImage/1.JPG");

            // ////////////////////////////////////////
            for (Int32 i = 0; i < s.PLN.Count; i++)
            {
                /*Bitmap myBitmap = new Bitmap("Sprites/PlanetImage/1.JPG");
                   for (int j=0; j < (int)s.PLN[i].SIZE; j++)
                  {

                      if (Math.Sqrt(((s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2) + j) ^ 2 + ((s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2) - j) ^ 2) < s.PLN[i].SIZE / 2)
                      {
                          Color pixelColor = myBitmap.GetPixel((s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2) + j, (s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2) - j);
                          RectangleF rect = new RectangleF((s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2) + j, (s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2) - j, (int)s.PLN[i].SIZE, (int)s.PLN[i].SIZE);
                          GBIT.DrawImage(newImage, rect);
                      }
                      else
                      {
                          RectangleF rect = new RectangleF((s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2) + j, (s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2) - j, (int)s.PLN[i].SIZE, (int)s.PLN[i].SIZE);
                          GBIT.FillRectangle(Brushes.Black,rect);
                      }
                  } */

                // int rad = (int)Math.Sqrt(s.PLN[i].GetPoint().X * s.PLN[i].GetPoint().X + s.PLN[i].GetPoint().Y * s.PLN[i].GetPoint().Y);
               GBIT.DrawEllipse(new Pen(Color.White), s.PLN[0].GetPoint().X - s.PLN[i].DISTANCE, s.PLN[0].GetPoint().Y - s.PLN[i].DISTANCE, s.PLN[i].DISTANCE * 2, s.PLN[i].DISTANCE * 2);
               // RectangleF rect = new RectangleF(s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2, s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2, (int)s.PLN[i].SIZE, (int)s.PLN[i].SIZE);

                /* for (int j = s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2; j < (s.PLN[i].GetPoint().X + (int)s.PLN[i].SIZE / 2) && j > (s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2); j++)
                 {
                     for (int j1 = s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2; j1 < (s.PLN[i].GetPoint().Y + (int)s.PLN[i].SIZE / 2) && j1 > (s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2); j1++)
                     {
                         BIT.SetPixel(j, j1, Color.White);
                     }
                 }
                 */
               // GBIT.DrawImage(newImage, rect);

                //BIT.GetPixel(300, 300);


              //  BIT.SetPixel(300, 300, Color.White);



                /*
                for (int j = 0; j < (int)s.PLN[i].SIZE / 2; j++)
                {
                    for (int j1 = 0; j1 < (int)s.PLN[i].SIZE / 2; j1++)
                    {
                        if (Math.Sqrt(((s.PLN[i].GetPoint().X + j - (int)s.PLN[i].SIZE / 2)) ^ 2 + ((s.PLN[i].GetPoint().Y + j1 - (int)s.PLN[i].SIZE / 2)) ^ 2) > s.PLN[i].SIZE / 2)
                        {
                            RectangleF rect1 = new RectangleF((s.PLN[i].GetPoint().X + j - (int)s.PLN[i].SIZE / 2), (s.PLN[i].GetPoint().Y + j1 - (int)s.PLN[i].SIZE / 2), 1, 1);
                            GBIT.FillRectangle(Brushes.Black, rect1);
                        }
                       
                     }
                    }
                */
                //RectangleF rect = new RectangleF(s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2, s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2, (int)s.PLN[i].SIZE, (int)s.PLN[i].SIZE);
                //GBIT.DrawImage(newImage, rect);
              // Rectangle rectan = new Rectangle((int)(centerX - 5 + (int)screenX - starSize / 2), (int)(centerY - 5 + (int)screenY - starSize / 2), (int)(starSize + 11), (int)(starSize + 11));
              //  if (s == s.PLN[planet_selected])
             //  {
             //      g.DrawEllipse(pen, rectan);
             //  }


                // /////////////////////////
                GBIT.FillEllipse(new SolidBrush(s.PLN[i].CLR), new Rectangle(s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2, s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2, (int)s.PLN[i].SIZE, (int)s.PLN[i].SIZE));
                 GBIT.DrawString(s.PLN[i].NAME, new Font("arial", 10), new SolidBrush(Color.White), new Point(s.PLN[i].GetPoint().X, s.PLN[i].GetPoint().Y));

            }
            // int rad = (int)Math.Sqrt(PLN[4].GetPoint().X * PLN[4].GetPoint().X + PLN[4].GetPoint().Y * PLN[4].GetPoint().Y);
            // ////////////////////////////////////////

            pictureBox1.Image = BIT;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PLANET PL = new PLANET();

            for (int j = 0; j < s.PLN.Count; j++)
            {
                //pln_selected = j;//переменная для связи планеты со формой2
                #region old
                /*
                string sizeText = "";
                string mineralsText = "";

                //ниже- определение размера планеты
                if (s.PLN[j].SIZE < 15)
                {
                    sizeText = "Small";
                }
                else
                    if ((s.PLN[j].SIZE >= 15) && (s.PLN[j].SIZE < 25))
                    {
                        sizeText = "Medium";
                    }
                    else
                        if (s.PLN[j].SIZE > 25)
                        {
                            sizeText = "Big";
                        }


                //ниже - определение ресурсов
                if (s.PLN[j].MINERALS == 0)
                {
                    mineralsText = "No Minerals";
                }
                else
                    if ((s.PLN[j].MINERALS > 0) && (s.PLN[j].MINERALS <= 10))
                    {
                        mineralsText = "Small";
                    }
                    else
                        if ((s.PLN[j].MINERALS > 10) && (s.PLN[j].MINERALS <= 20))
                        {
                            mineralsText = "Medium";
                        }
                        else
                            if (s.PLN[j].MINERALS > 20)
                            {
                                mineralsText = "Big";
                            }
                string playername111 = "Kolobok";
                if (playername111 != "")
                {
                    s.PLN[j].OWNERNAME = "None";
                }


                if ((e.X > s.PLN[j].GetPoint().X - (s.PLN[j].SIZE / 2)) &&
                    (e.X < s.PLN[j].GetPoint().X + (s.PLN[j].SIZE / 2)) &&    //клик по планете вызывает форму с информацией
                    (e.Y > s.PLN[j].GetPoint().Y - (s.PLN[j].SIZE / 2)) &&
                    (e.Y < s.PLN[j].GetPoint().Y + (s.PLN[j].SIZE / 2)))
                {
                    
                    planet_selected = j;
                    labelPlanetName.Text = s.PLN[j].NAME;
                    labelPlanetSize.Text = sizeText;
                    labelPlanetMinerals.Text = mineralsText;
                    labelPlanetPopulation.Text = s.PLN[j].POPULATIONMAX.ToString();
                    Populn.Text = s.PLN[j].POPULATION.ToString();
                    ownername.Text = s.PLN[j].OWNERNAME;
                    profit.Text = s.PLN[j].PROFIT.ToString();
                    buildings.Text = "";//set buildings textbox to empty string
                    for (int z = 0; z < Player.buildings.Count; z++)//chech all player builds
                    {
                        if (Player.buildings[z][0] == Form1.star_selected &&//check current starsystem
                            Player.buildings[z][1] == j)                    //check current planet
                        {
                            //if ok add builds to text box
                            buildings.AppendText(Buildings.buildings[Player.buildings[z][2]] + "\n");
                        }
                    }
                }
                */
                #endregion
                PLANET p = s.PLN[j];

                string sizeText = "";
                string mineralsText = "";

                //ниже- определение размера планеты
                if (p.SIZE < 15)
                {
                    sizeText = "Small";
                }
                else
                    if ((p.SIZE >= 15) && (p.SIZE < 25))
                    {
                        sizeText = "Medium";
                    }
                    else
                        if (p.SIZE > 25)
                        {
                            sizeText = "Big";
                        }


                //ниже - определение ресурсов
                if (p.MINERALS == 0)
                {
                    mineralsText = "No Minerals";
                }
                else
                    if ((p.MINERALS > 0) && (p.MINERALS <= 10))
                    {
                        mineralsText = "Small";
                    }
                    else
                        if ((p.MINERALS > 10) && (p.MINERALS <= 20))
                        {
                            mineralsText = "Medium";
                        }
                        else
                            if (p.MINERALS > 20)
                            {
                                mineralsText = "Big";
                            }
                string playername111 = "Kolobok";
                if (playername111 != "")
                {
                    s.PLN[j].OWNERNAME = "None";
                }


                if ((e.X > p.GetPoint().X - (p.SIZE / 2)) &&
                    (e.X < p.GetPoint().X + (p.SIZE / 2)) &&    //клик по планете вызывает форму с информацией
                    (e.Y > p.GetPoint().Y - (p.SIZE / 2)) &&
                    (e.Y < p.GetPoint().Y + (p.SIZE / 2)))
                {

                    planet_selected = j;
                    labelPlanetName.Text = p.NAME;
                    labelPlanetSize.Text = sizeText;
                    labelPlanetMinerals.Text = mineralsText;
                    labelPlanetPopulation.Text = p.POPULATIONMAX.ToString();
                    Populn.Text = p.POPULATION.ToString();
                    ownername.Text = p.OWNERNAME;
                    profit.Text = p.PROFIT.ToString();
                    buildings.Text = "";//set buildings textbox to empty string
                    for (int z = 0; z < Player.buildings.Count; z++)//chech all player builds
                    {
                        if (Player.buildings[z][0] == Form1.player.player_stars.IndexOf(Form1.selectedStar) &&//check current starsystem
                            Player.buildings[z][1] == j)                    //check current planet
                        {
                            //if ok add builds to text box
                            buildings.AppendText(Buildings.buildings[Player.buildings[z][2]] + "\n");
                        }
                    }
                }
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void localstepbutton_Click(object sender, EventArgs e)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < s.PLN.Count; i++)
            {
                s.PLN[i].POPULATION = s.PLN[i].Inc(s.PLN[i].POPULATION, r.NextDouble());
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void build_button1_Click(object sender, EventArgs e)
        {

        }

        private void Capture_Click(object sender, EventArgs e)
        {
            bool planet_consist = false;

            if (Player.player_planets.Contains(s.PLN[planet_selected]))
            {
                planet_consist = true;
            }

            if (planet_consist == false)
            {
                Player.player_planets.Add(s.PLN[planet_selected]);
                Form1.shop_form.set_listbox_planet(s.PLN[planet_selected]);
                
            }
            else
            {
                MessageBox.Show("Conqest");
            }
        }
    }
}
