using System;

namespace GalaxyConquest
{
    [Serializable]
    public class StarWarp
    {
        public string name;
        public int type;

        public StarSystem system1;
        public StarSystem system2;
    }
}
