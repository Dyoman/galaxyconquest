using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyConquest.Tactics
{
    public static class Constants
    {
        // типы объектов
        public const int SHIP = 1;
        public const int METEOR = 2;
        // классы кораблей
        public const int SCOUT = 1;
        public const int COLONIZER = 2;
        public const int ASSAULTER = 3;

        // вооружение
        public const int NONE = 0;
        public const int GAUSS = 1;
        public const int LASER = 2;
        public const int PLASMA = 3;
        // направления, относительно координат
        public const int NORMAL = 0;
        public const int LEFT = -1;
        public const int RIGHT = 1;
        public const int TOP = -2;
        public const int MEDIUM_TOP = -1;
        public const int BOTTOM = 2;
        public const int MEDIUM_BOTTOM = 1;
        // еще одни направления
        public const int MOVE_TOP = 1;
        public const int MOVE_RIGHT_TOP = 2;
        public const int MOVE_RIGHT_BOTTOM = 3;
        public const int MOVE_BOTTOM = 4;
        public const int MOVE_LEFT_BOTTOM = 5;
        public const int MOVE_LEFT_TOP = 6;
    }
}
