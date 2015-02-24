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
            x = (float)Math.Sin(time * speed) * distance + center.X;
            y = (float)Math.Cos(time * speed) * distance + center.Y;
        }

        public double Inc(double p, double f)
        {
            p = p + (p / 2.75) - (p / (f * 200));
            return p;
        }
    }

}
