using System;
using System.Drawing;
using System.Windows.Forms;



namespace GalaxyConquest.StarSystems
{
    public partial class StarSystemForm : Form
    {
        public static int pln_selected;
        public static string size1;
        public static string minerals1;

        Random rand = new Random();
        //int maxpln=4;
        //int dist = 100;
        //float speed = 0.001f;

       public StarSystem s;

       public StarSystemForm(StarSystem system)
        {
            InitializeComponent();
            s = system;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        // ///////////////////////
        static Bitmap BIT = new Bitmap(800, 800);
        static Graphics GBIT = Graphics.FromImage(BIT);
        // ///////////////////////

        private void timer1_Tick(object sender, EventArgs e)
        {
            GBIT.Clear(Color.Black);
            // ////////////////////////////////////////
            for (Int32 i = 0; i < s.PLN.Count; i++)
            {
                //PLN[2].CENTER = new Point(PLN[1].GetPoint().X, PLN[1].GetPoint().Y);
                GBIT.DrawEllipse(new Pen(Color.White), s.PLN[0].GetPoint().X - s.PLN[i].DISTANCE, s.PLN[0].GetPoint().Y - s.PLN[i].DISTANCE, s.PLN[i].DISTANCE * 2, s.PLN[i].DISTANCE * 2); 
                // /////////////////////////

                GBIT.FillEllipse(new SolidBrush(s.PLN[i].CLR), new Rectangle(s.PLN[i].GetPoint().X - (int)s.PLN[i].SIZE / 2, s.PLN[i].GetPoint().Y - (int)s.PLN[i].SIZE / 2, (int)s.PLN[i].SIZE, (int)s.PLN[i].SIZE));
                GBIT.DrawString(s.PLN[i].NAME, new Font("arial", 10), new SolidBrush(Color.White), new Point(s.PLN[i].GetPoint().X, s.PLN[i].GetPoint().Y));
            }
           // int rad = (int)Math.Sqrt(PLN[4].GetPoint().X * PLN[4].GetPoint().X + PLN[4].GetPoint().Y * PLN[4].GetPoint().Y);
            // ////////////////////////////////////////
            pictureBox1.Image = BIT;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PLANET PL = new PLANET();

            for (int j = 0; j < s.PLN.Count; j++)
            {
                pln_selected = j;//переменная для связи планеты со формой2

                string sizeText = "";
                string mineralsText = "";

                //ниже- определение размера планеты
                if (s.PLN[j].SIZE < 15) 
                {
                    sizeText = "Small";
                }
                else
                    if ((s.PLN[j].SIZE >= 15) && (s.PLN[j].SIZE < 25))
                {
                    sizeText = "Medium";
                }
                else
                        if (s.PLN[j].SIZE > 25)
                {
                    sizeText = "Large";
                }


                //ниже - определение ресурсов
                if (s.PLN[j].MINERALS == 0)
                {
                    mineralsText = "No Minerals";
                }
                else
                    if ((s.PLN[j].MINERALS > 0) && (s.PLN[j].MINERALS <= 10))
                    {
                        mineralsText = "Meager";
                    }
                    else
                        if ((s.PLN[j].MINERALS > 10) && (s.PLN[j].MINERALS <= 20))
                        {
                            mineralsText = "Saturated";
                        }
                        else
                            if (s.PLN[j].MINERALS > 20)
                            {
                                mineralsText = "Rich";
                            }

                if ((e.X > s.PLN[j].GetPoint().X - (s.PLN[j].SIZE / 2)) &&
                    (e.X < s.PLN[j].GetPoint().X + (s.PLN[j].SIZE / 2)) &&    //клик по планете вызывает форму с информацией
                    (e.Y > s.PLN[j].GetPoint().Y - (s.PLN[j].SIZE / 2)) &&
                    (e.Y < s.PLN[j].GetPoint().Y + (s.PLN[j].SIZE / 2)))
                {
                    labelPlanetName.Text = s.PLN[j].NAME;
                    labelPlanetSize.Text = sizeText;
                    labelPlanetMinerals.Text = mineralsText;
                    labelPlanetPopulation.Text = s.PLN[j].POPULATION.ToString();            
              }
            }

        }
    }
}
