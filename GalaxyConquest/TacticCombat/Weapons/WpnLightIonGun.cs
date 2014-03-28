using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GalaxyConquest.Tactics
{
    public class WpnLightIonGun : Weapon
    {
        public WpnLightIonGun()
        {
            attackPower = 25;
            attackRange = 3;
            energyСonsumption = 1;
        }
        public override string description()
        {
            return "";
        }
        public override void drawAttack(int x, int y, int targetx, int targety, ref System.Drawing.Bitmap bmap, System.Media.SoundPlayer player, ref PictureBox pictureMap)
        {
            System.Threading.Thread.Sleep(150);
            player.SoundLocation = @"../../Sounds/laser1.wav";

            Graphics g = Graphics.FromImage(bmap);
            Rectangle rect;  //  --- размер изображения
            Bitmap oldImage;  //  --- переменная, в которую его засун

            Pen laserPen1 = new Pen(Color.GreenYellow, 2);

            double angle = 0;

            if (x == targetx) // избегаем деления на ноль
            {
                if (y > targety)
                {
                    angle = -90;
                }
                else
                {
                    angle = 90;
                }
            }
            else // находим угол, на который нужно повернуть корабль (если он не равен 90 градусов)
            {
                angle = Math.Atan((targety - y) / (targetx - x)) * 180 / Math.PI;
            }

            SolidBrush brush = new SolidBrush(Color.DodgerBlue);
           
            player.Play();
            g.FillEllipse(brush, x - 5, y - 5, 10, 10);
            System.Threading.Thread.Sleep(100);
            /*
            for (int i = 0; i < 5; i++)
            {
                // --- 1) находим размер изображения
                rect = new Rectangle(0, 0, bmap.Width, bmap.Height); 
                // --- 2) клонируем наш битмап
                oldImage = bmap.Clone(rect, bmap.PixelFormat);

                g.DrawLine(laserPen1, new Point(x, y), new Point(targetx + i, targety));

                pictureMap.Image = bmap;
                pictureMap.Refresh();

                // --- 3) отрисовываем тот битмам, который сохранили выше
                g.DrawImage(oldImage, 0, 0);


                System.Threading.Thread.Sleep(35);
            } */
        }
    }
}
