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
        public static string size1;
        public static string minerals1;
        public int planet_selected;
        Random rand = new Random();

        public StarSystem starSystem;

        public StarSystemForm(StarSystem system)
        {
            TopMost = true;
            InitializeComponent();
            SetSystem(system);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            planet_selected = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        //Опять же отрисовка без битмапов :)
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (Int32 i = 0; i < starSystem.PLN.Count; i++)
            {
                g.DrawEllipse(new Pen(Color.White), starSystem.PLN[0].GetPoint().X - starSystem.PLN[i].DISTANCE, starSystem.PLN[0].GetPoint().Y - starSystem.PLN[i].DISTANCE, starSystem.PLN[i].DISTANCE * 2, starSystem.PLN[i].DISTANCE * 2);

                g.FillEllipse(new SolidBrush(starSystem.PLN[i].CLR), new RectangleF(starSystem.PLN[i].GetPoint().X - starSystem.PLN[i].SIZE / 2, starSystem.PLN[i].GetPoint().Y - starSystem.PLN[i].SIZE / 2, starSystem.PLN[i].SIZE, starSystem.PLN[i].SIZE));
                g.DrawString(starSystem.PLN[i].NAME, new Font("arial", 7.0f), new SolidBrush(Color.White), new PointF(starSystem.PLN[i].GetPoint().X, starSystem.PLN[i].GetPoint().Y));

            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PLANET PL = new PLANET();

            for (int j = 0; j < starSystem.PLN.Count; j++)
            {
                //pln_selected = j;//переменная для связи планеты со формой2
                PLANET p = starSystem.PLN[j];

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

                if ((e.X > p.GetPoint().X - (p.SIZE / 2)) &&
                    (e.X < p.GetPoint().X + (p.SIZE / 2)) &&    //клик по планете вызывает форму с информацией
                    (e.Y > p.GetPoint().Y - (p.SIZE / 2)) &&
                    (e.Y < p.GetPoint().Y + (p.SIZE / 2)))
                {

                    planet_selected = j;
                    labelPlanetName.Text = p.NAME;
                    labelPlanetSize.Text = sizeText;
                    labelPlanetMinerals.Text = mineralsText;
                    labelPlanetPopulationMax.Text = p.POPULATIONMAX.ToString();
                    labelPlanetPopulation.Text = p.POPULATION.ToString();
                    ownerNameLabel.Text = p.OWNERNAME;
                    profitLabel.Text = p.PROFIT.ToString();
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
                    break;
                }
            }

        }

        private void Capture_Click(object sender, EventArgs e)
        {
            bool planet_consist = false;

            if (Player.player_planets.Contains(starSystem.PLN[planet_selected]))
            {
                planet_consist = true;
            }

            if (planet_consist == false)
            {
                Player.player_planets.Add(starSystem.PLN[planet_selected]);
                Form1.shop_form.set_listbox_planet(starSystem.PLN[planet_selected]);
                
            }
            else
            {
                MessageBox.Show("Conqest");
            }
        }

        public void SetSystem(StarSystem s)
        {
            starSystem = s;
            this.Text = "Система " + s.name;

            planet_selected = 0;

            labelPlanetName.Text = "";
            labelPlanetPopulation.Text = "";
            labelPlanetPopulationMax.Text = "";
            labelPlanetMinerals.Text = "";
            labelPlanetSize.Text = "";
            profitLabel.Text = "";
            ownerNameLabel.Text = "";
            buildings.Text = "";
        }
    }
}
