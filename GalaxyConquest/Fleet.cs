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
        /// Максимальная дистанция, на которую флот способен лететь
        /// </summary>
        public static double MaxDistance = 440;

        public Fleet()
        {
            ships = new List<Ship>();
            onWay = false;
            Capturing = false;
            Owner = null;
        }

        public Fleet(Player player, StarSystem s1)
        {
            Owner = player;
            ships = new List<Ship>();
            onWay = false;
            Capturing = false;

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
            Owner = player;
            ships = new List<Ship>();
            onWay = false;
            Capturing = false;

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

                starDistanse = Math.Sqrt(Math.Pow(s2.x - x, 2) + Math.Pow(s2.y - y, 2) + Math.Pow(s2.z - z, 2));
            }
            else//Флот долетел до звезды
            {
                onWay = false;
                s1 = s2;
                s2 = null;
                starDistanse = 0;
                x = s1.x;
                y = s1.y;
                z = s1.z;

                s1.Discovered = true;
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
                s2 = null;
                starDistanse = 0;
            }
            else
            {
                s2 = s;
                starDistanse = Math.Sqrt(Math.Pow(s.x - x, 2) + Math.Pow(s.y - y, 2) + Math.Pow(s.z - z, 2));
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
                for (int i = 0; i < CaptureTarget.PLN.Count; i++)
                    Owner.player_planets.Add(CaptureTarget.PLN[i]);

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
    }
}
