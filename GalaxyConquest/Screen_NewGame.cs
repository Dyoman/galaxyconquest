using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using SFML;
using Gwen;
using Gwen.Control;

using GalaxyConquest.Drawing;
using GalaxyConquest.Game;
using GalaxyConquest.SpaceObjects;

namespace GalaxyConquest
{
    class Screen_NewGame : Gwen.Control.DockBase
    {
        public Screen_NewGame(Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);


            Gwen.Control.Label label = new Gwen.Control.Label(this);
            label.Text = "New game";
            label.SetPosition(Program.percentW(5), Program.percentH(5));
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            Gwen.Control.Button buttonBack = new Gwen.Control.Button(this);
            buttonBack.Text = "Back";
            buttonBack.Font = Program.fontButtonLabels;
            buttonBack.SetBounds(Program.percentW(5), Program.percentH(87), Program.percentW(18), Program.percentH(8));
            buttonBack.Clicked += onButtonBackClick;

            Gwen.Control.Button buttonNewGame = new Gwen.Control.Button(this);
            buttonNewGame.Text = "New game";
            buttonNewGame.Font = Program.fontButtonLabels;
            buttonNewGame.SetBounds(Program.percentW(77), Program.percentH(87), Program.percentW(18), Program.percentH(8));
            buttonNewGame.Clicked += onButtonNewGameClick;
        }

        private void onButtonBackClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("mainmenu");
        }

        private void onButtonNewGameClick(Base control, EventArgs args)
        {
            Program.Game = new GameState();
            GameSeed seed = new GameSeed();
            
            /*seed.pName = nd.namePlayer;
            seed.gName = nd.galaxyName;
            seed.gType = nd.getGalaxyType();
            seed.gSize = nd.getGalaxySize();
            seed.gStarsCount = nd.getStarsCount();
            seed.gGenerateRandomEvent = nd.getGalaxyRandomEvents();

            Program.Game.New(seed);

            UpdateControls();

            panel1.Enabled = true;
            mainMenuSave.Enabled = true;
            MainMenuTechTree.Enabled = true;
            systemsButton.Enabled = true;
            fleetsButton.Enabled = true;
            GameTimer.Start();
            */



            seed.pName = "Anton";
            seed.gName = "Milky Way";
            seed.gType = GalaxyType.Spiral;
            seed.gSize = 4;
            seed.gStarsCount = 20;
            seed.gGenerateRandomEvent = true;

            Program.Game.New(seed);

            Program.screenManager.LoadScreen("gamescreen");
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
