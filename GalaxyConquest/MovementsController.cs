using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest
{
    /// <summary>
    /// Абстрактный класс, описывающий общий принцип движения в галактике
    /// </summary>
    abstract class MovementsController
    {
        /// <summary>
        /// Фиксированное число, на которое будет изменяться время
        /// </summary>
        public const double FIXED_TIME_DELTA = 0.01;
        /// <summary>
        /// Двигает несколько космических объектов
        /// </summary>
        /// <param name="obj">Массив объектов</param>
        /// <param name="time">Время галактики</param>
        public static void Process(SpaceObject[] obj, double time)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].Move(time);
            }
        }
        /// <summary>
        /// Двигает отдельный космический объект
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="time">Время галактики</param>
        public static void Process(SpaceObject obj, double time)
        {
            obj.Move(time);
        }
    }
}
