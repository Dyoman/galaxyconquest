using System;
using System.Drawing;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows;

namespace GalaxyConquest
{
    public partial class Form1 : Form
    {
        public ModelGalaxy galaxy;
        public Bitmap galaxyBitmap;

        public double spinX = 0.0;
        public double spinY = 0.0;

        public float scaling = 1f;
        public int horizontal = 0;  //for moving galaxy
        public int vertical = 0;    //for moving galaxy
        public float dynamicStarSize = 5; //Variable for dynamic of fix scale 

        public int mouseX;
        public int mouseY;
        //Brushes for stars colors
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(255,123,104,238));
        public SolidBrush LightBlueBrush = new SolidBrush(Color.FromArgb(180,135,206,235));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255,225,250,240));
        public SolidBrush LightYellowBrush = new SolidBrush(Color.FromArgb(180,255,255,0));
        public SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255,255,255,0));
        public SolidBrush OrangeBrush = new SolidBrush(Color.FromArgb(255,255,140,0));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255,255,0,0));
        public SolidBrush SuperWhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 0));

        public Form1()
        {
            InitializeComponent();
            // инициализация Glut 
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
        }

        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = new Size(this.Width,this.Height);
           
            }
            
        }
        private void mainMenuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainMenuNew_Click(object sender, EventArgs e)
        {
            Form_NewGameDialog nd = new Form_NewGameDialog();
            if (nd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                galaxy = new ModelGalaxy();
                galaxy.name = "Млечный путь";
                switch (nd.getGalaxyType())
                {
                    case 0:
                        generate_spiral_galaxy(true, nd.getGalaxySize(), nd.getStarsCount());
                        generate_spiral_galaxy(false, nd.getGalaxySize(), nd.getStarsCount());
                        break;
                    case 1:
                        generate_elliptical_galaxy(true, nd.getGalaxySize(), nd.getStarsCount());
                        generate_elliptical_galaxy(false, nd.getGalaxySize(), nd.getStarsCount());
                        break;
                    case 2:
                        generate_irregular_galaxy(true, nd.getGalaxySize(), nd.getStarsCount());
                        break;
                    case 3:
                        generate_sphere_galaxy(true, nd.getGalaxySize(), nd.getStarsCount());
                        break;
                }
                if (nd.getGalaxyRandomEvents() == true)
                {
                    generate_random_events();
                }
                Redraw();
            }

#region someoldcode
            /*
            Random r = new Random();

            for (int i = 0; i < 100; i++)
            {

                StarSystem s = new StarSystem();
                s.name = "Солнце_" + i.ToString();
                s.type = 1;
                s.x = 200.0 - 400.0 * r.NextDouble();
                s.y = 200.0 - 400.0 * r.NextDouble();
                s.z = 200.0 - 400.0 * r.NextDouble();

                galaxy.stars.Add(s);
            }

            */
            /*
            galaxy = new ModelGalaxy();
            galaxy.name = "Млечный путь";

            StarSystem s = new StarSystem();
            s.name = "Солнце";
            s.type = 1;
            s.x = 50.0;
            s.y = 50.0;
            s.z = 50.0;

            galaxy.stars.Add(s);

            s = new StarSystem();
            s.name = "Альфа Центавра";
            s.type = 1;
            s.x = 50.0;
            s.y = 50.0;
            s.z = -50.0;

            galaxy.stars.Add(s);


            s = new StarSystem();
            s.name = "Бетельгейзе";
            s.type = 1;
            s.x = 50.0;
            s.y = -50.0;
            s.z = 50.0;

            galaxy.stars.Add(s);


            s = new StarSystem();
            s.name = "Бетельгейзе";
            s.type = 1;
            s.x = -50.0;
            s.y = 50.0;
            s.z = 50.0;
            galaxy.stars.Add(s);

            s = new StarSystem();
            s.name = "Бетельгейзе";
            s.type = 1;
            s.x = -50.0;
            s.y = -50.0;
            s.z = 50.0;
            galaxy.stars.Add(s);

            s = new StarSystem();
            s.name = "Бетельгейзе";
            s.type = 1;
            s.x = -50.0;
            s.y = 50.0;
            s.z = -50.0;
            galaxy.stars.Add(s);


            s = new StarSystem();
            s.name = "Бетельгейзе";
            s.type = 1;
            s.x = 50.0;
            s.y = -50.0;
            s.z = -50.0;
            galaxy.stars.Add(s);

            s = new StarSystem();
            s.name = "Бетельгейзе";
            s.type = 1;
            s.x = -50.0;
            s.y = -50.0;
            s.z = -50.0;
            galaxy.stars.Add(s);

            StarWarp w = new StarWarp();
            w.name = "warp1";
            w.type = 1;
            w.system1 = galaxy.stars[0];
            w.system2 = galaxy.stars[2];

            galaxy.lanes.Add(w);
             */
#endregion
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            Redraw();
        }

        private void buttonSpinLeft_Click(object sender, EventArgs e)
        {
            spinX -= 0.2;
            Redraw();
        }

        private void buttonSpinRight_Click(object sender, EventArgs e)
        {
            spinX += 0.2;
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
                if (dynamicStarSize >= 3)
                {
                    dynamicStarSize -= 0.4f;
                }
                else if (dynamicStarSize >= 2)
                {
                    dynamicStarSize -= 0.05f;
                }
                else if (dynamicStarSize >= 0)
                {
                    dynamicStarSize -= 0.01f;
                }
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
                if (dynamicStarSize <= 2)
                {
                    dynamicStarSize += 0.01f;
                }
                else if (dynamicStarSize <= 3)
                {
                    dynamicStarSize += 0.05f;
                }
                else if (dynamicStarSize <= 5)
                {
                    dynamicStarSize += 0.4f;
                }
                Redraw();
            }
        }

        private void buttonMoveRight_Click(object sender, EventArgs e)
        {
            horizontal += 5;
            Redraw();
        }

        private void buttonMoveLeft_Click(object sender, EventArgs e)
        {
            horizontal -= 5;
            Redraw();
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            vertical -= 5;
            Redraw();
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            vertical += 5;
            Redraw();
        }

        public void Redraw()
        {
            if (galaxy == null)
            {
                MessageBox.Show("Error occured :`(\n\n'Nothing to draw'", "Draw Galaxy", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            galaxyBitmap = new Bitmap(galaxyImage.Width, galaxyImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(galaxyBitmap);

            g.FillRectangle(Brushes.Black, 0, 0, galaxyBitmap.Width, galaxyBitmap.Height);
            g.DrawString(galaxy.name, new Font("Arial", 10.0F), Brushes.White, new PointF(1.0F, 1.0F));

            g.ScaleTransform(scaling, scaling);//resize image(zooming in/out)
          
            float centerX = galaxyBitmap.Width / 2 / scaling;
            float centerY = galaxyBitmap.Height / 2 / scaling;

            centerX += horizontal;  //move galaxy left/right
            centerY += vertical;    //move galaxy up/down

            float starSize = 0;

            double screenX;
            double screenY;

            //рисуем звездные системы
            for (int i = 0; i < galaxy.stars.Count; i++)
            {
                StarSystem s = galaxy.stars[i];

                double tX, tY, tZ;

                tX = s.x * Math.Cos(spinX) - s.z * Math.Sin(spinX);
                tZ = s.x * Math.Sin(spinX) + s.z * Math.Cos(spinX);
                tY = s.y * Math.Cos(spinY) - tZ * Math.Sin(spinY);
                screenX = tX;
                screenY = tY;

                starSize = s.type + dynamicStarSize;
                
                g.FillEllipse(s.br, centerX + (int)screenX - starSize / 2, centerY + (int)screenY - starSize / 2, starSize, starSize);
                g.DrawString(s.name, new Font("Arial", 8.0F), Brushes.White, new PointF(centerX + (int)screenX, centerY + (int)screenY));
                
            }

            //рисуем гиперпереходы
            for (int i = 0; i < galaxy.lanes.Count; i++)
            {
                StarWarp w = galaxy.lanes[i];

                g.DrawLine(Pens.Gray,
                    new Point(((int)centerX + (int)w.system1.x), ((int)centerY + (int)w.system1.y)),
                    new Point(((int)centerX + (int)w.system2.x), ((int)centerY + (int)w.system2.y)));
            }

            galaxyImage.Image = galaxyBitmap;
            galaxyImage.Refresh();
        }

        private void buttonSpinDown_Click(object sender, EventArgs e)
        {
            spinY -= 0.2;
            Redraw();
        }

        private void buttonSpinUp_Click(object sender, EventArgs e)
        {
            spinY += 0.2;
            Redraw();
        }

        private void mainMenuSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sDlg = new SaveFileDialog();
            if (sDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = sDlg.FileName;

                FileStream fs = new FileStream(fileName, FileMode.CreateNew);

                
                //XmlSerializer xs = new XmlSerializer(typeof(ModelGalaxy));
                //xs.Serialize(fs, galaxy);                
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, galaxy);

                fs.Close();
            }
        }

        private void mainMenuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog sDlg = new OpenFileDialog();
            if (sDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = sDlg.FileName;

                FileStream fs = new FileStream(fileName, FileMode.Open);

                //XmlSerializer xs = new XmlSerializer(typeof(ModelGalaxy));
                //xs.Serialize(fs, galaxy);                
                BinaryFormatter bf = new BinaryFormatter();
                galaxy = (ModelGalaxy)bf.Deserialize(fs);

                fs.Close();

                Redraw();
            }

        }

        private void mainMenuAbout_Click(object sender, EventArgs e)
        {
            Form_About af = new Form_About();
            af.ShowDialog();
        }

        public void generate_random_events()
        {
            int next;
            Random rand = new Random();
            for (int i = 0; i < galaxy.stars.Count/20; i++) // (1/20) of all stars
            {
                next = rand.Next(galaxy.stars.Count);       //random star from all stars
                galaxy.stars[next].name = "super nova";
                galaxy.stars[next].type = 8;                //type for "super nova"
                galaxy.stars[next].br = SuperWhiteBrush;    //brush for "super nova"
            }
        }

        public void generate_spiral_galaxy(bool rotate, int galaxysize, int starscount)
        {
            Double x;
            Double y;
            Double z = 0;
            Double r;           //radius
            Double t;           //space beetwen stars
            Double curve = 0;   //statching spirals
            Random rand = new Random();

            //we got 40 times for inner loop, so we need divide all stars coun for 40
            //and we have 2 spirals, so divide agan dy 40
            //as result (starscount/80)
            for (int j = 0; j < (starscount/80); j++)
            {
                r = 0;
                t = 0;
                for (int i = 0; i < 40; i++)
                {
                    r += rand.Next(4) + 2 + galaxysize;
                    curve = Math.Pow((r - 4), 2);
                    curve = curve / 150;
                    t += 0.2;

                    z = t + (rand.NextDouble() - 0.5) * 2;
                    x = curve * Math.Cos(z) + rand.Next(30) - 15;
                    y = curve * Math.Sin(z) + rand.Next(30) - 15;

                    if (rotate == true)
                    {
                        x = -x;
                        y = -y;
                    }

                    StarSystem s = new StarSystem();
                    s.x = x;
                    s.y = -5.0 + rand.NextDouble() * 10.0;
                    s.z = y;
                    s.type = rand.Next(7);  //type impact on size and color
                    s.name = "";
                    switch (s.type)
                    {
                        //O - Blue, t =30 000 — 60 000 K
                        case 0:
                            s.br = BlueBrush;
                        break;

                        //B - Light blue, t = 10 500 — 30 000 K
                        case 1:
                            s.br = LightBlueBrush;
                        break;

                        //A - White, t = 7500—10 000 K
                        case 2:
                            s.br = WhiteBrush;
                        break;

                        //F - Light Yellow, t = 6000—7200 K
                        case 3:
                            s.br = LightYellowBrush;
                        break;

                        //G - Yellow, t = 5500 — 6000 K
                        case 4:
                            s.br = YellowBrush;
                        break;

                        //K - Orange, t = 4000 — 5250 K
                        case 5:
                            s.br = OrangeBrush;
                        break;

                        //M - Red, t = 2600 — 3850 K
                        case 6:
                            s.br = RedBrush;
                        break;
                    }
                    galaxy.stars.Add(s);
                }
            }
        }

        public void generate_elliptical_galaxy(bool rotate, int galaxysize, int starscount)
        {
            Double x;
            Double y;
            Double r;
            Double t;
            Double z = 0;
            Double curve = 0;
            Random rand = new Random();

            for (int j = 0; j < (starscount/80); j++)
            {
                r = 0;
                t = Math.PI;
                for (int i = 0; i < 40; i++)
                {
                    r += rand.Next(4) + 2 + galaxysize;
                    curve = Math.Pow((r - 2), 2);
                    curve = curve / 150;

                    z = t + (rand.NextDouble() - 0.5) * Math.PI;
                    x = 1.5*curve * Math.Cos(z) + rand.Next(30) - 15;
                    y = curve * Math.Sin(z) + rand.Next(30) - 15;

                    if (rotate == true)
                    {
                        x = -x;
                        y = -y;
                    }

                    StarSystem s = new StarSystem();
                    s.x = x;
                    s.y = -10.0 + rand.NextDouble() * 20.0;
                    s.z = y;
                    s.type = rand.Next(7);  //type impact on size and color
                    s.name = "";
                    switch (s.type)
                    {
                        //O - Blue, t =30 000 — 60 000 K
                        case 0:
                            s.br = BlueBrush;
                            break;

                        //B - Light blue, t = 10 500 — 30 000 K
                        case 1:
                            s.br = LightBlueBrush;
                            break;

                        //A - White, t = 7500—10 000 K
                        case 2:
                            s.br = WhiteBrush;
                            break;

                        //F - Light Yellow, t = 6000—7200 K
                        case 3:
                            s.br = LightYellowBrush;
                            break;

                        //G - Yellow, t = 5500 — 6000 K
                        case 4:
                            s.br = YellowBrush;
                            break;

                        //K - Orange, t = 4000 — 5250 K
                        case 5:
                            s.br = OrangeBrush;
                            break;

                        //M - Red, t = 2600 — 3850 K
                        case 6:
                            s.br = RedBrush;
                            break;
                    }
                    galaxy.stars.Add(s);
                }
            }
        }

        public void generate_irregular_galaxy(bool rotate, int galaxysize, int starscount)//fix
        {
            Double x;
            Double y;
            Double r;
            Double t;
            Double z = 0;
            Double curve = 0;
            Random rand = new Random();

            for (int j = 0; j < (starscount / 80); j++)
            {
                r = 0;
                t = 0;
                for (int i = 0; i < 40; i++)
                {
                    r += rand.Next(4) + 2 + galaxysize;
                    curve = Math.Pow((r - 2), 2);
                    curve = curve / 150;

                    t += 0.2;
                    z = t + (rand.NextDouble() - 0.5) * 2;
                    x = curve + rand.Next(30) - 15;
                    y = curve * Math.Sin(z) + rand.Next(100) - 15;

                    StarSystem s = new StarSystem();
                    s.x = x;
                    s.y = -10.0 + rand.NextDouble() * 20.0;
                    s.z = y;
                    s.type = rand.Next(7);  //type impact on size and color
                    s.name = "";
                    switch (s.type)
                    {
                        //O - Blue, t =30 000 — 60 000 K
                        case 0:
                            s.br = BlueBrush;
                            break;

                        //B - Light blue, t = 10 500 — 30 000 K
                        case 1:
                            s.br = LightBlueBrush;
                            break;

                        //A - White, t = 7500—10 000 K
                        case 2:
                            s.br = WhiteBrush;
                            break;

                        //F - Light Yellow, t = 6000—7200 K
                        case 3:
                            s.br = LightYellowBrush;
                            break;

                        //G - Yellow, t = 5500 — 6000 K
                        case 4:
                            s.br = YellowBrush;
                            break;

                        //K - Orange, t = 4000 — 5250 K
                        case 5:
                            s.br = OrangeBrush;
                            break;

                        //M - Red, t = 2600 — 3850 K
                        case 6:
                            s.br = RedBrush;
                            break;
                    }
                    galaxy.stars.Add(s);
                }
            }
        }

        public void generate_sphere_galaxy(bool rotate, int galaxysize, int starscount)
        {
            Double x;
            Double y;
            Double z = 1;
            Double r;
            Double t;
            Double tX;
            Double tY;
            Double tZ;

            Random rand = new Random();
            t = 0;
            for (int j = 0; j < starscount/40; j++)
            {
                r = 0;
                t += 5;
                for (int i = 0; i < 40; i++)
                {
                    r += 1;

                    x = Math.Cos(r) * 100 * (galaxysize + 1);
                    y = Math.Sin(r) * 100 * (galaxysize + 1);

                    tX = x * Math.Cos(t) + z * Math.Sin(t);
                    tZ = x * Math.Sin(t) - z * Math.Cos(t);
                    tY = y * Math.Cos(t) + tZ * Math.Sin(t);

                    StarSystem s = new StarSystem();
                    s.x = tX;
                    s.y = tZ;
                    s.z = tY;
                    s.type = rand.Next(7);  //type impact on size and color
                    s.name = "";
                    switch (s.type)
                    {
                        //O - Blue, t =30 000 — 60 000 K
                        case 0:
                            s.br = BlueBrush;
                            break;

                        //B - Light blue, t = 10 500 — 30 000 K
                        case 1:
                            s.br = LightBlueBrush;
                            break;

                        //A - White, t = 7500—10 000 K
                        case 2:
                            s.br = WhiteBrush;
                            break;

                        //F - Light Yellow, t = 6000—7200 K
                        case 3:
                            s.br = LightYellowBrush;
                            break;

                        //G - Yellow, t = 5500 — 6000 K
                        case 4:
                            s.br = YellowBrush;
                            break;

                        //K - Orange, t = 4000 — 5250 K
                        case 5:
                            s.br = OrangeBrush;
                            break;

                        //M - Red, t = 2600 — 3850 K
                        case 6:
                            s.br = RedBrush;
                            break;
                    }
                    galaxy.stars.Add(s);
                }
            }

            //MessageBox.Show(galaxy.stars.Count.ToString(), "Draw Galaxy", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        //mouse down listener
        private void galaxyImage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;   //start x
            mouseY = e.Y;   //start y
        }

        //mouse move listener
        private void galaxyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = mouseX - e.X;
                int dy = mouseY - e.Y;
                if (dx > 0)
                {
                    horizontal -= 5;
                }
                if (dx < 0)
                {
                    horizontal += 5;
                }
                if (dy > 0)
                {
                    vertical -= 5;
                }
                if (dy < 0)
                {
                    vertical += 5;
                }
                mouseX = e.X;   //set start x again
                mouseY = e.Y;   //set start y again
                Redraw();
            }
        }

        private void MainMenuTechTree_Click(object sender, EventArgs e)
        {
            Tech_Tree tt = new Tech_Tree();
            tt.Show();
        }

        private void dModelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_3d pl = new Form_3d();
            pl.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void galaxyImage_MouseClick(object sender, MouseEventArgs e)
        {
            planets pl = new planets();

            for (int j = 0; j < galaxy.stars.Count; j++)
            {
                //all need to calculate the real x,y of star on the screen
                //(s.x ~ 10 to 30) but the real position x on the screen is ~ 100 to 600
                //--------------------------------------//
                StarSystem s = galaxy.stars[j];

                double screenX;
                double screenY;
                double tX, tY, tZ;
                double starSize;

                float centerX = galaxyBitmap.Width / 2 / scaling;
                float centerY = galaxyBitmap.Height / 2 / scaling;

                centerX += horizontal;  //move galaxy left/right
                centerY += vertical;    //move galaxy up/down

                tX = s.x * Math.Cos(spinX) - s.z * Math.Sin(spinX);
                tZ = s.x * Math.Sin(spinX) + s.z * Math.Cos(spinX);
                tY = s.y * Math.Cos(spinY) - tZ * Math.Sin(spinY);

                screenX = tX;
                screenY = tY;

                starSize = s.type + dynamicStarSize;

                //--------------------------------------//

                //check for mouse in the star ellipce
                if (e.X/scaling > (centerX + (int)screenX - starSize / 2) &&
                    e.X/scaling < (centerX + (int)screenX + starSize / 2) &&
                    e.Y/scaling > (centerY + (int)screenY - starSize / 2) &&
                    e.Y/scaling < (centerY + (int)screenY + starSize / 2))
                {
                    //if mouse clicked in the ellipce open new form
                    pl.ShowDialog();
                    return;
                }

            }
        }


    }
}
