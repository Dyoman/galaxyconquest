using System.Drawing;

namespace GalaxyConquest.Tactics
{
    public class Meteor : SpaceObject
    {
        public int explodeDmg;
        public string staticDescription = "Движущийся метеор";
        public int xdirection;
        public int ydirection;
        public Meteor(combatMap cMap, int box, int health, int dmg, int xdirect, int ydirect)
        {
            boxId = box;
            objectType = Constants.METEOR;
            player = 0;
            maxHealth = health;
            currentHealth = maxHealth;
            explodeDmg = dmg;
            xdirection = xdirect;
            ydirection = ydirect;

            x = cMap.boxes[boxId].xcenter;
            y = cMap.boxes[boxId].ycenter;

            objectImg = Image.FromFile(@"Sprites/objects/meteor.png");
            
        }

        public override void drawSpaceShit(ref combatMap cMap, ref System.Drawing.Bitmap bmap)
        {
            Graphics g = Graphics.FromImage(bmap);
            SolidBrush grayBrush = new SolidBrush(Color.Gray);
            g.FillEllipse(grayBrush, cMap.boxes[boxId].xcenter - 14, cMap.boxes[boxId].ycenter - 14, 28, 28);
            g.DrawString(currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(cMap.boxes[boxId].xpoint1 + 20, cMap.boxes[boxId].ypoint1 - 25));
        }

        public void move(combatMap cMap, Bitmap bmBackground, Bitmap bmFull)
        {
            int newx;
            int newy;
            int pointB;
            Rectangle rect;
            Image bg;

            Graphics g = Graphics.FromImage(bmFull);

            int boxWidth = (cMap.boxes[0].xpoint3 - cMap.boxes[0].xpoint2);
            int boxHeight = (cMap.boxes[0].xpoint5 - cMap.boxes[0].ypoint3);

            newx = cMap.boxes[boxId].x + xdirection;
            newy = cMap.boxes[boxId].y + ydirection;

            rect = new Rectangle(
                x - boxWidth / 2, y - boxHeight / 2,
                boxWidth, boxHeight);

            bg = bmBackground.Clone(rect, bmBackground.PixelFormat);

            g.DrawImage(bg, x - boxWidth / 2, y - boxHeight / 2);

            if (newx < 0 || newx > cMap.width - 1
                || newy < 0 || newy > cMap.height * 2 - 1)
            {
                cMap.boxes[boxId].spaceObject.player = -1;
                cMap.boxes[boxId].spaceObject = null;
                boxId = -1;
            }
            else
                {
                    pointB = cMap.getBoxByCoords(newx, newy).id;

                    if (cMap.boxes[pointB].spaceObject == null)
                    {
                        cMap.boxes[boxId].spaceObject = null;
                        cMap.boxes[pointB].spaceObject = this;
                        boxId = cMap.boxes[pointB].id;

                        x = cMap.boxes[boxId].xcenter;
                        y = cMap.boxes[boxId].ycenter;

                        g.DrawImage(objectImg,
                            new Rectangle(x - boxWidth / 6, y - boxHeight / 6, boxWidth / 3, boxHeight / 3));
                        g.DrawString(currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(cMap.boxes[boxId].xpoint1 + 20, cMap.boxes[boxId].ypoint1 - 25));
                    }
                    else
                    {
                        rect = new Rectangle(
                            cMap.boxes[pointB].xpoint2, cMap.boxes[pointB].ypoint2,
                            boxWidth, boxHeight + 5);

                        bg = bmBackground.Clone(rect, bmBackground.PixelFormat);
                        g.DrawImage(bg, cMap.boxes[pointB].xpoint2, cMap.boxes[pointB].ypoint2);

                        cMap.boxes[pointB].spaceObject.currentHealth -= explodeDmg;

                        if (cMap.boxes[pointB].spaceObject.currentHealth <= 0)
                        {
                            cMap.clearBox(pointB, ref bmBackground, ref bmFull);
                        }

                        else
                        {
                            if (cMap.boxes[pointB].spaceObject.objectType != Constants.METEOR)
                            {
                                g.DrawImage(cMap.boxes[pointB].spaceObject.objectImg,
                                    new Rectangle(cMap.boxes[pointB].xcenter - cMap.boxes[pointB].spaceObject.objectImg.Width / 2,
                                        cMap.boxes[pointB].ycenter - cMap.boxes[pointB].spaceObject.objectImg.Height / 2,
                                        cMap.boxes[pointB].spaceObject.objectImg.Width,
                                        cMap.boxes[pointB].spaceObject.objectImg.Height));
                                cMap.boxes[pointB].spaceObject.statusRefresh(ref bmBackground, ref bmFull);
                            }
                        }
                        cMap.boxes[boxId].spaceObject.player = -1;
                        cMap.boxes[boxId].spaceObject = null;
                        boxId = -1;

                    }
                    
                }
        }

        public override void statusRefresh(ref Bitmap bmBg, ref Bitmap bmFull)
        {
            Graphics g = Graphics.FromImage(bmFull);
            Image bg = bmBg.Clone(new Rectangle(x - 15, y - 25, 30, 10), bmBg.PixelFormat);
            g.DrawImage(bg, x - 15, y - 25);

            g.DrawString(currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(x - 10, y - 20));
        }
        public override string description()
        {
            string x = "";
            string y = "";

            switch(xdirection)
            {
                case -1:
                    x = "left ";
                    break;
                case 1:
                    x = "right ";
                    break;
            }
            switch(ydirection)
            {
                case -1:
                    y = "top ";
                    break;
                case 1:
                    y = "bottom ";
                    break;
            }

            return staticDescription + "\nУрон при попадании\n в корабль: " + explodeDmg 
                + "\nhp - " + currentHealth 
                + "\nНаправление: \n" + x + y;
        }
    }
}
