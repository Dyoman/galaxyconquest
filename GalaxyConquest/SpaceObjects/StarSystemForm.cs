using GalaxyConquest.Drawing;
using GalaxyConquest.Game;
using GalaxyConquest.SpaceObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace GalaxyConquest.SpaceObjects
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
//TODO: check            DrawControl = new DrawController(pictureBox1);

            SelfRef = this;
            captureButton.Click += Form1.SelfRef.captureButton_Click;   
            //Привязываем события клика на кнопку захвата к обработчику 
            //такого же события в главной форме
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
                        ShowPlanet(p);
                }

        }
        /// <summary>
        /// Передает звездную систему в форму
        /// </summary>
        /// <param name="s">Звездная система</param>
        public void SetSystem(StarSystem s)
        {
            if (s == null) return;
            starSystem = s;

            this.Text = "Система " + s.name;

            planet_selected = 0;

            buildingsTextBox.Text = "";

            ShowAll();
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            ShowAll();
        }

        void ShowAll()
        {
            Text = "Система " + starSystem.name;

            float size = 0;
            string sizeText = "";
            double population = 0;
            double maxPopulation = 0;
            double profit = 0;
            int minerals = 0;
            string mineralsText = "";
            int climate = 0;
            string climateText = "";
            double skillPoints = 0;

            for (int i = 0; i < starSystem.planets.Count; i++)
            {
                size += starSystem.planets[i].SIZE;
                population += starSystem.planets[i].POPULATION;
                maxPopulation += starSystem.planets[i].POPULATIONMAX;
                climate += starSystem.planets[i].CLIMATE;
                profit += starSystem.planets[i].PROFIT;
                skillPoints += starSystem.planets[i].skillPointProduce;
                minerals += starSystem.planets[i].MINERALS;
            }
            size /= starSystem.planets.Count;
            climate /= starSystem.planets.Count;
            minerals /= starSystem.planets.Count;

            //ниже- определение размера планеты
            if (size < 15)
                sizeText = "Small";
            else if ((size >= 15) && (size < 23))
                sizeText = "Medium";
            else if ((size >= 23) && (size < 30))
                sizeText = "Big";
            else if (size >= 30)
                sizeText = "Extra";

            switch (minerals)
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
            switch (climate)
            {
                case 0:
                    climateText = "Без атмосферы";
                    break;
                case 1:
                    climateText = "Лава";
                    break;
                case 2:
                    climateText = "Тундра";
                    break;
                case 3:
                    climateText = "Умеренный";
                    break;
                case 4:
                    climateText = "Идеальный";
                    break;
                default:
                    MessageBox.Show("Error occured with climat number(" + climate + ")");
                    break;
            }

            sizeLabel.Text = "Размер(ср): " + sizeText;
            mineralsLabel.Text = "Минералы(общ): " + mineralsText;
            populationLabel.Text = "Популяция(общ): " + Math.Round(population, 3) + "млн. / " + Math.Round(maxPopulation, 3) + "млн.";
            profitLabel.Text = "Прирост кредитов(общ): " + Math.Round(profit, 3) + " $";
            ownerLabel.Text = "Владелец: " + starSystem.Owner.name;
            climateLabel.Text = "Климат(ср): " + climateText;
            skillPointLabel.Text = "Прирост очков изучения(общ): " + skillPoints;
        }

        void ShowPlanet(Planet p)
        {
            Text = "Планета " + p.name;

            string sizeText = "";
            string mineralsText = "";
            string climateText = "";

            //ниже- определение размера планеты
            if (p.SIZE < 15)
                sizeText = "Small";
            else if ((p.SIZE >= 15) && (p.SIZE < 23))
                sizeText = "Medium";
            else if ((p.SIZE >= 23) && (p.SIZE < 30))
                sizeText = "Big";
            else if (p.SIZE >= 30)
                sizeText = "Extra";

            switch (p.MINERALS)
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
            switch (p.CLIMATE)
            {
                case 0:
                    climateText = "Без атмосферы";
                    break;
                case 1:
                    climateText = "Лава";
                    break;
                case 2:
                    climateText = "Тундра";
                    break;
                case 3:
                    climateText = "Умеренный";
                    break;
                case 4:
                    climateText = "Идеальный";
                    break;
                default:
                    MessageBox.Show("Error occured with climat number(" + p.CLIMATE + ")");
                    break;
            }

            sizeLabel.Text = "Размер: " + sizeText;
            mineralsLabel.Text = "Минералы: " + mineralsText;
            populationLabel.Text = "Популяция: " + Math.Round(p.POPULATION, 3) + "млн./" + Math.Round(p.POPULATIONMAX, 3) + "млн.";
            profitLabel.Text = "Прирост кредитов : " + Math.Round(p.PROFIT, 3) + " $";
            ownerLabel.Text = "Владелец: " + starSystem.Owner.name;
            climateLabel.Text = "Климат: " + climateText;
            skillPointLabel.Text = "Прирост очков изучения: " + p.skillPointProduce;


            buildingsTextBox.Text = "";//set buildings textbox to empty string
            for (int z = 0; z < Player.buildings.Count; z++)//chech all player builds
            {
                if (Player.buildings[z][0] == Form1.Game.Player.stars.IndexOf(Form1.Game.Player.selectedStar) &&//check current starsystem
                    Player.buildings[z][1] == planet_selected)                    //check current planet
                {
                    //if ok add builds to text box
                    buildingsTextBox.AppendText(Buildings.buildings[Player.buildings[z][2]] + "\n");
                }
            }
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
