using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GalaxyConquest.SpaceObjects
{
    public class Star : SpaceObject
    {
        public int size;
        public SolidBrush color = new SolidBrush(Color.White);

        public override void Move(double time)
        {

        }
    }
}
