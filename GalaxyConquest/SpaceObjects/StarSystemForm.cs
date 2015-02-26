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
using GalaxyConquest.Game;
using GalaxyConquest.Drawing;


namespace GalaxyConquest.StarSystems
{
    public partial class StarSystemForm : Form
    {
        DrawController DrawControl;

        public int planet_selected;
        Random rand = new Random();

        public StarSystem starSystem;

        public static StarSystemForm SelfRef         //need for get var from other classes
        {
            get;
            set;
        }

        public StarSystemForm(StarSystem system)
        {
            InitializeComponent();
            SetSystem(system);
            DrawControl = new DrawController(pictureBox1);
            DrawControl.Rotate(0f, (float)-Math.PI / 4 * 100);  //Доворачиваем "камеру" до вертикального положения

            SelfRef = this;
            captureButton.Click += Form1.SelfRef.captureButton_Click;   //Привязываем события клика на кнопку захвата к обработчику такого же события в главной форме
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawControl.Render(starSystem, e.Graphics);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                for (int j = 0; j < starSystem.planets.Count; j++)
                {
                    Planet p = starSystem.planets[j];

                    if (DrawControl.CursorIsOnObject(e, p))
                    {

                        string sizeText = "";
                        string mineralsText = "";
                        string climateText = "";
                        double population;
                        double profit;
                        float popfactor = 1;
                        double climfactor = 1;

                        //ниже- определение размера планеты
                        if (p.SIZE < 15)
                        {
                            sizeText = "Small";
                            
                        }
                        else
                            if ((p.SIZE >= 15) && (p.SIZE < 23))
                            {
                                sizeText = "Medium";
                               
                            }
                            else
                                if ((p.SIZE >= 23)&& (p.SIZE < 30))
                                {
                                    sizeText = "Big";
                                    
                                }
                                else
                                    if (p.SIZE >= 30)
                                    {
                                        sizeText = "Extra";
                                        
                                    }



                        //ниже - определение ресурсов
                     /*   if (p.MINERALS == 0)
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
                                        mineralsText = "Large";
                                    }
                        */

                        switch(p.MINERALS)
                        {
                            case 0:
                                mineralsText = "Very Small";
                                break;
                            case 1:
                                mineralsText = "Small";
                                break;
                            case 2:
                                mineralsText = "Medium";
                                break;
                            case 3:
                                mineralsText = "Large";
                                break;
                            case 4:
                                mineralsText = "Extra";
                                break;
                            
                        }


                        //ниже - определение климата
                        switch(p.CLIMATE)
                        {
                            case 0:
                                climateText = "no atmosphere";
                                break;
                            case 1:
                                climateText = "lava";
                                break;
                            case 2:
                                climateText = "tundra";
                                break;
                            case 3:
                                climateText = "temperate";
                                break;
                            case 4:
                                climateText = "gaya";
                                break;
                            default:
                                MessageBox.Show("Error occured with climat number("+p.CLIMATE+")");
                                break;
                        }

                        population = Math.Round(p.POPULATION, 3);
                        profit = Math.Round(p.PROFIT, 2);

                        planet_selected = j;
                        labelPlanetName.Text = p.name;
                        labelPlanetSize.Text = sizeText;
                        labelPlanetMinerals.Text = mineralsText;
                        p.POPULATIONFACTOR = popfactor;
                        climate1.Text = climateText;
                        labelPlanetPopulationMax.Text = p.POPULATIONMAX.ToString();
                        labelPlanetPopulation.Text = p.POPULATION.ToString();
                        ownerNameLabel.Text = p.ownerName;
                        profitLabel.Text = p.PROFIT.ToString();

                        buildings.Text = "";//set buildings textbox to empty string
                        for (int z = 0; z < Player.buildings.Count; z++)//chech all player builds
                        {
                            if (Player.buildings[z][0] == Form1.Game.Player.stars.IndexOf(Form1.Game.Player.selectedStar) &&//check current starsystem
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
        /// <summary>
        /// Передает звездную систему в форму
        /// </summary>
        /// <param name="s">Звездная система</param>
        public void SetSystem(StarSystem s)
        {
            starSystem = s;
            if (s == null) return;

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
        /// <summary>
        /// Обновляет кнопку захвата системы
        /// </summary>
        /// <param name="onStep"></param>
        public void UpdateCaptureControls(bool onStep)
        {
            if (onStep) //  Во время шага просто отключаем кнопку захвата
                captureButton.Enabled = false;
            else if (Form1.Game.Player.stars.Contains(starSystem) || Form1.Game.Player.fleets[Form1.Game.Player.selectedFleet].onWay || onStep
                || Form1.Game.Player.fleets[Form1.Game.Player.selectedFleet].s2 != null)
                SetCaptureControlsActive(-1);
            else if (Form1.Game.Player.fleets[Form1.Game.Player.selectedFleet].Capturing)
                SetCaptureControlsActive(1);
            else
                SetCaptureControlsActive(0);
        }
        /// <summary>
        /// Устанавливает активность кнопок захвата и их текст/ (1 - захват идёт, 0 - захват возможен, -1 - захват невозможен (флот в пути/система уже захвачена))
        /// </summary>
        /// <param name="value"></param>
        void SetCaptureControlsActive(int value)
        {
            switch (value)
            {
                case 0:
                    {
                        captureButton.Enabled = true;
                        captureButton.Text = "Захватить систему";
                        captureButton.ForeColor = SystemColors.ControlText;
                    } break;
                case 1:
                    {
                        captureButton.Enabled = true;
                        captureButton.Text = "Отменить захват";
                        captureButton.ForeColor = Color.DarkSlateGray;
                    } break;
                case -1:
                    {
                        captureButton.Enabled = false;
                        captureButton.Text = "Захватить систему";
                        captureButton.ForeColor = SystemColors.ControlText;
                    } break;
                default:
                    return;
            }
        }
    }
}
