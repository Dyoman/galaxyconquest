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
        public bool LoadGameOpend = false;

        public Gwen.Control.ListBox savedFilesListControl;

        Gwen.Control.WindowControl LoadGameWindow;
        public Screen_MainMenu(Base parent) : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "GalaxyConquest";            
            label.SetPosition(70, 30);
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;


            Gwen.Control.Button buttonNewGame = new Gwen.Control.Button(this);
            buttonNewGame.Text = "New game";
            buttonNewGame.Font = Program.fontButtonLabels;
            buttonNewGame.SetBounds(300, 200, 200, 50);
            buttonNewGame.Pressed += onButtonNewGameClick;

            Gwen.Control.Button buttonLoadGame = new Gwen.Control.Button(this);
            buttonLoadGame.Text = "Load game";
            //buttonLoadGame.Disable();
            buttonLoadGame.Font = Program.fontButtonLabels;
            buttonLoadGame.SetBounds(300, 260, 200, 50);
            buttonLoadGame.Pressed += onButtonLoadGameClick;


            Gwen.Control.Button buttonSettings = new Gwen.Control.Button(this);
            buttonSettings.Text = "Settings";
            buttonSettings.Font = Program.fontButtonLabels;
            buttonSettings.SetBounds(300, 320, 200, 50);
            buttonSettings.Pressed += onButtonSettingsClick;

            Gwen.Control.Button buttonCredits = new Gwen.Control.Button(this);
            buttonCredits.Text = "Credits";
            buttonCredits.Font = Program.fontButtonLabels;
            buttonCredits.SetBounds(300, 380, 200, 50);

            Gwen.Control.Button buttonQuit = new Gwen.Control.Button(this);
            buttonQuit.Text = "Quit";
            buttonQuit.Font = Program.fontButtonLabels;
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
            base.Dispose();
        }

        private void onButtonLoadGameClick(Base control, EventArgs args)
        {

            if (!LoadGameOpend)
            {
                LoadGameOpend = true;
                LoadGameWindow = new Gwen.Control.WindowControl(this);
                LoadGameWindow.Width = Program.percentW(50);
                LoadGameWindow.Height = Program.percentH(50);
                LoadGameWindow.SetPosition(Program.percentW(25), Program.percentH(20));
                LoadGameWindow.IsClosable = false;

                Gwen.Control.Button buttonBackMenu = new Gwen.Control.Button(LoadGameWindow);
                buttonBackMenu.Text = "BackMenu";
                buttonBackMenu.Font = Program.fontButtonLabels;
                buttonBackMenu.SetBounds(Program.percentW(50)/100, Program.percentH(50)*60/100, 150, 50);
                buttonBackMenu.Pressed += onButtonBackMenuClick;

                Gwen.Control.Button buttonLoadOk = new Gwen.Control.Button(LoadGameWindow);
                buttonLoadOk.Text = "Ok";
                buttonLoadOk.Font = Program.fontButtonLabels;
                buttonLoadOk.SetBounds(Program.percentW(50)/100+160, Program.percentH(50) * 60 / 100, 150, 50);
                buttonLoadOk.Pressed += onButtonLoadOkClick;

                savedFilesListControl = new Gwen.Control.ListBox(LoadGameWindow);
                savedFilesListControl.SetSize(Program.percentW(45), 200);
                savedFilesListControl.SetPosition(10, 10);

                IEnumerable<string> fileNamesCollection = System.IO.Directory.EnumerateFiles(@"C:\","*.sav");

                foreach (string fileName in fileNamesCollection)
                {
                    savedFilesListControl.AddRow(fileName);
                }
           }
            
        }
        public void onButtonBackMenuClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("mainmenu");
        }
        public void onButtonLoadOkClick(Base control, EventArgs args)
        {
     

        }


    }
}
