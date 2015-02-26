using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GalaxyConquest.StarSystems
{
    /// <summary>
    /// Представляет планету
    /// </summary>
    [Serializable]
    public class Planet : SpaceObject
    {
        /// <summary>
        /// Координаты центрального тела
        /// </summary>
        public PointF center = new PointF(0, 0);
        /// <summary>
        /// Скорость вращения вокруг центрального тела
        /// </summary>
        public float speed = 0.1f;
        /// <summary>
        /// Дистанция от центрального тела
        /// </summary>
        public float distance = 100f;
        /// <summary>
        /// Фаза вращения
        /// </summary>
        public float phase = 0f;
        /// <summary>
        /// Максимальное количество населения
        /// </summary>
        public float POPULATIONMAX = 0;
        /// <summary>
        /// Минимальное количество населения
        /// </summary>
        public double POPULATION = 0;
        /// <summary>
        /// Количество минералов на планете
        /// </summary>
        public float MINERALS = 10;
        /// <summary>
        /// Размер планеты
        /// </summary>
        public float SIZE = 10;
        /// <summary>
        /// Имя обладателя планеты
        /// </summary>
        public string ownerName = "None";
        /// <summary>
        /// Прибыль, которую можно получить, захватив планету
        /// </summary>
        public double PROFIT = 0;
        /// <summary>
        /// Прирост очков изучения, который можно получить, захватив планету
        /// </summary>
        public float skillPointProduce = 10;
        /// <summary>
        /// Цвет планеты
        /// </summary>
        public int CLIMATE = 0;
        public Color planetColor = Color.FromArgb(255, 255, 255);

        /// <summary>
        /// Получает текущие координаты планеты в двухмерном пространстве
        /// </summary>
        public PointF GetPosition()
        {
            return new PointF((float)x, (float)y);
        }

        public override void Move(double time)
        {
            x = (float)Math.Sin(time * speed) * distance;
            z = (float)Math.Cos(time * speed) * distance;
        }

        public double Inc(double p, double f)
        {
            p = p + (p / 2.75) - (p / (f * 200));
            return p;
        }
        /// <summary>
        /// Осуществляет все изменения, которые должны происходить с планетой во время шага.
        /// </summary>
        public override void Process()
        {
            float climateFactor = 0;
            float mineralFactor = 0;

            switch (CLIMATE)
            {
                case 0:
                    climateFactor = (float)0.3;
                    mineralFactor = (float)0.05;
                    break;
                case 1:
                    climateFactor = (float)0.5;
                    mineralFactor = (float)0.2;
                    break;
                case 2:
                    climateFactor = (float)0.8;
                    mineralFactor = (float)1;
                    break;
                case 3:
                    climateFactor = (float)1;
                    mineralFactor = (float)1.5;
                    break;
                case 4:
                    climateFactor = (float)2;
                    mineralFactor = (float)4;
                    break;
                default:
                    //MessageBox.Show("Error occured with climat number(" + Game.Player.player_planets[i].CLIMATE + ")");
                    break;
            }

            if (POPULATION < POPULATIONMAX)
            {
                POPULATION += POPULATION * 0.1 * climateFactor;
                POPULATION = Math.Round(POPULATION, 3);
            }

            float popfactor = 0;

            {
                if (SIZE < 15)

                    popfactor = 5;
                else if ((SIZE >= 15) && (SIZE < 23))
                    popfactor = 10;
                else if ((SIZE >= 23) && (SIZE < 30))
                    popfactor = 15;
                else if (SIZE >= 30)
                    popfactor = 20;

            }

            PROFIT = mineralFactor * POPULATION;
            PROFIT = Math.Round(PROFIT, 2);
            POPULATIONMAX = popfactor * climateFactor;

        }
    }

}
