using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Gwen;
using Gwen.Control;

namespace GalaxyConquest
{
    public class Screen_Settings : Gwen.Control.DockBase
    {
        Gwen.Font fontLogo;
        Gwen.Font fontButonLabels;

        public Screen_Settings(Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            // Note that when using a custom font, this font object has to stick around
            // for the lifetime of the label. Rethink, or is that ideal?
            //перевожу по-простому: шрифты надо повторно использовать и хранить для всего приложения
            fontLogo = new Gwen.Font(Skin.Renderer);
            fontLogo.FaceName = "OpenSans.ttf";
            fontLogo.Size = 35;

            fontButonLabels = new Gwen.Font(Skin.Renderer);
            fontButonLabels.FaceName = "OpenSans.ttf";
            fontButonLabels.Size = 25;

            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "Settings";
            label.SetPosition(30, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = fontLogo;

            Gwen.Control.Button buttonOK = new Gwen.Control.Button(this);
            buttonOK.Text = "OK";
            buttonOK.Font = fontButonLabels;
            buttonOK.SetBounds(550, 500, 200, 50);
            buttonOK.Clicked += onButtonOKClick;
        }

        private void onButtonOKClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("mainmenu");
        }

        public override void Dispose()
        {
            fontLogo.Dispose();
            fontButonLabels.Dispose();
            base.Dispose();
        }

    }
}
