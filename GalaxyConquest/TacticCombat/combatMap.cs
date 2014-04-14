using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GalaxyConquest.Tactics
{
    public class combatMap
    {
        public int width;
        public int height;
        public int scale = 4;
        public List<Box> boxes = new List<Box>();
        public int deltax; 
        public int deltay;

        public combatMap(int w, int h) 
        {
            width = w;
            height = h;

            deltax = 20 * scale;
            deltay = 20 * scale;

            iniBasicPoints();
        }

        public Box getBoxByCoords(int x, int y)
        {
            Box targetBox = null;
            for (int i = 0; i < boxes.Count; i++ )
            {
                if (boxes[i].x == x && boxes[i].y == y)
                {
                    targetBox = boxes[i];
                    break;
                }
            }
            return targetBox;
        }

        public void clearBox(int box, ref Bitmap bmBg, ref Bitmap bmFull)
        {
            boxes[box].spaceObject.player = -1;
            boxes[box].spaceObject.boxId = -1;
            boxes[box].spaceObject = null;

            Graphics g = Graphics.FromImage(bmFull);
            Image bg = bmBg.Clone(new Rectangle(boxes[box].xpoint1 - 3, boxes[box].ypoint2 - 3,
                boxes[box].xpoint4 - boxes[box].xpoint1 + 6,
                boxes[box].ypoint5 - boxes[box].ypoint3 + 6), bmBg.PixelFormat);
            g.DrawImage(bg, boxes[box].xpoint1 - 3, boxes[box].ypoint2 - 3);

        }
        public void iniBasicPoints()
        {
            int xcoord = -1;
            int ycoord = 0;
            int count = 0;
            for(int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (j % height == 0) xcoord += 1;

                    if(i % 2 == 1)
                    {
                        // нечетная
                        if (j % height == 0) ycoord = 1;
                        Box box = new Box(scale);
                        box.xmove(deltax * i - 10 * scale);
                        box.ymove(deltay * j + 10 * scale);
                        
                        box.x = xcoord;
                        box.y = ycoord;
                        box.id = count++;
                        box.centerDetermine();

                        boxes.Add(box);

                    }
                    else
                    {
                        // четная
                        if (j % height == 0) ycoord = 0;
                        Box box = new Box(scale);
                        box.xmove(deltax * i - 10 * scale);
                        box.ymove(deltay * j + 0);

                        box.x = xcoord;
                        box.y = ycoord;
                        box.id = count++;
                        box.centerDetermine();

                        boxes.Add(box);
                    }
                    ycoord += 2;
                }
            }
        }
    }

 
}
