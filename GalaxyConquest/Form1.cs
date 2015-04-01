/*using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GalaxyConquest;
using GalaxyConquest.Tactics;
using NAudio;
using NAudio.Wave;



namespace GalaxyConquest
{
    [Serializable]
    public partial class Form1 : Form
    {

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

        public Tech_Tree techTreeForm = new Tech_Tree();
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;

        public static Form1 SelfRef
        {
            get;
            set;
        }

        public Form1()
        {
            InitializeComponent();
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

        private void MainMenuTechTree_Click(object sender, EventArgs e)
        {
            techTreeForm.ShowDialog();

            UpdateControls();
        }

        //------------------------------------Events-----------------------------------

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
                        else if (Game.Player.fleets[Game.Player.selectedFleet].path.Last == s && !Game.Player.fleets[Game.Player.selectedFleet].onWay)
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

                Fleet selectedFleet = null;

                for (int i = 0; i < Game.Player.fleets.Count; i++)
                    if (DrawControl.CursorIsOnObject(e, Game.Player.fleets[i]))
                    {
                        selectedFleet = Game.Player.fleets[i];
                        break;
                    }
                for (int i = 0; i < Game.Galaxy.neutrals.Count; i++)
                    if (DrawControl.CursorIsOnObject(e, Game.Galaxy.neutrals[i]) && Game.Galaxy.neutrals[i].s1.Discovered)
                    {
                        selectedFleet = Game.Galaxy.neutrals[i];
                        break;
                    }

                if (selectedFleet != null)
                {
                    int scout = 0, aScount = 0, assault = 0, aAssault = 0;
                    double health = 0;
                    for (int j = 0; j < selectedFleet.ships.Count; j++)
                    {
                        health += Math.Max(selectedFleet.ships[j].currentHealth, 0);
                        if (selectedFleet.ships[j] is ShipScout)
                        {
                            scout++;
                            if (selectedFleet.ships[j].currentHealth > 0)
                                aScount++;
                        }
                        else if (selectedFleet.ships[j] is ShipAssaulter)
                        {
                            assault++;
                            if (selectedFleet.ships[j].currentHealth > 0)
                                aAssault++;
                        }
                    }
                    MessageBox.Show("Штурмовых кораблей - " + aAssault + " / (" + assault + ")\nИстребителей - " + aScount + " / (" + scout + ")\n\nОбщее количество здоровья: " + health + "hp", selectedFleet.name + " ( " + selectedFleet.s1.name + " )");
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

            UpdateControls();
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
                    Game.Player.warpTarget = star; 
                    statusStrip1.Items[1].Text = "x: " + star.x + " y: " + star.y;
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
                    if (Game.Player.fleets[Game.Player.selectedFleet].path.Empty)
                    {
                        StarSystem s = Game.Player.stars[listView.SelectedIndices[0]];
                        if (DrawController.Distance(Game.Player.fleets[Game.Player.selectedFleet], s) < Fleet.MaxDistance)
                        {

                            Game.Player.fleets[Game.Player.selectedFleet].setTarget(s);
                            Game.Player.selectedStar = s;
                        }
                    }
                    else if (Game.Player.fleets[Game.Player.selectedFleet].path.Last == Game.Player.stars[listView.SelectedIndices[0]])
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
            StepWorker.RunWorkerAsync();
            UpdateCaptureControls();//Во время шага кнопки захвата не должны быть активны, по-этому обновляем их
        }
        // Поток, в которм выполняются все рассчеты во время шага
        private void StepWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            StarSystem s = Game.Player.selectedStar;
            Random r = new Random(DateTime.Now.Millisecond);

            //---------------изменение популяции в системах---------
            for (int i = 0; i < Game.Galaxy.stars.Count; i++)
                Game.Galaxy.stars[i].Process();
            

            //---------------получение бабосиков и минералов и очков исследований с захваченных систем---------
            Game.Player.Process();

            //---------------процесс захвата систем---------
            for (int i = 0; i < Game.Player.fleets.Count; i++)
            {
                Game.Player.fleets[i].CaptureProcess();
            }

            while (onStep) ;    //Пока галактика находится в движении, ждем
        }
        //Метод вызывается BacgroundWirker-ом после завершения действий в потоке
        private void StepWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //проверяем нахождение нейтральных флотов и флотов игрока в одной системе
            for (int l = 0; l < Game.Player.fleets.Count; l++)
                for (int k = 0; k < Game.Galaxy.neutrals.Count; k++)
                {
                    if (Game.Galaxy.neutrals[k].s1 == Game.Player.fleets[l].s1)
                    {
                        if (!Game.Player.fleets[l].onWay)
                            if (MessageBox.Show("Ваш флот обнаружил нейтральный флот в системе " + Game.Player.fleets[l].s1.name + "!\nАтаковать его?", "", MessageBoxButtons.YesNo)
                                == System.Windows.Forms.DialogResult.Yes)
                            {
                                CombatForm cf = new CombatForm(Game.Player.fleets[l], Game.Galaxy.neutrals[k]);
                                cf.ShowDialog();
                            }
                    }
            }
            //проверяем флоты и удаляем из списка уничтоженные флоты
            //Проверяем с конца, потому что, если удалить флот в начале списка, в конце вылетит исключение out of range
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
            UpdateControls();
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
        //Обновляет все контролы
        private void UpdateControls()
        {
            galaxyNameLablel.Text = Game.Galaxy.name;
            dateLabel.Text = Math.Round(Game.Galaxy.Time).ToString() + " г.н.э.";
            CreditsStatus.Text = Math.Round(Game.Player.credit, 2).ToString() + " $";
            MineralStatus.Text = Math.Round(Game.Player.minerals, 3) + " Т";
            EnergyStatus.Text = Math.Round(Game.Player.energy, 2).ToString() + " Wt";
            SkillPointsStatus.Text = Math.Round(Game.Player.skillPoints, 2).ToString() + "SP";

            tech_label.Visible = Game.Player.Learning;
            tech_progressBar.Visible = Game.Player.Learning;
            if (Game.Player.Learning)
            {
                tech_label.Text = Tech.teches.tiers[Game.Player.learningTech.Tier][Game.Player.learningTech.Line][Game.Player.learningTech.Subtech].subtech;
                tech_progressBar.Value = Game.Player.getLearningProgress();
            }
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
        //Если форма закрывается в момент, когда шаг в процессе выполнения, ждем его завершения и тогда закрываемся
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (StepWorker.IsBusy || onStep)
            {
                e.Cancel = true;
                MessageBox.Show("Дождитесь завершения шага");
            }
        }
    }
}
*/