using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalaxyConquest
{
    abstract class MovementsController
    {
        public static void Process(SpaceObject[] obj, double time)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].Move(time);
            }
        }

        public static void Process(SpaceObject obj, double time)
        {
            obj.Move(time);
        }
    }
}
