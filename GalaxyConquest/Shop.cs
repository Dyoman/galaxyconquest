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
            if (Form1.Game.Player.credit > 50)
            {
                wpnLightLaser w = new wpnLightLaser();
                ShipAssaulter s = new ShipAssaulter(1, w);
                Form1.Game.Player.player_ship.Add(s);
                Form1.Game.Player.fleets[Form1.Game.Player.selectedFleet].ships.Add(s);
                Form1.Game.Player.credit -= 50;
                MessageBox.Show("Корабль построен");
            }
            else
            {
                MessageBox.Show("Недостаточно ресурсов");
            }
        }

        private void build_button2_Click(object sender, EventArgs e)
        {
            if (Form1.Game.Player.credit > 25)
            {
                wpnLightLaser w = new wpnLightLaser();
                ShipScout s = new ShipScout(1, w);
                Form1.Game.Player.player_ship.Add(s);
                Form1.Game.Player.fleets[Form1.Game.Player.selectedFleet].ships.Add(s);
                Form1.Game.Player.credit -= 25;
                MessageBox.Show("Корабль построен");
            }
            else
            {
                MessageBox.Show("Недостаточно ресурсов");
            }
        }
        private void button_form_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Form1.Game.Player.player_ship.Count; i++)
            {
                Fleet f = new Fleet();
                Ship ship = new Ship();
                ship = Form1.Game.Player.player_ship[i];
                f.ships.Add(ship);
            }
            MessageBox.Show("Флот сформирован");
            }
            

       
    }
}
