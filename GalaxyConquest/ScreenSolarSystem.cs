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
        Gwen.Font fontLogo;
        Gwen.Font fontText;
        Gwen.Font fontButonLabels;

        public static bool fullScreen = false;

        public ScreenSolarSystem(Base parent)
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
            label.Text = "Solar System";
            label.SetPosition(30, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = fontLogo;

            
            Gwen.Control.Button buttonOK = new Gwen.Control.Button(this);
            buttonOK.Text = "OK";
            buttonOK.Font = fontButonLabels;
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
            fontLogo.Dispose();
            fontText.Dispose();
            fontButonLabels.Dispose();
            base.Dispose();
        }

    }
}