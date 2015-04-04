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
        Gwen.Font fontButonLabels;

        public Image galaxyImage;
        public Gwen.Control.ImagePanel img;
        Gwen.Control.Label label;
        Gwen.Control.Button buttonCombat;

        /// <summary>
        /// Экземпляр класса DrawController, который будет отвечать за отрисовку в главной форме
        /// </summary>
        DrawController DrawControl;

        bool dragging = false;
        int mx = 0;
        int my = 0;

        public Screen_GameScreen(Base parent)
            : base(parent)
        {
            fontButonLabels = new Gwen.Font(Skin.Renderer);
            fontButonLabels.FaceName = "OpenSans.ttf";
            fontButonLabels.Size = 25;

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
            buttonTech.Font = fontButonLabels;
            buttonTech.SetBounds(550, 500, 200, 50);
            buttonTech.Clicked += onButtonTechClick;

            Gwen.Control.Button buttonSolarSystem = new Gwen.Control.Button(this);
            buttonSolarSystem.Text = "Solar System";
            buttonSolarSystem.Font = fontButonLabels;
            buttonSolarSystem.SetBounds(300, 500, 200, 50);
            buttonSolarSystem.Clicked += onSolarSystemClick;

            buttonCombat = new Gwen.Control.Button(this);
            buttonCombat.Text = "Combat";
            buttonCombat.Font = Program.fontButtonLabels;
            buttonCombat.SetBounds(Program.percentW(0), Program.percentH(92), Program.percentW(18), Program.percentH(8));
            buttonCombat.Clicked += onCombatClick;
        }

        private void onButtonTechClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("techtree");
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
        }

        public override void Dispose()
        {
            fontButonLabels.Dispose();
            base.Dispose();
        }



    }
}
