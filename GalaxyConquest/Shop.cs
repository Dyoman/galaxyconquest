using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GalaxyConquest.StarSystems;
using GalaxyConquest;
using GalaxyConquest.Tactics;

namespace GalaxyConquest
{
    public partial class Shop : Form
    {
        public Shop()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Shop_Load(object sender, EventArgs e)
        {

        }

        public void set_listbox_planet(PLANET p)
        {
            listBox_Planet.Items.Add(p);
        }

        private void build_button1_Click(object sender, EventArgs e)
        {
            wpnLightLaser w = new wpnLightLaser();
            ShipAssaulter s = new ShipAssaulter(1, w);
            Player.player_ship.Add(s);
        }

        private void button_form_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Player.player_ship.Count; i++)
            {
                Fleet f = new Fleet();
                Ship ship = new Ship();
                ship = Player.player_ship[i];
                f.ships.Add(ship);
            }
            }
    }
}
