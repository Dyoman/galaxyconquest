using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GalaxyConquest.SpaceObjects
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
        public Star center;
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

        public float POPULATIONFACTOR = 1;
       /// <summary>
       /// Количество минералов на планете
       /// </summary>
        public int MINERALS = 1;
       /// <summary>
       /// Размер планеты
       /// </summary>
        public float MINERALFACTOR = 1;
        /// <summary>
        /// Минимальное количество населения
        /// </summary>
        public double POPULATION = 0;
        /// <summary>
        /// Размер планеты
        /// </summary>
        public float SIZE = 10;
        /// <summary>
        /// Прибыль, которую можно получить, захватив планету
        /// </summary>
        public double PROFIT = 0;
        /// <summary>
        /// Прирост очков изучения, который можно получить, захватив планету
        /// </summary>
        public float skillPointProduce = 10;
        /// <summary>
        /// Климат планеты
        /// </summary>
        public int CLIMATE = 0;
        /// <summary>
        /// Цвет планеты
        /// </summary>
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

            POPULATION = Math.Min(POPULATIONMAX, POPULATION + POPULATION * 0.1 * climateFactor);

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
            POPULATIONMAX = popfactor * climateFactor;

        }
        /// <summary>
        /// Генерирует планеты для звездной системы
        /// </summary>
        public static void generatePlanets(StarSystem s, double Time)
        {
            int sizemin = 10;
            int sizemax = 40;
            int mineralmin = 0;
            int mineralmax = 4;
            int climatemin = 0;
            int climatemax = 4;
            int colormin = 0;
            int colormax = 255;
            float speed = 1f;

            if (s.planets.Count > 0)//Обновляем список планет
            {
                s.planets.Clear();
            }

            Random r = new Random(DateTime.Now.Millisecond);

            int planets_count = s.type + r.Next(1, 2);//Количество планет в системе варьируется

            s.centralStar = new Star();
            s.centralStar.name = s.name;
            s.centralStar.size = r.Next(sizemax, sizemax * 2);
            s.centralStar.color = s.color;

            int dist = s.centralStar.size + 30;
            for (int i = 0; i <= planets_count; i++)
            {
                Planet pln = new Planet();

                pln.center = s.centralStar;
                pln.distance = dist;
                pln.speed = speed;
                pln.planetColor = Color.FromArgb((r.Next(colormin, colormax)), (r.Next(colormin, colormax)), (r.Next(colormin, colormax)));
                pln.SIZE = r.Next(sizemin, sizemax);

                pln.name = s.name + " " + i.ToString();     //Имя планеты = <Имя звезды> <порядковый номер>

                pln.CLIMATE = r.Next(climatemin, climatemax);
                pln.POPULATIONMAX = 1;
                pln.PROFIT = pln.POPULATION * pln.MINERALS;
                pln.POPULATION = 1;
                pln.MINERALS = r.Next(mineralmin, mineralmax);
                pln.PROFIT = pln.POPULATION * pln.MINERALS;
                pln.Owner = s.Owner;

                pln.Move(Time);

                s.planets.Add(pln);

                dist += 30;//каждая следующая планета будет удалена от центра на 30 пикселей дальше
                speed = speed / 3 + 0.1f;
            }
        }
    }

}
