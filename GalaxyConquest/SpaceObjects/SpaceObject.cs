using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest
{
    /// <summary>
    /// Базовый класс космического объекта. Определяет общие свойства и методы для всех остальных космических объектов
    /// </summary>
    [Serializable]
    public abstract class SpaceObject
    {
        public double x, y, z;
        public string name;

        //Осуществляет движение объекта. Обязателен для переопределения в классе-потомке
        public abstract void Move(double time);
    }
}
