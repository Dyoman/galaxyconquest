using System;

namespace GalaxyConquest
{
    [Serializable]
    public class StarWarp : SpaceObject
    {
        public int type;

        public StarSystem system1;
        public StarSystem system2;

        public override void Move(double time)
        {
            return;
        }
    }
}
