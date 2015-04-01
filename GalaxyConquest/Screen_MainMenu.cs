using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Gwen;
using Gwen.Control;

namespace GalaxyConquest
{
    public class Screen_MainMenu : Gwen.Control.Base
    {
        Gwen.Font fontLogo;
        Gwen.Font fontButonLabels;

        public Screen_MainMenu(Base parent) : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            // Note that when using a custom font, this font object has to stick around
            // for the lifetime of the label. Rethink, or is that ideal?
            //перевожу по-простому: шрифты надо повторно использовать и хранить для всего приложения
            fontLogo = new Gwen.Font(Skin.Renderer);
            fontLogo.FaceName = "OpenSans.ttf";
            fontLogo.Size = 85;

            fontButonLabels = new Gwen.Font(Skin.Renderer);
            fontButonLabels.FaceName = "OpenSans.ttf";
            fontButonLabels.Size = 25;

            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "GalaxyConquest";            
            label.SetPosition(70, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = fontLogo;


            Gwen.Control.Button buttonNewGame = new Gwen.Control.Button(this);
            buttonNewGame.Text = "New game";
            buttonNewGame.Font = fontButonLabels;
            buttonNewGame.SetBounds(300, 200, 200, 50);
            buttonNewGame.Pressed += onButtonNewGameClick;

            Gwen.Control.Button buttonLoadGame = new Gwen.Control.Button(this);
            buttonLoadGame.Text = "Load game";
            buttonLoadGame.Disable();
            buttonLoadGame.Font = fontButonLabels;
            buttonLoadGame.SetBounds(300, 260, 200, 50);

            Gwen.Control.Button buttonSettings = new Gwen.Control.Button(this);
            buttonSettings.Text = "Settings";
            buttonSettings.Font = fontButonLabels;
            buttonSettings.SetBounds(300, 320, 200, 50);
            buttonSettings.Pressed += onButtonSettingsClick;

            Gwen.Control.Button buttonCredits = new Gwen.Control.Button(this);
            buttonCredits.Text = "Credits";
            buttonCredits.Font = fontButonLabels;
            buttonCredits.SetBounds(300, 380, 200, 50);

            Gwen.Control.Button buttonQuit = new Gwen.Control.Button(this);
            buttonQuit.Text = "Quit";
            buttonQuit.Font = fontButonLabels;
            buttonQuit.SetBounds(300, 440, 200, 50);
            buttonQuit.Pressed += onButtonQuitClick;

        }

        private void onButtonSettingsClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("settings");
        }

        private void onButtonNewGameClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("newgame");
        }

        private void onButtonQuitClick(Base control, EventArgs args)
        {
            Program.quitFlag = true;
        }

        public override void Dispose()
        {
            fontLogo.Dispose();
            fontButonLabels.Dispose();
            base.Dispose();
        }

    }
}
