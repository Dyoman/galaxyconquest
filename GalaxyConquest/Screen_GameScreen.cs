using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using SFML;
using Gwen;
using Gwen.Control;


using GalaxyConquest.Drawing;
using GalaxyConquest.Game;
using GalaxyConquest.SpaceObjects;

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

        bool dragging = false;
        bool menuOpenned = false;
        int mx = 0;
        int my = 0;

        public Screen_GameScreen(Base parent)
            : base(parent)
        {

            SetSize(parent.Width, parent.Height);

            label = new Gwen.Control.Label(this);
            label.Text = "GAME!!!";
            label.SetPosition(Program.percentW(5), Program.percentH(5));
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            img = new Gwen.Control.ImagePanel(this);
            

            galaxyImage = new Bitmap(Program.percentW(100), Program.percentH(80), PixelFormat.Format32bppArgb);
            DrawControl = new DrawController(galaxyImage);

            updateDrawing();

            img.SetPosition(Program.percentW(0), Program.percentH(20));
            img.SetSize(Program.percentW(100), Program.percentH(80));
            //img.Clicked += new GwenEventHandler<ClickedEventArgs>(img_Clicked);
            img.MouseMoved += new GwenEventHandler<MovedEventArgs>(img_MouseMoved);
            img.MouseDown += new GwenEventHandler<ClickedEventArgs>(img_MouseDown);
            img.MouseUp += new GwenEventHandler<ClickedEventArgs>(img_MouseUp);

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
            buttonStep.Clicked += onSolarSystemClick;

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

        private void onCombatClick(Base control, EventArgs args)
        {
            TacticState.left = Program.Game.Player.fleets[0];
            TacticState.right = Program.Game.Galaxy.neutrals[0];
            Program.screenManager.LoadScreen("combat");
        }

        void img_MouseUp(Base sender, ClickedEventArgs arguments)
        {
            label.Text = "UP";
            dragging = false;
        }

        void img_MouseDown(Base sender, ClickedEventArgs arguments)
        {
            label.Text = "DOWN";
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

        void img_Clicked(Base sender, ClickedEventArgs arguments)
        {
            System.Windows.Forms.MessageBox.Show(arguments.X.ToString() + "," + arguments.Y.ToString());
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
