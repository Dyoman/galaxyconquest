using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalaxyConquest.Tactics
{
    public abstract class Weapon
    {
        abstract public string description();
        abstract public void drawAttack(int x, int y, int targetx, int targety, ref System.Drawing.Bitmap bmap, System.Media.SoundPlayer player, ref PictureBox pictureMap, ref System.Drawing.Bitmap bmBackground, ref System.Drawing.Bitmap bmFull);
        public int attackRange;
        public int attackPower;
        public int energyСonsumption;
        public int cage; // сколько можно делать выстрелов за ход
        public int shotsleft; // сколько выстрелов осталось
    }
    
}
