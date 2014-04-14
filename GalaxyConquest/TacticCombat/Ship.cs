using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace GalaxyConquest.Tactics
{
    public class Ship : SpaceObject
    {
        public Weapon equippedWeapon;
        public int weaponPointX;
        public int weaponPointY;

        public override string description()
        {
            return "";
        }

        public override void drawSpaceShit(ref combatMap cMap, ref System.Drawing.Bitmap bmap)
        {
            
        }

        public void moveShip(combatMap cMap, int pointAId, int pointBId, int range)
        {

            if (actionsLeft >= range)
            {

                boxId = pointBId;

                x = cMap.boxes[boxId].xcenter;
                y = cMap.boxes[boxId].ycenter;

                cMap.boxes[pointAId].spaceObject = null;
                cMap.boxes[pointBId].spaceObject = this;
                actionsLeft -= range;

            }
        }
        public int attack(combatMap cMap, int pointB, ref System.Drawing.Bitmap bmap, System.Media.SoundPlayer player, ref PictureBox pictureMap, ref  Bitmap bmBackground, ref Bitmap bmFull)
        {
            int dmg;
            int flag = 0;

            if (actionsLeft >= equippedWeapon.energyСonsumption && equippedWeapon.shotsleft > 0)
            {
                equippedWeapon.drawAttack(cMap.boxes[boxId].xcenter + weaponPointX, cMap.boxes[boxId].ycenter + weaponPointY,
                    cMap.boxes[pointB].xcenter, cMap.boxes[pointB].ycenter,
                    ref bmap, player, ref pictureMap, ref bmBackground, ref bmFull
                );

                Random rand = new Random();
                dmg = rand.Next(-equippedWeapon.attackPower / 10, equippedWeapon.attackPower / 10) + equippedWeapon.attackPower;
                cMap.boxes[pointB].spaceObject.currentHealth -= dmg;

                actionsLeft -= equippedWeapon.energyСonsumption;
                equippedWeapon.shotsleft -= 1;

                
                if (cMap.boxes[pointB].spaceObject.currentHealth <= 0)
                {
                    cMap.clearBox(pointB, ref bmBackground, ref bmFull);
                    flag = 1;
                }
                else
                {
                    cMap.boxes[pointB].spaceObject.statusRefresh(ref bmBackground, ref bmFull);
                }

                pictureMap.Image = bmFull;
                pictureMap.Refresh();
            }
            return flag;
        }
        public override void statusRefresh(ref Bitmap bmBg, ref Bitmap bmFull)
        {
            Graphics g = Graphics.FromImage(bmFull);
            Image bg = bmBg.Clone(new Rectangle(x - 25, y + 28, 50, 10), bmBg.PixelFormat);
            g.DrawImage(bg, x - 25, y + 28);

            g.DrawString(actionsLeft.ToString(), new Font("Arial", 8.0F), Brushes.Blue, new PointF(x + 10, y + 26));
            g.DrawString(currentHealth.ToString(), new Font("Arial", 8.0F), Brushes.Red, new PointF(x - 20, y + 26));
        }
        public void placeShip(ref combatMap cMap)
        {
            if (this.player == 1)
            {
                while (true)
                {
                    Random rand = new Random();
                    int randomBox = rand.Next(0, cMap.height * 2);

                    if (cMap.boxes[randomBox].spaceObject == null)
                    {
                        cMap.boxes[randomBox].spaceObject = this;
                        boxId = randomBox;

                        

                        break;
                    }
                }
            }
            else if (this.player != 1)
            {
                while (true)
                {
                    Random rand = new Random();
                    int randomBox = rand.Next(cMap.boxes.Count - cMap.height * 2, cMap.boxes.Count);

                    if (cMap.boxes[randomBox].spaceObject == null)
                    {
                        cMap.boxes[randomBox].spaceObject = this;
                        boxId = randomBox;
                        break;
                    }
                }
            }
            x = cMap.boxes[boxId].xcenter;
            y = cMap.boxes[boxId].ycenter;
        }

        public void shipRotate(double angle)
        {
            int xold, yold;
            angle = angle * Math.PI / 180;
            for (int i = 0; i < xpoints.Count; i++)
            {
                xold = xpoints[i];
                yold = ypoints[i];
                xpoints[i] = (int)(Math.Round((double)xold * Math.Cos(angle) - (double)yold * Math.Sin(angle), 0));
                ypoints[i] = (int)(Math.Round((double)xold * Math.Sin(angle) + (double)yold * Math.Cos(angle), 0));
            }
            weaponPointX = (int)(Math.Round((double)weaponPointX * Math.Cos(angle) - (double)weaponPointY * Math.Sin(angle), 0));
            weaponPointY = (int)(Math.Round((double)weaponPointX * Math.Sin(angle) + (double)weaponPointY * Math.Cos(angle), 0));
        }
        public void refill()
        {
            actionsLeft = maxActions;
            equippedWeapon.shotsleft = equippedWeapon.cage;
        }
    }
}
