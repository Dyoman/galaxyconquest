using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GalaxyConquest.Tactics
{
    public class WpnHeavyLaser : Weapon
    {
        public WpnHeavyLaser()
        {
            attackPower = 50;
            attackRange = 5;
            energyСonsumption = 1;
            cage = 1;
            shotsleft = cage;
        }
        public override string description()
        {
            return "\nТяжелый лазер\nВыстрелов: " + shotsleft;
        }
        public override void drawAttack(int x, int y, int targetx, int targety, ref System.Drawing.Bitmap bmap, System.Media.SoundPlayer player, ref PictureBox pictureMap, ref System.Drawing.Bitmap bmBackground, ref System.Drawing.Bitmap bmFull)
        {
            System.Threading.Thread.Sleep(150);
            player.SoundLocation = @"Sounds/laser1.wav";

            int xmin, ymin, xmax, ymax;

            if (x <= targetx) { xmin = x - 5; xmax = targetx + 5; }
            else { xmin = targetx - 5; xmax = x + 3; }
            if (y <= targety) { ymin = y - 5; ymax = targety + 5; }
            else { ymin = targety - 5; ymax = y + 5; }

            Graphics g = Graphics.FromImage(bmFull);
            Rectangle rect;
            Image oldImage;

            rect = new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);

            oldImage = bmFull.Clone(rect, bmFull.PixelFormat);

            Pen laserPen1 = new Pen(Color.Orange, 3);

            player.Play();
            for (int i = -3; i < 3; i++)
            {
                // --- 1) находим размер изображения
                //rect = new Rectangle(0, 0, combatBitmap.Width, combatBitmap.Height); 
                // --- 2) клонируем наш битмап
                //oldImage = combatBitmap.Clone(rect, combatBitmap.PixelFormat);

                g.DrawLine(laserPen1, new Point(x, y), new Point(targetx + i, targety));

                pictureMap.Image = bmFull;
                pictureMap.Refresh();

                // --- 3) отрисовываем тот битмам, который сохранили выше
                //g.DrawImage(oldImage, 0, 0);


                System.Threading.Thread.Sleep(35);
            }
            g.DrawImage(oldImage, xmin, ymin);
            pictureMap.Refresh();
        }
    }
}
