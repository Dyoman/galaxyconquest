using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest.Tactics
{
    class ArmorMolibden : Armor
    {
        public ArmorMolibden()
        {
            factor = 1.5;
        }
        public override string description()
        {
            return "\nМолибденовая броня\nHP*"+factor;
        }
    }
}
