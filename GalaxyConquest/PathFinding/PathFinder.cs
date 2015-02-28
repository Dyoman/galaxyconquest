using GalaxyConquest.Drawing;
using GalaxyConquest.SpaceObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest.PathFinding
{
    /// <summary>
    /// Представляет инструмент для поиска пути.
    /// </summary>
    public static class PathFinder
    {
        /// <summary>
        /// Дистанции от каждой звездной системы до каждой. Нужно для поиска оптимального пути.
        /// Вне класса доступен только для чтения.
        /// </summary>
        public static double[][] Distances { get; private set; }
        /// <summary>
        /// Расчитывает дистанции от каждой системы до каждой и заносит их в массив Distances
        /// </summary>
        public static void FillDistancesFrom(List<StarSystem> systems)
        {
            Distances = new double[systems.Count][];
            for (int i = 0; i < systems.Count; i++)
            {
                Distances[i] = new double[systems.Count];
                for (int j = 0; j < systems.Count; j++)
                    if (i == j)
                        Distances[i][j] = double.MaxValue;
                    else
                        Distances[i][j] = DrawController.Distance(systems[i], systems[j]);
            }
        }
        /// <summary>
        /// Ищет путь от одной звездной системы до другой. (Метод в разработке)
        /// </summary>
        /// <param name="from">Начальная точка полета.</param>
        /// <param name="to">Конечная точка полета.</param>
        public static List<StarSystem> FindOptimalWay(StarSystem from, StarSystem to)
        {
            double[][] path = Distances;

            for (int i = 1; i < Distances.Length + 1; i++)
                for (int j = 0; j < Distances.Length - 1; j++)
                    for (int k = 0; k < Distances.Length - 1; k++)
                        if (path[j][k] > path[j][i - 1] + path[i - 1][k])
                            path[j][k] = path[j][i - 1] + path[i - 1][k];

            return new List<StarSystem>();
        }
        /// <summary>
        /// Ищет путь от одной звездной системы до другой. (Старый метод)
        /// </summary>
        /// <param name="from">Начальная точка полета.</param>
        /// <param name="to">Конечная точка полета.</param>
        public static List<StarSystem> FindWay(StarSystem from, StarSystem to)
        {
            List<StarSystem> path = new List<StarSystem>();
            if (from == null || to == null) return path;

            StarSystem currSys = from, s, tS = null;
            path.Add(currSys);
            int breakPoint = 90;
            while (true)
            {
                Vector3 mainDirection = new Vector3(to.x - currSys.x, to.y - currSys.y, to.z - currSys.z);
                double minDistance = double.MaxValue;
                for (int i = 0; i < Form1.Game.Galaxy.stars.Count; i++)
                {
                    s = Form1.Game.Galaxy.stars[i];
                    if (s == currSys) continue;

                    Vector3 direction = new Vector3(s.x - currSys.x, s.y - currSys.y, s.z - currSys.z);
                    double angle = mainDirection.ScalarWith(direction);
                    double distance = DrawController.Distance(currSys, s);
                    if (distance < minDistance && angle > 0)
                    {
                        minDistance = distance;
                        tS = s;
                    }
                }
                currSys = tS;
                path.Add(tS);
                if (path.Count > breakPoint) //Если путь не находится, тупо делаем как было)
                {
                    path.Clear();
                    path.Add(from);
                    path.Add(to);
                    break;
                }
                if (tS == to) break;
            }
            return path;
        }
        /// <summary>
        /// Получает прямой путь от одной звездной системы до другой. (Старый метод)
        /// </summary>
        /// <param name="from">Начальная точка полета.</param>
        /// <param name="to">Конечная точка полета.</param>
        public static List<StarSystem> GetWay(StarSystem from, StarSystem to)
        {
            List<StarSystem> path = new List<StarSystem>();
            path.Add(from);
            path.Add(to);
            return path;
        }
    }
}
