using GalaxyConquest.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using  System.Threading;
using GalaxyConquest.Tactics;

namespace GalaxyConquest.Drawing
{
    class TacticDraw
    {
        public Bitmap bmBackground;
        public Bitmap bmShips;
        public Bitmap bmFull;
        public void New(PictureBox pictureMap, TacticSeed seed, TacticState tacticState)
        {
            bmBackground = DrawBackground(pictureMap, tacticState);
            bmShips = DrawShips(pictureMap, seed, tacticState);
            bmFull = ImageRefrash(bmBackground, bmShips, pictureMap);
        }
        // выводит на экран сетку и задний фон, возвращает эти два слоя склеенными
        public Bitmap ImageRefrash(Bitmap bm, Bitmap ships, PictureBox pictureMap)
        {
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(tmpBitmap);
            Image.FromHbitmap(bm.GetHbitmap());
            Image.FromHbitmap(ships.GetHbitmap());
            g.DrawImage(bm, 0, 0);
            g.DrawImage(ships, 0, 0);
            pictureMap.Image = tmpBitmap;
            return tmpBitmap;
        }
        // отрисовывает рамку вокруг активного корабля и вражеских кораблей в зоне поражения
        public void DrawActiveShipFrames(PictureBox pictureMap, /*Bitmap bmFull,*/ TacticSeed seed, TacticState tacticState)
        {
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmFull);
            Rectangle rect;  // размер части изображения, которую нужно сохранить
            Image oldImage;  // часть изображения, которую нужно сохранить
            rect = new Rectangle(
                tacticState.cMap.boxes[seed.activeShip.boxId].xpoint1,
                tacticState.cMap.boxes[seed.activeShip.boxId].ypoint2,
                tacticState.cMap.boxes[seed.activeShip.boxId].xpoint4 - tacticState.cMap.boxes[seed.activeShip.boxId].xpoint1,
                tacticState.cMap.boxes[seed.activeShip.boxId].ypoint6 - tacticState.cMap.boxes[seed.activeShip.boxId].ypoint2
            );
            oldImage = bmFull.Clone(rect, bmFull.PixelFormat);
            seed.savedImages.Add(new SavedImage(oldImage, tacticState.cMap.boxes[seed.activeShip.boxId].xpoint1, tacticState.cMap.boxes[seed.activeShip.boxId].ypoint2, seed.activeShip));
            g.DrawEllipse(new Pen(Color.Yellow, 2), seed.activeShip.x - 27, seed.activeShip.y - 27, 54, 54);
            for (int shipCount = 0; shipCount < seed.allShips.Count; shipCount++)
            {
                if (seed.allShips[shipCount] != null && seed.allShips[shipCount].player != seed.activePlayer)
                {
                    double x1 = tacticState.cMap.boxes[seed.activeShip.boxId].x;
                    double y1 = tacticState.cMap.boxes[seed.activeShip.boxId].y;
                    double x2 = tacticState.cMap.boxes[seed.allShips[shipCount].boxId].x;
                    double y2 = tacticState.cMap.boxes[seed.allShips[shipCount].boxId].y;
                    double range;
                    range = Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);
                    if ((int)range <= seed.activeShip.equippedWeapon.attackRange)
                    {
                        rect = new Rectangle(
                            tacticState.cMap.boxes[seed.allShips[shipCount].boxId].xpoint1,
                            tacticState.cMap.boxes[seed.allShips[shipCount].boxId].ypoint2,
                            tacticState.cMap.boxes[seed.allShips[shipCount].boxId].xpoint4 - tacticState.cMap.boxes[seed.allShips[shipCount].boxId].xpoint1,
                            tacticState.cMap.boxes[seed.allShips[shipCount].boxId].ypoint6 - tacticState.cMap.boxes[seed.allShips[shipCount].boxId].ypoint2
                        );
                        oldImage = bmFull.Clone(rect, bmFull.PixelFormat);
                        seed.savedImages.Add(new SavedImage(oldImage, tacticState.cMap.boxes[seed.allShips[shipCount].boxId].xpoint1, tacticState.cMap.boxes[seed.allShips[shipCount].boxId].ypoint2, seed.allShips[shipCount]));
                        g.DrawEllipse(new Pen(Color.Red, 2), seed.allShips[shipCount].x - 27, seed.allShips[shipCount].y - 27, 54, 54);
                    }
                }
            }
            pictureMap.Image = bmFull;
            
        }
        // восстанавливает сохраненные куски изображения
        public void DrawSavedImages(PictureBox pictureMap, /*Bitmap bmFull,*/ TacticSeed seed)
        {
            if (seed.savedImages != null)
            {
                for (int i = 0; i < seed.savedImages.Count; i++)
                {
                    if (seed.savedImages[i].spaceObject != null && seed.savedImages[i].spaceObject.currentHealth >= 0)
                    {
                        Graphics g = Graphics.FromImage(bmFull);
                        g.DrawImage(seed.savedImages[i].img, seed.savedImages[i].x, seed.savedImages[i].y);
                    }

                }
                seed.savedImages.Clear();
                pictureMap.Image = bmFull; 
            }
            for (int count = 0; count < seed.allShips.Count; count++)
            {
                seed.allShips[count].statusRefresh(ref bmBackground, ref bmFull);
            }
        }
        // возвращает битмап с задним фоном
        public Bitmap DrawBackground(PictureBox pictureMap, TacticState tacticState)
        {
            Bitmap tmpBitmap;
            tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(tmpBitmap);
            g.DrawImage(Image.FromFile(@"Sprites/background/bg1.jpg"),
                            new Rectangle(0,
                                0,
                                pictureMap.Width,
                                pictureMap.Height));
            Pen boxPen = new Pen(Color.Purple, 1);
            for (int i = 0; i < tacticState.cMap.boxes.Count; i++)
            {
                Point[] myPointArrayHex = {  //точки для отрисовки шестиугольника
                        new Point(tacticState.cMap.boxes[i].xpoint1, tacticState.cMap.boxes[i].ypoint1),
                        new Point(tacticState.cMap.boxes[i].xpoint2, tacticState.cMap.boxes[i].ypoint2),
                        new Point(tacticState.cMap.boxes[i].xpoint3, tacticState.cMap.boxes[i].ypoint3),
                        new Point(tacticState.cMap.boxes[i].xpoint4, tacticState.cMap.boxes[i].ypoint4),
                        new Point(tacticState.cMap.boxes[i].xpoint5, tacticState.cMap.boxes[i].ypoint5),
                        new Point(tacticState.cMap.boxes[i].xpoint6, tacticState.cMap.boxes[i].ypoint6)
                };
                g.DrawPolygon(boxPen, myPointArrayHex);
            }
            return tmpBitmap;
        }
        // возвращает битмап с кораблями и косм. объектами
        public Bitmap DrawShips(PictureBox pictureMap, TacticSeed seed, TacticState tacticState)
        {
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(tmpBitmap);
            for (int i = 0; i < seed.allShips.Count; i++)
            {
                //g.DrawImage(seed.allShips[i].objectImg,);

                g.DrawImage(seed.allShips[i].objectImg,
                           new Rectangle(seed.allShips[i].x - seed.allShips[i].objectImg.Width / 2,
                               seed.allShips[i].y - seed.allShips[i].objectImg.Height / 2,
                               seed.allShips[i].objectImg.Width,
                               seed.allShips[i].objectImg.Height));
                g.DrawString(seed.allShips[i].actionsLeft.ToString(), new Font("Arial", 8.0F), Brushes.Blue, new PointF(seed.allShips[i].x + 10, seed.allShips[i].y + 26));
                g.DrawString(seed.allShips[i].currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(seed.allShips[i].x - 20, seed.allShips[i].y + 26));
            }
            for (int i = 0; i < tacticState.objectManager.meteors.Count; i++)
            {
                g.DrawImage(tacticState.objectManager.meteors[i].objectImg,
                    new Rectangle(tacticState.objectManager.meteors[i].x - seed.boxWidth / 6,
                        tacticState.objectManager.meteors[i].y - seed.boxHeight / 6,
                        seed.boxWidth / 3,
                        seed.boxHeight / 3));
                g.DrawString(tacticState.objectManager.meteors[i].currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(tacticState.cMap.boxes[tacticState.objectManager.meteors[i].boxId].xpoint1 + 20, tacticState.cMap.boxes[tacticState.objectManager.meteors[i].boxId].ypoint1 - 25));
            }
            return tmpBitmap;
        }

        public void doShipRotate(double angle, int reset, bool saveImage, PictureBox pictureMap, TacticSeed seed)
        {
            Graphics g;// = Graphics.FromImage(bmFull);
            Rectangle rect = new Rectangle(
                seed.activeShip.x - seed.boxWidth / 2,
                seed.activeShip.y - seed.boxHeight / 2,
                seed.boxWidth,
                seed.boxHeight);
            Image bg = bmBackground.Clone(rect, bmBackground.PixelFormat);
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(tmpBitmap);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 255, 255)), new Rectangle(0, 0, pictureMap.Width, pictureMap.Height));
            g = Graphics.FromImage(bmFull);
            int xold = seed.activeShip.x;
            int yold = seed.activeShip.y;
            int sign;
            if (angle >= 0) sign = 1;
            else sign = -1;
            for (int count = 0; count < (int)Math.Abs(angle); count += 5)
            {
                g.DrawImage(bg, rect);
                g.TranslateTransform(xold, yold);
                g.RotateTransform(sign * count * reset);
                g.DrawImage(seed.activeShip.objectImg, new Rectangle(
                    0 - seed.activeShip.objectImg.Width / 2,
                    0 - seed.activeShip.objectImg.Height / 2,
                    seed.activeShip.objectImg.Width,
                    seed.activeShip.objectImg.Height));
                
                if (count + 5 >= (int)Math.Abs(angle))
                {
                    g = Graphics.FromImage(tmpBitmap);
                    g.TranslateTransform(xold, yold);
                    g.RotateTransform(sign * count * reset);
                    g.DrawImage(seed.activeShip.objectImg, new Rectangle(
                            0 - seed.activeShip.objectImg.Width / 2,
                            0 - seed.activeShip.objectImg.Height / 2,
                            seed.activeShip.objectImg.Width,
                            seed.activeShip.objectImg.Height));
                    g = Graphics.FromImage(bmFull);
                }
                //Thread.Sleep(15);
                g.ResetTransform();
            }
            if (seed.activeShip.player == 1) sign = 1;
            else sign = -1;
            seed.activeShip.weaponPointX = (int)Math.Round((double)seed.activeShip.weaponR * sign * Math.Cos(angle * reset * Math.PI / 180));
            seed.activeShip.weaponPointY = (int)Math.Round((double)seed.activeShip.weaponR * sign * Math.Sin(angle * reset * Math.PI / 180));
            if (reset == 1) seed.activeShip.objectImg = tmpBitmap.Clone(rect, tmpBitmap.PixelFormat);
            else seed.activeShip.objectImg = seed.activeShip.baseObjectImg;
            if (!saveImage) g.DrawImage(bg, rect);
        }

        public void Move(PictureBox pictureMap, TacticSeed seed, TacticState tacticState, List<Box> completeBoxWay, int x1, int x2, int y1, int y2)
        {
            double rotateAngle;
            int range, dx;
            Graphics g = Graphics.FromImage(bmFull);
            int actualDeltaX0, actualDeltaY0, actualDeltaX1, actualDeltaY1;
            actualDeltaX0 = actualDeltaY0 = 0;
            int savedAngle = 0;
            DrawSavedImages(pictureMap, /*bmFull,*/ seed);
            for (int cnt = 0; cnt < completeBoxWay.Count; cnt++)
            {
                if (seed.activeShip == null) break;
                rotateAngle = FindWay.AttackAngleSearch(tacticState.cMap.boxes[completeBoxWay[cnt].id].x,
                    tacticState.cMap.boxes[completeBoxWay[cnt].id].y, seed, tacticState);
                x1 = tacticState.cMap.boxes[seed.activeShip.boxId].xcenter;
                y1 = tacticState.cMap.boxes[seed.activeShip.boxId].ycenter;
                x2 = tacticState.cMap.boxes[completeBoxWay[cnt].id].xcenter;
                y2 = tacticState.cMap.boxes[completeBoxWay[cnt].id].ycenter;
                int stepLineRange = (int) Math.Sqrt((x2 - x1)*(x2 - x1) + ((y2 - y1)*(y2 - y1))*0.35);
                range = (int) Math.Sqrt((x2 - x1)*(x2 - x1) + (y2 - y1)*(y2 - y1));
                dx = range/15;
                int deltax;
                int deltay;
                deltax = (x2 - x1)/15;
                deltay = (y2 - y1)/15;
                Image bg;
                Rectangle rect;
                int halfBoxWidth = (tacticState.cMap.boxes[0].xpoint3 - tacticState.cMap.boxes[0].xpoint2)/2;
                int halfBoxHeight = (tacticState.cMap.boxes[0].ypoint6 - tacticState.cMap.boxes[0].ypoint2)/2;
                //закрашиваем выделенный корабль
                rect = new Rectangle(
                    seed.activeShip.x - halfBoxWidth,
                    seed.activeShip.y - halfBoxHeight,
                    halfBoxWidth + halfBoxWidth,
                    halfBoxHeight + halfBoxHeight
                    );
                bg = bmBackground.Clone(rect, bmBackground.PixelFormat);
                g.DrawImage(bg, seed.activeShip.x - halfBoxWidth, seed.activeShip.y - halfBoxHeight);
                
                actualDeltaX1 = completeBoxWay[cnt].x - tacticState.cMap.boxes[seed.activeShip.boxId].x;
                actualDeltaY1 = completeBoxWay[cnt].y - tacticState.cMap.boxes[seed.activeShip.boxId].y;
                if (cnt > 0)
                {
                    if (actualDeltaX1 != actualDeltaX0 || actualDeltaY1 != actualDeltaY0)
                    {
                        if (savedAngle != 0)
                        {
                            doShipRotate(savedAngle, -1, false, pictureMap, seed);
                        }
                        doShipRotate(rotateAngle, 1, false, pictureMap, seed);
                        savedAngle = (int) rotateAngle;
                    }
                }
                else
                {
                    doShipRotate(rotateAngle, 1, false, pictureMap, seed);
                    savedAngle = (int) rotateAngle;
                }

                for (int count1 = 0; count1 < range - 10; count1 += dx)
                {
                    seed.activeShip.x += deltax;
                    seed.activeShip.y += deltay;
                    // запоминаем кусок картинки, на которой уже нет активного корабля
                    rect = new Rectangle(
                        seed.activeShip.x - halfBoxWidth,
                        seed.activeShip.y - halfBoxHeight,
                        halfBoxWidth + halfBoxWidth,
                        halfBoxHeight + halfBoxHeight
                        );
                    bg = bmFull.Clone(rect, bmFull.PixelFormat);
                    // рисуем корабль по новым координатам
                    g.DrawImage(seed.activeShip.objectImg,
                        new Rectangle(seed.activeShip.x - seed.activeShip.objectImg.Width/2,
                            seed.activeShip.y - seed.activeShip.objectImg.Height/2,
                            seed.activeShip.objectImg.Width,
                            seed.activeShip.objectImg.Height)
                        );
                    pictureMap.Image = bmFull;
                    Screen_Combat.UpdateDrawing();
                    //Thread.Sleep(5);
                    g.DrawImage(bg, seed.activeShip.x - halfBoxWidth, seed.activeShip.y - halfBoxHeight);
                }
                seed.activeShip.moveShip(tacticState.cMap, seed.activeShip.boxId, completeBoxWay[cnt].id, 1);
                g.DrawImage(seed.activeShip.objectImg,
                    new Rectangle(seed.activeShip.x - seed.activeShip.objectImg.Width/2,
                        seed.activeShip.y - seed.activeShip.objectImg.Height/2,
                        seed.activeShip.objectImg.Width,
                        seed.activeShip.objectImg.Height));
                if (cnt == completeBoxWay.Count - 1)
                {
                    doShipRotate(rotateAngle, -1, true, pictureMap, seed);
                }
                actualDeltaX0 = actualDeltaX1;
                actualDeltaY0 = actualDeltaY1;
            }
        }

        public int Attack(PictureBox pictureMap, TacticSeed seed, TacticState tacticState, Bitmap combatBitmap, System.Media.SoundPlayer player)
        {
            int ret = 0;
            if (seed.activeShip.actionsLeft >= seed.activeShip.equippedWeapon.energyСonsumption
                && seed.activeShip.equippedWeapon.shotsleft > 0) // если у корабля остались очки действий
            {
                double angle, targetx, targety;
                targetx = tacticState.cMap.boxes[seed.select].x;
                targety = tacticState.cMap.boxes[seed.select].y;
                angle = FindWay.AttackAngleSearch(targetx, targety, seed, tacticState);
                doShipRotate(angle, 1, true, pictureMap, seed);
                // отрисовка атаки
                //Thread.Sleep(150);
                ret = seed.activeShip.attack(tacticState.cMap, tacticState.cMap.boxes[seed.select].id, ref combatBitmap,
                    player, ref pictureMap, ref bmBackground, ref bmFull);
                // возвращаем корабль в исходное положение
                doShipRotate(angle, -1, false, pictureMap, seed);
            }
            return ret;
        }

        public void DrawMeteor(TacticSeed seed, TacticState tacticState, Meteor newMeteor)
        {
            Graphics g = Graphics.FromImage(bmFull);
            g.DrawImage(newMeteor.objectImg,
                new Rectangle(newMeteor.x - seed.boxWidth / 6,
                    newMeteor.y - seed.boxHeight / 6,
                    seed.boxWidth / 3,
                    seed.boxHeight / 3));
            g.DrawString(newMeteor.currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(tacticState.cMap.boxes[newMeteor.boxId].xpoint1 + 20, tacticState.cMap.boxes[newMeteor.boxId].ypoint1 - 25));
        }
    }
}
