using System;
using System.Windows.Forms;

namespace GalaxyConquest
{
    public partial class Form_NewGameDialog : Form
    {
        int galaxytype = 0;
        int galaxysize = 0;

        public Form_NewGameDialog()
        {
            InitializeComponent();
        }

        private void buttonGalaxyTypeLeft_Click(object sender, EventArgs e)
        {
            switch (galaxytype)
            {
                case 0:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Sphere";
                    galaxytype = 3;
                    break;
                case 1:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
                    labelGalaxyType.Text = "Spiral";
                    galaxytype = 0;
                    break;
                case 2:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Elliptical";
                    galaxytype = 1;
                    break;
                case 3:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_irregular;
                    labelGalaxyType.Text = "Irregular";
                    galaxytype = 2;
                    break;
            }
        }

        private void buttonGalaxyTypeRight_Click(object sender, EventArgs e)
        {
            switch (galaxytype)
            {
                case 0:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Elliptical";
                    galaxytype = 1;
                    break;
                case 1:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_irregular;
                    labelGalaxyType.Text = "Irregular";
                    galaxytype = 2;
                    break;
                case 2:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_sphere;
                    labelGalaxyType.Text = "Sphere";
                    galaxytype = 3;
                    break;
                case 3:
                    pictureBoxGalaxyType.Image = Properties.Resources.icon_newgame_spiral;
                    labelGalaxyType.Text = "Spiral";
                    galaxytype = 0;
                    break;
            }
        }

        private void buttonGalaxySizeLeft_Click(object sender, EventArgs e)
        {
            if (galaxysize == 0)
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
            starsCount.Text = StarsScrollBar.Value.ToString();
        }

        public int getGalaxySize()
        {
            return (galaxysize);
        }

        public int getStarsCount()
        {
            return (StarsScrollBar.Value);
        }

        public int getGalaxyType()
        {
            return (galaxytype);
        }

        public bool getGalaxyRandomEvents()
        {
            return (checkBoxRandomEvents.Checked);
        }

    }
}
