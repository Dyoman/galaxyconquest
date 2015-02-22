using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GalaxyConquest.Tactics
{
    class ShipСolonizer : Ship
    {
        public string staticDescription;
        public override string description()
        {
            return "" + staticDescription + "\nHP - " + currentHealth + "/" + maxHealth + "\nactions - "
                            + actionsLeft + "/" + maxActions + equippedWeapon.description() + "\nmax damage - " + equippedWeapon.maxAttackPower + "\nmin damage - " + equippedWeapon.minAttackPower + "\nRange - " + equippedWeapon.attackRange;
        }

        public ShipСolonizer(int p)
        {
            objectType = Constants.SHIP;
            classShip = Constants.COLONIZER;
            objectImg = Image.FromFile(@"Sprites/ships/player/Assaultboat.png");
            baseObjectImg = objectImg;
            equippedWeapon = new WpnNone();
            sumWeapon = 0;
            player = p;
            maxHealth = 200;
            currentHealth = maxHealth;
            maxActions = 4;
            actionsLeft = maxActions;
            staticDescription = "Корабль-колонизатор";
            weaponR = 22;
            if (player != 1)
            {
                weaponR *= -1;
                objectImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            weaponPointX = weaponR;
            weaponPointY = 0;
        }
    }
}
