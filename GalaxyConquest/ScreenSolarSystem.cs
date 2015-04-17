using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GalaxyConquest.Drawing;
using System.Drawing.Imaging;
using Gwen;
using System.Windows.Forms;
using Gwen.Control;
namespace GalaxyConquest
{
    public class ScreenSolarSystem : Gwen.Control.DockBase
    {
        public static PictureBox pictureMap = new PictureBox();
        public Image SolarSystemImage;
        DrawController DrawControl;
        public Gwen.Control.ImagePanel SolarSystemImg;

        public ScreenSolarSystem(Base parent)

            : base(parent)
        {
            SetSize(parent.Width, parent.Height);
            ////отрисовка
            SolarSystemImage = new Bitmap(Program.percentW(50), Program.percentH(50), PixelFormat.Format32bppArgb);
            DrawControl = new DrawController(SolarSystemImage);

          //  updateDrawing();

            SolarSystemImg = new Gwen.Control.ImagePanel(this);
            SolarSystemImg.SetPosition(Program.percentW(0), Program.percentH(0));
            SolarSystemImg.SetSize(Program.percentW(100), Program.percentH(100));



            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "Solar System";
            label.SetPosition(30, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            
            Gwen.Control.Button buttonOK = new Gwen.Control.Button(this);
            buttonOK.Text = "OK";
            buttonOK.Font = Program.fontButtonLabels;
            buttonOK.SetBounds(300, 500, 200, 50);
            buttonOK.Clicked += onButtonOKClick;
            

            Gwen.Control.Button buttonPlanet = new Gwen.Control.Button(this);
            buttonPlanet.Text = "Planet";
            buttonPlanet.Font = Program.fontButtonLabels;
            buttonPlanet.SetBounds(550, 500, 200, 50);
            buttonPlanet.Clicked += onButtonPlanetClick;


        }

        private void onButtonOKClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("gamescreen");
        }

        private void onButtonPlanetClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("planet");
        }

        public void updateDrawing()
        {
            Graphics gr = Graphics.FromImage(SolarSystemImage);
            gr.FillRectangle(Brushes.Black, 0, 0, SolarSystemImage.Width, SolarSystemImage.Height);

            DrawControl.Render(Program.Game, gr);
            SolarSystemImg.Image = (Bitmap)SolarSystemImage;

            Program.m_Canvas.RenderCanvas();
            Program.m_Window.Display();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}