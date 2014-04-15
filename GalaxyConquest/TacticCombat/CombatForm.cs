using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace GalaxyConquest.Tactics
{
    public partial class CombatForm : Form
    {
        public Bitmap combatBitmap;

        combatMap cMap = new combatMap(7, 5);  // создаем поле боя с указанной размерностью
        ObjectManager objectManager = new ObjectManager();
        int select = -1; 
        int activePlayer = 1; // ход 1-ого или 2-ого игрока
        Ship activeShip = null; // выделенное судно
        List<Ship> allShips = new List<Ship>();
        List<Meteor> meteors = new List<Meteor>();
        int blueShipsCount;
        int redShipsCount;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        Bitmap bmBackground;
        Bitmap bmShips;
        Bitmap bmFull;

        List<SavedImage> savedImages = new List<SavedImage>();


        public int boxWidth;
        public int boxHeight;
        
        public CombatForm(Fleet left, Fleet right)
        {
            allShips.Clear();

            allShips.AddRange(left.ships);
            allShips.AddRange(right.ships);

            boxWidth = cMap.boxes[0].xpoint3 - cMap.boxes[0].xpoint2;
            boxHeight = cMap.boxes[0].ypoint6 - cMap.boxes[0].ypoint2;

            objectManager.meteorCreate(cMap);

            // расставляем корабли по полю, синие - слева, красные - справа
            for (int count = 0; count < allShips.Count; count++ )
            {
                if (allShips[count].currentHealth > 0)
                {
                    allShips[count].refill();
                    allShips[count].placeShip(ref cMap);
                }
            }

            InitializeComponent();

            shipsCount();

            bmBackground = drawBackground();
            bmShips = drawShips();
            bmFull = imageRefrash(bmBackground, bmShips);

        }

        public void shipsCount()
        {
            blueShipsCount = 0;
            redShipsCount = 0;
            for (int count = 0; count < allShips.Count; count++)
            {
                if (allShips[count] == null) continue;
                if (allShips[count].currentHealth <= 0)
                {
                    allShips.Remove(allShips[count]);
                    count--;
                }
                else
                {
                    if (allShips[count].player == 1)
                        blueShipsCount++;
                    else if (allShips[count].player != 1)
                        redShipsCount++;
                }
            }
            txtBlueShips.Text = "" + blueShipsCount;
            txtRedShips.Text = "" + redShipsCount;
        }


        // ************************************
        //     НОВАЯ ОТРИСОВКА 
        // ************************************

        // выводит на экран сетку и задний фон, возвращает эти два слоя склеенными
        public Bitmap imageRefrash(Bitmap bm, Bitmap ships)
        {
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(tmpBitmap);

            Image.FromHbitmap(bm.GetHbitmap());
            Image.FromHbitmap(ships.GetHbitmap());

            g.DrawImage(bm, 0, 0);
            g.DrawImage(ships, 0, 0);

            pictureMap.Image = tmpBitmap;
            pictureMap.Refresh();

            return tmpBitmap;
        }

        // отрисовывает рамку вокруг активного корабля и вражеских кораблей в зоне поражения
        public void drawActiveShipFrames()
        {
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmFull);

            Rectangle rect;  // размер части изображения, которую нужно сохранить
            Image oldImage;  // часть изображения, которую нужно сохранить

            rect = new Rectangle(
                cMap.boxes[activeShip.boxId].xpoint1,
                cMap.boxes[activeShip.boxId].ypoint2, 
                cMap.boxes[activeShip.boxId].xpoint4 - cMap.boxes[activeShip.boxId].xpoint1, 
                cMap.boxes[activeShip.boxId].ypoint6 - cMap.boxes[activeShip.boxId].ypoint2
            );

            oldImage = bmFull.Clone(rect, bmFull.PixelFormat);
            
            savedImages.Add(new SavedImage(oldImage, cMap.boxes[activeShip.boxId].xpoint1, cMap.boxes[activeShip.boxId].ypoint2, activeShip));

            g.DrawEllipse(new Pen(Color.Yellow, 2), activeShip.x - 27, activeShip.y - 27, 54, 54);

            for (int shipCount = 0; shipCount < allShips.Count; shipCount++ )
            {
                if(allShips[shipCount] != null && allShips[shipCount].player != activePlayer)
                {
                    double x1 = cMap.boxes[activeShip.boxId].x;
                    double y1 = cMap.boxes[activeShip.boxId].y;
                    double x2 = cMap.boxes[allShips[shipCount].boxId].x;
                    double y2 = cMap.boxes[allShips[shipCount].boxId].y;
                    double range;
                    range = Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);

                    if ((int)range <= activeShip.equippedWeapon.attackRange)
                    {
                        rect = new Rectangle(
                            cMap.boxes[allShips[shipCount].boxId].xpoint1,
                            cMap.boxes[allShips[shipCount].boxId].ypoint2,
                            cMap.boxes[allShips[shipCount].boxId].xpoint4 - cMap.boxes[allShips[shipCount].boxId].xpoint1,
                            cMap.boxes[allShips[shipCount].boxId].ypoint6 - cMap.boxes[allShips[shipCount].boxId].ypoint2
                        );

                        oldImage = bmFull.Clone(rect, bmFull.PixelFormat);
                        savedImages.Add(new SavedImage(oldImage, cMap.boxes[allShips[shipCount].boxId].xpoint1, cMap.boxes[allShips[shipCount].boxId].ypoint2, allShips[shipCount]));

                        g.DrawEllipse(new Pen(Color.Red, 2), allShips[shipCount].x - 27, allShips[shipCount].y - 27, 54, 54);
                    }
                }
            }

            pictureMap.Image = bmFull;
            pictureMap.Refresh();

        }

        // восстанавливает сохраненные куски изображения
        public void drawSavedImages()
        {
            if(savedImages != null)
            {
                for (int i = 0; i < savedImages.Count; i++)
                {
                    if (savedImages[i].spaceObject != null && savedImages[i].spaceObject.currentHealth >= 0)
                    {
                        Graphics g = Graphics.FromImage(bmFull);
                        g.DrawImage(savedImages[i].img, savedImages[i].x, savedImages[i].y);
                    }
                   
                }
                savedImages.Clear();
                pictureMap.Image = bmFull;
                pictureMap.Refresh();
            }
        }

        // возвращает битмап с задним фоном
        public Bitmap drawBackground()
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

            for (int i = 0; i < cMap.boxes.Count; i++)
            {
                Point[] myPointArrayHex = {  //точки для отрисовки шестиугольника
                        new Point(cMap.boxes[i].xpoint1, cMap.boxes[i].ypoint1),
                        new Point(cMap.boxes[i].xpoint2, cMap.boxes[i].ypoint2),
                        new Point(cMap.boxes[i].xpoint3, cMap.boxes[i].ypoint3),
                        new Point(cMap.boxes[i].xpoint4, cMap.boxes[i].ypoint4),
                        new Point(cMap.boxes[i].xpoint5, cMap.boxes[i].ypoint5),
                        new Point(cMap.boxes[i].xpoint6, cMap.boxes[i].ypoint6)
                };

                g.DrawPolygon(boxPen, myPointArrayHex);
            }

            return tmpBitmap;
        }

        // возвращает битмап с кораблями и косм. объектами
        public Bitmap drawShips()
        {
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(tmpBitmap);

            for (int i = 0; i < allShips.Count; i++ )
            {
                g.DrawImage(allShips[i].objectImg,
                           new Rectangle(allShips[i].x - allShips[i].objectImg.Width / 2,
                               allShips[i].y - allShips[i].objectImg.Height / 2,
                               allShips[i].objectImg.Width,
                               allShips[i].objectImg.Height));

                g.DrawString(allShips[i].actionsLeft.ToString(), new Font("Arial", 8.0F), Brushes.Blue, new PointF(allShips[i].x + 10, allShips[i].y + 26));
                g.DrawString(allShips[i].currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(allShips[i].x - 20, allShips[i].y + 26));               
            }

            for (int i = 0; i < objectManager.meteors.Count; i++ )
            {
                g.DrawImage(objectManager.meteors[i].objectImg,
                    new Rectangle(objectManager.meteors[i].x - boxWidth / 6,
                        objectManager.meteors[i].y - boxHeight / 6,
                        boxWidth/3, 
                        boxHeight/3));
                g.DrawString(objectManager.meteors[i].currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(cMap.boxes[objectManager.meteors[i].boxId].xpoint1 + 20, cMap.boxes[objectManager.meteors[i].boxId].ypoint1 - 25));
            }

            return tmpBitmap;
        }


        /*
        public void Draw()
        {
            combatBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(combatBitmap);
            //g.FillRectangle(Brushes.Black, 0, 0, combatBitmap.Width, combatBitmap.Height); //рисуем фон окна

            Pen generalPen;
            Pen redPen = new Pen(Color.Red, 3);
            Pen grayPen = new Pen(Color.Gray, 3);
            Pen PurplePen = new Pen(Color.Purple);
            Pen activeShipAriaPen = new Pen(Color.Purple, 5);

            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush grayBrush = new SolidBrush(Color.Gray);
            SolidBrush activeShipBrush = new SolidBrush(Color.DarkGreen);
            SolidBrush mediumPurpleBrush = new SolidBrush(Color.MediumPurple);
            SolidBrush brush;

            for (int i = 0; i < cMap.boxes.Count; i++)
            {
                generalPen = PurplePen;
                Point[] myPointArrayHex = {  //точки для отрисовки шестиугольника
                        new Point(cMap.boxes[i].xpoint1, cMap.boxes[i].ypoint1),
                        new Point(cMap.boxes[i].xpoint2, cMap.boxes[i].ypoint2),
                        new Point(cMap.boxes[i].xpoint3, cMap.boxes[i].ypoint3),
                        new Point(cMap.boxes[i].xpoint4, cMap.boxes[i].ypoint4),
                        new Point(cMap.boxes[i].xpoint5, cMap.boxes[i].ypoint5),
                        new Point(cMap.boxes[i].xpoint6, cMap.boxes[i].ypoint6)
                };
                // Если выделили судно с очками передвижения, подсвечиваем его и соседние клетки

                if (activeShip != null)
                {
                    for (int count = 0; count < allShips.Count; count++)
                    {
                        if (allShips[count].player != activePlayer && allShips[count].boxId >= 0)
                        {
                            if (allShips[count].player == 0)
                            {
                                // рисовать ли рамку вокруг нейтральных объектов
                                // в зоне досягаемости? пока решили что нет,
                                // но заглушка осталась
                                //int x1;
                            }
                            else
                            {
                                double x1 = cMap.boxes[activeShip.boxId].x;
                                double y1 = cMap.boxes[activeShip.boxId].y;
                                double x2 = cMap.boxes[allShips[count].boxId].x;
                                double y2 = cMap.boxes[allShips[count].boxId].y;
                                double range;
                                range = Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);

                                if ((int)range <= activeShip.equippedWeapon.attackRange)
                                {
                                    Point[] myPointArrayHex99 = {  //точки для отрисовки шестиугольника
                                        new Point(cMap.boxes[allShips[count].boxId].xpoint1, cMap.boxes[allShips[count].boxId].ypoint1),
                                        new Point(cMap.boxes[allShips[count].boxId].xpoint2, cMap.boxes[allShips[count].boxId].ypoint2),
                                        new Point(cMap.boxes[allShips[count].boxId].xpoint3, cMap.boxes[allShips[count].boxId].ypoint3),
                                        new Point(cMap.boxes[allShips[count].boxId].xpoint4, cMap.boxes[allShips[count].boxId].ypoint4),
                                        new Point(cMap.boxes[allShips[count].boxId].xpoint5, cMap.boxes[allShips[count].boxId].ypoint5),
                                        new Point(cMap.boxes[allShips[count].boxId].xpoint6, cMap.boxes[allShips[count].boxId].ypoint6)
                                     };
                                    g.DrawPolygon(redPen, myPointArrayHex99);
                                }
                            }
                        }
                    }
                }

                g.DrawPolygon(PurplePen, myPointArrayHex);

                if (activeShip != null && activeShip.boxId == i)
                {
                    g.DrawPolygon(activeShipAriaPen, myPointArrayHex);

                }

                if(cMap.boxes[i].spaceObject != null && cMap.boxes[i].spaceObject.objectType == Constants.METEOR)
                {
                    brush = grayBrush;
                    cMap.boxes[i].spaceObject.drawSpaceShit(ref cMap, ref combatBitmap);
                }
                else if (cMap.boxes[i].spaceObject != null && cMap.boxes[i].spaceObject.objectImg != null)
                {
                     if (cMap.boxes[i].spaceObject.player == 1)
                        brush = blueBrush;
                    else if (cMap.boxes[i].spaceObject.player == 2)
                        brush = redBrush;
                    else 

                    
                    
                    //qwerty

                    if (cMap.boxes[i].spaceObject.objectImg.Width >= boxWidth)
                    {
                        g.DrawImage(cMap.boxes[i].spaceObject.objectImg,
                            new Rectangle(cMap.boxes[i].spaceObject.x - boxWidth/2,
                                cMap.boxes[i].spaceObject.y - boxHeight/2,
                                boxWidth, boxHeight));
                    }
                    else
                    {
                        //g.DrawImage(cMap.boxes[i].spaceObject.objectImg,
                            //new Rectangle(cMap.boxes[i].xcenter - cMap.boxes[i].spaceObject.objectImg.Width/2, 
                            //    cMap.boxes[i].ycenter cMap.boxes[i].spaceObject.objectImg.Height/2, 
                            //    boxWidth, boxHeight));
                        g.DrawImage(cMap.boxes[i].spaceObject.objectImg,
                            new Rectangle(cMap.boxes[i].spaceObject.x - cMap.boxes[i].spaceObject.objectImg.Width / 2,
                                cMap.boxes[i].spaceObject.y - cMap.boxes[i].spaceObject.objectImg.Height / 2,
                                cMap.boxes[i].spaceObject.objectImg.Width,
                                cMap.boxes[i].spaceObject.objectImg.Height));

                        cMap.boxes[i].spaceObject.statusRefresh(ref bmBackground, ref bmFull);
                    }

                }

                //image
                //g.DrawImage

                //g.DrawString(cMap.boxes[i].id.ToString(), new Font("Arial", 8.0F), Brushes.Green, new PointF(cMap.boxes[i].xpoint1 + 20, cMap.boxes[i].ypoint1 + 10));
                //g.DrawString(cMap.boxes[i].x.ToString(), new Font("Arial", 8.0F), Brushes.Green, new PointF(cMap.boxes[i].xpoint1 + 10, cMap.boxes[i].ypoint1 + 10));
                //g.DrawString(cMap.boxes[i].y.ToString(), new Font("Arial", 8.0F), Brushes.Green, new PointF(cMap.boxes[i].xpoint1 + 40, cMap.boxes[i].ypoint1 + 10));

            }
            //Image image = Image.FromFile("Cruiser2.png");
            //g.DrawImage(image, new Rectangle(100, 100, 60, 40));
            
                pictureMap.Image = combatBitmap;
                pictureMap.Refresh();
            
        }
        */
        public double attackAngleSearch(double targetx, double targety)
        {
            double angle = 0;
            double shipx, shipy;

            if (activeShip != null)
            {
                shipx = cMap.boxes[activeShip.boxId].x;  // координаты выделенного корабля
                shipy = cMap.boxes[activeShip.boxId].y;

                if (shipx == targetx) // избегаем деления на ноль
                {
                    if (shipy > targety)
                    {
                        angle = -90;
                    }
                    else
                    {
                        angle = 90;
                    }
                    if (activePlayer != 1) angle = -angle;

                }
                else // находим угол, на который нужно повернуть корабль (если он не равен 90 градусов)
                {
                    angle = Math.Atan((targety - shipy) / (targetx - shipx)) * 180 / Math.PI;
                }
                // дальше идет коррекция, не пытайся разобраться как это работает, просто оставь как есть
                if (activePlayer == 1)
                {
                    if (shipy == targety && shipx > targetx)
                    {
                        angle = 180;
                    }
                    else if (shipx > targetx && shipy < targety)
                    {
                        angle += 180;
                    }
                    else if (shipx > targetx && shipy > targety)
                    {
                        angle = angle - 180;
                    }
                }
                else if (activePlayer != 1)
                {
                    if (shipy == targety && shipx < targetx)
                    {
                        angle = 180;
                    }
                    else if (shipx < targetx && shipy < targety)
                    {
                        angle -= 180;
                    }
                    else if (shipx < targetx && shipy > targety)
                    {
                        angle += 180;
                    }
                }

                //if (angle > 150) angle = 150;
                //else if (angle < -150) angle = -150;
            }
            return angle;
        }

        
        public void doShipRotate(double angle, int reset, bool saveImage)
        {
            Graphics g;// = Graphics.FromImage(bmFull);
            Rectangle rect = new Rectangle(
                activeShip.x - boxWidth/2,
                activeShip.y - boxHeight/2,
                boxWidth,
                boxHeight);
            Image bg = bmBackground.Clone(rect, bmBackground.PixelFormat);
            Bitmap tmpBitmap = new Bitmap(pictureMap.Width, pictureMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            g = Graphics.FromImage(tmpBitmap);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0,255,255,255)), new Rectangle(0,0,pictureMap.Width,pictureMap.Height));
            g = Graphics.FromImage(bmFull);

            int xold = activeShip.x;
            int yold = activeShip.y;

            int sign;

            if (angle >= 0) sign = 1;
            else sign = -1;

            for (int count = 0; count < (int)Math.Abs(angle); count += 5)
            {
                g.DrawImage(bg, rect);

                g.TranslateTransform(xold, yold);
                g.RotateTransform(sign * count * reset);

                g.DrawImage(activeShip.objectImg, new Rectangle(
                    0 - activeShip.objectImg.Width/2,
                    0 - activeShip.objectImg.Height/2,
                    activeShip.objectImg.Width,
                    activeShip.objectImg.Height));

                pictureMap.Refresh();

                if(count + 5 >= (int)Math.Abs(angle))
                {
                    g = Graphics.FromImage(tmpBitmap);
                    g.TranslateTransform(xold, yold);
                    g.RotateTransform(sign * count * reset);
                   
                    g.DrawImage(activeShip.objectImg, new Rectangle(
                            0 - activeShip.objectImg.Width / 2,
                            0 - activeShip.objectImg.Height / 2,
                            activeShip.objectImg.Width,
                            activeShip.objectImg.Height));
                    
                    g = Graphics.FromImage(bmFull);

                    

                }
                //angle = angle * Math.PI / 180;
                
                Thread.Sleep(15);
                g.ResetTransform();
            }

            if (activeShip.player == 1) sign = 1;
            else sign = - 1;

            activeShip.weaponPointX = (int)Math.Round((double)activeShip.weaponR * sign * Math.Cos(angle * reset * Math.PI / 180));
            activeShip.weaponPointY = (int)Math.Round((double)activeShip.weaponR * sign * Math.Sin(angle * reset * Math.PI / 180));

            if (reset == 1) activeShip.objectImg = tmpBitmap.Clone(rect, tmpBitmap.PixelFormat);
            else activeShip.objectImg = activeShip.baseObjectImg;
            if(!saveImage) g.DrawImage(bg, rect);
        }
        public void resetShipRotate(double angle)
        {
            for (int count = 1; count < (int)Math.Abs(angle); count += 5)
            {
                
                activeShip.shipRotate(-5 * (int)(angle / Math.Abs(angle)));
                //Draw();
            }
        }
        
        public int findDirection(int x1, int x2, int y1, int y2)
        {
            int direction = 0;
            if (x2 == x1 && y2 - y1 < 0) direction = Constants.MOVE_TOP;
            else if (x2 - x1 > 0 && y2 - y1 <= 0) direction = Constants.MOVE_RIGHT_TOP;
            else if (x2 - x1 > 0 && y2 - y1 > 0) direction = Constants.MOVE_RIGHT_BOTTOM;
            else if (x2 == x1 && y2 - y1 > 0) direction = Constants.MOVE_BOTTOM;
            else if (x2 - x1 < 0 && y2 - y1 > 0) direction = Constants.MOVE_LEFT_BOTTOM;
            else if (x2 - x1 < 0 && y2 - y1 <= 0) direction = Constants.MOVE_LEFT_TOP;
            return direction;
        }

        public void findPriority(int direction, ref List<int> priority)
        {
            //if (priority != null) priority.RemoveRange(0, 6);
            switch (direction)
            {
                case Constants.MOVE_TOP:
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_BOTTOM);
                    break;
                case Constants.MOVE_RIGHT_TOP:
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    break;
                case Constants.MOVE_RIGHT_BOTTOM:
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    break;
                case Constants.MOVE_BOTTOM:
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_TOP);
                    break;
                case Constants.MOVE_LEFT_BOTTOM:
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    break;
                case Constants.MOVE_LEFT_TOP:
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    break;
            }
        }

        public Box getNextWaybox(Box currentBox, Box baseBox, ref List<int> priority, ref List<Box> previousBoxes)
        {
            Box nextWaybox = null;
            int j;
            for (int i = 0; i < 6; i++ )
            {
                switch (priority[i])
                {
                    case Constants.MOVE_TOP:
                        if (currentBox.y <= 1) continue;
                        nextWaybox = cMap.boxes[currentBox.id - 1];
                        break;
                    case Constants.MOVE_RIGHT_TOP:
                        if (currentBox.y == 0 || currentBox.x == cMap.width - 1) continue;
                        nextWaybox = cMap.getBoxByCoords(currentBox.x + 1, currentBox.y -1);
                        break;
                    case Constants.MOVE_RIGHT_BOTTOM:
                        if (currentBox.x == cMap.width - 1 || currentBox.y == cMap.height * 2 - 1) continue;
                        nextWaybox = cMap.getBoxByCoords(currentBox.x + 1, currentBox.y + 1);
                        break;
                    case Constants.MOVE_BOTTOM:
                        if (currentBox.y >= cMap.height * 2 - 2) continue;
                        nextWaybox = cMap.boxes[currentBox.id + 1];
                        break;
                    case Constants.MOVE_LEFT_BOTTOM:
                        if (currentBox.x == 0 || currentBox.y == cMap.height * 2 - 1) continue;
                        nextWaybox = cMap.getBoxByCoords(currentBox.x - 1, currentBox.y + 1);
                        break;
                    case Constants.MOVE_LEFT_TOP:
                        if (currentBox.x == 0 || currentBox.y == 0) continue;
                        nextWaybox = cMap.getBoxByCoords(currentBox.x - 1, currentBox.y - 1);
                        break;
                }
                //if (nextWaybox != null && nextWaybox.id != previousBox.id && nextWaybox.spaceObject == null)
                if (nextWaybox != null && nextWaybox.id != baseBox.id && nextWaybox.spaceObject == null)
                {
                    for(j = 0; j < previousBoxes.Count; j++)
                    {
                        if(nextWaybox.id == previousBoxes[j].id)break;
                    }
                    if(j == previousBoxes.Count)
                    {
                        break;
                    }
                }
            }
            if (nextWaybox.spaceObject != null) nextWaybox = null;
            return nextWaybox;
        }

        public int getBoxway(Box currentBox, Box baseBox, Box targetBox, ref List<Box> boxWay, int maxLength, int k)
        {
            if (maxLength > 0
                && (int)Math.Sqrt((targetBox.x - currentBox.x) * (targetBox.x - currentBox.x) + ((targetBox.y - currentBox.y) * (targetBox.y - currentBox.y)) * 0.35) <= maxLength)
            {
                //boxDescription.Text = "" + currentBox.id;
                int currentDirection = findDirection(currentBox.x, targetBox.x, currentBox.y, targetBox.y);

                if (k != 0)
                {
                    currentDirection = (currentDirection + k)%6;
                    if (currentDirection == 0) currentDirection = 6;
                }

                List<int> currentPriority = new List<int>();
                findPriority(currentDirection, ref currentPriority);
                Box nextWaybox = getNextWaybox(currentBox, baseBox, ref currentPriority, ref boxWay);

                if (nextWaybox == null) return -1;

                boxWay.Add(nextWaybox);
                if (nextWaybox.id == targetBox.id)
                {
                    return 0;
                }
                else
                {
                    if (getBoxway(nextWaybox, baseBox, targetBox, ref boxWay, maxLength - 1, 0) == -1)
                    {
                        int i = 1;
                        for (i = 1; i < 6; i++)
                        {
                            if (getBoxway(nextWaybox, baseBox, targetBox, ref boxWay, maxLength - 1, i) == -1)
                            {
                                continue;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        boxWay.Remove(boxWay[boxWay.Count - 1]);
                        //txtRedShips.Text = txtRedShips.Text + "\nоп\n";
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                
                return -1;
            }
        }

        private void pictureMap_MouseClick(object sender, MouseEventArgs e)
        {

            for (int i = 0; i < cMap.boxes.Count; i++)
            {

                if ((e.X > cMap.boxes[i].xpoint2) &&
                    (e.X < cMap.boxes[i].xpoint3) &&
                    (e.Y > cMap.boxes[i].ypoint2) &&
                    (e.Y < cMap.boxes[i].ypoint6))
                {
                    select = i;

                    if (activeShip == null && cMap.boxes[select].spaceObject != null)
                    {
                        if (cMap.boxes[select].spaceObject != null)
                        {
                            if (activePlayer == cMap.boxes[select].spaceObject.player)
                            {
                                // отрисовываем рамку вокруг активного корабля
                                boxDescription.Text = cMap.boxes[select].spaceObject.description();
                                activeShip = (Ship)cMap.boxes[select].spaceObject;

                                drawSavedImages();
                                drawActiveShipFrames();

                            }
                            else
                            {
                                boxDescription.Text = cMap.boxes[i].spaceObject.description();
                            }
                        }
                    }


                // Если до этого ткнули по дружественному судну
                    else if (activeShip != null)
                    {

                        // если выбранная клетка пуста - определяем возможность перемещения 
                        
                        if (activeShip.actionsLeft > 0 && cMap.boxes[select].spaceObject == null)
                        {
                            int flag = 0;
                            int a = activeShip.boxId;
                            int x1, x2, y1, y2;

                            x1 = cMap.boxes[a].x;
                            x2 = cMap.boxes[select].x;
                            y1 = cMap.boxes[a].y;
                            y2 = cMap.boxes[select].y;

                            int lineRange = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);
                            List<Box> completeBoxWay = new List<Box>();

                            if (lineRange <= activeShip.actionsLeft)
                            {
                                // запоминаю начальную клетку
                                Box baseBox = cMap.boxes[a];
                                // определяю направление, в котором находится целевая клетка
                                int direction = findDirection(x1,x2,y1,y2);
                                // определяю приоритет
                                List<int> priority = new List<int>();
                                findPriority(direction, ref priority);

                                for (int k = 0; k < 6; k++)
                                {
                                    if( getBoxway(baseBox, baseBox, cMap.boxes[select], ref completeBoxWay, activeShip.actionsLeft, k) == 0)break;
                                }

                                if (completeBoxWay.Count > 0) flag = 1;
                            }
                            
                            if (flag == 1)
                            {
                                double rotateAngle;
                                int range, dx;
                                Graphics g = Graphics.FromImage(bmFull);

                                drawSavedImages();

                                for (int cnt = 0; cnt < completeBoxWay.Count; cnt++)
                                {
                                    if (activeShip == null) break;
                                    rotateAngle = attackAngleSearch(cMap.boxes[completeBoxWay[cnt].id].x, cMap.boxes[completeBoxWay[cnt].id].y);

                                    x1 = cMap.boxes[activeShip.boxId].xcenter;
                                    y1 = cMap.boxes[activeShip.boxId].ycenter;
                                    x2 = cMap.boxes[completeBoxWay[cnt].id].xcenter;
                                    y2 = cMap.boxes[completeBoxWay[cnt].id].ycenter;

                                    int stepLineRange = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);

                                    range = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                                    
                                    dx = range / 15;
                                    int deltax;
                                    int deltay;
                                
                                    deltax = (x2 - x1) / 15;
                                    deltay = (y2 - y1) / 15;

                                    Image bg;
                                    Rectangle rect;
                                    
                                    int halfBoxWidth = (cMap.boxes[0].xpoint3 - cMap.boxes[0].xpoint2)/2;
                                    int halfBoxHeight = (cMap.boxes[0].ypoint6 - cMap.boxes[0].ypoint2)/2;

                                    //закрашиваем выделенный корабль

                                    rect = new Rectangle(
                                            activeShip.x - halfBoxWidth,
                                            activeShip.y - halfBoxHeight,
                                            halfBoxWidth + halfBoxWidth,
                                            halfBoxHeight + halfBoxHeight
                                        );

                                    bg = bmBackground.Clone(rect, bmBackground.PixelFormat);
                                    g.DrawImage(bg, activeShip.x - halfBoxWidth, activeShip.y - halfBoxHeight);

                                    pictureMap.Refresh();
                                    
                                    doShipRotate(rotateAngle, 1, false);

                                    for (int count1 = 0; count1 < range - 10; count1 += dx)
                                    {
                                        activeShip.x += deltax;
                                        activeShip.y += deltay;

                                        // запоминаем кусок картинки, на которой уже нет активного корабля
                                        rect = new Rectangle(
                                            activeShip.x - halfBoxWidth,
                                            activeShip.y - halfBoxHeight,
                                            halfBoxWidth + halfBoxWidth,
                                            halfBoxHeight + halfBoxHeight
                                        );

                                        bg = bmFull.Clone(rect, bmFull.PixelFormat);
                                      
                                        // рисуем корабль по новым координатам

                                        g.DrawImage(activeShip.objectImg,
                                               new Rectangle(activeShip.x - activeShip.objectImg.Width / 2,
                                                   activeShip.y - activeShip.objectImg.Height / 2,
                                                   activeShip.objectImg.Width,
                                                   activeShip.objectImg.Height)
                                        );

                                        pictureMap.Image = bmFull;
                                        pictureMap.Refresh();

                                        Thread.Sleep(5);
                                        g.DrawImage(bg, activeShip.x - halfBoxWidth, activeShip.y - halfBoxHeight);
                                    } 

                                    activeShip.moveShip(cMap, activeShip.boxId, completeBoxWay[cnt].id, 1);

                                    g.DrawImage(activeShip.objectImg,
                                        new Rectangle(activeShip.x - activeShip.objectImg.Width / 2, 
                                            activeShip.y - activeShip.objectImg.Height / 2,
                                            activeShip.objectImg.Width,
                                            activeShip.objectImg.Height));

                                    pictureMap.Refresh();

                                    doShipRotate(rotateAngle, -1, true);
                                    
                                    boxDescription.Text = activeShip.description();

                                }
                                activeShip.statusRefresh(ref bmBackground, ref bmFull);

                                if (activeShip.actionsLeft != 0)
                                {
                                    drawSavedImages();
                                    drawActiveShipFrames();
                                }
                                else
                                {
                                    drawSavedImages();
                                    activeShip = null;
                                }
                                
                                //break;
                            }
                        }
                        else if (cMap.boxes[select].spaceObject != null)
                        {
                            if (cMap.boxes[select].spaceObject.player == activePlayer)
                            {
                                boxDescription.Text = cMap.boxes[select].spaceObject.description();
                                activeShip = (Ship)cMap.boxes[select].spaceObject;

                                drawSavedImages();
                                drawActiveShipFrames();
                                break;
                            }

                            // просчет возможности атаки 

                            else if (cMap.boxes[select].spaceObject.player != activePlayer)
                            {
                                int flag = 0;
                                int a = activeShip.boxId;

                                double x1 = cMap.boxes[a].x;
                                double y1 = cMap.boxes[a].y;
                                double x2 = cMap.boxes[select].x;
                                double y2 = cMap.boxes[select].y;
                                double range;

                                // определяем расстояние между объектами

                                range = Math.Sqrt((x2 - x1) * (x2 - x1) + ((y2 - y1) * (y2 - y1)) * 0.35);

                                if(activeShip.equippedWeapon.attackRange >= (int)range)
                                {
                                    flag = 1; // устанавливаем флаг, если расстояние не превышает дальности атаки
                                }
                                if (flag == 1)
                                {
                                    if(activeShip.actionsLeft >= activeShip.equippedWeapon.energyСonsumption
                                        && activeShip.equippedWeapon.shotsleft > 0)  // если у корабля остались очки действий
                                    {
                                        double angle, targetx, targety;

                                        targetx = cMap.boxes[select].x;
                                        targety = cMap.boxes[select].y;

                                        angle = attackAngleSearch(targetx, targety);

                                        
                                        doShipRotate(angle, 1, true);
                                        

                                        // отрисовка атаки
                                        Thread.Sleep(150);

                                        if (activeShip.attack(cMap, cMap.boxes[select].id, ref combatBitmap, player, ref pictureMap, ref bmBackground, ref bmFull) == 1)
                                            shipsCount();

                                        boxDescription.Text = activeShip.description();
                                        pictureMap.Refresh();
                                        //Draw();

                                        // возвращаем корабль в исходное положение
                                        doShipRotate(angle, -1, false);

                                        // убираем подсветку с корабля, если у него не осталось очков передвижений
                                        if (activeShip.actionsLeft == 0)
                                        {
                                            activeShip = null;
                                            //Draw();
                                        }
                                        else
                                        {
                                            drawSavedImages();
                                            drawActiveShipFrames();
                                        }
                                        flag = 0;

                                        break;
                                    } 
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
            if (activePlayer == 1) activePlayer = 2;
            else activePlayer = 1;

            lblTurn.Text = "Ходит " + activePlayer + "-й игрок";

            activeShip = null;

            drawSavedImages();

            for (int count = 0; count < allShips.Count; count++)
            {
                allShips[count].refill();
                allShips[count].statusRefresh(ref bmBackground, ref bmFull);
            }

            objectManager.moveMeteors(cMap, bmBackground, bmFull);
            
            if(objectManager.whether2createMeteor() == 1)
            {
                Meteor newMeteor = objectManager.meteorCreate(cMap);

                if (newMeteor != null)
                {
                    Graphics g = Graphics.FromImage(bmFull);
                    g.DrawImage(newMeteor.objectImg,
                        new Rectangle(newMeteor.x - boxWidth / 6,
                            newMeteor.y - boxHeight / 6,
                            boxWidth / 3,
                            boxHeight / 3));
                    g.DrawString(newMeteor.currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(cMap.boxes[newMeteor.boxId].xpoint1 + 20, cMap.boxes[newMeteor.boxId].ypoint1 - 25));
                }
            }


            shipsCount();
            
            pictureMap.Refresh();
            
        }

    }
}
