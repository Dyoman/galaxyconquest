using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GalaxyConquest.StarSystems;
using GalaxyConquest.Tactics;

// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;

namespace GalaxyConquest
{
    [Serializable]
    public partial class Form1 : Form
    {
        public ModelGalaxy galaxy;
        public Bitmap galaxyBitmap;

        public double spinX = 0.0;
        public double spinY = Math.PI/4;

        public float scaling = 1f;
        public int horizontal = 0;  //for moving galaxy
        public int vertical = 0;    //for moving galaxy
        public float dynamicStarSize = 5; //Variable for dynamic of fix scale 

        public int star_selected;
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
        public SolidBrush GoldBrush = new SolidBrush(Color.Gold);

        public Player player = new Player();//contain player staff

        public static Form1 SelfRef         //need for get var from other classes
        {
            get;
            set;
        }

        public Form1()
        {
            InitializeComponent();
            SelfRef = this;
            tech_progressBar.Visible = false;
            tech_label.Visible = false;
            this.MouseWheel += new MouseEventHandler(this_MouseWheel); // for resizing of galaxy at event change wheel mouse
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
                galaxy.player = player;

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

                Random rand = new Random((int)DateTime.Now.Ticks);
                int planet_start;
                planet_start = rand.Next(galaxy.stars.Count);
                star_selected = planet_start;
                StarSystem ss = galaxy.stars[planet_start];

                if (nd.getGalaxyRandomEvents() == true)
                {
                    generate_random_events();
                }
                
                //флот игрока
                Fleet fl = generateFleet(rand.Next(5, 7), 1);

                player.player_stars.Add(galaxy.stars[rand.Next(0, galaxy.stars.Count - 1)]);


                player.player_fleets.Add(fl);
                fl.s1 = player.player_stars[0];
                player.player_fleets[0].x = player.player_fleets[0].s1.x;
                player.player_fleets[0].y = player.player_fleets[0].s1.y;
                player.player_fleets[0].z = player.player_fleets[0].s1.z;
                fl.name = nd.namePlayer;

                for (int i = 0; i < 3; i++)
                {
                    StarSystem sr = galaxy.stars[rand.Next(0, galaxy.stars.Count - 1)];
                    while (sr == player.player_stars[0])
                    {

                        sr = galaxy.stars[rand.Next(0, galaxy.stars.Count - 1)];
                    }
                    Fleet flneutrals = generateFleet(rand.Next(2, 4), 2);
                    flneutrals.s1 = sr;
                    flneutrals.name="Neutrals "+(i+1);
                    galaxy.neutrals.Add(flneutrals);
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

        public Fleet generateFleet(int size, int player)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            Fleet fleet = new Fleet();

            for (int i = 0; i < size; i++)
            {
                int ship_type = (rand.Next(0, 100))%2;
                int weapon_type = rand.Next(0, 2);

                Weapon weapon = null;
                Ship ship = null;
                switch (weapon_type)
                {
                    case 0: weapon = new wpnLightLaser(); break;
                    case 1: weapon = new WpnLightIon(); break;
                    case 2: weapon = new WpnHeavyLaser(); break;
                }

                switch (ship_type)
                {
                    case 0: ship = new ShipScout(player, weapon); break;
                    case 1: ship = new ShipAssaulter(player, weapon); break;
                }

                ship.player = player;
                fleet.ships.Add(ship);
            }

            return fleet;
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
            int r = 6;
            Pen pen = new Pen(Color.Gold);
            double ugol = 2 * Math.PI / (3);
            Point[] points = new Point[3];

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
            double screenZ;

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
                screenZ = tZ;

                starSize = s.type + dynamicStarSize;

#region oldcode
                //-------------------------------added
                /*Point[] compPointArrayShip = {  //точки для рисование корабля
                                    new Point((int)centerX + (int)ttX + Convert.ToInt32(r * Math.Cos(-1 * ugol)), (int)centerY + (int)ttY + Convert.ToInt32(r * Math.Sin(-1 * ugol))),
                                    new Point((int)centerX + (int)ttX + Convert.ToInt32(r * Math.Cos(-2 * ugol)), (int)centerY + (int)ttY + Convert.ToInt32(r * Math.Sin(-2 * ugol))),
                                    new Point((int)centerX + (int)ttX + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + (int)ttY + Convert.ToInt32(r * Math.Sin(-3 * ugol)))};
                g.FillPolygon(GoldBrush, compPointArrayShip);
                g.DrawString(player.name, new Font("Arial", 8.0F), Brushes.White, new Point((int)centerX-3 + (int)ttX + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY-12 + (int)ttY + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                */
                /*                for (int j = 0; j <= 3 - 1; j++)
                                {
                                    points[j].X = (int)centerX + (int)ttX + Convert.ToInt32(r * Math.Cos(-j * ugol));
                                    points[j].Y = (int)centerY + (int)ttY + Convert.ToInt32(r * Math.Sin(-j * ugol));
                                    g.FillEllipse(Brushes.Gold, points[j].X - 1, points[j].Y - 1, 1, 1);
                                }
                                for (int j = 0; j <= 3 - 1; j++)
                                {
                                    if (j + 1 + 1 <= 3) g.DrawLine(pen, points[j].X, points[j].Y, points[j + 1].X, points[j + 1].Y);
                                    else g.DrawLine(pen, points[j].X, points[j].Y, points[j + 1 - 3].X, points[j + 1 - 3].Y);
                                }
                                if (star_selected != galaxy.stars.Count-1 & star_selected != 0)
                                {
                                    if (s == galaxy.stars[star_selected - 1] | s == galaxy.stars[star_selected + 1])
                                    {
                                        g.FillEllipse(Brushes.Pink, centerX - 1 + (int)screenX - starSize / 2, centerY - 1 + (int)screenY - starSize / 2, starSize + 2, starSize + 2);
                                    }
                                }
                                else if (star_selected == galaxy.stars.Count-1)
                                {
                                    if (s == galaxy.stars[star_selected - 1])
                                    {
                                        g.FillEllipse(Brushes.Pink, centerX - 1 + (int)screenX - starSize / 2, centerY - 1 + (int)screenY - starSize / 2, starSize + 2, starSize + 2);
                                    }
                                }

                                else if (star_selected == 0)
                                {
                                    if (s == galaxy.stars[star_selected + 1])
                                    {
                                        g.FillEllipse(Brushes.Pink, centerX - 1 + (int)screenX - starSize / 2, centerY - 1 + (int)screenY - starSize / 2, starSize + 2, starSize + 2);
                                    }
                                }
                */
                #endregion

                Rectangle rectan = new Rectangle((int)(centerX - 5 + (int)screenX - starSize / 2), (int)(centerY - 5 + (int)screenY - starSize / 2), (int)(starSize + 11), (int)(starSize + 11));

                if (s == galaxy.stars[star_selected]) 
                {
                      g.DrawEllipse(pen, rectan);
                }

                rectan = new Rectangle((int)(centerX - 4 + (int)screenX - starSize / 2), (int)(centerY - 4 + (int)screenY - starSize / 2), (int)(starSize + 9), (int)(starSize + 9));
                
                if (player.player_stars.Contains(s))
                {
                    g.DrawEllipse(Pens.Red, rectan);
                }
                
                if (player.player_fleets[0].s1 == s)
                {
                    double screenXfl = player.player_fleets[0].x * Math.Cos(spinX) - player.player_fleets[0].z * Math.Sin(spinX)-10;
                    double screenZfl = player.player_fleets[0].x * Math.Sin(spinX) + player.player_fleets[0].z * Math.Cos(spinX);
                    double screenYfl = player.player_fleets[0].y * Math.Cos(spinY) - screenZfl * Math.Sin(spinY)-10; 
                    Point[] compPointArrayShip = {  //точки для рисование корабля
                                    new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-1 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-1 * ugol))),
                                    new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-2 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-2 * ugol))),
                                    new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol)))};
                    g.FillPolygon(GoldBrush, compPointArrayShip);
                    g.DrawString(player.player_fleets[0].name, new Font("Arial", 8.0F), Brushes.White, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                }


                for (int k = 0; k < galaxy.neutrals.Count; k++)
                {
                    if (s == galaxy.neutrals[k].s1)
                    {
                        int screenXfl = (int)screenX - 10;

                        int screenYfl = (int)screenY - 10;
                        Point[] compPointArrayShip = {  //точки для рисование корабля
                                    new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-1 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-1 * ugol))),
                                    new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-2 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-2 * ugol))),
                                    new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol)))};
                        g.FillPolygon(Brushes.Lime, compPointArrayShip);
                        g.DrawString(galaxy.neutrals[k].name, new Font("Arial", 8.0F), Brushes.White, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                    }
                }

                g.FillEllipse(s.br, centerX + (int)screenX - starSize / 2, centerY + (int)screenY - starSize / 2, starSize, starSize);
                
                g.DrawString(i.ToString(), new Font("Arial", 8.0F), Brushes.White, new PointF(centerX + (int)screenX, centerY + (int)screenY));
                
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
            for (int i = 0; i < galaxy.stars.Count/10; i++) // (1/10) of all stars
            {
                next = rand.Next(galaxy.stars.Count);       //random star from all stars
                galaxy.stars[next].name = "super nova";     //name for new star
                galaxy.stars[next].type = 8;                //type for "super nova"
                galaxy.stars[next].br = SuperWhiteBrush;    //brush for "super nova"
            }
        }

        public void generatePlanets(StarSystem s)
        {
            int sizemin = 5;
            int sizemax = 15;
            int popmin = 0;
            int popmax = 10;
            int mineralmin = 0;
            int mineralmax = 35;
            int colormin = 0;
            int colormax = 255;
            int dist = 50;
            float speed = 0.001f;

            if (s.PLN.Count > 0)
            {
                throw new Exception("Планеты уже есть");
            }

            Random r = new Random(DateTime.Now.Millisecond);

            int planets_count = s.type + 1 + (-1 + r.Next(0,2));

            s.PLN.Add(new PLANET());
            s.PLN[0].CENTER = new Point(190, 190);
            s.PLN[0].DISTANCE = 0;
            s.PLN[0].SPEED = 0;
            s.PLN[0].CLR = s.br.Color;
            s.PLN[0].SIZE = 25;
            s.PLN[0].NAME = "STAR";
            s.PLN[0].POPULATION = 0;
            s.PLN[0].MINERALS = 0;

            for (int i = 1; i <= planets_count; i++)
            {
                s.PLN.Add(new PLANET());
                s.PLN[i].CENTER = new Point(s.PLN[0].GetPoint().X, s.PLN[0].GetPoint().Y);
                s.PLN[i].DISTANCE = dist;
                s.PLN[i].SPEED = speed;
                s.PLN[i].CLR = Color.FromArgb((r.Next(colormin, colormax)), (r.Next(colormin, colormax)), (r.Next(colormin, colormax)));
                s.PLN[i].SIZE = r.Next(sizemin, sizemax);
                s.PLN[i].NAME = "Planet " + i.ToString();
                s.PLN[i].POPULATION = r.Next(popmin, popmax);
                s.PLN[i].MINERALS = r.Next(mineralmin, mineralmax);
                dist = dist + 25;
                speed = speed / 3 + 0.0001f;
            }
        }

        public void generate_spiral_galaxy(bool rotate, int galaxysize, int starscount)
        {
            Double x;
            Double y;
            Double r;           //radius
            Double t;           //rotate angle
            Random rand = new Random();

                r = 0;
                t = 0;
                for (int i = 0; i < starscount/2; i++)
                {
                    r += rand.Next(4) + 10 * (galaxysize+1);
                    t += 0.2;

                    x = r * Math.Cos(t) + rand.Next(5 * (galaxysize + 1));
                    y = r * Math.Sin(t) + rand.Next(5 * (galaxysize + 1));

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
                    s.name = (i+1).ToString();

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

                    generatePlanets(s);

                    galaxy.stars.Add(s);
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

            for (int j = 0; j < (starscount/2); j++)
            {
                r = 0;
                t = Math.PI;
                for (int i = 0; i < 2; i++)
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
                    s.planets_count = s.type + 1;
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

            for (int j = 0; j < (starscount / 2); j++)
            {
                r = 0;
                t = 0;
                for (int i = 0; i < 2; i++)
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
                    s.planets_count = s.type + 1;
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
            for (int j = 0; j < starscount/2; j++)
            {
                r = 0;
                t += 5;
                for (int i = 0; i < 2; i++)
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
                    s.planets_count = s.type + 1;
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

        //mouse down listener
        private void galaxyImage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;   //start x
            mouseY = e.Y;   //start y
        }

        //mouse move listener
        private void galaxyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (galaxy != null)
            {
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
                    if (e.X / scaling > (centerX + (int)screenX - starSize / 2) &&
                        e.X / scaling < (centerX + (int)screenX + starSize / 2) &&
                        e.Y / scaling > (centerY + (int)screenY - starSize / 2) &&
                        e.Y / scaling < (centerY + (int)screenY + starSize / 2))
                    {
                        textBox_planets.Text = s.planets_count.ToString();
                        return;
                    }


                }

            }

            //---------------------

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
            tt.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void galaxyImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
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
                if (e.X / scaling > (centerX + (int)screenX - starSize / 2) &&
                    e.X / scaling < (centerX + (int)screenX + starSize / 2) &&
                    e.Y / scaling > (centerY + (int)screenY - starSize / 2) &&
                    e.Y / scaling < (centerY + (int)screenY + starSize / 2))
                {
                    //if mouse clicked in the ellipce open new form
                    star_selected = j;//store type for selected star

                    StarSystemForm ssm = new StarSystemForm(galaxy.stars[j]);
                    ssm.ShowDialog();



                    return;
                }

            }
        }

        //-------------------------------added
        private void galaxyImage_MouseClick(object sender, MouseEventArgs e)
        {
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
                if (e.X / scaling > (centerX + (int)screenX - starSize / 2) &&
                    e.X / scaling < (centerX + (int)screenX + starSize / 2) &&
                    e.Y / scaling > (centerY + (int)screenY - starSize / 2) &&
                    e.Y / scaling < (centerY + (int)screenY + starSize / 2))
                {
                    //if mouse clicked in the ellipce open new form
                    if (conquer_progressBar.Visible == false)
                    {
                        star_selected = j;//store type for selected star
                    }

                    Redraw();
                    return;
                }

            }
        }
        //-------------------------------added

        void this_MouseWheel(object sender, MouseEventArgs e) // resizing of galaxy at event change wheel mouse
        {
            if (e.Delta > 0)
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
               
            else
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
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            vertical = vScrollBar1.Value;
            Redraw();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            horizontal = hScrollBar1.Value;
            Redraw();
        }

        private void galaxyImage_Click(object sender, EventArgs e)
        {

        }
        public void animationFleets(Fleet fl, StarSystem stars)
        {
            double x = stars.x - fl.x;
            double y = stars.y - fl.y;
            double z = stars.z - fl.z;
            double dx = x / 100;
            double dy = y / 100;
            double dz = z / 100;

            for (int i = 0; i < 100; i++)
            {
                fl.x += dx;
                fl.y += dy;
                fl.z += dz;
                Redraw();
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            StarSystem s = galaxy.stars[star_selected];



            if (conquer_progressBar.Visible == true)
            {
                if (conquer_progressBar.Value == conquer_progressBar.Maximum - 1)
                {
                    player.player_stars.Add(galaxy.stars[star_selected]);
                    conquer_progressBar.Visible = false;
                    button3.Visible = false;
                    conquer_progressBar.Value = conquer_progressBar.Minimum;
                }
                else
                {
                    conquer_progressBar.Value = conquer_progressBar.Value + 1;
                }
            }
            else
            {
                animationFleets(player.player_fleets[0], s);
            }
                                  
            for (int k = 0; k < galaxy.neutrals.Count; k++)
            {
                if (galaxy.neutrals[k].s1 == s)
                {
                    //MessageBox.Show("Обнаружен противник!");

                    CombatForm cf = new CombatForm(player.player_fleets[0], galaxy.neutrals[k]);
                    cf.ShowDialog();
                }
            }
            Redraw();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conquer_progressBar.Visible = true;
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conquer_progressBar.Visible = false;
            conquer_progressBar.Value = conquer_progressBar.Minimum;
            button3.Visible = false;
        }

        


    }
}
