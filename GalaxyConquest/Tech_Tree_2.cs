using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using GalaxyConquest.Game;

namespace GalaxyConquest
{
    class Tech_Tree
    {
        public Bitmap TechTreeBitmap;
        public int tierClicked = 1000;
        public int techLineClicked = 1000;
        public int subtechClicked = 1000;

        public float scaling = 1f;
        public float horizontal = 0;
        public float vertical = 0;

        public int mouseX;
        public int mouseY;

        float centerX;
        float centerY;

        public Brush br;
        public Font fnt = new Font("Consolas", 10.0F);

        public Tech_Tree()
        {
            Tech.Inint();
            Redraw();
        }
        public void Redraw()
        {

            TechTreeBitmap = new Bitmap(Program.percentW(100), Program.percentH(80), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(TechTreeBitmap);

            centerX = TechTreeBitmap.Width / 2 / scaling;
            centerY = TechTreeBitmap.Height / 2 / scaling;

            centerX += horizontal;
            centerY += vertical;

            g.ScaleTransform(scaling, scaling);
            br = Brushes.White;
            //достаем технологии из Tech.teches i - столбец(Tier); j - строка(TechLine); k - подстрока(Subtech)
            for (int i = 0; i < Tech.teches.tiers.Count; i++)
            {
                for (int j = 0; j < Tech.teches.tiers[i].Count; j++)
                {
                    for (int k = 0; k < Tech.teches.tiers[i][j].Count; k++)
                    {
                        for (int z = 0; z < Player.technologies.Count; z++)
                        {
                            if (i == Player.technologies[z][0] &&
                                j == Player.technologies[z][1] &&
                                k == Player.technologies[z][2])
                            {
                                br = Brushes.Yellow;
                                break;
                            }
                            else
                            {
                                br = Brushes.White;
                            }
                        }
                        Size string_lenght = TextRenderer.MeasureText(Tech.teches.tiers[i][j][k].subtech, fnt);
                        g.DrawString(Tech.teches.tiers[i][j][k].subtech, fnt, br,
                                    new PointF(centerX + 340 * i, centerY + 300 - (80 + Tech.teches.tiers[i][j].Count + 1 * 10) * j - (30 * k) + (30 * Tech.teches.tiers[i][j].Count / 2)));

                        g.DrawRectangle(Pens.AliceBlue, centerX + 340 * i - 2,
                            centerY + 300 - (80 + Tech.teches.tiers[i][j].Count + 1 * 10) * j - (30 * k) + (30 * Tech.teches.tiers[i][j].Count / 2) - 2, string_lenght.Width + 2, string_lenght.Height + 2);
                    }

                }
            }


        }
    }
}
