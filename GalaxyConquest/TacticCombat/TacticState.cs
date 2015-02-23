using System;
using System.Collections.Generic;
using GalaxyConquest.Tactics;
using System.Linq;
using System.Text;

namespace GalaxyConquest.Game
{
    public struct TacticSeed
    {
        public int select;
        public int activePlayer; // ход 1-ого или 2-ого игрока
        public Ship activeShip; // выделенное судно
        public List<Ship> allShips;
        public List<Meteor> meteors;
        public int blueShipsCount;
        public int redShipsCount;
        public int boxWidth;
        public int boxHeight;
        public int boxId;
        public Fleet left;
        public Fleet right;
        public List<SavedImage> savedImages;
    }

    class TacticState
    {
        public combatMap cMap = new combatMap(7, 5);
        public ObjectManager objectManager = new ObjectManager();

        public void New(ref TacticSeed seed, Fleet left, Fleet right)
        {
            seed.activePlayer = 1;
            seed.activeShip = null;
            seed.allShips = new List<Ship>();
            seed.left = left;
            Installation.InstallWpn(seed);
            Installation.InstallArmor(seed);
            seed.meteors = new List<Meteor>();
            seed.right = right;
            seed.select = -1;
            seed.savedImages = new List<SavedImage>();
            seed.boxWidth = cMap.boxes[0].xpoint3 - cMap.boxes[0].xpoint2;
            seed.boxHeight = cMap.boxes[0].ypoint6 - cMap.boxes[0].ypoint2;
            seed.allShips.Clear();
            seed.allShips.AddRange(seed.left.ships);
            seed.allShips.AddRange(seed.right.ships);
            objectManager.meteorCreate(cMap);
            // расставляем корабли по полю, синие - слева, красные - справа
            for (int count = 0; count < seed.allShips.Count; count++)
            {
                if (seed.allShips[count].currentHealth > 0)
                {
                    seed.allShips[count].refill();
                    seed.allShips[count].placeShip(ref cMap);
                }
            }
        }
        
    }
}
