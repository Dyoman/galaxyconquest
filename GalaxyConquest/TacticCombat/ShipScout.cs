using System.Drawing;

namespace GalaxyConquest.Tactics
{
    public class ShipScout : Ship
    {
        public string staticDescription;
        public override string description()
        {
            return "" + staticDescription + "\nhp - " + currentHealth + "/" + maxHealth + "\nactions - "
                            + actionsLeft + "/" + maxActions + equippedWeapon.description() + "\nAP - " + equippedWeapon.attackPower + "\nRange - " + equippedWeapon.attackRange;
        }

        public override void drawSpaceShit(ref combatMap cMap, ref System.Drawing.Bitmap bmap)
        {
            Graphics g = Graphics.FromImage(bmap);

            SolidBrush generalBrush;

            if (player == 1)
                generalBrush = new SolidBrush(Color.Blue);
            else if (player == 2)
                generalBrush = new SolidBrush(Color.Red);
            else
                generalBrush = new SolidBrush(Color.Gray);

            Point[] myPointArray = {
                    new Point(cMap.boxes[boxId].xcenter + xpoints[0], cMap.boxes[boxId].ycenter + ypoints[0]),
                    new Point(cMap.boxes[boxId].xcenter + xpoints[1], cMap.boxes[boxId].ycenter + ypoints[1]),
                    new Point(cMap.boxes[boxId].xcenter + xpoints[2], cMap.boxes[boxId].ycenter + ypoints[2]),
                    };
            g.FillPolygon(generalBrush, myPointArray);
            g.DrawString(actionsLeft.ToString(), new Font("Arial", 8.0F), Brushes.Blue, new PointF(cMap.boxes[boxId].xpoint1 + 25, cMap.boxes[boxId].ypoint1 + 15));
            g.DrawString(currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(cMap.boxes[boxId].xpoint1 + 20, cMap.boxes[boxId].ypoint1 - 25));
        }

        public ShipScout(int p, Weapon weapon)
        {
            objectType = Constants.SHIP;

            equippedWeapon = weapon;

            
            objectImg = Image.FromFile(@"Sprites/ships/player/Bomber2.png");
            baseObjectImg = objectImg;

            player = p;
            maxHealth = 50;
            currentHealth = maxHealth;
            maxActions = 7;
            actionsLeft = maxActions;
            staticDescription = "Лёгкий корабль\nкласса Scout";

            // координаты точек относительно центра ячейки
            xpoints.Add(-15);
            xpoints.Add(-15);
            xpoints.Add(17);
            // лишние точки

            ypoints.Add(-14);
            ypoints.Add(14);
            ypoints.Add(0);
     
            // лишние точки

            weaponR = 12;

            if (player != 1)
            {
                weaponR *= -1;
                objectImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            
            
            weaponPointX = weaponR;
            weaponPointY = 0;

        }
    }
}
