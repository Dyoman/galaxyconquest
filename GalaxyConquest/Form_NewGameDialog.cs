using System;
using System.Windows.Forms;

namespace GalaxyConquest
{
    public partial class Form_NewGameDialog : Form
    {
        GalaxyType galaxytype = GalaxyType.Spiral;
        int galaxysize = 4;
        
        public Form_NewGameDialog()
        {
            InitializeComponent();
        }

        public string namePlayer
        {
            get
            {
                return textNamePlayer.Text;
            }
        }

        public string galaxyName
        {
            get
            {
                return galaxyNameTextBox.Text;
            }
        }        

        private void buttonGalaxyTypeLeft_Click(object sender, EventArgs e)
        {
            switch (galaxytype)
            {
                case GalaxyType.Irregular:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Sphere";
                    galaxytype = GalaxyType.Sphere;
                    break;
                case GalaxyType.Sphere:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Elliptical";
                    galaxytype = GalaxyType.Eliptical;
                    break;
                case GalaxyType.Eliptical:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
                    labelGalaxyType.Text = "Spiral";
                    galaxytype = GalaxyType.Spiral;
                    break;
                case GalaxyType.Spiral:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_irregular;
                    labelGalaxyType.Text = "Irregular";
                    galaxytype = GalaxyType.Irregular;
                    break;
            }
        }

        private void buttonGalaxyTypeRight_Click(object sender, EventArgs e)
        {
            switch (galaxytype)
            {
                case GalaxyType.Irregular:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
                    labelGalaxyType.Text = "Spiral";
                    galaxytype = GalaxyType.Spiral;
                    break;
                case GalaxyType.Spiral:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Elliptical";
                    galaxytype = GalaxyType.Eliptical;
                    break;
                case GalaxyType.Eliptical:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Sphere";
                    galaxytype = GalaxyType.Sphere;
                    break;
                case GalaxyType.Sphere:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_irregular;
                    labelGalaxyType.Text = "Irregular";
                    galaxytype = GalaxyType.Irregular;
                    break;
            }
        }

        private void buttonGalaxySizeLeft_Click(object sender, EventArgs e)
        {
            if (galaxysize == 1)
            {
                return;
            }
            else
            {
                galaxysize -= 1;
                labelGalaxySize.Text = galaxysize.ToString();
            }
        }

        private void buttonGalaxySizeRight_Click(object sender, EventArgs e)
        {
            if (galaxysize == 5)
            {
                return;
            }
            else
            {
                galaxysize += 1;
                labelGalaxySize.Text = galaxysize.ToString();
            }
        }

        private void StarsScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            label2.Text = StarsScrollBar.Value.ToString() + " stars";
        }

        public int getGalaxySize()
        {
            return (galaxysize);
        }

        public int getStarsCount()
        {
            return (StarsScrollBar.Value);
        }

        public GalaxyType getGalaxyType()
        {
            return (galaxytype);
        }

        public bool getGalaxyRandomEvents()
        {
            return (checkBoxRandomEvents.Checked);
        }   
    }
}
