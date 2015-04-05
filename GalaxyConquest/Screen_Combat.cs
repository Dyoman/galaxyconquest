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
        public static PictureBox pictureMap = new PictureBox();
        public Bitmap combatBitmap;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        static Gwen.Control.ImagePanel imgCombatMap;
        Gwen.Control.Button buttonStep;
        Gwen.Control.Button buttonConcede;
        Gwen.Control.Button buttonAutoBattle;
        Gwen.Control.Label labelStep;
        Gwen.Control.Label labelDescription;

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
            imgCombatMap.MouseDown += new GwenEventHandler<ClickedEventArgs>(imgCombatMap_MouseDown);

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
            labelStep.TextColor = Color.GreenYellow;
            labelStep.SetBounds(Program.percentW(65), Program.percentH(90), Program.percentW(20), Program.percentH(8));

            labelDescription = new Gwen.Control.Label(this);
            labelDescription.Text = "";
            labelDescription.Font = Program.fontText;
            labelDescription.TextColor = Color.GreenYellow;
            labelDescription.SetBounds(Program.percentW(0), Program.percentH(82), Program.percentW(50), Program.percentH(18));
        }

        private void onStepClick(Base control, EventArgs args)
        {
            labelDescription.Text = "";
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
            UpdateDrawing();
            if (seed.redShipsCount == 0)
                EndCombat(1);
            if (seed.blueShipsCount == 0)
                EndCombat(2);
        }

        private void onConcedeClick(Base control, EventArgs args)
        {
            if (seed.activePlayer == 1)
                EndCombat(2);
            if (seed.activePlayer == 2)
                EndCombat(1);
        }

        private void onAutoBattleClick(Base control, EventArgs args)
        {

        }

        void imgCombatMap_MouseDown(Base sender, ClickedEventArgs e)
        {
            for (int i = 0; i < tacticState.cMap.boxes.Count; i++)
            {
                if ((e.X > tacticState.cMap.boxes[i].xpoint2) &&
                    (e.X < tacticState.cMap.boxes[i].xpoint3) &&
                    (e.Y > tacticState.cMap.boxes[i].ypoint2) &&
                    (e.Y < tacticState.cMap.boxes[i].ypoint6))
                {
                    seed.select = i;
                    if (seed.activeShip == null && tacticState.cMap.boxes[seed.select].spaceObject != null)
                    {
                        if (tacticState.cMap.boxes[seed.select].spaceObject != null)
                        {
                            if (seed.activePlayer == tacticState.cMap.boxes[seed.select].spaceObject.player)
                            {
                                // отрисовываем рамку вокруг активного корабля
                                labelDescription.Text = tacticState.cMap.boxes[seed.select].spaceObject.description();
                                seed.activeShip = (Ship)tacticState.cMap.boxes[seed.select].spaceObject;
                                tacticDraw.DrawSavedImages(pictureMap, seed);
                                tacticDraw.DrawActiveShipFrames(pictureMap, seed, tacticState);
                            }
                            else
                            {
                                labelDescription.Text = tacticState.cMap.boxes[i].spaceObject.description();
                            }
                        }
                    }
                    // Если до этого ткнули по дружественному судну
                    else if (seed.activeShip != null)
                    {
                        // если выбранная клетка пуста - определяем возможность перемещения 
                        if (seed.activeShip.actionsLeft > 0 && tacticState.cMap.boxes[seed.select].spaceObject == null)
                        {
                            int flag = 0;
                            int a = seed.activeShip.boxId;
                            int x1, x2, y1, y2;
                            x1 = tacticState.cMap.boxes[a].x;
                            x2 = tacticState.cMap.boxes[seed.select].x;
                            y1 = tacticState.cMap.boxes[a].y;
                            y2 = tacticState.cMap.boxes[seed.select].y;
                            int lineRange = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);
                            List<Box> completeBoxWay = new List<Box>();
                            if (lineRange <= seed.activeShip.actionsLeft)
                            {
                                // запоминаю начальную клетку
                                Box baseBox = tacticState.cMap.boxes[a];
                                // определяю направление, в котором находится целевая клетка
                                int direction = FindWay.FindDirection(x1, x2, y1, y2);
                                // определяю приоритет
                                List<int> priority = new List<int>();
                                FindWay.FindPriority(direction, ref priority);
                                for (int k = 0; k < 6; k++)
                                {
                                    if (FindWay.GetBoxway(baseBox, baseBox, tacticState.cMap.boxes[seed.select], ref completeBoxWay, seed.activeShip.actionsLeft, k, tacticState) == 0) break;
                                }
                                if (completeBoxWay.Count > 0) flag = 1;
                            }
                            if (flag == 1)
                            {
                                tacticDraw.Move(pictureMap, seed, tacticState, completeBoxWay, x1, x2, y1, y2);
                                labelDescription.Text = seed.activeShip.description();
                                if (seed.activeShip.actionsLeft != 0)
                                {
                                    tacticDraw.DrawSavedImages(pictureMap, seed);
                                    tacticDraw.DrawActiveShipFrames(pictureMap, seed, tacticState);
                                }
                                else
                                {
                                    tacticDraw.DrawSavedImages(pictureMap, seed);
                                    seed.activeShip = null;
                                }
                            }
                        }
                        else if (tacticState.cMap.boxes[seed.select].spaceObject != null)
                        {
                            if (tacticState.cMap.boxes[seed.select].spaceObject.player == seed.activePlayer)
                            {
                                labelDescription.Text = tacticState.cMap.boxes[seed.select].spaceObject.description();
                                seed.activeShip = (Ship)tacticState.cMap.boxes[seed.select].spaceObject;
                                tacticDraw.DrawSavedImages(pictureMap, seed);
                                tacticDraw.DrawActiveShipFrames(pictureMap, seed, tacticState);
                            }
                            // просчет возможности атаки 
                            else if (tacticState.cMap.boxes[seed.select].spaceObject.player != seed.activePlayer)
                            {
                                int flag = 0;
                                int a = seed.activeShip.boxId;
                                double x1 = tacticState.cMap.boxes[a].x;
                                double y1 = tacticState.cMap.boxes[a].y;
                                double x2 = tacticState.cMap.boxes[seed.select].x;
                                double y2 = tacticState.cMap.boxes[seed.select].y;
                                double range;
                                // определяем расстояние между объектами
                                range = Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);
                                if (seed.activeShip.equippedWeapon.attackRange >= (int)range)
                                {
                                    flag = 1; // устанавливаем флаг, если расстояние не превышает дальности атаки
                                }
                                if (flag == 1)
                                {
                                    if (tacticDraw.Attack(pictureMap, seed, tacticState, combatBitmap, player) == 1)
                                        ShipsCounter.ShipsCount(ref seed);
                                    labelDescription.Text = seed.activeShip.description();
                                    
                                    // убираем подсветку с корабля, если у него не осталось очков передвижений
                                    if (seed.activeShip.actionsLeft == 0)
                                    {
                                        tacticDraw.DrawSavedImages(pictureMap, seed);
                                        seed.activeShip = null;
                                    }
                                    else
                                    {
                                        tacticDraw.DrawSavedImages(pictureMap, seed);
                                        tacticDraw.DrawActiveShipFrames(pictureMap, seed, tacticState);
                                    }
                                }
                            }
                        }
                    }
                    UpdateDrawing();
                    if (seed.redShipsCount == 0)
                        EndCombat(1);
                    if (seed.blueShipsCount == 0)
                        EndCombat(2);
                    break;
                }
            }
        }

        public static void UpdateDrawing()
        {
            imgCombatMap.Image = (Bitmap)pictureMap.Image;

            Program.m_Canvas.RenderCanvas();
            Program.m_Window.Display();
        }

        private void EndCombat(int win)
        {
            Gwen.Control.WindowControl windowEnd = new Gwen.Control.WindowControl(this);
            windowEnd.Width = this.Width / 2;
            windowEnd.Height = this.Height / 2;
            windowEnd.SetPosition(this.Width / 2 - windowEnd.Width / 2, this.Height / 2 - windowEnd.Height / 2);

            Gwen.Control.Label labelWin = new Gwen.Control.Label(windowEnd);
            labelWin.Text = "Win Player " + win.ToString();
            labelWin.Font = Program.fontLogo;
            labelWin.SetBounds(windowEnd.Width / 2 - labelWin.Width / 2, windowEnd.Height / 2 - labelWin.Height, windowEnd.Width, windowEnd.Height * 15 / 100);

            Gwen.Control.Button buttonOK = new Gwen.Control.Button(windowEnd);
            buttonOK.Text = "OK";
            buttonOK.Font = Program.fontButtonLabels;
            buttonOK.SetBounds(windowEnd.Width / 2 - buttonOK.Width/2, windowEnd.Height * 70 / 100, windowEnd.Width * 25 / 100, windowEnd.Height * 15 / 100);
            buttonOK.Clicked += onOKClick;

            windowEnd.IsClosable = false;
        }

        private void onOKClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("gamescreen");
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
