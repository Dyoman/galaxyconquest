using System;
using System.Collections.Generic;
using GalaxyConquest.Tactics;
using GalaxyConquest.Drawing;
using GalaxyConquest.Game;

namespace GalaxyConquest
{
    /// <summary>
    /// Представляет флот
    /// </summary>
    [Serializable]
    public class Fleet : SpaceObject
    {
        /// <summary>
        /// Владелец флота
        /// </summary>
        public Player Owner { get; private set; }
        /// <summary>
        /// Корабли во флоте
        /// </summary>
        public List<Ship> ships;
        /// <summary>
        /// Система в которой находится флот в данный момент
        /// </summary>
        public StarSystem s1 = null;
        /// <summary>
        /// Система в которую флот полетит во время шага
        /// </summary>
        public StarSystem s2 = null;
        /// <summary>
        /// Цель для захвата!
        /// </summary>
        public StarSystem CaptureTarget { get; private set; }
        /// <summary>
        /// Дистанция до цели (s2)
        /// </summary>
        public double starDistanse;
        /// <summary>
        /// Флаг, показывающий находится флот в пути или нет
        /// </summary>
        public bool onWay;
        /// <summary>
        /// Флаг, показывающий находится флот в процессе захвата системы или нет
        /// </summary>
        public bool Capturing { get; private set; }
        /// <summary>
        /// Прогресс захвата (0-5)
        /// </summary>
        int captureProgress;
        /// <summary>
        /// Путь флота
        /// </summary>
        public Way way { get; private set; }

        /// <summary>
        /// Максимальная дистанция, на которую флот способен лететь
        /// </summary>
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
                Random rand = new Random((int) DateTime.Now.Ticks);
                for (int i = 0; i < size; i++)
                {
                    int ship_type = rand.Next(0, 2);
                    int weapon_type = rand.Next(0, 2);
                    int armor_type = rand.Next(0, 3);
                    Weapon weapon = null;
                    Ship ship = null;
                    Armor armor = null;
                    switch (weapon_type)
                    {
                        case 0:
                            weapon = new WpnGauss();
                            break;
                        case 1:
                            weapon = new wpnLightLaser();
                            break;
                        case 2:
                            weapon = new WpnPlasma();
                            break;
                    }
                    switch (armor_type)
                    {
                        case 0:
                            armor = new ArmorNone();
                            break;
                        case 1:
                            armor = new ArmorTitan();
                            break;
                        case 2:
                            armor = new ArmorMolibden();
                            break;
                        case 3:
                            armor = new ArmorNanocom();
                            break;
                    }
                    switch (ship_type)
                    {
                        case 0:
                            ship = new ShipScout(playerID, weapon, armor);
                            break;
                        case 1:
                            ship = new ShipAssaulter(playerID, weapon, armor);
                            break;
                        case 2:
                            ship = new ShipСolonizer(playerID, new WpnNone(), armor);
                            break;
                    }
                    ship.player = playerID;
                    ships.Add(ship);
                }
            }
            else
            {
                name = "Флот <" + player.name + ">";
                Ship ship = new ShipСolonizer(playerID, new WpnNone(), new ArmorNone());
                ship.player = playerID;
                ships.Add(ship);
                for (int i = 1; i < size; i++)
                {
                    ship = new ShipScout(playerID, new WpnNone(), new ArmorNone());
                    ship.player = playerID;
                    ships.Add(ship);
                }
            }
            this.s1 = s1;
            x = s1.x;
            y = s1.y;
            z = s1.z;
        }

        public override void Move(double time)
        {
            if (s2 == null)//обновляем координаты флота, если во время шага он остается в своей системе
            {
                y = s1.y;
                z = s1.z;
                x = s1.x;
            }
            else if (starDistanse > 0.5)//Обновляем координаты, если флот летит. динамический рассчет дистанции нужен для равномерного движения
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
            else//Флот долетел до звезды
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
        /// <summary>
        /// Устанавливает цель для флота
        /// </summary>
        /// <param name="s">Звездная система</param>
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
        /// <summary>
        /// Процесс захвата системы. Если флот ничего не захватывает, метод ничего не делает
        /// </summary>
        public void CaptureProcess()
        {
            if (!Capturing)
                return;

            captureProgress += 1;

            if (captureProgress >= 5)
            {
                Owner.stars.Add(CaptureTarget);
                for (int i = 0; i < CaptureTarget.planets.Count; i++)
                    Owner.player_planets.Add(CaptureTarget.planets[i]);

                CaptureTarget = null;
                Capturing = false;
            }
        }
        /// <summary>
        /// Возвращает прогресс захвата звездной системы (0 - 5)
        /// </summary>
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
        /// <summary>
        /// Получает информацию о жизни флота. Если хотябы один корабль имеет ненулевой запас прочности, флот считается живым
        /// </summary>
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
