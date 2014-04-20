using System;
using System.Collections.Generic;

namespace GalaxyConquest
{
    [Serializable]
    public class Buildings
    {
        public static List<string> buildings = new List<string>();

        public Buildings()
        {
            buildings.Add("FORPOST");
            buildings.Add("LAB");
        }

    }
}
