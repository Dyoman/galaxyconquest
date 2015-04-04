using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GalaxyConquest.Game;

namespace GalaxyConquest.Tactics
{
    public class Installation
    {
        public static void InstallWpn(TacticSeed seed)
        {
            for (int i = 0; i < seed.left.ships.Count; i++)
            {
                if (seed.left.ships[i].classShip != Constants.COLONIZER)
                {
                    for (int j = 0; j < Player.technologies.Count; j++)
                    {
                        if (Player.technologies[j][1] == 2 && Player.technologies[j][2] == 0)
                        {
                            switch (Player.technologies[j][0])
                            {
                                case 0:
                                    seed.left.ships[i].InstallWpn(new WpnGauss());
                                    break;
                                case 1:
                                    seed.left.ships[i].InstallWpn(new WpnGauss());
                                    break;
                                case 2:
                                    seed.left.ships[i].InstallWpn(new wpnLightLaser());
                                    break;
                                case 3:
                                    seed.left.ships[i].InstallWpn(new WpnPlasma());
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public static void InstallArmor(TacticSeed seed)
        {
            for (int i = 0; i < seed.left.ships.Count; i++)
            {
                for (int j = 0; j < Player.technologies.Count; j++)
                {
                    if (Player.technologies[j][1] == 1 && Player.technologies[j][2] == 0)
                    {
                        switch (Player.technologies[j][0])
                        {
                            case 0:
                                seed.left.ships[i].InstallArmor(new ArmorNone());
                                break;
                            case 1:
                                seed.left.ships[i].InstallArmor(new ArmorTitan());
                                break;
                            case 2:
                                seed.left.ships[i].InstallArmor(new ArmorMolibden());
                                break;
                            case 3:
                                seed.left.ships[i].InstallArmor(new ArmorNanocom());
                                break;
                        }
                    }
                }
            }
        }
    }
}
