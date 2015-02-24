using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest.Tactics
{
    class ArmorNanocom : Armor
    {
        public ArmorNanocom()
        {
            factor = 1.75;
        }
        public override string description()
        {
            return "\nНанокомпозитная броня\nHP*" + factor;
        }
    }
}
