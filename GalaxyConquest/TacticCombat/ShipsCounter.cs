using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaxyConquest.Game;

namespace GalaxyConquest.Tactics
{
    public class ShipsCounter
    {
        public static void ShipsCount(ref TacticSeed seed)
        {
            seed.blueShipsCount = 0;
            seed.redShipsCount = 0;
            for (int count = 0; count < seed.allShips.Count; count++)
            {
                if (seed.allShips[count] == null) continue;
                if (seed.allShips[count].currentHealth <= 0)
                {
                    seed.allShips.Remove(seed.allShips[count]);
                    count--;
                }
                else
                {
                    if (seed.allShips[count].player == 1)
                        seed.blueShipsCount++;
                    else if (seed.allShips[count].player != 1)
                        seed.redShipsCount++;
                }
            }
        }
    }
}
