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
    public class PLANET : SpaceObject
    {
       /// <summary>
       /// Координаты центрального тела
       /// </summary>
        public PointF CENTER = new PointF(0, 0);
       /// <summary>
       /// Скорость вращения вокруг центрального тела
       /// </summary>
        public float SPEED = 0.1f;
       /// <summary>
       /// Дистанция от центрального тела
       /// </summary>
        public float DISTANCE = 100f;
       /// <summary>
       /// Фаза вращения
       /// </summary>
        public float ROT = 0f;
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
        public string OWNERNAME = "None";
       /// <summary>
       /// Прибыль, которую можно получить, захватив планету
       /// </summary>
        public double PROFIT = 0;
       /// <summary>
       /// Прирост очков изучения, который можно получить, захватив планету
       /// </summary>
        public float skillPointProduce = 10;
        public Color CLR = Color.FromArgb(255, 255, 255);

       /// <summary>
       /// Получает текущие координаты планеты в двухмерном пространстве
       /// </summary>
        public PointF GetPoint()
        {
            return new PointF((float)x, (float)y);
        }

        public override void Move(double time)
        {
            x = (float)Math.Sin(time * SPEED) * DISTANCE + CENTER.X;
            y = (float)Math.Cos(time * SPEED) * DISTANCE + CENTER.Y;
        }

        public double Inc(double p, double f)
        {
            p = p + (p / 2.75) - (p / (f * 200));
            return p;
        }
    }

}
