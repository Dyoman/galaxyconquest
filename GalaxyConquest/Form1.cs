﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GalaxyConquest.StarSystems;
using GalaxyConquest.Tactics;
using NAudio;
using NAudio.Wave;

// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;

namespace GalaxyConquest
{
    [Serializable]
    public partial class Form1 : Form
    {
        //-----added
        public static Int64 Time = 3000;
        //Цвета для флотов
        public static Brush activeFleetColor = Brushes.GreenYellow;
        public static Brush passiveFleetColor = Brushes.Aqua;
        public static Brush neutralFleetColor = Brushes.WhiteSmoke;
        public static Brush enemyFleetColor = Brushes.OrangeRed;

        private int dispersion = 7;    //Размер выделяемой области вокруг звезды


        private float centerX;
        private float centerY;
        //-------------

        public ModelGalaxy galaxy;
        public Bitmap galaxyBitmap;
        public static double credit = 0;
        public double spinX = 0.0;
        public double spinY = Math.PI / 4;

        public float scaling = 1f;
        //---edited
        public float horizontal = 0;  //для увеличения плавности прокручивания стал float (был int)
        public float vertical = 0;    //

        public float dynamicStarSize = 5; //Variable for dynamic of fix scale 
        public int selectFleet = 0;
        public double starDistanse;
        public double maxDistanse = 150;
        public double s2x, s2y, s2z;
        public double warp;

        public static int star_selected; ////-----------------попытаюсь заменить на ссылку на объект звезды
        public static StarSystem selectedStar;  ///----------added

        public int mouseX;
        public int mouseY;
        //Brushes for stars colors
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(255, 123, 104, 238));
        public SolidBrush LightBlueBrush = new SolidBrush(Color.FromArgb(180, 135, 206, 235));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 225, 250, 240));
        public SolidBrush LightYellowBrush = new SolidBrush(Color.FromArgb(180, 255, 255, 0));
        public SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
        public SolidBrush OrangeBrush = new SolidBrush(Color.FromArgb(255, 255, 140, 0));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
        public SolidBrush SuperWhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
        public SolidBrush GoldBrush = new SolidBrush(Color.Gold);

        public static Shop shop_form;

        public Player player = new Player();//contain player staff
        public Tech_Tree tt = new Tech_Tree();
        IWavePlayer waveOutDevice;
        AudioFileReader audioFileReader;

        public static Form1 SelfRef         //need for get var from other classes
        {
            get;
            set;
        }

        public Form1()
        {
            InitializeComponent();
            shop_form = new Shop();
            Buildings builds = new Buildings();
            SelfRef = this;
            tech_progressBar.Visible = false;
            tech_label.Visible = false;
            this.MouseWheel += new MouseEventHandler(this.this_MouseWheel); // for resizing of galaxy at event change wheel mouse
            waveOutDevice = new WaveOutEvent();
            audioFileReader = new AudioFileReader("Sounds/Untitled45.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            statusStrip1.Items[0].Text = "Выбран 1 флот";
            listView.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            centerX = galaxyImage.Width / 2;
            centerY = galaxyImage.Height / 2;

            dateLabel.Text = Time.ToString() + " г.н.э.";
        }

        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = new Size(this.Width, this.Height);

            }

        }

        #region старый метод. Всё перенесено в обработку события Paint
        /*
        public void Redraw()
        {
            if (galaxy == null)
            {
                MessageBox.Show("Error occured :`(\n\n'Nothing to draw'", "Draw Galaxy", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            int r = 6;
            Pen pen = new Pen(Color.Gold);
            double ugol = 2 * Math.PI / (3);
            Point[] points = new Point[3];

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

                Rectangle rectan = new Rectangle((int)(centerX - 5 + (int)screenX - starSize / 2), (int)(centerY - 5 + (int)screenY - starSize / 2), (int)(starSize + 11), (int)(starSize + 11));

                if (s == selectedStar)       //if (s == galaxy.stars[star_selected])
                {
                    g.DrawEllipse(pen, rectan);
                }

                rectan = new Rectangle((int)(centerX - 4 + (int)screenX - starSize / 2), (int)(centerY - 4 + (int)screenY - starSize / 2), (int)(starSize + 9), (int)(starSize + 9));

                if (player.player_stars.Contains(s))
                {
                    g.DrawEllipse(Pens.GreenYellow, rectan);
                }

                //----------------------Player Fleets----------------------
                for (int k = 0; k < player.player_fleets.Count; k++)
                {
                    Fleet fleet = player.player_fleets[k];
                    StarSystem playerSys = fleet.s1;
                    StarSystem targSys = fleet.s2;

                    if (playerSys != null)
                    {

                    }

                    if (fleet.s1 == s)
                    {
                        double screenXfl = fleet.x * Math.Cos(spinX) - fleet.z * Math.Sin(spinX) - 10;
                        double screenZfl = fleet.x * Math.Sin(spinX) + fleet.z * Math.Cos(spinX);
                        double screenYfl = fleet.y * Math.Cos(spinY) - screenZfl * Math.Sin(spinY) - 10;
                        Point[] compPointArrayShip = {  //точки для рисование корабля
                                        new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-1 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-1 * ugol))),
                                        new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-2 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-2 * ugol))),
                                        new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol)))};
                        //g.FillPolygon(GoldBrush, compPointArrayShip); //---old
                        if (k == selectFleet)
                        {
                            g.FillPolygon(activeFleetColor, compPointArrayShip);
                            g.DrawString(fleet.name, new Font("Arial", 8.0F), activeFleetColor, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));

                        }
                        else
                        {
                            g.FillPolygon(passiveFleetColor, compPointArrayShip);
                            g.DrawString(fleet.name, new Font("Arial", 8.0F), passiveFleetColor, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                        }

                        if (warp == 1 && k == selectFleet && player.player_fleets[selectFleet].starDistanse == 0)
                        {
                            //pen.Color = Color.Red;
                            //pen.DashStyle = DashStyle.Dash; 
                            //rectan = new Rectangle((int)(centerX - 150 + (int)screenX - starSize / 2), (int)(centerY - 150 + (int)screenY - starSize / 2), (int)(starSize + 300), (int)(starSize + 300));
                            //g.DrawEllipse(pen, rectan);
                            string dis = ((int)starDistanse).ToString();//+-

                            double ts2X, ts2Y, ts2Z;
                            double screens2X;
                            double screens2Y;
                            double screens2Z;


                            ts2X = s2x * Math.Cos(spinX) - s2z * Math.Sin(spinX);
                            ts2Z = s2x * Math.Sin(spinX) + s2z * Math.Cos(spinX);
                            ts2Y = s2y * Math.Cos(spinY) - ts2Z * Math.Sin(spinY);

                            screens2X = ts2X;
                            screens2Y = ts2Y;
                            screens2Z = ts2Z;

                            if (starDistanse < maxDistanse)
                            {
                                pen.Color = Color.Lime;
                                g.DrawString(dis, new Font("Arial", 8.0F), Brushes.Lime,
                                    new Point((int)centerX - 3 + (int)screens2X + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + 12 + (int)screens2Y + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                            }
                            else
                            {
                                pen.Color = Color.Red;
                                g.DrawString(dis, new Font("Arial", 8.0F), Brushes.Red,
                                    new Point((int)centerX - 3 + (int)screens2X + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + 12 + (int)screens2Y + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                            }
                            //pen.DashStyle = DashStyle.DashDot;
                            g.DrawLine(pen,
                                new Point(((int)centerX + (int)screenX), ((int)centerY + (int)screenY)),
                                new Point(((int)centerX + (int)screens2X), ((int)centerY + (int)screens2Y)));
                        }
                    }

                    if (targSys != null)
                    {
                        double screenXflS1 = playerSys.x * Math.Cos(spinX) - playerSys.z * Math.Sin(spinX) - 10;
                        double screenZflS1 = playerSys.x * Math.Sin(spinX) + playerSys.z * Math.Cos(spinX);
                        double screenYflS1 = playerSys.y * Math.Cos(spinY) - screenZflS1 * Math.Sin(spinY) - 10;

                        double screenXflS2 = targSys.x * Math.Cos(spinX) - targSys.z * Math.Sin(spinX) - 10;
                        double screenZflS2 = targSys.x * Math.Sin(spinX) + targSys.z * Math.Cos(spinX);
                        double screenYflS2 = targSys.y * Math.Cos(spinY) - screenZflS2 * Math.Sin(spinY) - 10;

                        pen.Color = Color.Red;
                        pen.DashStyle = DashStyle.Dash;

                        g.DrawLine(pen,
                                new Point(((int)centerX + (int)screenXflS1 + 10), ((int)centerY + (int)screenYflS1) + 10),
                                new Point(((int)centerX + (int)screenXflS2 + 10), ((int)centerY + (int)screenYflS2) + 10));

                        //g.DrawLine(pen, new Point((int)centerX + (int)screenXflS1 + Convert.ToInt32(r * Math.Cos(-1 * ugol)), (int)centerX + (int)screenYflS1 + Convert.ToInt32(r * Math.Cos(-1 * ugol))), new Point((int)centerX + (int)screenXflS2 + Convert.ToInt32(r * Math.Cos(-2 * ugol)), (int)centerX + (int)screenYflS2 + Convert.ToInt32(r * Math.Cos(-2 * ugol))));
                        //g.DrawLine(pen, new Point((int)player.player_fleets[k].s1.x, (int)player.player_fleets[k].s1.y), new Point((int)player.player_fleets[k].s2.x, (int)player.player_fleets[k].s2.y));
                    }
                    pen.Color = Color.Gold;
                    pen.DashStyle = DashStyle.Solid;
                }


                //----------------Neutral fleets------------------
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
                        g.FillPolygon(neutralFleetColor, compPointArrayShip);
                        g.DrawString(galaxy.neutrals[k].name, new Font("Arial", 8.0F), neutralFleetColor, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
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
        */
#endregion

        //------------------------------------Sound------------------------------------

        public void PlayMusic()
        {

        }

        private void sound_button_Click(object sender, EventArgs e)
        {
            if (waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                waveOutDevice.Pause();
            }
            else
            {
                waveOutDevice.Play();
            }

        }

        #region will delete
        /*/-------------------------------------------------------------------
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
        *///----------------------------------------------------------------------------
        #endregion

        //-----------------------------Main Menu --------------------------------------

        private void mainMenuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainMenuNew_Click(object sender, EventArgs e)
        {
            Form_NewGameDialog nd = new Form_NewGameDialog();
            if (nd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                player = new Player();
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
                selectedStar = null;      //star_selected = planet_start;
                StarSystem ss = galaxy.stars[planet_start];

                if (nd.getGalaxyRandomEvents() == true)
                {
                    generate_random_events();
                }

                //флот игрока
                /*      Fleet fl = generateFleet(rand.Next(5, 7), 1);

                      player.player_stars.Add(galaxy.stars[rand.Next(0, galaxy.stars.Count - 1)]);

               
                          player.player_fleets.Add(fl);
                          fl.s1 = player.player_stars[0];
                          player.player_fleets[0].x = player.player_fleets[0].s1.x;
                          player.player_fleets[0].y = player.player_fleets[0].s1.y;
                          player.player_fleets[0].z = player.player_fleets[0].s1.z;
                          fl.name = nd.namePlayer+1;
                      */


                int fleetsCount = rand.Next(1, 5);
                for (int i = 0; i < fleetsCount; i++)
                {
                    Fleet fl = generateFleet(rand.Next(2, 5), 1);
                    player.player_stars.Add(galaxy.stars[rand.Next(0, galaxy.stars.Count - 1)]);
                    fl.s1 = player.player_stars[i];
                    ///fl.name = nd.namePlayer + (i + 1);
                    fl.name = "Флот " + (i + 1) + " <" + nd.namePlayer + ">";
                    player.player_fleets.Add(fl);
                    player.player_fleets[i].x = player.player_fleets[i].s1.x;
                    player.player_fleets[i].y = player.player_fleets[i].s1.y;
                    player.player_fleets[i].z = player.player_fleets[i].s1.z;
                }


                for (int i = 0; i < 3; i++)
                {
                    StarSystem sr = galaxy.stars[rand.Next(0, galaxy.stars.Count - 1)];
                    while (sr == player.player_stars[0])
                    {

                        sr = galaxy.stars[rand.Next(0, galaxy.stars.Count - 1)];
                    }
                    Fleet flneutrals = generateFleet(rand.Next(2, 4), 2);
                    flneutrals.s1 = sr;
                    flneutrals.name = "Нейтральный флот";
                    galaxy.neutrals.Add(flneutrals);
                }
                //Redraw();
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

                //Redraw();
            }

        }

        private void mainMenuAbout_Click(object sender, EventArgs e)
        {
            Form_About af = new Form_About();
            af.ShowDialog();
        }

        private void MainMenuTechTree_Click(object sender, EventArgs e)
        {
            tt.ShowDialog();
        }


        //-----------------------------Galaxy & Fleet---------------------------------------

        public Fleet generateFleet(int size, int player)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            Fleet fleet = new Fleet();

            for (int i = 0; i < size; i++)
            {
                int ship_type = (rand.Next(0, 100)) % 2;
                int weapon_type = rand.Next(0, 2);

                Weapon weapon = null;
                Ship ship = null;
                switch (weapon_type)
                {
                    case 0: weapon = new wpnLightLaser(); break;
                    //case 1: weapon = new WpnLightIon(); break;
                    case 1: weapon = new WpnHeavyLaser(); break;
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

        public void animationFleets(Fleet fl, StarSystem stars, int fly)
        {
            double x = stars.x - fl.x;
            double y = stars.y - fl.y;
            double z = stars.z - fl.z;
            double dx = x / fl.starDistanse;
            double dy = y / fl.starDistanse;
            double dz = z / fl.starDistanse;

            for (int i = 0; i < fly; i++)
            {
                fl.x += dx;
                fl.y += dy;
                fl.z += dz;
                //Redraw();
                galaxyImage.Refresh();
            }

        }

        public void generate_random_events()
        {
            int next;
            Random rand = new Random();
            for (int i = 0; i < galaxy.stars.Count / 10; i++) // (1/10) of all stars
            {
                next = rand.Next(galaxy.stars.Count);       //random star from all stars
                galaxy.stars[next].name = "super nova";     //name for new star
                galaxy.stars[next].type = 8;                //type for "super nova"
                galaxy.stars[next].br = SuperWhiteBrush;    //brush for "super nova"
            }
        }

        public void generatePlanets(StarSystem s)
        {
            int sizemin = 10;
            int sizemax = 40;
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

            int planets_count = s.type + 1 + (-1 + r.Next(0, 2));

            s.PLN.Add(new PLANET());
            s.PLN[0].CENTER = new Point(190, 190);
            s.PLN[0].DISTANCE = 0;
            s.PLN[0].SPEED = 0;
            s.PLN[0].CLR = s.br.Color;
            s.PLN[0].SIZE = 25;
            s.PLN[0].NAME = "STAR";
            s.PLN[0].POPULATIONMAX = 0;
            s.PLN[0].POPULATION = 0;
            s.PLN[0].MINERALS = 0;
            double p = 1;
            //p = p + (p / 2.75) - (p / (r.NextDouble() * 20));
            for (int i = 1; i <= planets_count; i++)
            {
                s.PLN.Add(new PLANET());
                s.PLN[i].CENTER = new Point(s.PLN[0].GetPoint().X, s.PLN[0].GetPoint().Y);
                s.PLN[i].DISTANCE = dist;
                s.PLN[i].SPEED = speed;
                s.PLN[i].CLR = Color.FromArgb((r.Next(colormin, colormax)), (r.Next(colormin, colormax)), (r.Next(colormin, colormax)));
                s.PLN[i].SIZE = r.Next(sizemin, sizemax);
                s.PLN[i].NAME = "Planet " + i.ToString();
                s.PLN[i].POPULATION = s.PLN[i].Inc(p, r.NextDouble());
                s.PLN[i].POPULATIONMAX = r.Next(popmin, popmax);
                s.PLN[i].MINERALS = r.Next(mineralmin, mineralmax);
                s.PLN[i].PROFIT = s.PLN[i].POPULATION * s.PLN[i].MINERALS;

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
            for (int i = 0; i < starscount / 2; i++)
            {
                r += rand.Next(4) + 10 * (galaxysize + 1);
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
                s.name = (i + 1).ToString();

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
            Double t = Math.PI;
            Double z = 0;
            Random rand = new Random();

                for (int i = 0; i < starscount/2; i++)
                {

                    x = 100 * (galaxysize + 1) * Math.Cos(t) + rand.Next(200);
                    y = 100 * (galaxysize + 1) * Math.Sin(t) + rand.Next(200);
                    z = 0;

                    if (rotate == true)
                    {
                        x = -x;
                        y = -y;
                    }

                    StarSystem s = new StarSystem();
                    s.x = x;
                    s.y = z;
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

                    generatePlanets(s);
                    galaxy.stars.Add(s);
                    t += 0.2;
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
            for (int j = 0; j < starscount / 2; j++)
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

        //------------------------------------Events-----------------------------------

        ///********      ADDED       *********      новый метод для отрисовки. Без битмапа
        private void galaxyImage_Paint(object sender, PaintEventArgs e)
        {
            if (galaxy == null)
            {
                //MessageBox.Show("Error occured :`(\n\n'Nothing to draw'", "Draw Galaxy", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            int r = 6;
            Pen pen = new Pen(Color.Gold);
            double ugol = 2 * Math.PI / (3);
            Point[] points = new Point[3];

            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.Black, 0, 0, galaxyImage.Width, galaxyImage.Height);
            g.DrawString(galaxy.name, new Font("Arial", 10.0F), Brushes.White, new PointF(1.0F, 1.0F));

            g.ScaleTransform(scaling, scaling);//resize image(zooming in/out)

            #region old
            /*
            float centerX = galaxyBitmap.Width / 2 / scaling;
            float centerY = galaxyBitmap.Height / 2 / scaling;

            centerX += horizontal;  //move galaxy left/right
            centerY += vertical;    //move galaxy up/down
            */
            #endregion

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

                if (s == selectedStar)       //if (s == galaxy.stars[star_selected])
                {
                    g.DrawEllipse(pen, rectan);
                }

                rectan = new Rectangle((int)(centerX - 4 + (int)screenX - starSize / 2), (int)(centerY - 4 + (int)screenY - starSize / 2), (int)(starSize + 9), (int)(starSize + 9));

                if (player.player_stars.Contains(s))
                {
                    g.DrawEllipse(Pens.GreenYellow, rectan);
                }

                //----------------------Player Fleets----------------------
                for (int k = 0; k < player.player_fleets.Count; k++)
                {
                    Fleet fleet = player.player_fleets[k];
                    StarSystem playerSys = fleet.s1;
                    StarSystem targSys = fleet.s2;

                    if (playerSys != null)
                    {

                    }

                    if (fleet.s1 == s)
                    {
                        double screenXfl = fleet.x * Math.Cos(spinX) - fleet.z * Math.Sin(spinX) - 10;
                        double screenZfl = fleet.x * Math.Sin(spinX) + fleet.z * Math.Cos(spinX);
                        double screenYfl = fleet.y * Math.Cos(spinY) - screenZfl * Math.Sin(spinY) - 10;
                        Point[] compPointArrayShip = {  //точки для рисование корабля
                                        new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-1 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-1 * ugol))),
                                        new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-2 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-2 * ugol))),
                                        new Point((int)centerX + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol)))};
                        //g.FillPolygon(GoldBrush, compPointArrayShip); //---old
                        if (k == selectFleet)
                        {
                            g.FillPolygon(activeFleetColor, compPointArrayShip);
                            g.DrawString(fleet.name, new Font("Arial", 9.0F, FontStyle.Bold), activeFleetColor, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));

                        }
                        else
                        {
                            g.FillPolygon(passiveFleetColor, compPointArrayShip);
                            g.DrawString(fleet.name, new Font("Arial", 8.0F), passiveFleetColor, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                        }

                        if (warp == 1 && k == selectFleet && player.player_fleets[selectFleet].starDistanse == 0)
                        {
                            //pen.Color = Color.Red;
                            //pen.DashStyle = DashStyle.Dash; 
                            //rectan = new Rectangle((int)(centerX - 150 + (int)screenX - starSize / 2), (int)(centerY - 150 + (int)screenY - starSize / 2), (int)(starSize + 300), (int)(starSize + 300));
                            //g.DrawEllipse(pen, rectan);
                            string dis = ((int)starDistanse).ToString();//+-

                            double ts2X, ts2Y, ts2Z;
                            double screens2X;
                            double screens2Y;
                            double screens2Z;


                            ts2X = s2x * Math.Cos(spinX) - s2z * Math.Sin(spinX);
                            ts2Z = s2x * Math.Sin(spinX) + s2z * Math.Cos(spinX);
                            ts2Y = s2y * Math.Cos(spinY) - ts2Z * Math.Sin(spinY);

                            screens2X = ts2X;
                            screens2Y = ts2Y;
                            screens2Z = ts2Z;

                            if (starDistanse < maxDistanse)
                            {
                                pen.Color = Color.Lime;
                                g.DrawString(dis, new Font("Arial", 8.0F), Brushes.Lime,
                                    new Point((int)centerX - 3 + (int)screens2X + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + 12 + (int)screens2Y + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                            }
                            else
                            {
                                pen.Color = Color.Red;
                                g.DrawString(dis, new Font("Arial", 8.0F), Brushes.Red,
                                    new Point((int)centerX - 3 + (int)screens2X + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY + 12 + (int)screens2Y + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
                            }
                            //pen.DashStyle = DashStyle.DashDot;
                            g.DrawLine(pen,
                                new Point(((int)centerX + (int)screenX), ((int)centerY + (int)screenY)),
                                new Point(((int)centerX + (int)screens2X), ((int)centerY + (int)screens2Y)));
                        }
                    }

                    if (targSys != null)
                    {
                        double screenXflS1 = playerSys.x * Math.Cos(spinX) - playerSys.z * Math.Sin(spinX) - 10;
                        double screenZflS1 = playerSys.x * Math.Sin(spinX) + playerSys.z * Math.Cos(spinX);
                        double screenYflS1 = playerSys.y * Math.Cos(spinY) - screenZflS1 * Math.Sin(spinY) - 10;

                        double screenXflS2 = targSys.x * Math.Cos(spinX) - targSys.z * Math.Sin(spinX) - 10;
                        double screenZflS2 = targSys.x * Math.Sin(spinX) + targSys.z * Math.Cos(spinX);
                        double screenYflS2 = targSys.y * Math.Cos(spinY) - screenZflS2 * Math.Sin(spinY) - 10;

                        pen.Color = Color.Red;
                        pen.DashStyle = DashStyle.Dash;

                        g.DrawLine(pen,
                                new Point(((int)centerX + (int)screenXflS1 + 10), ((int)centerY + (int)screenYflS1) + 10),
                                new Point(((int)centerX + (int)screenXflS2 + 10), ((int)centerY + (int)screenYflS2) + 10));

                        //g.DrawLine(pen, new Point((int)centerX + (int)screenXflS1 + Convert.ToInt32(r * Math.Cos(-1 * ugol)), (int)centerX + (int)screenYflS1 + Convert.ToInt32(r * Math.Cos(-1 * ugol))), new Point((int)centerX + (int)screenXflS2 + Convert.ToInt32(r * Math.Cos(-2 * ugol)), (int)centerX + (int)screenYflS2 + Convert.ToInt32(r * Math.Cos(-2 * ugol))));
                        //g.DrawLine(pen, new Point((int)player.player_fleets[k].s1.x, (int)player.player_fleets[k].s1.y), new Point((int)player.player_fleets[k].s2.x, (int)player.player_fleets[k].s2.y));
                    }
                    pen.Color = Color.Gold;
                    pen.DashStyle = DashStyle.Solid;
                }


                //----------------Neutral fleets------------------
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
                        g.FillPolygon(neutralFleetColor, compPointArrayShip);
                        g.DrawString(galaxy.neutrals[k].name, new Font("Arial", 8.0F), neutralFleetColor, new Point((int)centerX - 3 + (int)screenXfl + Convert.ToInt32(r * Math.Cos(-3 * ugol)), (int)centerY - 12 + (int)screenYfl + Convert.ToInt32(r * Math.Sin(-3 * ugol))));
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
        }
        //----new
        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateCenters();
            galaxyImage.Refresh();
        }

        //mouse down listener
        private void galaxyImage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = e.X;   //start x
            mouseY = e.Y;   //start y
        }

        //mouse move listener   ***EDITED
        private void galaxyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (galaxy != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    #region old
                    /*
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
                    */
                    #endregion

                    if (ModifierKeys == Keys.Shift)
                    {
                        spinX += (e.X - mouseX) * 0.01;
                        spinY += (e.Y - mouseY) * 0.01;
                    }
                    else
                    {
                        horizontal += (e.X - mouseX) / scaling;
                        vertical += (e.Y - mouseY) / scaling;
                    }

                    mouseX = e.X;   //set start x again
                    mouseY = e.Y;   //set start y again

                    UpdateCenters();
                }

                for (int j = 0; j < galaxy.stars.Count; j++)
                {
                    StarSystem s = galaxy.stars[j];

                    #region old
                    /*
                    //all need to calculate the real x,y of star on the screen
                    //(s.x ~ 10 to 30) but the real position x on the screen is ~ 100 to 600
                    //--------------------------------------//
                     * 
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

                    s2x = s.x;
                    s2y = s.y;
                    s2z = s.z;

                    screenX = tX;
                    screenY = tY;

                    starSize = s.type + dynamicStarSize;                    
                    //--------------------------------------//
                    //check for mouse in the star ellipce                    
                    if (e.X / scaling > (centerX + (int)screenX - starSize / 2) &&
                        e.X / scaling < (centerX + (int)screenX + starSize / 2) &&
                        e.Y / scaling > (centerY + (int)screenY - starSize / 2) &&
                        e.Y / scaling < (centerY + (int)screenY + starSize / 2))
                    */
                    #endregion

                    if (mouseIsInStar(e, s))
                    {
                        if (player.player_fleets[selectFleet].s1 == s)
                            return;

                        warp = 1;
                        starDistanse = Math.Sqrt(Math.Pow((s.x - player.player_fleets[selectFleet].s1.x), 2) + Math.Pow((s.y - player.player_fleets[selectFleet].s1.y), 2) + Math.Pow((s.z - player.player_fleets[selectFleet].s1.z), 2));
                        statusStrip1.Items[1].Text = "x: " + s.x + " y: " + s.y;
                        break;
                    }
                    else
                    {
                        warp = 0;
                        statusStrip1.Items[1].Text = "";
                        //Redraw();
                        //return;
                    }
                }
                //Redraw();
                galaxyImage.Refresh();
            }
        }

        //  ***edited
        private void this_MouseWheel(object sender, MouseEventArgs e) // resizing of galaxy at event change wheel mouse
        {
            if (e.Delta > 0)
            {
                if (scaling >= 10)
                    return;
                else
                {
                    scaling += 0.2f;
                    if (dynamicStarSize >= 3) dynamicStarSize -= 0.4f;
                    else if (dynamicStarSize >= 2) dynamicStarSize -= 0.05f;
                    else if (dynamicStarSize >= 0) dynamicStarSize -= 0.01f;
                    //Redraw();
                }
            }
            else
            {
                if (scaling <= 0.4)
                    return;
                else
                {
                    scaling -= 0.2f;
                    if (dynamicStarSize <= 2)
                        dynamicStarSize += 0.01f;
                    else if (dynamicStarSize <= 3)
                        dynamicStarSize += 0.05f;
                    else if (dynamicStarSize <= 5)
                        dynamicStarSize += 0.4f;
                    //Redraw();
                }
            }
            UpdateCenters();
            //Redraw();
            galaxyImage.Refresh();
        }

        //      ***EDITED
        private void galaxyImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int j = 0; j < galaxy.stars.Count; j++)
            {
                #region old
                /*
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
                    */
                #endregion

                if (mouseIsInStar(e, galaxy.stars[j]))
                {
                    selectedStar = galaxy.stars[j];     //star_selected = j;//store type for selected star

                    StarSystemForm ssm = new StarSystemForm(galaxy.stars[j]);
                    ssm.ShowDialog();

                    return;
                }
            }

            for (int i = 0; i < player.player_fleets.Count; i++)
            {
                if (mouseIsInFleet(e, player.player_fleets[i]))
                {
                    MessageBox.Show("Флот номер " + (i + 1) + "\nСостав флота - " + player.player_fleets[i].ships.Count + " кораблей.", "Просмотр флота");
                    return;
                }
            }
        }

        //      ***EDITED
        private void galaxyImage_MouseClick(object sender, MouseEventArgs e)
        {
            for (int j = 0; j < galaxy.stars.Count; j++)
            {
                StarSystem s = galaxy.stars[j];

                #region old
                /*
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
                    */
                #endregion

                if (mouseIsInStar(e, s))
                {
                    if ((conquer_progressBar.Visible == false) && (player.player_fleets[selectFleet].starDistanse == 0))
                    {
                        starDistanse = Math.Sqrt(Math.Pow((s.x - player.player_fleets[selectFleet].s1.x), 2) + Math.Pow((s.y - player.player_fleets[selectFleet].s1.y), 2) + Math.Pow((s.z - player.player_fleets[selectFleet].s1.z), 2));
                        if (starDistanse <= maxDistanse)
                        {
                            player.player_fleets[selectFleet].s2 = s;
                            player.player_fleets[selectFleet].starDistanse = starDistanse;
                            selectedStar = s;    //star_selected = j;   //store type for selected star
                        }
                    }
                    else if (player.player_fleets[selectFleet].s2 == s)///-----new
                    {
                        if (player.player_fleets[selectFleet].onWay)
                            break;

                        player.player_fleets[selectFleet].s2 = null;
                        player.player_fleets[selectFleet].starDistanse = 0;
                        selectedStar = null;
                    }
                    //Redraw();
                    //return;
                    break;
                }
            }

            for (int i = 0; i < player.player_fleets.Count; i++)
            {
                #region old
                /*
                double screenXfl = player.player_fleets[i].x * Math.Cos(spinX) - player.player_fleets[i].z * Math.Sin(spinX) - 10;
                double screenZfl = player.player_fleets[i].x * Math.Sin(spinX) + player.player_fleets[i].z * Math.Cos(spinX);
                double screenYfl = player.player_fleets[i].y * Math.Cos(spinY) - screenZfl * Math.Sin(spinY) - 10;

                float centerX = galaxyBitmap.Width / 2 / scaling;
                float centerY = galaxyBitmap.Height / 2 / scaling;


                centerX += horizontal;  //move galaxy left/right
                centerY += vertical;    //move galaxy up/down

                if (e.X / scaling > (centerX + (int)screenXfl - 15 / 2) &&
                    e.X / scaling < (centerX + (int)screenXfl + 15 / 2) &&
                    e.Y / scaling > (centerY + (int)screenYfl - 15 / 2) &&
                    e.Y / scaling < (centerY + (int)screenYfl + 15 / 2))
                    */
            #endregion
                if (mouseIsInFleet(e, player.player_fleets[i]))
                {
                    selectFleet = i;

                    selectedStar = player.player_fleets[i].s2;//----new

                    statusStrip1.Items[0].Text = "Выбран " + (i + 1) + " флот";
                }
            }
            galaxyImage.Refresh();
            //Redraw();
        }

        //      ***Edited
        private void step_button_Click(object sender, EventArgs e)
        {
            if (galaxy == null)
            {
                return;
            }

            //----new
            Time++; //Длина шага - 1 год
            dateLabel.Text = Time.ToString() + " г.н.э.";
            //-----

            StarSystem s = selectedStar;    //StarSystem s = galaxy.stars[star_selected];
            Random r = new Random(DateTime.Now.Millisecond);
            for (int j = 0; j < galaxy.stars.Count; j++)
            {
                for (int i = 0; i < galaxy.stars[j].planets_count; i++)
                {
                    galaxy.stars[j].PLN[i].POPULATION = galaxy.stars[j].PLN[i].Inc(galaxy.stars[j].PLN[i].POPULATION, r.NextDouble());
                }
            }

           
           
                for (int i = 0; i < Player.player_planets.Count; i++)
                {
                    credit = credit + Player.player_planets[i].PROFIT;//StarSystem.PLN[i].PROFIT;
                }
            
            CreditsStatus.Text = credit.ToString();

            if (conquer_progressBar.Visible == true)
            {
                if (conquer_progressBar.Value == conquer_progressBar.Maximum - 1)
                {
                    player.player_stars.Add(selectedStar);      //player.player_stars.Add(galaxy.stars[star_selected]);
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
                for (int k = 0; k < player.player_fleets.Count; k++)
                {
                    int fly = 50;
                    if (player.player_fleets[k].s2 != null)
                    {
                        if (player.player_fleets[k].starDistanse >= 50)
                        {
                            player.player_fleets[k].onWay = true;
                            animationFleets(player.player_fleets[k], player.player_fleets[k].s2, fly);
                            player.player_fleets[k].starDistanse -= fly;
                        }
                        else
                        {
                            fly = (int)player.player_fleets[k].starDistanse;
                            animationFleets(player.player_fleets[k], player.player_fleets[k].s2, fly);
                            player.player_fleets[k].s1 = player.player_fleets[k].s2;
                            player.player_fleets[k].s2 = null;
                            player.player_fleets[k].starDistanse = 0;
                            player.player_fleets[k].onWay = false;
                        }
                    }
                }
            }

            #region old
            /*
            for (int k = 0; k < galaxy.neutrals.Count; k++)
            {
                if (galaxy.neutrals[k].s1 == s && player.player_fleets[selectFleet].starDistanse==0)
                {
                    //MessageBox.Show("Обнаружен противник!");

                    CombatForm cf = new CombatForm(player.player_fleets[0], galaxy.neutrals[k]);
                    cf.ShowDialog();
                }
            }*/
            #endregion

            for (int k = 0; k < galaxy.neutrals.Count; k++)
            {
                for (int l = 0; l < player.player_fleets.Count; l++)
                    if (galaxy.neutrals[k].s1 == player.player_fleets[l].s1)
                    {
                        if (player.player_fleets[l].starDistanse == 0)
                            if (MessageBox.Show("Ваш " + (l + 1) + " флот обнаружил нейтральный флот на планете " + player.player_fleets[l].s1.name + "!\nАтаковать его?", "", MessageBoxButtons.YesNo)
                                == System.Windows.Forms.DialogResult.Yes)
                            {
                                CombatForm cf = new CombatForm(player.player_fleets[l], galaxy.neutrals[k]);
                                cf.ShowDialog();

                                //Удалить флот из списка, если флот уничтожен
                                if (galaxy.neutrals[k].ships.Count == 0)
                                    galaxy.neutrals.RemoveAt(k);
                            }
                    }
            }

            if (tech_progressBar.Value < tt.learning_tech_time && tt.tech_clicked != 1000 && tt.subtech_clicked != 1000)
            {
                tech_progressBar.Value += 1;
            }

            if (tech_progressBar.Value == tt.learning_tech_time)
            {
                Player.technologies.Add(new int[] { tt.tech_clicked, tt.subtech_clicked });
                tech_progressBar.Value = 0;
                tech_progressBar.Visible = false;
                tech_label.Visible = false;
                tt.tech_clicked = 1000;
                tt.subtech_clicked = 1000;
            }
            galaxyImage.Refresh();
            //Redraw();
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

        private void buildings_TextChanged(object sender, EventArgs e)
        { 
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Shop_button_Click(object sender, EventArgs e)
        {
            shop_form.ShowDialog();
        }

        private void fleetsButton_Click(object sender, EventArgs e)
        {
            if (listView.Tag.Equals(fleetsButton.Tag))
            {
                listView.Visible = false;
                listView.Tag = "";
                return;
            }
            else
                listView.Visible = true;
            listView.Items.Clear();
            if (listView.Visible)
            {
                listView.Tag = fleetsButton.Tag;
                for (int i = 0; i < player.player_fleets.Count; i++)
                    listView.Items.Add(player.player_fleets[i].name);
            }
        }

        private void planetsButton_Click(object sender, EventArgs e)
        {
            ///if (listView.Tag.Equals(planetsButton.Tag))
            ///
            if (listView.Tag.Equals(planetsButton.Tag))
            {
                listView.Visible = false;
                listView.Tag = "";
                return;
            }
            else
                listView.Visible = true;

            listView.Items.Clear();
            if (listView.Visible)
            {
                listView.Tag = planetsButton.Tag;
                for (int i = 0; i < player.player_stars.Count; i++)
                    listView.Items.Add(player.player_stars[i].name);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.Tag.Equals(fleetsButton.Tag))
                if (listView.SelectedIndices.Count > 0)
                {
                    selectFleet = listView.SelectedIndices[0];

                    selectedStar = player.player_fleets[selectFleet].s2;//----new

                    statusStrip1.Items[0].Text = "Выбран " + (selectFleet + 1) + " флот";
                    galaxyImage.Refresh();
                }
        }

        //-------------------------------other----------------------------------
        //added
        //Проверка находится ли курсор "e" в планете "star"
        private bool mouseIsInStar(MouseEventArgs e, StarSystem star)
        {
            double screenX;
            double screenY;
            double tX, tY, tZ;
            double starSize;

            tX = star.x * Math.Cos(spinX) - star.z * Math.Sin(spinX);
            tZ = star.x * Math.Sin(spinX) + star.z * Math.Cos(spinX);
            tY = star.y * Math.Cos(spinY) - tZ * Math.Sin(spinY);

            s2x = star.x;
            s2y = star.y;
            s2z = star.z;

            screenX = tX;
            screenY = tY;

            starSize = star.type + dynamicStarSize;

            return (e.X + dispersion) / scaling > (centerX + (int)screenX - starSize / 2) &&
                   (e.X - dispersion) / scaling < (centerX + (int)screenX + starSize / 2) &&
                   (e.Y + dispersion) / scaling > (centerY + (int)screenY - starSize / 2) &&
                   (e.Y - dispersion) / scaling < (centerY + (int)screenY + starSize / 2);
        }
        //то же самое, только с флотом
        private bool mouseIsInFleet(MouseEventArgs e, Fleet fleet)
        {
            double screenXfl = fleet.x * Math.Cos(spinX) - fleet.z * Math.Sin(spinX) - 10;
            double screenZfl = fleet.x * Math.Sin(spinX) + fleet.z * Math.Cos(spinX);
            double screenYfl = fleet.y * Math.Cos(spinY) - screenZfl * Math.Sin(spinY) - 10;
            
            return (e.X / scaling > (centerX + (int)screenXfl - 15 / 2) &&
                e.X / scaling < (centerX + (int)screenXfl + 15 / 2) &&
                e.Y / scaling > (centerY + (int)screenYfl - 15 / 2) &&
                e.Y / scaling < (centerY + (int)screenYfl + 15 / 2));
        }
        //обновляет центры
        private void UpdateCenters()
        {
            centerX = galaxyImage.Width / 2 / scaling + horizontal;
            centerY = galaxyImage.Height / 2 / scaling + vertical;
        }
        //added
    }
}
