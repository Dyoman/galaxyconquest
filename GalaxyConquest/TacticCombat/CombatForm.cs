using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Threading;
using GalaxyConquest.Drawing;
using GalaxyConquest.Game;

namespace GalaxyConquest.Tactics
{
    public partial class CombatForm : Form
    {
        TacticDraw tacticDraw = new TacticDraw();
        TacticState tacticState = new TacticState();
        TacticSeed seed = new TacticSeed();
        public Bitmap combatBitmap;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public CombatForm(Fleet left, Fleet right)
        {
            InitializeComponent();
            tacticState.New(ref seed, left, right);
            ReadShipsCount();
            tacticDraw.New(pictureMap, seed, tacticState);
        }

        public void ReadShipsCount()
        {
            ShipsCounter.ShipsCount(ref seed);
            txtBlueShips.Text = "" + seed.blueShipsCount;
            txtRedShips.Text = "" + seed.redShipsCount;
        }

        private void pictureMap_MouseClick(object sender, MouseEventArgs e)
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
                                boxDescription.Text = tacticState.cMap.boxes[seed.select].spaceObject.description();
                                seed.activeShip = (Ship)tacticState.cMap.boxes[seed.select].spaceObject;

                                tacticDraw.DrawSavedImages(pictureMap, /*bmFull,*/ seed);
                                tacticDraw.DrawActiveShipFrames(pictureMap, /*bmFull,*/ seed, tacticState);
                            }
                            else
                            {
                                boxDescription.Text = tacticState.cMap.boxes[i].spaceObject.description();
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
                                boxDescription.Text = seed.activeShip.description();
                                seed.activeShip.statusRefresh(ref tacticDraw.bmBackground, ref tacticDraw.bmFull);

                                if (seed.activeShip.actionsLeft != 0)
                                {
                                    tacticDraw.DrawSavedImages(pictureMap, /*bmFull,*/ seed);
                                    tacticDraw.DrawActiveShipFrames(pictureMap, /*bmFull,*/ seed, tacticState);
                                }
                                else
                                {
                                    tacticDraw.DrawSavedImages(pictureMap, /*bmFull,*/ seed);
                                    seed.activeShip = null;
                                }
                            }
                        }
                        else if (tacticState.cMap.boxes[seed.select].spaceObject != null)
                        {
                            if (tacticState.cMap.boxes[seed.select].spaceObject.player == seed.activePlayer)
                            {
                                boxDescription.Text = tacticState.cMap.boxes[seed.select].spaceObject.description();
                                seed.activeShip = (Ship)tacticState.cMap.boxes[seed.select].spaceObject;
                                tacticDraw.DrawSavedImages(pictureMap, /*bmFull,*/ seed);
                                tacticDraw.DrawActiveShipFrames(pictureMap, /*bmFull,*/ seed, tacticState);
                                break;
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
                                        ReadShipsCount();
                                    boxDescription.Text = seed.activeShip.description();

                                    // убираем подсветку с корабля, если у него не осталось очков передвижений
                                    if (seed.activeShip.actionsLeft == 0)
                                    {
                                        seed.activeShip = null;
                                    }
                                    else
                                    {
                                        tacticDraw.DrawSavedImages(pictureMap, /*bmFull,*/ seed);
                                        tacticDraw.DrawActiveShipFrames(pictureMap, /*bmFull,*/ seed, tacticState);
                                    }
                                    flag = 0;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            if (seed.activePlayer == 1) seed.activePlayer = 2;
            else seed.activePlayer = 1;
            lblTurn.Text = "Ходит " + seed.activePlayer + "-й игрок";
            seed.activeShip = null;
            tacticDraw.DrawSavedImages(pictureMap, /*bmFull,*/ seed);
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
            ReadShipsCount();
            if (seed.redShipsCount == 0 || seed.blueShipsCount == 0)
                this.Dispose();
            pictureMap.Refresh();
        }

    }
}
