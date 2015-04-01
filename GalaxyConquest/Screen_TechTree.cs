using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using SFML;
using Gwen;
using Gwen.Control;

using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using GalaxyConquest.Game;

using GalaxyConquest.Drawing;
using GalaxyConquest.SpaceObjects;


namespace GalaxyConquest
{
    class Screen_TechTree : Gwen.Control.DockBase
    {
        public Bitmap TechTreeBitmap = new Bitmap(Program.percentW(100), Program.percentH(80), PixelFormat.Format32bppArgb);
        public float scaling = 1f;
        public float horizontal = 0;
        public float vertical = 0;

        public int mouseX;
        public int mouseY;

        float centerX;
        float centerY;

        public Brush br;
        public System.Drawing.Font fnt = new System.Drawing.Font("Consolas", 10.0F);

        public Gwen.Control.ImagePanel img;
        Gwen.Control.Label label;


        bool dragging = false;
        int mx = 0;
        int my = 0;

        public Screen_TechTree (Base parent)
            : base(parent)
        {
            Tech.Inint();
            SetSize(parent.Width, parent.Height);

            label = new Gwen.Control.Label(this);
            label.Text = "Tech_Tree Probe";
            label.SetPosition(Program.percentW(5), Program.percentH(5));
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            img = new Gwen.Control.ImagePanel(this);

            
            updateDrawing();

            img.SetPosition(Program.percentW(0), Program.percentH(20));
            img.SetSize(Program.percentW(100), Program.percentH(80));
            //img.Clicked += new GwenEventHandler<ClickedEventArgs>(img_Clicked);
            img.MouseMoved += new GwenEventHandler<MovedEventArgs>(img_MouseMoved);
            img.MouseDown += new GwenEventHandler<ClickedEventArgs>(img_MouseDown);
            img.MouseUp += new GwenEventHandler<ClickedEventArgs>(img_MouseUp);
        }
        // label Up и Down в Tech_Tree пожалуй не нужен
        void img_MouseUp(Base sender, ClickedEventArgs arguments)
        {
            label.Text = "UP";
            dragging = false;
        }

        void img_MouseDown(Base sender, ClickedEventArgs arguments)
        {
            label.Text = "DOWN";
            dragging = true;
            mx = arguments.X;
            my = arguments.Y;
        }

        void img_MouseMoved(Base sender, MovedEventArgs arguments)
        {
            if (dragging)
            {
                label.Text = arguments.X.ToString() + "," + arguments.Y.ToString();
                //DrawControl.Move(arguments.X - mx, arguments.Y - my);
                updateDrawing();
                mx = arguments.X;
                my = arguments.Y;
            }
        }

        void img_Clicked(Base sender, ClickedEventArgs arguments)
        {
            System.Windows.Forms.MessageBox.Show(arguments.X.ToString() + "," + arguments.Y.ToString());
        }

        protected override bool OnKeyReturn(bool down)
        {
            System.Windows.Forms.MessageBox.Show("URA");
            return base.OnKeyEscape(down);
        }

        private void updateDrawing()
        {

            

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

            img.Image = TechTreeBitmap;
        }
            
        

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
