using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest.Tactics
{
    class ArmorNone : Armor
    {
        public ArmorNone()
        {
            factor = 1.0;
        }
        public override string description()
        {
            return "\nНет брони";
        }
    }
}
