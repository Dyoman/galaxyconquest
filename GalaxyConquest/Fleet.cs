using System;
using System.Collections.Generic;
using GalaxyConquest.Tactics;

namespace GalaxyConquest
{
    [Serializable]
    public class Fleet : SpaceObject
    {
        public List<Ship> ships;

        public StarSystem s1 = null;
        public StarSystem s2 = null;

        public double starDistanse;
        public bool onWay;

        public Fleet()
        {
            ships = new List<Ship>();
            onWay = false;
        }

        public Fleet(Player player, int size, StarSystem s1)
        {
            int playerID = 1;
            if (player == null)
            {
                playerID = 2;
                name = "Нейтральный флот";
            }
            else
                name = "Флот <" + player.name + ">";

            ships = new List<Ship>();
            onWay = false;
            Random rand = new Random((int)DateTime.Now.Ticks);

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
                    case 0: ship = new ShipScout(playerID, weapon); break;
                    case 1: ship = new ShipAssaulter(playerID, weapon); break;
                }

                ship.player = playerID;
                ships.Add(ship);
            }
            this.s1 = s1;
            x = s1.x;
            y = s1.y;
            z = s1.z;
        }

        public override void Move(double time)
        {
            if (s2 == null)
            {
                y = s1.y;
                z = s1.z;
                x = s1.x;
            }
            else if (starDistanse > 0.5)
            {
                onWay = true;

                double dx = (s2.x - x) / starDistanse;
                double dy = (s2.y - y) / starDistanse;
                double dz = (s2.z - z) / starDistanse;
                                
                x += dx;
                y += dy;
                z += dz;

                starDistanse = Math.Sqrt(Math.Pow(s2.x - x, 2) + Math.Pow(s2.y - y, 2) + Math.Pow(s2.z - z, 2));
            }
            else
            {
                onWay = false;
                s1 = s2;
                s2 = null;
                starDistanse = 0;
                x = s1.x;
                y = s1.y;
                z = s1.z;
            }
        }

        public void setTarget(StarSystem s)
        {
            if (s == null)
            {
                s2 = null;
                starDistanse = 0;
            }
            else
            {
                s2 = s;
                starDistanse = Math.Sqrt(Math.Pow(s.x - x, 2) + Math.Pow(s.y - y, 2) + Math.Pow(s.z - z, 2));
            }
        }

        public bool Allive
        {
            get
            {
                bool allive = false;
                for (int i = 0; i < ships.Count; i++)
                    allive |= ships[i].currentHealth > 0;
                return allive;
            }
        }
    }
}
