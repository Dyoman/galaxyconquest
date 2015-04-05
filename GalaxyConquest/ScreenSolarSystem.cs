using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Gwen;
using Gwen.Control;

namespace GalaxyConquest
{
    public class ScreenSolarSystem : Gwen.Control.DockBase
    {

        public ScreenSolarSystem(Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);

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

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}