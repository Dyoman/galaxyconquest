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
    public partial class Tech_Tree : Form
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
            InitializeComponent();
            Tech.Inint();
            this.MouseWheel += new MouseEventHandler(this_MouseWheel);
            Redraw();
        }

        private void Redraw()
        {

            TechTreeBitmap = new Bitmap(TechTreeImage.Width, TechTreeImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

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

            TechTreeImage.Image = TechTreeBitmap;
            TechTreeImage.Refresh();

        }


        private void TechTreeImage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void TechTreeImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                horizontal += (e.X - mouseX) / scaling;
                vertical += (e.Y - mouseY) / scaling;

                mouseX = e.X;
                mouseY = e.Y;

                centerX = TechTreeImage.Width / 2 / scaling + horizontal;
                centerY = TechTreeImage.Height / 2 / scaling + vertical;

                Redraw();
            }
        }

        private void Tech_Tree_Resize(object sender, EventArgs e)
        {
            Redraw();
        }

        private void buttonScalingUp_Click(object sender, EventArgs e)
        {
            if (scaling >= 10)
            {
                return;
            }

            else
            {
                scaling += 0.2f;
                Redraw();
            }
        }

        private void buttonScalingDown_Click(object sender, EventArgs e)
        {
            if (scaling <= 0.4)
            {
                return;
            }
            else
            {
                scaling -= 0.2f;
                Redraw();
            }
        }


        private void this_MouseWheel(object sender, MouseEventArgs e) // resizing of galaxy at event change wheel mouse
        {
            if (e.Delta > 0)
            {
                if (scaling >= 10)
                    return;
                else
                {
                    scaling += 0.2f;
                }
            }
            else
            {
                if (scaling <= 0.4)
                    return;
                else
                {
                    scaling -= 0.2f;
                }
            }
            centerX = TechTreeImage.Width / 2 / scaling + horizontal;
            centerY = TechTreeImage.Height / 2 / scaling + vertical;
            Redraw();
        }

        private void TechTreeImage_MouseClick(object sender, MouseEventArgs e)
        {

            for (int i = 0; i < Tech.teches.tiers.Count; i++)
            {
                for (int j = 0; j < Tech.teches.tiers[i].Count; j++)
                {
                    for (int k = 0; k < Tech.teches.tiers[i][j].Count; k++)
                    {
                        Size string_lenght = TextRenderer.MeasureText(Tech.teches.tiers[i][j][k].subtech, fnt);

                        if (e.X < (centerX + 340 * i + (string_lenght.Width + 2)) * scaling &&
                            e.X > (centerX + 340 * i - 2) * scaling &&
                            e.Y < (centerY + 300 - (80 + Tech.teches.tiers[i][j].Count + 1 * 10) * j - (30 * k) + (30 * Tech.teches.tiers[i][j].Count / 2) + (string_lenght.Height + 2)) * scaling &&
                            e.Y > (centerY + 300 - (80 + Tech.teches.tiers[i][j].Count + 1 * 10) * j - (30 * k) + (30 * Tech.teches.tiers[i][j].Count / 2) - 2) * scaling)
                        {
                            tierClicked = i;
                            techLineClicked = j;
                            subtechClicked = k;


                            properties_tech_textBox.Text = Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].description;
                            groupBox1.Visible = true;
                            groupBox1.Text = Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].subtech;
                        }
                    }
                }
            }

        }


        private void Tech_Tree_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void learn_tech_button_Click(object sender, EventArgs e)
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
                    if (Form1.Game.Player.skillPoints >= tierClicked * 100)
                    {
                        Form1.Game.Player.skillPoints -= tierClicked * 100;
                        tech_logic2 = true;
                    }
                    else
                    {
                        MessageBox.Show("Not enough skill popints!");
                        tierClicked = 1000;
                        techLineClicked = 1000;
                        subtechClicked = 1000;
                        return;
                    }
                }
            }
            if (tech_logic == false)
            {
                MessageBox.Show("You alrady have this tech!");
                tierClicked = 1000;
                techLineClicked = 1000;
                subtechClicked = 1000;
            }
            else
            {
                if (tech_logic2 == true)
                {
                    Form1.SelfRef.tech_label.Visible = true;
                    Form1.SelfRef.tech_progressBar.Visible = true;
                    Form1.SelfRef.tech_label.Text = Tech.teches.tiers[tierClicked][techLineClicked][subtechClicked].subtech;
                    Form1.SelfRef.tech_progressBar.Maximum = Tech.learning_tech_time;

                    Redraw();
                }
                else
                {
                    MessageBox.Show("Learn previos tech before!");
                    tierClicked = 1000;
                    techLineClicked = 1000;
                    subtechClicked = 1000;
                }
            }

        }


        private void Tech_Tree_VisibleChanged(object sender, EventArgs e)
        {
            Redraw();
        }


    }

}
