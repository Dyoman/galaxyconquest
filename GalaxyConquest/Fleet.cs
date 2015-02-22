using System;
using System.Collections.Generic;
using GalaxyConquest.Tactics;
using GalaxyConquest.Drawing;
using GalaxyConquest.Game;

namespace GalaxyConquest
{
    [Serializable]
    public class Fleet : SpaceObject
    {
        public Player Owner { get; private set; }
        public List<Ship> ships;

        public StarSystem s1 = null;
        public StarSystem s2 = null;

        public StarSystem CaptureTarget { get; private set; }

        public double starDistanse;
        public bool onWay;

        public bool Capturing { get; private set; }
        int captureProgress;

        public Way way { get; private set; }

        public static double MaxDistance = 440;

        public Fleet()
        {
            ships = new List<Ship>();
            onWay = false;
            Capturing = false;
            Owner = null;
            way = new Way();
        }

        public Fleet(Player player, StarSystem s1)
        {
            ships = new List<Ship>();
            onWay = false;
            Capturing = false;
            Owner = player;
            way = new Way();

            if (player == null)
                name = "Нейтральный флот";
            else
                name = "Флот <" + player.name + ">";

            this.s1 = s1;
            x = s1.x;
            y = s1.y;
            z = s1.z;
        }

        public Fleet(Player player, int size, StarSystem s1)
        {
            ships = new List<Ship>();
            onWay = false;
            Capturing = false;
            Owner = player;
            way = new Way();

            int playerID = 1;
            if (player == null)
            {
                playerID = 2;
                name = "Нейтральный флот";
            }
            else
                name = "Флот <" + player.name + ">";

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

                //starDistanse = Math.Sqrt(Math.Pow(s2.x - x, 2) + Math.Pow(s2.y - y, 2) + Math.Pow(s2.z - z, 2));
                starDistanse = DrawController.Distance(this, s2);
            }
            else
            {
                s1 = s2;
                s2 = way.Next();

                if (s2 == null)
                {
                    way.Clear();
                    starDistanse = 0;
                    onWay = false;

                    s1.Discovered = true;
                }
                else
                    starDistanse = DrawController.Distance(s1, s2);

                x = s1.x;
                y = s1.y;
                z = s1.z;
            }
        }

        public void setTarget(StarSystem s)
        {
            if (s == null)
            {
                way.Clear();
                s2 = null;
                starDistanse = 0;
            }
            else
            {
                way.CalculateWay(s1, s);
                s2 = way[0];
                starDistanse = Math.Sqrt(Math.Pow(way[0].x - x, 2) + Math.Pow(way[0].y - y, 2) + Math.Pow(way[0].z - z, 2));
            }
        }

        public void CaptureProcess()
        {
            if (!Capturing)
                return;

            captureProgress += 1;

            if (captureProgress >= 5)
            {
                Owner.stars.Add(CaptureTarget);
                for (int i = 0; i < CaptureTarget.PLN.Count; i++)
                    Owner.player_planets.Add(CaptureTarget.PLN[i]);

                CaptureTarget = null;
                Capturing = false;
            }
        }

        /// <summary>
        /// Возвращает прогресс захвата звездной системы (1 - 5)
        /// </summary>
        /// <returns></returns>
        public int getCaptureProgress()
        {
            return (int)captureProgress;
        }

        /// <summary>
        /// Пытается начать захват звездной системы флотом
        /// </summary>
        /// <param name="s">Система, которую нужно захватить</param>
        /// <returns>true, если захват начался и false, если захват уже идёт, либо выбрана не та система, в которой находится флот</returns>
        public bool StartCapturing(StarSystem s)
        {
            if (Capturing || s != s1)
                return false;

            CaptureTarget = s;
            Capturing = true;
            captureProgress = 0;
            return true;
        }

        /// <summary>
        /// Останавливает захват системы
        /// </summary>
        public void StopCapturing()
        {
            CaptureTarget = null;
            Capturing = false;
            captureProgress = 0;
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

        void CalculateWayTo(StarSystem target)
        {
            way = new Way();
            StarSystem currSys = s1, s, tS = null;

            while (true)
            {
                Vector3 mainDirection = new Vector3(target.x - currSys.x, target.y - currSys.y, target.z - currSys.z);
                double minDistance = double.MaxValue;
                for (int i = 0; i < Form1.Game.Galaxy.stars.Count; i++)
                {
                    s = Form1.Game.Galaxy.stars[i];
                    if (s == currSys) continue;

                    Vector3 direction = new Vector3(s.x -  currSys.x, s.y - currSys.y, s.z - currSys.z);
                    double angle = mainDirection.ScalarWith(direction);
                    double distance = DrawController.Distance(currSys, s);
                    if (distance < minDistance && angle > 0.3)
                    {
                        minDistance = distance;
                        tS = s;
                    }
                }
                currSys = tS;
                way.Add(tS);
                if (tS == target) break;
            }
        }
    }

    /// <summary>
    /// Представляет путь в виде последовательности звездных систем
    /// </summary>
    public class Way : List<StarSystem>
    {
        int current = 0;
        /// <summary>
        /// Суммарный путь, который нужно пройти при движении от первого элемента до последнего
        /// </summary>
        public double Distance
        {
            get
            {
                double dist = 0;
                for (int i = 1; i < Count; i++)
                    dist += DrawController.Distance(this[i], this[i - 1]);
                return dist;
            }
        }
        /// <summary>
        /// Рассчитывает путь от одной звезды до другой
        /// </summary>
        /// <param name="from">Начальная точка</param>
        /// <param name="to">Конечная точка</param>
        public void CalculateWay(StarSystem from, StarSystem to)
        {
            Clear();
            current = 0;
            if (from == null || to == null) return;

            StarSystem currSys = from, s, tS = null;
            Add(currSys);
            int breakPoint = 90;
            while (true)
            {
                Vector3 mainDirection = new Vector3(to.x - currSys.x, to.y - currSys.y, to.z - currSys.z);
                double minDistance = double.MaxValue;
                for (int i = 0; i < Form1.Game.Galaxy.stars.Count; i++)
                {
                    s = Form1.Game.Galaxy.stars[i];
                    if (s == currSys) continue;

                    Vector3 direction = new Vector3(s.x - currSys.x, s.y - currSys.y, s.z - currSys.z);
                    double angle = mainDirection.ScalarWith(direction);
                    double distance = DrawController.Distance(currSys, s);
                    if (distance < minDistance && angle > 0)
                    {
                        minDistance = distance;
                        tS = s;
                    }
                }
                currSys = tS;
                Add(tS);
                if (Count > breakPoint) //Если путь не находится, тупо делаем как было)
                {
                    Clear();
                    Add(from);
                    Add(to);
                    break;
                }
                if (tS == to) break;
            }
        }

        public StarSystem Next()
        {
            if (current == Count - 1)
                return null;
            else
            {
                current++;
                return this[current];
            }
        }

        public int Current
        {
            get
            {
                return current;
            }
        }
        /// <summary>
        /// Конечная точка пути
        /// </summary>
        public StarSystem Last
        {
            get
            {
                if (Count > 0)
                    return this[Count - 1];
                else
                    return null;
            }
        }
    }
}
