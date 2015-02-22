using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GalaxyConquest.Tactics
{
    class WpnNone : Weapon
    {
        public WpnNone()
        {
            maxAttackPower = 0;
            minAttackPower = 0;
            attackRange = 0;
            energyСonsumption = 0;
            cage = 0;
            shotsleft = cage;
        }
        public override string description()
        {
            return "\nНет вооружения";
        }
        public override void drawAttack(int x, int y, int targetx, int targety, ref System.Drawing.Bitmap bmap, System.Media.SoundPlayer player, ref PictureBox pictureMap, ref System.Drawing.Bitmap bmBackground, ref System.Drawing.Bitmap bmFull)
        {
            
        }
    }
}
