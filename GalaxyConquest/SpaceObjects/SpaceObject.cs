using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest
{
    /// <summary>
    /// Абстрактный класс космического объекта. Определяет общие свойства и методы для всех остальных космических объектов
    /// </summary>
    [Serializable]
    public abstract class SpaceObject
    {
        /// <summary>
        /// Координаты объекта в трехмерном пространстве
        /// </summary>
        public double x, y, z;
        /// <summary>
        /// Имя объекта
        /// </summary>
        public string name;
        /// <summary>
        /// Осуществляет движение космического объекта. Обязателен для переопределения в классах-потомках
        /// </summary>
        /// <param name="time">Время галактики</param>
        public abstract void Move(double time);
    }
}
