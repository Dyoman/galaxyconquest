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

using GalaxyConquest.Drawing;
using GalaxyConquest.Game;
using GalaxyConquest.SpaceObjects;


namespace GalaxyConquest
{
    class Screen_TechTree : Gwen.Control.DockBase
    {
         public Image galaxyImage;
        public Gwen.Control.ImagePanel img;
        Gwen.Control.Label label;

        /// <summary>
        /// Экземпляр класса DrawController, который будет отвечать за отрисовку в главной форме
        /// </summary>
        DrawController DrawControl;

        bool dragging = false;
        int mx = 0;
        int my = 0;

        public Screen_TechTree (Base parent)
            : base(parent)
        {
            SetSize(parent.Width, parent.Height);

            label = new Gwen.Control.Label(this);
            label.Text = "Tech_Tree Probe";
            label.SetPosition(Program.percentW(5), Program.percentH(5));
            label.TextColor = Color.FromArgb(200, 80, 0, 250);
            label.Font = Program.fontLogo;

            img = new Gwen.Control.ImagePanel(this);
            

            galaxyImage = new Bitmap(Program.percentW(100), Program.percentH(80), PixelFormat.Format32bppArgb);
            DrawControl = new DrawController(galaxyImage);

            updateDrawing();

            img.SetPosition(Program.percentW(0), Program.percentH(20));
            img.SetSize(Program.percentW(100), Program.percentH(80));
            //img.Clicked += new GwenEventHandler<ClickedEventArgs>(img_Clicked);
            img.MouseMoved += new GwenEventHandler<MovedEventArgs>(img_MouseMoved);
            img.MouseDown += new GwenEventHandler<ClickedEventArgs>(img_MouseDown);
            img.MouseUp += new GwenEventHandler<ClickedEventArgs>(img_MouseUp);
        }
        // label Up и Down в Tech_Tree пожалуй не нужен
        /*void img_MouseUp(Base sender, ClickedEventArgs arguments)
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
        }*/

        void img_MouseMoved(Base sender, MovedEventArgs arguments)
        {
            if (dragging)
            {
                label.Text = arguments.X.ToString() + "," + arguments.Y.ToString();
                DrawControl.Move(arguments.X - mx, arguments.Y - my);
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

        public void updateDrawing()
        {
            Graphics gr = Graphics.FromImage(galaxyImage);
            gr.FillRectangle(Brushes.Black, 0, 0, galaxyImage.Width, galaxyImage.Height);

            DrawControl.Render(Program.Game, gr);

            img.Image = (Bitmap)galaxyImage;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
