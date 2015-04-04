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

            int width; 
            int height;

            Gwen.Control.WindowControl settingsWindow = new Gwen.Control.WindowControl(this);
            settingsWindow.Width = parent.Width / 2;
            settingsWindow.Height = parent.Height / 2;
            settingsWindow.SetPosition(width = parent.Width / 2 - settingsWindow.Width / 2, height = parent.Height / 2 - settingsWindow.Height / 2);

            Gwen.Control.Button buttonBack = new Gwen.Control.Button(settingsWindow);
            buttonBack.Text = "Back";
            buttonBack.Font = Program.fontButtonLabels;
            buttonBack.SetBounds(width / 100 * 8, height / 100 * 8, width / 3, height / 3);
            buttonBack.Clicked += onButtonBackClick;
        }

        private void onButtonBackClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("solarSystem");
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}