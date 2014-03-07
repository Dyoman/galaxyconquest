using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows;

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
            Gl.glColor3f(1.0f, 0, 0);//set drawing color

            Gl.glPushMatrix();//save coordinates for all objects
            Gl.glTranslated(-15, 0, -20);//move draw point (x=-15, y=0, z=-20)

            Gl.glRotated(HorizontalSpin, 0, 1, 0);//rotate
            Gl.glRotated(VerticalSpin, 1, 0, 0);//rotate

            generate_planets();

            Gl.glPopMatrix();//restore from saving coordinates(now draw point in 0,0,0)
            Gl.glFlush();
            planetsViev.Invalidate();
        }

        private void generate_planets()
        {
            int star = 1;//type of selected star
            if (Form1.SelfRef != null)
            {
                star = Form1.SelfRef.star_selected;
            }
            //count of planets depends on type (1:1)
            for (int i = 0; i < star; i++)
            {
                Gl.glTranslated(6, 0, 0);
                // рисуем сферу с помощью библиотеки FreeGLUT
                Glut.glutWireSphere(2, 32, 32);
            }
        }

        private void showbutton_Click(object sender, EventArgs e)
        {
            show();
        }

        private void planets_Load(object sender, EventArgs e)
        {

            // очитка окна 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соотвествии с размерами элемента anT 
            Gl.glViewport(0, 0, planetsViev.Width, planetsViev.Height);


            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)planetsViev.Width / (float)planetsViev.Height, 0.1, 200);
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
