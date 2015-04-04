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

        public static bool fullScreen = false;

        public Screen_Settings(Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "Settings";
            label.SetPosition(30, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            Gwen.Control.WindowControl settingsWindow = new Gwen.Control.WindowControl(this);
            settingsWindow.Width = parent.Width / 2;
            settingsWindow.Height = parent.Height / 2;
            settingsWindow.SetPosition(parent.Width / 2 - settingsWindow.Width / 2, parent.Height / 2 - settingsWindow.Height / 2);

            Gwen.Control.Label musicLabel = new Gwen.Control.Label(settingsWindow);
            musicLabel.Text = "Music:";
            musicLabel.SetPosition(parent.Width / 10, parent.Height / 10);
            musicLabel.TextColor = Color.FromArgb(255, 0, 0, 0);
            musicLabel.Font = Program.fontText;

            Gwen.Control.HorizontalSlider musicSlider = new Gwen.Control.HorizontalSlider(settingsWindow);
            musicSlider.SetPosition(parent.Width / 5, parent.Height / 10);
            musicSlider.SetSize(parent.Width / 10, musicLabel.Height);

            Gwen.Control.Label sfxLabel = new Gwen.Control.Label(settingsWindow);
            sfxLabel.Text = "SFX:";
            sfxLabel.SetPosition(parent.Width / 10, musicLabel.Y + musicLabel.Height);
            sfxLabel.TextColor = Color.FromArgb(255, 0, 0, 0);
            sfxLabel.Font = Program.fontText;

            Gwen.Control.CheckBox sfxCheckBox = new Gwen.Control.CheckBox(settingsWindow);
            sfxCheckBox.SetPosition(parent.Width / 5, musicLabel.Y + musicLabel.Height);

            Gwen.Control.Label fpsLabel = new Gwen.Control.Label(settingsWindow);
            fpsLabel.Text = "FPS Limit:";
            fpsLabel.SetPosition(parent.Width / 10, sfxLabel.Y + sfxLabel.Height);
            fpsLabel.TextColor = Color.FromArgb(255, 0, 0, 0);
            fpsLabel.Font = Program.fontText;

            Gwen.Control.HorizontalSlider fpsSlider = new Gwen.Control.HorizontalSlider(settingsWindow);
            fpsSlider.SetPosition(parent.Width / 5, sfxLabel.Y + sfxLabel.Height);
            fpsSlider.SetSize(parent.Width / 10, sfxLabel.Height);

            Gwen.Control.Label resolutionLabel = new Gwen.Control.Label(settingsWindow);
            resolutionLabel.Text = "Resolution:";
            resolutionLabel.SetPosition(parent.Width / 10, fpsLabel.Y + fpsLabel.Height);
            resolutionLabel.TextColor = Color.FromArgb(255, 0, 0, 0);
            resolutionLabel.Font = Program.fontText;

            Gwen.Control.ComboBox resolution = new ComboBox(settingsWindow);
            resolution.AddItem("800x600");
            resolution.AddItem("1024x768");
            resolution.SetPosition(parent.Width / 5, fpsLabel.Y + fpsLabel.Height);
            resolution.SetSize(parent.Width / 10, resolutionLabel.Height);

            Gwen.Control.Label fullScreenLabel = new Gwen.Control.Label(settingsWindow);
            fullScreenLabel.Text = "Full screen:";
            fullScreenLabel.SetPosition(parent.Width / 10, resolutionLabel.Y + resolutionLabel.Height);
            fullScreenLabel.TextColor = Color.FromArgb(255, 0, 0, 0);
            fullScreenLabel.Font = Program.fontText;

            Gwen.Control.CheckBox fullScreenCheckBox = new Gwen.Control.CheckBox(settingsWindow);
            fullScreenCheckBox.SetPosition(parent.Width / 5, resolutionLabel.Y + resolutionLabel.Height);
            if (fullScreen == true)
            {
                fullScreenCheckBox.Toggle();
            }
            fullScreenCheckBox.Checked += fullScreenEnable;
            fullScreenCheckBox.UnChecked += fullScreenDisable;

            Gwen.Control.Button buttonOK = new Gwen.Control.Button(this);
            buttonOK.Text = "OK";
            buttonOK.Font = Program.fontButtonLabels;
            buttonOK.SetBounds(550, 500, 200, 50);
            buttonOK.Clicked += onButtonOKClick;
        }

        private void fullScreenEnable(Base control, EventArgs args)
        {
            fullScreen = true;
        }
        private void fullScreenDisable(Base control, EventArgs args)
        {
            fullScreen = false;
        }
        private void onButtonOKClick(Base control, EventArgs args)
        {
            Program.applyVideoSettings();
            Program.screenManager.LoadScreen("mainmenu");
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
