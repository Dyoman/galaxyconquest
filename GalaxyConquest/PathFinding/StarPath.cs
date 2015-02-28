using GalaxyConquest.Drawing;
using GalaxyConquest.SpaceObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest.PathFinding
{
    /// <summary>
    /// Представляет путь от одной звездной системы до другой в виде коллекции типа StarSystem. 
    /// Расширяет класс System.Collections.Generic.List.
    /// Доступны операции поиска пути, получение начальной, текущей, следующей и конечной точек пути.
    /// </summary>
    public class StarPath : List<StarSystem>
    {
        /// <summary>
        /// Рассчитывает путь от одной звездной системы до другой
        /// </summary>
        /// <param name="from">Начальная точка</param>
        /// <param name="to">Конечная точка</param>
        public void CalculateWay(StarSystem from, StarSystem to)
        {
            Clear();
            Current = 0;
            AddRange(PathFinder.GetWay(from, to));
        }
        /// <summary>
        /// Возвращает true, если путь состоит как минимум из двух точек.
        /// </summary>
        public bool Empty
        {
            get
            {
                return Count < 1;
            }
        }
        /// <summary>
        /// Расстояние, которое нужно пройти при движении от первого элемента до последнего
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
        /// Переходит на следующую точку пути и возвращает её.
        /// </summary>
        /// <returns></returns>
        public StarSystem Next()
        {
            if (Current == Count - 1)
                return null;
            else
            {
                Current++;
                return this[Current];
            }
        }
        /// <summary>
        /// Получает начальную точку пути
        /// </summary>
        public StarSystem First
        {
            get
            {
                if (Count > 0)
                    return this[0];
                else
                    return null;
            }
        }
        /// <summary>
        /// Получает текущую точку пути
        /// </summary>
        public int Current { get; private set; }
        /// <summary>
        /// Получает конечную точку пути
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
