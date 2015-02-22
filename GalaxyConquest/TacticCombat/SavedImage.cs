using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GalaxyConquest.Tactics
{
    public class SavedImage
    {
        public Image img;
        public int x;
        public int y;
        public SpaceObject spaceObject;
        public SavedImage(Image image, int xcoord, int ycoord, SpaceObject obj)
        {
            img = image;
            x = xcoord;
            y = ycoord;
            spaceObject = obj;
        }
    }
}
