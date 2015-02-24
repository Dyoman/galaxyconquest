using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest.Tactics
{
    class ArmorTitan : Armor
    {
        public ArmorTitan()
        {
            factor = 1.25;
        }
        public override string description()
        {
            return "\nТитановая броня\nHP*" + factor;
        }
    }
}
