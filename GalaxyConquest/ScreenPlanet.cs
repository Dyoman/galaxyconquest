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
        Gwen.Font fontLogo;
        Gwen.Font fontText;
        Gwen.Font fontButonLabels;

        public static bool fullScreen = false;

        public ScreenPlanet(Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            // Note that when using a custom font, this font object has to stick around
            // for the lifetime of the label. Rethink, or is that ideal?
            //перевожу по-простому: шрифты надо повторно использовать и хранить для всего приложения
            fontLogo = new Gwen.Font(Skin.Renderer);
            fontLogo.FaceName = "OpenSans.ttf";
            fontLogo.Size = 35;

            fontText = new Gwen.Font(Skin.Renderer);
            fontText.FaceName = "OpenSans.ttf";
            fontText.Size = 15;

            fontButonLabels = new Gwen.Font(Skin.Renderer);
            fontButonLabels.FaceName = "OpenSans.ttf";
            fontButonLabels.Size = 25;

            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "Planet";
            label.SetPosition(30, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = fontLogo;

            int width; 
            int height;

            Gwen.Control.WindowControl settingsWindow = new Gwen.Control.WindowControl(this);
            settingsWindow.Width = parent.Width / 2;
            settingsWindow.Height = parent.Height / 2;
            settingsWindow.SetPosition(width = parent.Width / 2 - settingsWindow.Width / 2, height = parent.Height / 2 - settingsWindow.Height / 2);

            Gwen.Control.Button buttonBack = new Gwen.Control.Button(settingsWindow);
            buttonBack.Text = "Back";
            buttonBack.Font = fontButonLabels;
            buttonBack.SetBounds(width / 100 * 8, height / 100 * 8, width / 3, height / 3);
            buttonBack.Clicked += onButtonBackClick;
        }

        private void onButtonBackClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("solarSystem");
        }

        public override void Dispose()
        {
            fontLogo.Dispose();
            fontText.Dispose();
            fontButonLabels.Dispose();
            base.Dispose();
        }

    }
}