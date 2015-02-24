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

        public static string size1;
        public static string minerals1;
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
            TopMost = true;
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
                        float climatefactor = 1;
                        double population;
                        double profit;
                        //ниже- определение размера планеты
                        if (p.size < 15)
                        {
                            sizeText = "Small";
                        }
                        else
                            if ((p.size >= 15) && (p.size < 25))
                            {
                                sizeText = "Medium";
                            }
                            else
                                if (p.size >= 25)
                                {
                                    sizeText = "Big";
                                }


                        //ниже - определение ресурсов
                        if (p.minerals == 0)
                        {
                            mineralsText = "No Minerals";
                        }
                        else
                            if ((p.minerals > 0) && (p.minerals <= 10))
                            {
                                mineralsText = "Small";
                            }
                            else
                                if ((p.minerals > 10) && (p.minerals <= 20))
                                {
                                    mineralsText = "Medium";
                                }
                                else
                                    if (p.minerals > 20)
                                    {
                                        mineralsText = "Big";
                                    }
                        if (p.CLIMATE == 0)
                        {
                            climateText = "no atmosphere";
                            climatefactor = (float)0.3;
                        }
                        else
                            if (p.CLIMATE == 1)
                            {
                                climateText = "lava";
                                climatefactor = (float)0.5;
                            }
                            else
                                if(p.CLIMATE == 2)
                                {
                                    climateText = "tundra";
                                    climatefactor = (float)0.8;
                                }
                                else
                                    if(p.CLIMATE == 3)
                                    {
                                        climateText = "temperate";
                                        climatefactor = (float)1;
                                    }
                                    else
                                        if(p.CLIMATE == 5)
                                        {
                                            climateText = "gaya";
                                            climatefactor = (float)2;
                                        }
                        
                        planet_selected = j;

                        population = p.currentPopulation + p.currentPopulation * 0.1 * climatefactor;
                        population = Math.Round(population, 3);

                        profit = p.minerals * population;
                        profit = Math.Round(p.profit, 2);

                        labelPlanetName.Text = p.name;
                        labelPlanetSize.Text = sizeText;
                        labelPlanetMinerals.Text = mineralsText;
                        climate1.Text = climateText;
                        labelPlanetPopulationMax.Text = p.maxPopulation.ToString();
                        labelPlanetPopulation.Text = p.currentPopulation.ToString();
                        ownerNameLabel.Text = p.ownerName;
                        profitLabel.Text = p.profit.ToString();
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
            climate1.Text = "";
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
