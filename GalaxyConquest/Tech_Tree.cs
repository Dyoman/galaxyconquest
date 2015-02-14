using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace GalaxyConquest
{
    public partial class Tech_Tree : Form
    {

        public Bitmap TechTreeBitmap;
        //public List<string> tech = new List<string>();
        public List<List<string>> tech_desc = new List<List<string>>();
        public List<List<string>> tech_subtech = new List<List<string>>();

        public float scaling = 1f;
        public float horizontal = 0;
        public float vertical = 0;

        public int mouseX;
        public int mouseY;
        public int tech_clicked = 1000;
        public int subtech_clicked = 1000;
        public int learning_tech_time = 2;

        float centerX;
        float centerY;

        public Brush br;

        public Tech_Tree()
        {
            InitializeComponent();

            this.MouseWheel += new MouseEventHandler(this_MouseWheel);

            StreamReader tech_str = new StreamReader(@"Tech.txt");
            int counter = 0;
            string line;

            while ((line = tech_str.ReadLine()) != null)
            {
                string[] words = line.Split('~');

                //tech.Add(words[0]);
                tech_desc.Add(new List<string>());
                tech_subtech.Add(new List<string>());


                for (int j = 0; j < words.GetLength(0); j++)
                {
                    string[] desc = words[j].Split('#');
                    tech_desc[counter].Add(desc[1]);

                    tech_subtech[tech_subtech.Count - 1].Add(desc[0]);

                }

                counter++;
            }
            tech_str.Close();

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

            //чтение из фала списка технологий
            for (int i = 0; i < tech_subtech.Count; i++)
            {
                for (int z = 0; z < tech_subtech[i].Count; z++)
                {
                    for (int j = 0; j < Player.technologies.Count; j++)
                    {
                        if (i == Player.technologies[j][0] && z <= Player.technologies[j][1])
                        {
                            br = Brushes.Yellow;
                            break;
                        }
                        else
                        {
                            br = Brushes.White;
                        }
                    }
                    Font fnt = new Font("Consolas", 10.0F);
                    Size string_lenght = TextRenderer.MeasureText(tech_subtech[i][z], fnt);
                    g.DrawString(tech_subtech[i][z], fnt, br,
                                new PointF(centerX + 300 * z, centerY + 300 - 30 * i));

                    g.DrawRectangle(Pens.AliceBlue, centerX + 300 * z - 2,
                        centerY + 300 - 30 * i - 2, string_lenght.Width + 2, string_lenght.Height + 2);
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
            Font fnt = new Font("Consolas", 10.0F);

            for (int i = 0; i < tech_subtech.Count; i++)
            {
                for (int j = 0; j < tech_subtech[i].Count; j++)
                {
                    Size string_lenght = TextRenderer.MeasureText(tech_subtech[i][j], fnt);

                    if (e.X < (centerX + 300 * j + (string_lenght.Width + 2)) * scaling &&
                        e.X > (centerX + 300 * j - 2) * scaling &&
                        e.Y < (centerY + 300 - 30 * i + (string_lenght.Height + 2)) * scaling &&
                        e.Y > (centerY + 300 - 30 * i - 2) * scaling)
                    {
                        tech_clicked = i;
                        subtech_clicked = j;

                        properties_tech_textBox.Text = tech_desc[tech_clicked][subtech_clicked];
                        groupBox1.Visible = true;
                        groupBox1.Text = tech_subtech[tech_clicked][subtech_clicked];
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
                if (tech_clicked == Player.technologies[i][0] &&
                    subtech_clicked <= Player.technologies[i][1])
                {
                    tech_logic = false;
                    //break;
                }
                if (tech_clicked == Player.technologies[i][0] + 1 && subtech_clicked == 0 ||
                    tech_clicked == Player.technologies[i][0] && subtech_clicked <= Player.technologies[i][1] + 1)
                {
                    tech_logic2 = true;
                    //break;
                }
            }
            if (tech_logic == false)
            {
                MessageBox.Show("You alrady have this tech!");
            }
            else
            {

                if (tech_logic2 == true)
                {
                    Form1.SelfRef.tech_label.Visible = true;
                    Form1.SelfRef.tech_progressBar.Visible = true;
                    Form1.SelfRef.tech_label.Text = tech_subtech[tech_clicked][subtech_clicked];
                    Form1.SelfRef.tech_progressBar.Maximum = learning_tech_time;

                    Redraw();
                }
                else
                {
                    MessageBox.Show("Learn previos tech before!");
                }
            }

        }

        private void Tech_Tree_VisibleChanged(object sender, EventArgs e)
        {
            Redraw();
        }


    }
}
