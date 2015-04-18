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
        int galaxysize = 5;
        int starsCount = 20;
        Gwen.Control.ImagePanel imageGalaxyType;
        Gwen.Control.Label label;
        Gwen.Control.Label labelGalaxySize;
        Gwen.Control.Label labelStarsCount;

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

            //----------------------------------------------------------------------

            imageGalaxyType = new Gwen.Control.ImagePanel(this);
            imageGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
            imageGalaxyType.SetBounds(Program.percentW(5), Program.percentH(20), Program.percentW(15), Program.percentH(15));

            Gwen.Control.Button buttonGalaxyTypeLeft = new Gwen.Control.Button(this);
            buttonGalaxyTypeLeft.Text = "<";
            buttonGalaxyTypeLeft.SetBounds(Program.percentW(0), Program.percentH(35), Program.percentW(5), Program.percentH(5));
            buttonGalaxyTypeLeft.Clicked += onButtonGalaxyTypeLeftClick;

            Gwen.Control.Button buttonGalaxyTypeRight = new Gwen.Control.Button(this);
            buttonGalaxyTypeRight.Text = ">";
            buttonGalaxyTypeRight.SetBounds(Program.percentW(20), Program.percentH(35), Program.percentW(5), Program.percentH(5));
            buttonGalaxyTypeRight.Clicked += onButtonGalaxyTypeRightClick;

            //-----------------------------------------------------------------------

            labelGalaxySize = new Gwen.Control.Label(this);
            labelGalaxySize.Text = galaxysize.ToString();
            labelGalaxySize.SetPosition(Program.percentW(40), Program.percentH(20));
            labelGalaxySize.TextColor = Color.FromArgb(200, 80, 0, 250);
            labelGalaxySize.Font = Program.fontLogo;

            Gwen.Control.Button buttonGalaxySizeLeft = new Gwen.Control.Button(this);
            buttonGalaxySizeLeft.Text = "<";
            buttonGalaxySizeLeft.SetBounds(Program.percentW(30), Program.percentH(35), Program.percentW(5), Program.percentH(5));
            buttonGalaxySizeLeft.Clicked += onButtonGalaxySizeLeftClick;

            Gwen.Control.Button buttonGalaxySizeRight = new Gwen.Control.Button(this);
            buttonGalaxySizeRight.Text = ">";
            buttonGalaxySizeRight.SetBounds(Program.percentW(50), Program.percentH(35), Program.percentW(5), Program.percentH(5));
            buttonGalaxySizeRight.Clicked += onButtonGalaxySizeRightClick;

            //-----------------------------------------------------------------------

            labelStarsCount = new Gwen.Control.Label(this);
            labelStarsCount.Text = starsCount.ToString();
            labelStarsCount.SetPosition(Program.percentW(70), Program.percentH(20));
            labelStarsCount.TextColor = Color.FromArgb(200, 80, 0, 250);
            labelStarsCount.Font = Program.fontLogo;

            Gwen.Control.Button buttonStarsCountLeft = new Gwen.Control.Button(this);
            buttonStarsCountLeft.Text = "<";
            buttonStarsCountLeft.SetBounds(Program.percentW(60), Program.percentH(35), Program.percentW(5), Program.percentH(5));
            buttonStarsCountLeft.Clicked += onButtonStarsCountLeftClick;

            Gwen.Control.Button buttonStarsCountRight = new Gwen.Control.Button(this);
            buttonStarsCountRight.Text = ">";
            buttonStarsCountRight.SetBounds(Program.percentW(80), Program.percentH(35), Program.percentW(5), Program.percentH(5));
            buttonStarsCountRight.Clicked += onButtonStarsCountRightClick;

            //----------------------------------------------------------------------

            Gwen.Control.Button buttonRaceLeft = new Gwen.Control.Button(this);
            buttonRaceLeft.Text = "<";
            buttonRaceLeft.SetBounds(Program.percentW(0), Program.percentH(65), Program.percentW(5), Program.percentH(5));
            buttonRaceLeft.Clicked += onButtonRaceLeftClick;

            Gwen.Control.Button buttonRaceRight = new Gwen.Control.Button(this);
            buttonRaceRight.Text = ">";
            buttonRaceRight.SetBounds(Program.percentW(20), Program.percentH(65), Program.percentW(5), Program.percentH(5));
            buttonRaceRight.Clicked += onButtonRaceRightClick;

            //-----------------------------------------------------------------------

            Gwen.Control.Button buttonBannerLeft = new Gwen.Control.Button(this);
            buttonBannerLeft.Text = "<";
            buttonBannerLeft.SetBounds(Program.percentW(30), Program.percentH(65), Program.percentW(5), Program.percentH(5));
            buttonBannerLeft.Clicked += onButtonBannerLeftClick;

            Gwen.Control.Button buttonBannerRight = new Gwen.Control.Button(this);
            buttonBannerRight.Text = ">";
            buttonBannerRight.SetBounds(Program.percentW(50), Program.percentH(65), Program.percentW(5), Program.percentH(5));
            buttonBannerRight.Clicked += onButtonBannerRightClick;

            //-----------------------------------------------------------------------

            Gwen.Control.Button buttonFleetStyleLeft = new Gwen.Control.Button(this);
            buttonFleetStyleLeft.Text = "<";
            buttonFleetStyleLeft.SetBounds(Program.percentW(60), Program.percentH(65), Program.percentW(5), Program.percentH(5));
            buttonFleetStyleLeft.Clicked += onButtonFleetStyleLeftClick;

            Gwen.Control.Button buttonFleetStyleRight = new Gwen.Control.Button(this);
            buttonFleetStyleRight.Text = ">";
            buttonFleetStyleRight.SetBounds(Program.percentW(80), Program.percentH(65), Program.percentW(5), Program.percentH(5));
            buttonFleetStyleRight.Clicked += onButtonFleetStyleRightClick;

        }

        private void onButtonGalaxySizeLeftClick(Base control, EventArgs args)
        {
            if (galaxysize == 5)
            {
                return;
            }
            else
            {
                galaxysize -= 1;
                labelGalaxySize.Text = galaxysize.ToString();
            }
        }

        private void onButtonGalaxySizeRightClick(Base control, EventArgs args)
        {
            if (galaxysize == 10)
            {
                return;
            }
            else
            {
                galaxysize += 1;
                labelGalaxySize.Text = galaxysize.ToString();
            }
        }

        private void onButtonStarsCountLeftClick(Base control, EventArgs args)
        {
            if (starsCount == 10)
            {
                return;
            }
            else
            {
                starsCount -= 10;
                labelStarsCount.Text = starsCount.ToString();
            }
        }

        private void onButtonStarsCountRightClick(Base control, EventArgs args)
        {
            if (starsCount == 90)
            {
                return;
            }
            else
            {
                starsCount += 10;
                labelStarsCount.Text = starsCount.ToString();
            }
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

        private void onButtonRaceLeftClick(Base control, EventArgs args)
        {

        }

        private void onButtonRaceRightClick(Base control, EventArgs args)
        {

        }

        private void onButtonBannerLeftClick(Base control, EventArgs args)
        {

        }

        private void onButtonBannerRightClick(Base control, EventArgs args)
        {

        }

        private void onButtonFleetStyleLeftClick(Base control, EventArgs args)
        {

        }

        private void onButtonFleetStyleRightClick(Base control, EventArgs args)
        {

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
            seed.gType = galaxytype;
            seed.gSize = galaxysize;
            seed.gStarsCount = starsCount;
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
