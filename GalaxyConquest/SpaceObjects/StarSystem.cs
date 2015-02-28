using System;
using System.Drawing;
using System.Collections.Generic;

namespace GalaxyConquest.SpaceObjects
{
    /// <summary>
    /// Представляет звездную систему
    /// </summary>
    [Serializable]
    public class StarSystem : SpaceObject
    {
        /// <summary>
        /// Угловая скорость планеты
        /// </summary>
        public double angVel = 0.05;

        public double R = 0, timeOffset = 0, increment = 0;//Радиус движения системы, смещение фазы и смещение координат
        /// <summary>
        /// Тип звезды
        /// </summary>
        public int type;
        /// <summary>
        /// Браш, в виде которого планета будет представлена на экране
        /// </summary>
        public SolidBrush color;
        /// <summary>
        /// Флаг, показывающий открыта ли система
        /// </summary>
        public bool Discovered = false;
        /// <summary>
        /// Центральное тело звездной системы.
        /// </summary>
        public Star centralStar = new Star();
        /// <summary>
        /// Планеты в звездной системе
        /// </summary>
        public List<Planet> planets = new List<Planet>();

        public override void Move(double time)
        {
            for (int i = 0; i < planets.Count; i++)
                planets[i].Move(time);
        }
        /// <summary>
        /// Осуществляет все изменения, которые должны происходить со звездной системой во время шага.
        /// </summary>
        public override void Process()
        {
            for (int i = 0; i < planets.Count; i++)
                planets[i].Process();
        }
    }
}
