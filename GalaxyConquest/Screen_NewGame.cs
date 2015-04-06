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
        GalaxyType galaxytype = GalaxyType.Spiral;
        int galaxysize = 4;
        Gwen.Control.ImagePanel imageGalaxyType;
        Gwen.Control.Label label;

        public Screen_NewGame(Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);


            label = new Gwen.Control.Label(this);
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

            imageGalaxyType = new Gwen.Control.ImagePanel(this);
            imageGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
            imageGalaxyType.SetBounds(Program.percentW(20), Program.percentH(20), Program.percentW(25), Program.percentH(25));

            Gwen.Control.Button buttonGalaxyTypeLeft = new Gwen.Control.Button(this);
            buttonGalaxyTypeLeft.Text = "<";
            buttonGalaxyTypeLeft.SetBounds(Program.percentW(20), Program.percentH(40), Program.percentW(5), Program.percentH(5));
            buttonGalaxyTypeLeft.Clicked += onButtonGalaxyTypeLeftClick;

            Gwen.Control.Button buttonGalaxyTypeRight = new Gwen.Control.Button(this);
            buttonGalaxyTypeRight.Text = ">";
            buttonGalaxyTypeRight.SetBounds(Program.percentW(40), Program.percentH(40), Program.percentW(5), Program.percentH(5));
            buttonGalaxyTypeRight.Clicked += onButtonGalaxyTypeRightClick;

        }

        private void onButtonGalaxyTypeLeftClick(Base control, EventArgs args)
        {
            switch (galaxytype)
            {
                case GalaxyType.Irregular:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    label.Text = "Sphere";
                    galaxytype = GalaxyType.Sphere;
                    break;
                case GalaxyType.Sphere:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    label.Text = "Elliptical";
                    galaxytype = GalaxyType.Eliptical;
                    break;
                case GalaxyType.Eliptical:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
                    label.Text = "Spiral";
                    galaxytype = GalaxyType.Spiral;
                    break;
                case GalaxyType.Spiral:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_irregular;
                    label.Text = "Irregular";
                    galaxytype = GalaxyType.Irregular;
                    break;
            }
        }

        private void onButtonGalaxyTypeRightClick(Base control, EventArgs args)
        {
            switch (galaxytype)
            {
                case GalaxyType.Irregular:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
                    label.Text = "Spiral";
                    galaxytype = GalaxyType.Spiral;
                    break;
                case GalaxyType.Spiral:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    label.Text = "Elliptical";
                    galaxytype = GalaxyType.Eliptical;
                    break;
                case GalaxyType.Eliptical:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    label.Text = "Sphere";
                    galaxytype = GalaxyType.Sphere;
                    break;
                case GalaxyType.Sphere:
                    imageGalaxyType.Image = Properties.Resources.icon_newgame_irregular;
                    label.Text = "Irregular";
                    galaxytype = GalaxyType.Irregular;
                    break;
            }
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
