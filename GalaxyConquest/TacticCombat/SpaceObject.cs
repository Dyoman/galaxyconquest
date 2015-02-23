using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GalaxyConquest.Tactics
{
    [Serializable]
    public abstract class SpaceObject
    {
        public int objectType; // подробности смотри в константах
        public int player; // 0,1,2 ..0 - нейтральные объекты 
        public int boxId; // ячейка, в которой находится
        public double maxHealth; // hit points
        public double currentHealth;
        public abstract string description(); // описание объекта
        public Image objectImg;
        public Image baseObjectImg;
        public int x;
        public int y;
        public int maxActions; // максимальное количество действий на одном ходу
        public int actionsLeft; // оставшееся количество действий

        public abstract void statusRefresh(ref Bitmap bmBg, ref Bitmap bmFull);
    }
}
