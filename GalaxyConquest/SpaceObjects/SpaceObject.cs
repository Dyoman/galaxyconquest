using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest
{
    /// <summary>
    /// Абстрактный класс космического объекта. Определяет общие свойства и методы для всех остальных космических объектов.
    /// </summary>
    [Serializable]
    public abstract class SpaceObject
    {
        /// <summary>
        /// Координаты объекта в трехмерном пространстве.
        /// </summary>
        public double x, y, z;
        /// <summary>
        /// Имя объекта.
        /// </summary>
        public string name;
        /// <summary>
        /// Осуществляет движение космического объекта. Обязателен для переопределения в классах-потомках.
        /// </summary>
        /// <param name="time">Время галактики</param>
        public abstract void Move(double time);
        /// <summary>
        /// Осуществляет процесс развития объекта во время шага. Доступен для переопределения в классах-потомках.
        /// </summary>
        public virtual void Process()
        {
            throw new NotImplementedException("Виртуальный метод SpaceObject.Process() не переопределён.");
        }
    }
}
