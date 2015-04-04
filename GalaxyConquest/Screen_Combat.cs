using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Gwen.Control;

using GalaxyConquest.Tactics;
using GalaxyConquest.Drawing;
using GalaxyConquest.Game;

namespace GalaxyConquest
{
    class Screen_Combat : Gwen.Control.DockBase
    {
        public PictureBox pictureMap = new PictureBox();
        Gwen.Control.ImagePanel imgCombatMap;
        Gwen.Control.Button buttonStep;
        Gwen.Control.Button buttonConcede;
        Gwen.Control.Button buttonAutoBattle;
        Gwen.Control.Label labelStep;

        TacticDraw tacticDraw = new TacticDraw();
        TacticState tacticState = new TacticState();
        TacticSeed seed = new TacticSeed();

        public Screen_Combat(Base parent) 
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            imgCombatMap = new Gwen.Control.ImagePanel(this);

            tacticState.New(ref seed);
            ShipsCounter.ShipsCount(ref seed);
            pictureMap.Width = Program.percentW(100);
            pictureMap.Height = Program.percentH(82);
            tacticDraw.New(pictureMap, seed, tacticState);

            UpdateDrawing();

            imgCombatMap.SetPosition(Program.percentW(0), Program.percentH(0));
            imgCombatMap.SetSize(pictureMap.Width, pictureMap.Height);

            buttonStep = new Gwen.Control.Button(this);
            buttonStep.Text = "Step";
            buttonStep.Font = Program.fontButtonLabels;
            buttonStep.SetBounds(Program.percentW(82), Program.percentH(82), Program.percentW(18), Program.percentH(6));
            buttonStep.Clicked += onStepClick;

            buttonConcede = new Gwen.Control.Button(this);
            buttonConcede.Text = "Concede";
            buttonConcede.Font = Program.fontButtonLabels;
            buttonConcede.SetBounds(Program.percentW(82), Program.percentH(88), Program.percentW(18), Program.percentH(6));
            buttonConcede.Clicked += onConcedeClick;

            buttonAutoBattle = new Gwen.Control.Button(this);
            buttonAutoBattle.Text = "Auto Battle";
            buttonAutoBattle.Font = Program.fontButtonLabels;
            buttonAutoBattle.SetBounds(Program.percentW(82), Program.percentH(94), Program.percentW(18), Program.percentH(6));
            buttonAutoBattle.Clicked += onAutoBattleClick;

            labelStep = new Gwen.Control.Label(this);
            labelStep.Text = "Ходит 1-й игрок";
            labelStep.Font = Program.fontText;
            labelStep.TextColor = Color.Red;
            labelStep.SetBounds(Program.percentW(65), Program.percentH(90), Program.percentW(20), Program.percentH(8));
        }

        private void onStepClick(Base control, EventArgs args)
        {
            if (seed.activePlayer == 1) seed.activePlayer = 2;
            else seed.activePlayer = 1;
            labelStep.Text = "Ходит " + seed.activePlayer + "-й игрок";
            seed.activeShip = null;
            tacticDraw.DrawSavedImages(pictureMap, seed);
            for (int count = 0; count < seed.allShips.Count; count++)
            {
                seed.allShips[count].refill();
                seed.allShips[count].statusRefresh(ref tacticDraw.bmBackground, ref tacticDraw.bmFull);
            }
            tacticState.objectManager.moveMeteors(tacticState.cMap, tacticDraw.bmBackground, tacticDraw.bmFull);
            if (tacticState.objectManager.whether2createMeteor() == 1)
            {
                Meteor newMeteor = tacticState.objectManager.meteorCreate(tacticState.cMap);
                if (newMeteor != null)
                {
                    tacticDraw.DrawMeteor(seed, tacticState, newMeteor);
                }
            }
            ShipsCounter.ShipsCount(ref seed);
            if (seed.redShipsCount == 0 || seed.blueShipsCount == 0)
                Program.screenManager.LoadScreen("gamescreen");
            UpdateDrawing();
        }

        private void onConcedeClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("gamescreen");
        }

        private void onAutoBattleClick(Base control, EventArgs args)
        {

        }

        private void UpdateDrawing()
        {
            imgCombatMap.Image = (Bitmap)pictureMap.Image;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
