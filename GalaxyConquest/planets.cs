using System;
using System.Windows.Forms;

// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;

namespace GalaxyConquest
{
    public partial class planets : Form
    {
        public float HorizontalSpin;
        public float VerticalSpin;

        public planets()
        {
            InitializeComponent();
            planetsViev.InitializeContexts();
        }


        private void show()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            Glu.gluLookAt(200, 20, 200, -6, 0, -100, 0, -1, 0);//set point of view
            Gl.glColor3f(1.0f, 0, 0);//set drawing color

            Gl.glPushMatrix();//save coordinates for all objects

            Gl.glTranslated(0, 0, -120);//move draw point by (x=0, y=0, z=-120)
            Gl.glRotated(HorizontalSpin, 0, 1, 0);//rotate
            Gl.glRotated(VerticalSpin, 1, 0, 0);//rotate
            Gl.glTranslated(0, 0, 120);//move draw point by (x=0, y=0, z=120)
            generate_planets();

            Gl.glPopMatrix();//restore from saving coordinates(now draw point in 0,0,0)
            Gl.glFlush();
            planetsViev.Invalidate();
        }

        private void generate_planets()
        {
            int star = 1;       //type of selected star
            int r_tor = 50;     //radius of tor
            int r_sphere = 32;  //radius of sphere
            if (Form1.SelfRef != null)
            {
                star = Form1.SelfRef.galaxy.stars[Form1.SelfRef.star_selected].planets_count;
            }
            //count of planets depends on type (1:1)+1
            Gl.glPushMatrix();//save coordinates for all objects (this methods can be included)
            Gl.glTranslated(-15, 0, -100);

            for (int i = 0; i < star; i++)
            {
                Gl.glTranslated(6 , 0, 0);
                // рисуем сферу с помощью библиотеки FreeGLUT
                Glut.glutWireSphere(2, 32, 32);
                Gl.glPushMatrix();//save coordinates for all objects
                Gl.glTranslated(-r_tor - r_sphere * i, 0, 0);
                // torus
                Glut.glutWireTorus(0.2, r_tor + r_sphere * i, 32, 32);
                Gl.glPopMatrix();//restore from saving coordinates(now draw point in 0,0,0)
            }
            Gl.glPopMatrix();//restore from saving coordinates(now draw point in 0,0,0)
        }

        private void showbutton_Click(object sender, EventArgs e)
        {
            show();
        }

        private void planets_Load(object sender, EventArgs e)
        {

            if (Form1.SelfRef.galaxy.stars[Form1.SelfRef.star_selected].name == "Player")
            {
                this.Text = "Player planets";
            }

            // очитка окна 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соотвествии с размерами элемента anT 
            Gl.glViewport(0, 0, planetsViev.Width, planetsViev.Height);


            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)planetsViev.Width / (float)planetsViev.Height, 0.1, 1200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            

            // настройка параметров OpenGL для визуализации 
            Gl.glEnable(Gl.GL_DEPTH_TEST);
        }

        private void buttonSpinUp_Click(object sender, EventArgs e)
        {
            VerticalSpin += 5;
            show();
        }

        private void buttonSpinDown_Click(object sender, EventArgs e)
        {
            VerticalSpin -= 5;
            show();
        }

        private void buttonSpinRight_Click(object sender, EventArgs e)
        {
            HorizontalSpin += 5;
            show();
        }

        private void buttonSpinLeft_Click(object sender, EventArgs e)
        {
            HorizontalSpin -= 5;
            show();
        }

        private void planets_Resize(object sender, EventArgs e)
        {
            planets_Load(sender, e);//need to reload our drawing place
            show();
        }
    }
}
