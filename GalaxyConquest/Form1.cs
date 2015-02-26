using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GalaxyConquest.StarSystems;
using GalaxyConquest;
using GalaxyConquest.Tactics;
using NAudio;
using NAudio.Wave;

// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
using GalaxyConquest.Drawing;
using GalaxyConquest.Game;


namespace GalaxyConquest
{
    [Serializable]
    public partial class Form1 : Form
    {
        /// <summary>
        /// Экземпляр класса DrawController, который будет отвечать за отрисовку в главной форме
        /// </summary>
        DrawController DrawControl;
        
        static public GameState Game;
        /// <summary>
        /// SaveFileDialog
        /// </summary>
        SaveFileDialog sfd;
        /// <summary>
        /// OpenFileDialog
        /// </summary>
        OpenFileDialog ofd;

        StarSystemForm ssf;
        /// <summary>
        /// Флаг, показывающий находится ли игра на стадии шага
        /// </summary>
        bool onStep = false;
        /// <summary>
        /// Переменная для синхронизации времени во время шага
        /// </summary>
        double syncTime = 1;
        /// <summary>
        /// Позиция курсора мыши
        /// </summary>
        int mouseX, mouseY;

        //public static Shop shop_form;
        public static StarShop shop_form;

        public Tech_Tree tt = new Tech_Tree();
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;

        public static Form1 SelfRef         //need for get var from other classes
        {
            get;
            set;
        }

        public Form1()
        {
            InitializeComponent();
            shop_form = new StarShop();
            Buildings builds = new Buildings();
            SelfRef = this;
            tech_progressBar.Visible = false;
            tech_label.Visible = false;
            listView.Visible = false;
            this.MouseWheel += new MouseEventHandler(this.onMouseWheel); // for resizing of galaxy at event change wheel mouse

            waveOutDevice = new WaveOutEvent();
            audioFileReader = new AudioFileReader(@"Sounds\Untitled45.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            statusStrip1.Items[0].Text = "Выбран 1 флот";

            sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Galaxy files|*.gal|All files|*.*";
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Galaxy files|*.gal|All files|*.*";

            DrawControl = new DrawController(galaxyImage);
            ssf = new StarSystemForm(null);
        }

        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = new Size(this.Width, this.Height);
            }
        }
        
        //-----------------------------------Sound------------------------------------

        private void sound_button_Click(object sender, EventArgs e)
        {
            if (waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                waveOutDevice.Pause();
                sound_button.Text = "Unmute";
            }
            else
            {
                waveOutDevice.Play();
                sound_button.Text = "Mute";
            }
        }

        //-----------------------------Main Menu--------------------------------------

        private void mainMenuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainMenuNew_Click(object sender, EventArgs e)
        {
            Form_NewGameDialog nd = new Form_NewGameDialog();
            if (nd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Game = new GameState();
                GameSeed seed = new GameSeed();
                seed.pName = nd.namePlayer;
                seed.gName = nd.galaxyName;
                seed.gType = nd.getGalaxyType();
                seed.gSize = nd.getGalaxySize();
                seed.gStarsCount = nd.getStarsCount();
                seed.gGenerateRandomEvent = nd.getGalaxyRandomEvents();

                Game.New(seed);

                UpdateLabels();

                panel1.Enabled = true;
                mainMenuSave.Enabled = true;
                MainMenuTechTree.Enabled = true;
                systemsButton.Enabled = true;
                fleetsButton.Enabled = true;
                GameTimer.Start();
            }
        }

        private void mainMenuSave_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;

                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);

                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, Game);

                fs.Close();
            }
        }

        private void mainMenuOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                panel1.Enabled = true;
                mainMenuSave.Enabled = true;
                MainMenuTechTree.Enabled = true;
                systemsButton.Enabled = true;
                fleetsButton.Enabled = true;
            }
        }

        private void mainMenuAbout_Click(object sender, EventArgs e)
        {
            Form_About af = new Form_About();
            af.ShowDialog();
        }

        private void MainMenuTechTree_Click(object sender, EventArgs e)
        {
            tt.ShowDialog();
        }

        //------------------------------------Events-----------------------------------

        private void galaxyImage_Paint(object sender, PaintEventArgs e)
        {
            if (Game == null)
                return;

            DrawControl.Render(Game, e.Graphics);
        }

        private void galaxyImage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;//Задаем координаты курсора
            mouseY = e.Y;
        }

        private void galaxyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (Game == null)
                return;

            if (e.Button == MouseButtons.Left)  // с зажатой левой клавишей двигаем изображение
            {
                DrawControl.Move(e.X - mouseX, e.Y - mouseY);
                mouseX = e.X;
                mouseY = e.Y;
                return;
            }
            if (e.Button == MouseButtons.Right) // с зажатой правой - вращаем
            {
                DrawControl.Rotate(e.X - mouseX, e.Y - mouseY);
                mouseX = e.X;
                mouseY = e.Y;
                return;
            }

            for (int j = 0; j < Game.Galaxy.stars.Count; j++)
            {
                StarSystem s = Game.Galaxy.stars[j];

                if (DrawControl.CursorIsOnObject(e, s))
                {
                    if (Game.Player.fleets[Game.Player.selectedFleet].s1 == s)
                        break;

                    Game.Player.warpTarget = s;
                    statusStrip1.Items[1].Text = "x: " + s.x + " y: " + s.y;
                    break;
                }
                else
                {
                    Game.Player.warpTarget = null;
                    statusStrip1.Items[1].Text = "";
                }
            }
        }

        private void onMouseWheel(object sender, MouseEventArgs e)
        {
            DrawControl.ChangeScale(e.Delta);
        }

        private void galaxyImage_MouseClick(object sender, MouseEventArgs e)
        {
            if (Game == null)
                return;

            //по нажатию левой клавиши выделяем флот/звезду для активного флота
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                for (int i = 0; i < Game.Player.fleets.Count; i++)
                {
                    if (DrawControl.CursorIsOnObject(e, Game.Player.fleets[i]))
                    {
                        Game.Player.selectedFleet = i;
                        Game.Player.selectedStar = Game.Player.fleets[i].s2;
                        statusStrip1.Items[0].Text = "Выбран " + (i + 1) + " флот";
                        //Обновляем кнопки захвата при смене флота
                        UpdateCaptureControls();
                        return;
                    }
                }

                if (onStep) return;

                for (int j = 0; j < Game.Galaxy.stars.Count; j++)
                {
                    StarSystem s = Game.Galaxy.stars[j];

                    if (DrawControl.CursorIsOnObject(e, s))
                    {
                        /// Если флот ничего не захватывает, не в пути и еще не имеет конечной цели, тогда выбираем ему цель
                        if (!Game.Player.fleets[Game.Player.selectedFleet].Capturing && !Game.Player.fleets[Game.Player.selectedFleet].onWay
                            && Game.Player.fleets[Game.Player.selectedFleet].s2 == null)
                        {
                            if (DrawController.Distance(Game.Player.fleets[Game.Player.selectedFleet], s) < Fleet.MaxDistance)
                            {
                                Game.Player.fleets[Game.Player.selectedFleet].setTarget(s);
                                Game.Player.selectedStar = s;
                            }
                        }   //Если мы кликаем на систему, которая выбрана для флота как конечная цель, тогда снимаем цель   -- без этого мы не сможем отменить перемещение!!!!
                        //else if (Game.Player.fleets[Game.Player.selectedFleet].s2 == s && Game.Player.fleets[Game.Player.selectedFleet].starDistanse == 0)
                        else if (Game.Player.fleets[Game.Player.selectedFleet].s2 == s && !Game.Player.fleets[Game.Player.selectedFleet].onWay)
                        {
                            Game.Player.fleets[Game.Player.selectedFleet].setTarget(null);
                            Game.Player.selectedStar = null;
                        }
                        return;
                    }
                }
            }

            //по нажатию правой клавиши просматриваем звездную систему/флот игрока/нейтральный флот
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                for (int j = 0; j < Game.Galaxy.stars.Count; j++)
                {
                    if (DrawControl.CursorIsOnObject(e, Game.Galaxy.stars[j]) && Game.Galaxy.stars[j].Discovered)
                    {
                        if (!ssf.IsDisposed)
                        {
                            ssf.SetSystem(Game.Galaxy.stars[j]);
                            ssf.Show();
                            ssf.Focus();
                        }
                        else
                        {
                            ssf = new StarSystemForm(Game.Galaxy.stars[j]);
                            ssf.Show();
                        }
                        UpdateCaptureControls();//Обновляем кнопку захвата после выбора системы для просмотра
                        return;
                    }
                }

                for (int i = 0; i < Game.Player.fleets.Count; i++)
                {
                    if (DrawControl.CursorIsOnObject(e, Game.Player.fleets[i]))
                    {
                        int scout = 0, aScount = 0, assault = 0, aAssault = 0;
                        double health = 0;
                        for (int j = 0; j < Game.Player.fleets[i].ships.Count; j++)
                        {
                            health += Math.Max(Game.Player.fleets[i].ships[j].currentHealth, 0);
                            if (Game.Player.fleets[i].ships[j] is ShipScout)
                            {
                                scout++;
                                if (Game.Player.fleets[i].ships[j].currentHealth > 0)
                                    aScount++;
                            }
                            else if (Game.Player.fleets[i].ships[j] is ShipAssaulter)
                            {
                                assault++;
                                if (Game.Player.fleets[i].ships[j].currentHealth > 0)
                                    aAssault++;
                            }
                        }

                        MessageBox.Show("Штурмовых кораблей - " + aAssault + " / (" + assault + ")\nИстребителей - " + aScount + " / (" + scout + ")\n\nОбщее количество здоровья: " + health + "hp", "Флот " + (i + 1));
                        return;
                    }
                }

                for (int i = 0; i < Game.Galaxy.neutrals.Count; i++)
                {
                    if (DrawControl.CursorIsOnObject(e, Game.Galaxy.neutrals[i]) && Game.Galaxy.neutrals[i].s1.Discovered)
                    {
                        int scout = 0, aScount = 0, assault = 0, aAssault = 0;
                        double health = 0;
                        for (int j = 0; j < Game.Galaxy.neutrals[i].ships.Count; j++)
                        {
                            health += Math.Max(Game.Galaxy.neutrals[i].ships[j].currentHealth, 0);
                            if (Game.Galaxy.neutrals[i].ships[j] is ShipScout)
                            {
                                scout++;
                                if (Game.Galaxy.neutrals[i].ships[j].currentHealth > 0)
                                    aScount++;
                            }
                            else if (Game.Galaxy.neutrals[i].ships[j] is ShipAssaulter)
                            {
                                assault++;
                                if (Game.Galaxy.neutrals[i].ships[j].currentHealth > 0)
                                    aAssault++;
                            }
                        }

                        MessageBox.Show("Штурмовых кораблей - " + aAssault + " / (" + assault + ")\nИстребителей - " + aScount + " / (" + scout + ")\n\nОбщее количество здоровья: " + health + "hp", "Нейтральный флот ( " + Game.Galaxy.neutrals[i].s1.name + " )");
                        return;
                    }
                }
            }
        }

        public void captureButton_Click(object sender, EventArgs e)
        {
            //  Если StartCapturing возвращает false, то прерываем захват   --- описание функции написано
            if (!Game.Player.fleets[Game.Player.selectedFleet].StartCapturing(Game.Player.fleets[Game.Player.selectedFleet].s1))
                Game.Player.fleets[Game.Player.selectedFleet].StopCapturing();
            //Обновляем кнопки захвата после нажатия на неё
            UpdateCaptureControls();
        }

        private void Shop_button_Click(object sender, EventArgs e)
        {
            if (Game == null)
                return;

            StarShop shop = new StarShop();
            shop.ShowDialog();
            shop_form = new StarShop();
            shop_form.ShowDialog();

            UpdateLabels();
        }

        //---------------------Fast access panel--------------------------

        private void fleetsButton_Click(object sender, EventArgs e)
        {
            if (Game == null)
                return;

            if (listView.Tag.Equals(fleetsButton.Tag))
            {
                listView.Visible = false;
                listView.Tag = "";
                return;
            }
            else
                listView.Visible = true;
            listView.Items.Clear();
            if (listView.Visible)
            {
                listView.Tag = fleetsButton.Tag;
                for (int i = 0; i < Game.Player.fleets.Count; i++)
                {
                    listView.Items.Add(Game.Player.fleets[i].name);
                    listView.Items[i].ForeColor = FleetColors.PassiveFleet;
                }

                listView.Items[Game.Player.selectedFleet].ForeColor = FleetColors.ActiveFleet;
            }
        }

        private void planetsButton_Click(object sender, EventArgs e)
        {
            if (Game == null)
                return;

            if (listView.Tag.Equals(systemsButton.Tag))
            {
                listView.Visible = false;
                listView.Tag = "";
                return;
            }
            else
                listView.Visible = true;

            listView.Items.Clear();
            if (listView.Visible)
            {
                listView.Tag = systemsButton.Tag;
                for (int i = 0; i < Game.Player.stars.Count; i++)
                    listView.Items.Add(Game.Player.stars[i].name);
            }
        }        
        // отображает путь до системы игрока от выбранного флота
        private void listView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            if (listView.Tag.Equals(systemsButton.Tag))
            {
                StarSystem star = Game.Player.stars[e.Item.Index];

                if (star != Game.Player.fleets[Game.Player.selectedFleet].s1)
                {
                    Game.Player.warpTarget = star; statusStrip1.Items[1].Text = "x: " + star.x + " y: " + star.y;
                }
                else
                {
                    Game.Player.warpTarget = null;
                    statusStrip1.Items[1].Text = "";
                }
            }
        }
        // выбор активного флота + цветовое отображение в списке
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.Tag.Equals(fleetsButton.Tag))
                if (listView.SelectedIndices.Count > 0)
                {
                    listView.Items[Game.Player.selectedFleet].ForeColor = FleetColors.PassiveFleet;

                    Game.Player.selectedFleet = listView.SelectedIndices[0];
                    Game.Player.selectedStar = Game.Player.fleets[Game.Player.selectedFleet].s2;
                    statusStrip1.Items[0].Text = "Выбран " + (Game.Player.selectedFleet + 1) + " флот";

                    listView.Items[Game.Player.selectedFleet].ForeColor = FleetColors.ActiveFleet;
                    //Опять же, после выбора флота обновляем кнопки захвата
                    UpdateCaptureControls();
                }
        }
        // выбор системы игрока в качестве цели для активного флота
        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (onStep) return;

            if (listView.Tag.Equals(systemsButton.Tag))
                if (listView.SelectedIndices.Count > 0)
                {
                    if (Game.Player.fleets[Game.Player.selectedFleet].starDistanse == 0)
                    {
                        StarSystem s = Game.Player.stars[listView.SelectedIndices[0]];
                        if (DrawController.Distance(Game.Player.fleets[Game.Player.selectedFleet], s) < Fleet.MaxDistance)
                        {

                            Game.Player.fleets[Game.Player.selectedFleet].setTarget(s);
                            Game.Player.selectedStar = s;
                        }
                    }
                    else if (Game.Player.fleets[Game.Player.selectedFleet].s2 == Game.Player.stars[listView.SelectedIndices[0]])
                    {
                        if (Game.Player.fleets[Game.Player.selectedFleet].onWay)
                            return;

                        Game.Player.fleets[Game.Player.selectedFleet].setTarget(null);
                        Game.Player.selectedStar = null;
                    }
                }
        }

        //--------------------------Step----------------------------------

        private void step_button_Click(object sender, EventArgs e)
        {
            if (Game == null) return;

            //выключаем всю панель, пока запущен поток
            panel1.Enabled = false;
            captureButton.Enabled = false;//кнопка захвата по умолчанию будет неактивна. Включается только если система, в которой находится активный флот еще не захвачена

            onStep = true;      //Устанавливаем флаг шага
            UpdateCaptureControls();//Во время шага кнопки захвата не должны быть активны, по-этому обновляем их
        }
        // Поток, в которм выполняются все рассчеты во время шага
        private void StepWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            StarSystem s = Game.Player.selectedStar;
            Random r = new Random(DateTime.Now.Millisecond);

            //---------------изменение популяции в системах---------
            for (int j = 0; j < Game.Galaxy.stars.Count; j++)
            {
                for (int i = 0; i < Game.Galaxy.stars[j].planets_count; i++)
                {
                    Game.Galaxy.stars[j].planets[i].POPULATION *= 1.1;
                }
            }

            //---------------получение бабосиков и минералов и очков исследований с захваченных систем---------
            for (int i = 0; i < Game.Player.player_planets.Count; i++)
            {
                  
                float climateFactor = 0 ;
                switch (Game.Player.player_planets[i].CLIMATE)
                {
                    case 0:
                        climateFactor = (float)0.3;
                        break;
                    case 1:
                        climateFactor = (float)0.5;
                        break;
                    case 2:
                        climateFactor = (float)0.8;
                        break;
                    case 3:
                        climateFactor = (float)1;
                        break;
                    case 4:
                        climateFactor = (float)2;
                        break;
                    default:
                        MessageBox.Show("Error occured with climat number(" + Game.Player.player_planets[i].CLIMATE + ")");
                        break;
                }

                if (Game.Player.player_planets[i].POPULATION < Game.Player.player_planets[i].POPULATIONMAX)
                {
                    Game.Player.player_planets[i].POPULATION += Game.Player.player_planets[i].POPULATION * 0.1 * climateFactor;
                    Game.Player.player_planets[i].POPULATION = Math.Round(Game.Player.player_planets[i].POPULATION, 3);
                }
                else
                {
                }
                float popfactor = 0;
              
                {
                      if (Game.Player.player_planets[i].SIZE < 15)
                        {
                            
                            popfactor = 5;
                        }
                        else
                          if ((Game.Player.player_planets[i].SIZE >= 15) && (Game.Player.player_planets[i].SIZE < 23))
                            {
                                
                                popfactor = 10;
                            }
                            else
                              if ((Game.Player.player_planets[i].SIZE >= 23) && (Game.Player.player_planets[i].SIZE < 30))
                                {
                                    
                                    popfactor = 15;
                                }
                                else
                                  if (Game.Player.player_planets[i].SIZE >= 30)
                                    {
                                        
                                        popfactor = 20;
                                    }
                    
                }

                float mineraFactor = 0;
                switch (Game.Player.player_planets[i].CLIMATE)
                {
                    case 0:
                        mineraFactor = (float)0.05;
                        break;
                    case 1:
                        mineraFactor = (float)0.2;
                        break;
                    case 2:
                        mineraFactor = (float)1;
                        break;
                    case 3:
                        mineraFactor = (float)1.5;
                        break;
                    case 4:
                        mineraFactor = (float)4;
                        break;
                   
                }

                Game.Player.player_planets[i].PROFIT = mineraFactor * Game.Player.player_planets[i].POPULATION;
                Game.Player.player_planets[i].PROFIT = Math.Round(Game.Player.player_planets[i].PROFIT, 2);
                Game.Player.player_planets[i].POPULATIONMAX = popfactor * climateFactor;

                Game.Player.credit += Game.Player.player_planets[i].PROFIT;
                Game.Player.minerals += Game.Player.player_planets[i].MINERALS;
                Game.Player.skillPoints += Game.Player.player_planets[i].skillPointProduce;
            }

            //---------------процесс захвата систем---------
            for (int i = 0; i < Game.Player.fleets.Count; i++)
            {
                Game.Player.fleets[i].CaptureProcess();
            }

            if (tech_progressBar.Value < Tech.learning_tech_time &&
                tt.tierClicked != 1000 &&
                tt.techLineClicked != 1000 &&
                tt.subtechClicked != 1000)
            {
                if (InvokeRequired)
                    Invoke(new Action(() => tech_progressBar.Value += 1));
            }

            if (tech_progressBar.Value == Tech.learning_tech_time)
            {
                Player.technologies.Add(new int[] { tt.tierClicked, tt.techLineClicked, tt.subtechClicked });
                if (InvokeRequired)
                {
                    Invoke(new Action(() => tech_progressBar.Value = 0));
                    Invoke(new Action(() => tech_progressBar.Visible = false));
                    Invoke(new Action(() => tech_label.Visible = false));
                }
                Tech.CheckTechInnovaions();//Проверка, нужно ли поменять броню, добавить здание, в зависимости от выученной технологии
                tt.tierClicked = 1000;
                tt.techLineClicked = 1000;
                tt.subtechClicked = 1000;
            }
        }
        //Метод вызывается BacgroundWirker-ом после завершения действий в потоке
        private void StepWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //проверяем нахождение нейтральных флотов и флотов игрока в одной системе
            for (int k = 0; k < Game.Galaxy.neutrals.Count; k++)
            {
                for (int l = 0; l < Game.Player.fleets.Count; l++)
                    if (Game.Galaxy.neutrals[k].s1 == Game.Player.fleets[l].s1)
                    {
                        if (Game.Player.fleets[l].starDistanse == 0)
                            if (MessageBox.Show("Ваш " + (l + 1) + " флот обнаружил нейтральный флот в системе " + Game.Player.fleets[l].s1.name + "!\nАтаковать его?", "", MessageBoxButtons.YesNo)
                                == System.Windows.Forms.DialogResult.Yes)
                            {
                                Fleet fl = Game.Galaxy.neutrals[k];

                                CombatForm cf = new CombatForm(Game.Player.fleets[l], fl);
                                cf.ShowDialog();
                            }
                    }
            }
            //проверяем флоты и удаляем из списка уничтоженные флоты
            for (int i = Game.Galaxy.neutrals.Count - 1; i >= 0; i--)
                if (!Game.Galaxy.neutrals[i].Allive)
                    Game.Galaxy.neutrals.RemoveAt(i);

            for (int i = Game.Player.fleets.Count - 1; i >= 0; i--)
                if (!Game.Player.fleets[i].Allive)
                {
                    Game.Player.fleets.RemoveAt(i);
                    if (i == Game.Player.selectedFleet)
                        if (Game.Player.fleets.Count > 0)
                            Game.Player.selectedFleet = 0;
                }

            //Обновляем лейблы
            UpdateLabels();
            //включам панель с кнопками
            panel1.Enabled = true;
            step_button.Focus();//задаём фокус для кнопки шага
            //Обновляем кнопки захвата по завершению шага
            UpdateCaptureControls();
        }
        
        //----------------------Timer-------------------
        //Обновление изображения и движение во время шага строго по тику таймера
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (onStep)
            {
                if (syncTime <= 0)
                {
                    syncTime = 1;
                    onStep = false; //Снимаем флаг шага
                    StepWorker.RunWorkerAsync();
                }

                MovementsController.Process(Game.Galaxy, Game.Galaxy.Time);
                MovementsController.Process(Game.Galaxy.neutrals.ToArray(), Game.Galaxy.Time);
                MovementsController.Process(Game.Player.fleets.ToArray(), Game.Galaxy.Time);

                syncTime -= MovementsController.FIXED_TIME_DELTA;
                Game.Galaxy.Time += MovementsController.FIXED_TIME_DELTA;
            }
            galaxyImage.Refresh();
        }
        
        //-------------------------------other----------------------------------
        //Обновляет все текстовые поля
        private void UpdateLabels()
        {
            galaxyNameLablel.Text = Game.Galaxy.name;
            dateLabel.Text = Math.Round(Game.Galaxy.Time).ToString() + " г.н.э.";
            CreditsStatus.Text = Math.Round(Game.Player.credit, 2).ToString() + " $";
            MineralStatus.Text = Math.Round(Game.Player.minerals, 3) + " Т";
            EnergyStatus.Text = Math.Round(Game.Player.energy, 2).ToString() + " Wt";
            SkillPointsStatus.Text = Math.Round(Game.Player.skillPoints, 2).ToString() + "SP";
        }
        //Обновляет кнопки захвата системы
        void UpdateCaptureControls()
        {
            if (onStep)//Во время шага просто выключим кнопку захвата и оставим прогрессбар, если он видим
                captureButton.Enabled = false;
            else
                if (Game.Player.stars.Contains(Game.Player.fleets[Game.Player.selectedFleet].s1) || Game.Player.fleets[Game.Player.selectedFleet].s2 != null
                    || Game.Player.fleets[Game.Player.selectedFleet].onWay)
                    SetCaptureControlsActive(-1);
                else if (Game.Player.fleets[Game.Player.selectedFleet].Capturing)
                    SetCaptureControlsActive(1);
                else
                    SetCaptureControlsActive(0);

            StarSystemForm.SelfRef.UpdateCaptureControls(onStep);   //обновляем кнопку захвата в форме звездной системы
        }
        //  Устанавливает активность кнопок захвата и их текст/ (1 - захват идёт, 0 - захват возможен, -1 - захват невозможен (флот в пути/система уже захвачена))
        void SetCaptureControlsActive(int value)
        {
            switch (value)
            {
                case 0:
                    {
                        captureButton.Enabled = true;
                        captureButton.Text = "Захватить систему";
                        conquer_progressBar.Visible = false;
                    } break;
                case 1:
                    {
                        captureButton.Enabled = true;
                        captureButton.Text = "Отменить захват";
                        conquer_progressBar.Visible = true;
                        conquer_progressBar.Value = Game.Player.fleets[Game.Player.selectedFleet].getCaptureProgress();
                    } break;
                case -1:
                    {
                        captureButton.Enabled = false;
                        captureButton.Text = "Захватить систему";
                        conquer_progressBar.Visible = false;
                    } break;
                default:
                    return;
            }
        }
        //Если форма закрывает в момент, когда шаг в процессе выполнения, ждем его завершения и тогда закрываемся
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (StepWorker.IsBusy || onStep) ;
        }
    }
}
