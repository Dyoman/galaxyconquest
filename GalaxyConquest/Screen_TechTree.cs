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
        /// <summary>
        /// Полотно на котором отрисовывается все технологии
        /// </summary>
        private Image TechTreeBitmap;
        /// <summary>
        /// Полотно на котором отрисовывается описание технологии и пр. информация
        /// </summary>
        private Image TechDescriptionBitmap;

        private float scaling = 1f;
        private float horizontal = 0;
        private float vertical = 0;

        private int mouseX;
        private int mouseY;

        private float centerX;
        private float centerY;

        private bool dragging = false;

        private int tierClicked = 1000;
        private int techLineClicked = 1000;
        private int subtechClicked = 1000;

        private float progress = 0;

        private Brush br;
        private Pen pen;
        private Pen whitePen = new Pen(Brushes.White);
        private Pen grayPen = new Pen(Brushes.Gray);
        private Pen yellowPen = new Pen(Brushes.Yellow);
        private System.Drawing.Font fnt = new System.Drawing.Font("Consolas", 10.0F);

        private Gwen.Control.ImagePanel img;
        private Gwen.Control.ImagePanel techDescriptionImg;
        private Gwen.Control.Label label;

        public Screen_TechTree(Base parent)
            : base(parent)
        {

            Tech.Inint();
            SetSize(parent.Width, parent.Height);

            TechTreeBitmap = new Bitmap(Program.percentW(100), Program.percentH(100), PixelFormat.Format32bppArgb);
            TechDescriptionBitmap = new Bitmap(Program.percentW(20), Program.percentH(20), PixelFormat.Format32bppArgb);

            whitePen.Width = 4;
            grayPen.Width = 4;
            yellowPen.Width = 4;

            img = new Gwen.Control.ImagePanel(this);
            img.SetPosition(Program.percentW(0), Program.percentH(0));
            img.SetSize(Program.percentW(100), Program.percentH(100));
            img.Clicked += new GwenEventHandler<ClickedEventArgs>(img_Clicked);
            img.MouseMoved += new GwenEventHandler<MovedEventArgs>(img_MouseMoved);
            img.MouseDown += new GwenEventHandler<ClickedEventArgs>(img_MouseDown);
            img.MouseUp += new GwenEventHandler<ClickedEventArgs>(img_MouseUp);
            img.MouseWheeled += new GwenEventHandler<MouseWheeledEventArgs>(img_MouseWheeled);

            techDescriptionImg = new Gwen.Control.ImagePanel(this);
            techDescriptionImg.SetPosition(Program.percentW(0), Program.percentH(80));
            techDescriptionImg.SetSize(Program.percentW(20), Program.percentH(20));

            label = new Gwen.Control.Label(this);
            label.Text = "Tech_Tree Probe";
            label.SetPosition(Program.percentW(5), Program.percentH(5));
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            Gwen.Control.Button buttonBack = new Gwen.Control.Button(this);
            buttonBack.Text = "Back";
            buttonBack.Font = Program.fontButtonLabels;
            buttonBack.SetBounds(Program.percentW(60), Program.percentH(80), 100, 50);
            buttonBack.Clicked += onButtonBackClick;

            Gwen.Control.Button buttonLearn = new Gwen.Control.Button(this);
            buttonLearn.Text = "Learn";
            buttonLearn.Font = Program.fontButtonLabels;
            buttonLearn.SetBounds(Program.percentW(80), Program.percentH(80), 100, 50);
            buttonLearn.Clicked += onButtonLearnClick;

            updateDrawing();
        }

        private void img_MouseUp(Base sender, ClickedEventArgs arguments)
        {
            dragging = false;
        }

        private void img_MouseDown(Base sender, ClickedEventArgs arguments)
        {
            dragging = true;
            mouseX = arguments.X;
            mouseY = arguments.Y;
        }

        private void img_MouseWheeled(Base sender, MouseWheeledEventArgs arguments)
        {
            if (arguments.Delta > 0)
                scaling = (float)Math.Min(scaling + 0.1, 10);
            else
                scaling = (float)Math.Max(scaling - 0.1, 0.1);

            updateDrawing();

        }

        private void img_MouseMoved(Base sender, MovedEventArgs arguments)
        {
            if (dragging)
            {
                horizontal += (arguments.X - mouseX) / scaling;
                vertical += (arguments.Y - mouseY) / scaling;

                mouseX = arguments.X;
                mouseY = arguments.Y;

                centerX = TechTreeBitmap.Width / 2 / scaling + horizontal;
                centerY = TechTreeBitmap.Height / 2 / scaling + vertical;

                updateDrawing();
            }
        }

        private void img_Clicked(Base sender, ClickedEventArgs arguments)
        {
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

                        RectangleF imageRect = new RectangleF(centerX + j * 500 - Tech.teches.tiers[i][j].Count / 2 * 150 + k * 150,
                                centerY - i * 300 - 120,
                                100,
                                100
                                );

                        RectangleF boundRect = new RectangleF(imageRect.Left, imageRect.Top, 100, 100 + 60);

                        if (arguments.X < (boundRect.Right) * scaling &&
                            arguments.X > (boundRect.Left) * scaling &&
                            arguments.Y < (boundRect.Bottom) * scaling &&
                            arguments.Y > (boundRect.Top) * scaling)
                        {
                            tierClicked = i;
                            techLineClicked = j;
                            subtechClicked = k;

                            //label.Text = i+";"+j+";"+k;
                            //groupBox1.Visible = true;
                            //groupBox1.Text = Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].subtech;
                            updateDrawing();
                            return;
                        }
                        else
                        {
                            tierClicked = 1000;
                            techLineClicked = 1000;
                            subtechClicked = 1000;
                        }
                    }
                }
            }

            updateDrawing();

            //label.Text = "DOWN";

            mouseX = arguments.X;
            mouseY = arguments.Y;
        }

        protected override bool OnKeyReturn(bool down)
        {
            System.Windows.Forms.MessageBox.Show("URA");
            return base.OnKeyEscape(down);
        }

        private void updateDrawing()
        {

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Graphics g = Graphics.FromImage(TechTreeBitmap);
            g.FillRectangle(Brushes.Black, 0, 0, TechTreeBitmap.Width, TechTreeBitmap.Height);

            RenderTechDescriptionImg();

            centerX = TechTreeBitmap.Width / 2 / scaling;
            centerY = TechTreeBitmap.Height / 2 / scaling;

            centerX += horizontal;
            centerY += vertical;

            g.ScaleTransform(scaling, scaling);
            //достаем технологии из Tech.teches i - столбец(Tier); j - строка(TechLine); k - подстрока(Subtech)
            for (int i = 0; i < Tech.teches.tiers.Count; i++)
            {
                for (int j = 0; j < Tech.teches.tiers[i].Count; j++)
                {
                    for (int k = 0; k < Tech.teches.tiers[i][j].Count; k++)
                    {
                        for (int z = 0; z < Player.technologies.Count; z++)
                        {
                            pen = grayPen;
                            br = Brushes.White;
                            if (i == tierClicked && j == techLineClicked && k == subtechClicked)
                            {
                                pen = whitePen;
                            }
                            if (i == Player.technologies[z][0] &&
                                j == Player.technologies[z][1] &&
                                k == Player.technologies[z][2])
                            {
                                br = Brushes.Yellow;
                                progress = 100;
                                break;
                            }

                        }

                        if (Program.Game.Player.Learning == true &&
                            Program.Game.Player.learningTech.Line == j &&
                            Program.Game.Player.learningTech.Tier == i &&
                            Program.Game.Player.learningTech.Subtech == k)
                            progress = Program.Game.Player.getLearningProgressPercent();

                        RectangleF imageRect = new RectangleF(centerX + j * 500 - Tech.teches.tiers[i][j].Count / 2 * 150 + k * 150,
                                centerY - i * 300 - 120,
                                100,
                                100
                                );
                        RectangleF progressRect = new RectangleF(centerX + j * 500 - Tech.teches.tiers[i][j].Count / 2 * 150 + k * 150 - 2,
                                centerY - i * 300 - 120 - 2,
                                100 + 4,
                                100 + 4
                                );
                        RectangleF boundRect = new RectangleF(imageRect.Left, imageRect.Top,
                                100,
                                100 + 60
                                );

                        g.DrawString(Tech.teches.tiers[i][j][k].subtech, fnt, br,
                           new RectangleF(imageRect.Left, imageRect.Bottom + 20, 100, 40), stringFormat);



                        //----------------------------------------------------

                        //g.DrawRectangle(Pens.AliceBlue, Rectangle.Round(boundRect));
                        //g.DrawRectangle(Pens.AliceBlue, Rectangle.Round(imageRect));
                        //g.DrawRectangle(Pens.AliceBlue, Rectangle.Round(progressRect));
                        g.DrawArc(pen, imageRect, 0, 360);
                        g.DrawArc(grayPen, progressRect, 0, 360);
                        g.DrawArc(yellowPen, progressRect, -90, (int)(progress * 3.6));

                        progress = 0;
                    }

                }
            }
            img.Image = (Bitmap)TechTreeBitmap;
            techDescriptionImg.Image = (Bitmap)TechDescriptionBitmap;

            Program.m_Canvas.RenderCanvas();
            Program.m_Window.Display();
        }

        private void onButtonBackClick(Base control, EventArgs args)
        {
            Program.screenManager.LoadScreen("gamescreen");
        }

        private void onButtonLearnClick(Base control, EventArgs args)
        {
            bool tech_logic = true;
            bool tech_logic2 = false;
            for (int i = 0; i < Player.technologies.Count; i++)
            {
                if (tierClicked == Player.technologies[i][0] &&
                    techLineClicked == Player.technologies[i][1] &&
                    subtechClicked == Player.technologies[i][2])
                {
                    tech_logic = false;
                    break;
                }

                if (tierClicked == Player.technologies[i][0] + 1 && techLineClicked == Player.technologies[i][1])
                {
                    if (Program.Game.Player.skillPoints >= Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].RP)
                    {
                        tech_logic2 = true;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Not enough skill popints!");
                        tierClicked = 1000;
                        techLineClicked = 1000;
                        subtechClicked = 1000;
                        return;
                    }
                }
            }
            if (tech_logic == false)
            {
                System.Windows.Forms.MessageBox.Show("You alrady have this tech!");
                tierClicked = 1000;
                techLineClicked = 1000;
                subtechClicked = 1000;
            }
            else
            {
                if (tech_logic2 == true)
                {
                    //Form1.SelfRef.tech_progressBar.Maximum = Tech.learning_tech_time;
                    Program.Game.Player.skillPoints -= Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].RP;
                    Program.Game.Player.Learn(new TechData(tierClicked, techLineClicked, subtechClicked));
                    System.Windows.Forms.MessageBox.Show("Learn started!");

                    updateDrawing();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Learn previos tech before!");
                    tierClicked = 1000;
                    techLineClicked = 1000;
                    subtechClicked = 1000;
                }
            }
        }

        private void RenderTechDescriptionImg()
        {
            //Формат для выводимого текста (центрирование по вертикали и горизонтали)
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Graphics gr2 = Graphics.FromImage(TechDescriptionBitmap);
            gr2.FillRectangle(Brushes.White, 0, 0, TechDescriptionBitmap.Width, TechDescriptionBitmap.Height);

            if (tierClicked != 1000 && techLineClicked != 1000 && subtechClicked != 1000)
            {
                //выводим очки исследований
                gr2.DrawString("RP cost: " + Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].RP.ToString(),
                    fnt, Brushes.Black,
                    new RectangleF(10, 5, TechDescriptionBitmap.Width - 20, TechDescriptionBitmap.Height / 2),
                    stringFormat);

                //выводим описание технологии
                gr2.DrawString(Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].description,
                    fnt, Brushes.Black,
                    new RectangleF(10, TechDescriptionBitmap.Height / 2, TechDescriptionBitmap.Width - 20, TechDescriptionBitmap.Height / 2),
                    stringFormat);
            }
        }
        public override void Dispose()
        {
            fnt.Dispose();
            base.Dispose();
        }

    }
}
