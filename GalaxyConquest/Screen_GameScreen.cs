using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

using SFML;
using Gwen;
using Gwen.Control;


using GalaxyConquest.Drawing;
using GalaxyConquest.Game;
using GalaxyConquest.SpaceObjects;
using GalaxyConquest.Tactics;

namespace GalaxyConquest
{
    class Screen_GameScreen : Gwen.Control.DockBase
    {


        public Image galaxyImage;
        public Gwen.Control.ImagePanel img;
        Gwen.Control.Label label;
        Gwen.Control.Button buttonCombat;

        /// <summary>
        /// Экземпляр класса DrawController, который будет отвечать за отрисовку в главной форме
        /// </summary>
        DrawController DrawControl;

        /// <summary>
        /// Флаг, показывающий находится ли игра на стадии шага
        /// </summary>
        bool onStep = false;

        /// <summary>
        /// Переменная для синхронизации времени во время шага
        /// </summary>
        double syncTime = 1;

        System.ComponentModel.BackgroundWorker StepWorker;
        System.Timers.Timer GameTimer; 

        bool dragging = false;
        bool menuOpenned = false;
        int mx = 0;
        int my = 0;

        public Screen_GameScreen(Base parent)
            : base(parent)
        {
            StepWorker = new BackgroundWorker();
            GameTimer = new System.Timers.Timer(2000);
            InitializeComponent();
            SetSize(parent.Width, parent.Height);

            

            img = new Gwen.Control.ImagePanel(this);


            galaxyImage = new Bitmap(Program.percentW(100), Program.percentH(100), PixelFormat.Format32bppArgb);
            DrawControl = new DrawController(galaxyImage);

            updateDrawing();

            img.SetPosition(Program.percentW(0), Program.percentH(0));
            img.SetSize(Program.percentW(100), Program.percentH(100));
            img.Clicked += new GwenEventHandler<ClickedEventArgs>(img_Clicked);
            img.RightClicked += new GwenEventHandler<ClickedEventArgs>(img_RightClicked);
            img.MouseMoved += new GwenEventHandler<MovedEventArgs>(img_MouseMoved);
            img.MouseDown += new GwenEventHandler<ClickedEventArgs>(img_MouseDown);
            img.MouseUp += new GwenEventHandler<ClickedEventArgs>(img_MouseUp);
            img.MouseWheeled += new GwenEventHandler<MouseWheeledEventArgs>(img_MouseWheeled);

            label = new Gwen.Control.Label(this);
            label.Text = "GAME!!!";
            label.SetPosition(Program.percentW(5), Program.percentH(5));
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            Gwen.Control.Button buttonTech = new Gwen.Control.Button(this);
            buttonTech.Text = "Tech Tree";
            buttonTech.Font = Program.fontButtonLabels;
            buttonTech.SetBounds(Program.percentW(80), Program.percentH(84), Program.percentW(20), Program.percentH(8));
            buttonTech.Clicked += onButtonTechClick;

            Gwen.Control.Button buttonMenu = new Gwen.Control.Button(this);
            buttonMenu.Text = "Menu";
            buttonMenu.Font = Program.fontButtonLabels;
            buttonMenu.SetBounds(Program.percentW(0), Program.percentH(0), Program.percentW(13), Program.percentH(8));
            buttonMenu.Clicked += onButtonMenuClick;

            Gwen.Control.Button buttonSolarSystem = new Gwen.Control.Button(this);
            buttonSolarSystem.Text = "Solar System";
            buttonSolarSystem.Font = Program.fontButtonLabels;
            buttonSolarSystem.SetBounds(Program.percentW(0), Program.percentH(92), Program.percentW(19), Program.percentH(8));
            buttonSolarSystem.Clicked += onSolarSystemClick;

            Gwen.Control.Button buttonStep = new Gwen.Control.Button(this);
            buttonStep.Text = "Step";
            buttonStep.Font = Program.fontButtonLabels;
            buttonStep.SetBounds(Program.percentW(90), Program.percentH(92), Program.percentW(10), Program.percentH(8));
            buttonStep.Clicked += onButtonStepClick;

            buttonCombat = new Gwen.Control.Button(this);
            buttonCombat.Text = "Combat";
            buttonCombat.Font = Program.fontButtonLabels;
            buttonCombat.SetBounds(Program.percentW(19), Program.percentH(92), Program.percentW(18), Program.percentH(8));
            buttonCombat.Clicked += onCombatClick;
        }

        private void onButtonTechClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("techtree");
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeComponent()
        {
            StepWorker.DoWork += new DoWorkEventHandler(StepWorker_DoWork);
            StepWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(StepWorker_RunWorkerCompleted);
            GameTimer.Elapsed += GameTimer_Tick;
            GameTimer.Start();
        }


        private void onButtonMenuClick(Base control, EventArgs args)
        {


            if (!menuOpenned)
            {
                menuOpenned = true;

                Gwen.Control.WindowControl menuWindow = new Gwen.Control.WindowControl(this);
                menuWindow.Width = Program.percentW(50);
                menuWindow.Height = Program.percentH(50);
                menuWindow.SetPosition(Program.percentW(25), Program.percentH(20));

                Gwen.Control.Button buttonNewGame = new Gwen.Control.Button(menuWindow);
                buttonNewGame.Text = "New game";
                buttonNewGame.Font = Program.fontButtonLabels;
                buttonNewGame.SetBounds(Program.percentW(12), Program.percentH(0), 200, 50);
                buttonNewGame.Pressed += onButtonNewGameClick;

                Gwen.Control.Button buttonLoadGame = new Gwen.Control.Button(menuWindow);
                buttonLoadGame.Text = "Load game";
                buttonLoadGame.Disable();
                buttonLoadGame.Font = Program.fontButtonLabels;
                buttonLoadGame.SetBounds(Program.percentW(12), Program.percentH(10), 200, 50);

                Gwen.Control.Button buttonSaveGame = new Gwen.Control.Button(menuWindow);
                buttonSaveGame.Text = "Save Game";
                buttonSaveGame.Disable();
                buttonSaveGame.Font = Program.fontButtonLabels;
                buttonSaveGame.SetBounds(Program.percentW(12), Program.percentH(20), 200, 50);

                Gwen.Control.Button buttonQuit = new Gwen.Control.Button(menuWindow);
                buttonQuit.Text = "Quit";
                buttonQuit.Font = Program.fontButtonLabels;
                buttonQuit.SetBounds(Program.percentW(12), Program.percentH(30), 200, 50);
                buttonQuit.Pressed += onButtonQuitClick;
            }
        }


        private void onButtonNewGameClick(Base control, EventArgs args)
        {
            menuOpenned = false;
            Program.screenManager.LoadScreen("newgame");
        }

        private void onButtonQuitClick(Base control, EventArgs args)
        {
            menuOpenned = false;
            Program.quitFlag = true;
        }


        private void onSolarSystemClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("solarSystem");
        }

        //--------------------------Step----------------------------------
        private void onButtonStepClick(Base control, EventArgs args)
        {
            if (Program.Game == null) return;

            //выключаем всю панель, пока запущен поток
            //panel1.Enabled = false;
            //captureButton.Enabled = false;//кнопка захвата по умолчанию будет неактивна. Включается только если система, в которой находится активный флот еще не захвачена

            onStep = true;      //Устанавливаем флаг шага
            StepWorker.RunWorkerAsync();
            //UpdateCaptureControls();//Во время шага кнопки захвата не должны быть активны, по-этому обновляем их
        }

        // Поток, в которм выполняются все рассчеты во время шага
        private void StepWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            StarSystem s = Program.Game.Player.selectedStar;
            Random r = new Random(DateTime.Now.Millisecond);

            //---------------изменение популяции в системах---------
            for (int i = 0; i < Program.Game.Galaxy.stars.Count; i++)
                Program.Game.Galaxy.stars[i].Process();


            //---------------получение бабосиков и минералов и очков исследований с захваченных систем---------
            Program.Game.Player.Process();

            //---------------процесс захвата систем---------
            for (int i = 0; i < Program.Game.Player.fleets.Count; i++)
            {
                Program.Game.Player.fleets[i].CaptureProcess();
            }

            while (onStep) ;    //Пока галактика находится в движении, ждем
        }
        //Метод вызывается BacgroundWirker-ом после завершения действий в потоке
        private void StepWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //проверяем нахождение нейтральных флотов и флотов игрока в одной системе
            for (int l = 0; l < Program.Game.Player.fleets.Count; l++)
                for (int k = 0; k < Program.Game.Galaxy.neutrals.Count; k++)
                {
                    if (Program.Game.Galaxy.neutrals[k].s1 == Program.Game.Player.fleets[l].s1)
                    {
                        if (!Program.Game.Player.fleets[l].onWay)
                            if (System.Windows.Forms.MessageBox.Show("Ваш флот обнаружил нейтральный флот в системе " + Program.Game.Player.fleets[l].s1.name + "!\nАтаковать его?", "", MessageBoxButtons.YesNo)
                                == System.Windows.Forms.DialogResult.Yes)
                            {
                                //CombatForm cf = new CombatForm(Program.Game.Player.fleets[l], Program.Game.Galaxy.neutrals[k]);
                                //cf.ShowDialog();
                            }
                    }
                }
            //проверяем флоты и удаляем из списка уничтоженные флоты
            //Проверяем с конца, потому что, если удалить флот в начале списка, в конце вылетит исключение out of range
            for (int i = Program.Game.Galaxy.neutrals.Count - 1; i >= 0; i--)
                if (!Program.Game.Galaxy.neutrals[i].Allive)
                    Program.Game.Galaxy.neutrals.RemoveAt(i);

            for (int i = Program.Game.Player.fleets.Count - 1; i >= 0; i--)
                if (!Program.Game.Player.fleets[i].Allive)
                {
                    Program.Game.Player.fleets.RemoveAt(i);
                    if (i == Program.Game.Player.selectedFleet)
                        if (Program.Game.Player.fleets.Count > 0)
                            Program.Game.Player.selectedFleet = 0;
                }

            //Обновляем лейблы
            //UpdateControls();
            //включам панель с кнопками
            //panel1.Enabled = true;
            //step_button.Focus();//задаём фокус для кнопки шага
            //Обновляем кнопки захвата по завершению шага
            //UpdateCaptureControls();
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

                MovementsController.Process(Program.Game.Galaxy, Program.Game.Galaxy.Time);
                MovementsController.Process(Program.Game.Galaxy.neutrals.ToArray(), Program.Game.Galaxy.Time);
                MovementsController.Process(Program.Game.Player.fleets.ToArray(), Program.Game.Galaxy.Time);

                syncTime -= MovementsController.FIXED_TIME_DELTA;
                Program.Game.Galaxy.Time += MovementsController.FIXED_TIME_DELTA;
            }
            //updateDrawing();
        }

        private void onCombatClick(Base control, EventArgs args)
        {
            TacticState.left = Program.Game.Player.fleets[0];
            TacticState.right = Program.Game.Galaxy.neutrals[0];
            Program.screenManager.LoadScreen("combat");
        }

        void img_MouseWheeled(Base sender, MouseWheeledEventArgs arguments)
        {
            DrawControl.ChangeScale(arguments.Delta);
            updateDrawing();
        }

        void img_MouseUp(Base sender, ClickedEventArgs arguments)
        {
            label.Text = "UP";
            dragging = false;
        }

        void img_MouseDown(Base sender, ClickedEventArgs arguments)
        {

            dragging = true;
            mx = arguments.X;
            my = arguments.Y;
        }

        void img_MouseMoved(Base sender, MovedEventArgs arguments)
        {
            if (dragging)
            {
                label.Text = arguments.X.ToString() + "," + arguments.Y.ToString();
                DrawControl.Move(arguments.X - mx, arguments.Y - my);
                updateDrawing();
                mx = arguments.X;
                my = arguments.Y;
            }
        }

        //по нажатию левой клавиши выделяем флот/звезду для активного флота
        void img_Clicked(Base sender, ClickedEventArgs arguments)
        {
            label.Text = arguments.X.ToString() + " : " + arguments.Y.ToString();
            if (Program.Game == null)
                return;

            for (int i = 0; i < Program.Game.Player.fleets.Count; i++)
            {
                if (DrawControl.CursorIsOnObject(arguments, Program.Game.Player.fleets[i]))
                {
                    Program.Game.Player.selectedFleet = i;
                    Program.Game.Player.selectedStar = Program.Game.Player.fleets[i].s2;
                    label.Text = "Выбран " + (i + 1) + " флот";
                    //Обновляем кнопки захвата при смене флота
                    //UpdateCaptureControls();
                    updateDrawing();
                    return;
                }
            }

            //if (onStep) return;

            for (int j = 0; j < Program.Game.Galaxy.stars.Count; j++)
            {
                StarSystem s = Program.Game.Galaxy.stars[j];

                if (DrawControl.CursorIsOnObject(arguments, s))
                {
                    /// Если флот ничего не захватывает, не в пути и еще не имеет конечной цели, тогда выбираем ему цель
                    if (!Program.Game.Player.fleets[Program.Game.Player.selectedFleet].Capturing && !Program.Game.Player.fleets[Program.Game.Player.selectedFleet].onWay
                        && Program.Game.Player.fleets[Program.Game.Player.selectedFleet].s2 == null)
                    {
                        if (DrawController.Distance(Program.Game.Player.fleets[Program.Game.Player.selectedFleet], s) < Fleet.MaxDistance)
                        {
                            Program.Game.Player.fleets[Program.Game.Player.selectedFleet].setTarget(s);
                            Program.Game.Player.selectedStar = s;
                        }
                    }   //Если мы кликаем на систему, которая выбрана для флота как конечная цель, тогда снимаем цель   -- без этого мы не сможем отменить перемещение!!!!
                    //else if (Game.Player.fleets[Game.Player.selectedFleet].s2 == s && Game.Player.fleets[Game.Player.selectedFleet].starDistanse == 0)
                    else if (Program.Game.Player.fleets[Program.Game.Player.selectedFleet].path.Last == s && !Program.Game.Player.fleets[Program.Game.Player.selectedFleet].onWay)
                    {
                        Program.Game.Player.fleets[Program.Game.Player.selectedFleet].setTarget(null);
                        Program.Game.Player.selectedStar = null;
                    }
                    updateDrawing();
                    return;
                }
            }
        }

        private void img_RightClicked(Base control, ClickedEventArgs args)
        {
                for (int j = 0; j < Program.Game.Galaxy.stars.Count; j++)
                {
                    if (DrawControl.CursorIsOnObject(args, Program.Game.Galaxy.stars[j]) && Program.Game.Galaxy.stars[j].Discovered)
                    {
                        //загрузка экрана звездной системы
                        /*if (!ssf.IsDisposed)
                        {
                            ssf.SetSystem(Program.Game.Galaxy.stars[j]);
                            ssf.Show();
                            ssf.Focus();
                        }
                        else
                        {
                            ssf = new StarSystemForm(Program.Game.Galaxy.stars[j]);
                            ssf.Show();
                        }*/
                        //UpdateCaptureControls();//Обновляем кнопку захвата после выбора системы для просмотра
                        updateDrawing();
                        return;
                    }
                }

                Fleet selectedFleet = null;

                for (int i = 0; i < Program.Game.Player.fleets.Count; i++)
                    if (DrawControl.CursorIsOnObject(args, Program.Game.Player.fleets[i]))
                    {
                        selectedFleet = Program.Game.Player.fleets[i];
                        break;
                    }
                for (int i = 0; i < Program.Game.Galaxy.neutrals.Count; i++)
                    if (DrawControl.CursorIsOnObject(args, Program.Game.Galaxy.neutrals[i]) && Program.Game.Galaxy.neutrals[i].s1.Discovered)
                    {
                        selectedFleet = Program.Game.Galaxy.neutrals[i];
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
                    System.Windows.Forms.MessageBox.Show("Штурмовых кораблей - " + aAssault + " / (" + assault + ")\nИстребителей - " + aScount + " / (" + scout + ")\n\nОбщее количество здоровья: " + health + "hp", selectedFleet.name + " ( " + selectedFleet.s1.name + " )");
                }
                updateDrawing();

        }

        protected override bool OnKeyReturn(bool down)
        {
            System.Windows.Forms.MessageBox.Show("URA");
            return base.OnKeyEscape(down);
        }

        public void updateDrawing()
        {
            Graphics gr = Graphics.FromImage(galaxyImage);
            gr.FillRectangle(Brushes.Black, 0, 0, galaxyImage.Width, galaxyImage.Height);

            DrawControl.Render(Program.Game, gr);

            img.Image = (Bitmap)galaxyImage;

            Program.m_Canvas.RenderCanvas();
            Program.m_Window.Display();
        }

        public override void Dispose()
        {
            base.Dispose();
        }



    }
}
