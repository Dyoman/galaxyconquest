using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Gwen;
using Gwen.Control;

namespace GalaxyConquest
{
    public class ScreenPlanet : Gwen.Control.DockBase
    {

        public static bool fullScreen = false;

        public ScreenPlanet(Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "Planet";
            label.SetPosition(30, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            Gwen.Control.Button buttonBack = new Gwen.Control.Button(this);
            buttonBack.Text = "Back";
            buttonBack.Font = Program.fontButtonLabels;
            buttonBack.SetBounds(Program.percentW(90), Program.percentH(92), Program.percentW(10), Program.percentH(8));
            buttonBack.Clicked += onButtonBackClick;
        }

        private void onButtonBackClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("gamescreen");
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}