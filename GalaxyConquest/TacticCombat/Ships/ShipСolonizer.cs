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
            return "" + staticDescription + "\n\nБроня:" + equippedArmor.description()
                            + "\n\nВооружение:" + equippedWeapon.description();
        }

        public ShipСolonizer(int p, Weapon weapon, Armor armor)
        {
            objectType = Constants.SHIP;
            classShip = Constants.COLONIZER;
            objectImg = Image.FromFile(@"Sprites/ships/player/Assaultboat.png");
            baseObjectImg = objectImg;
            equippedWeapon = weapon;
            equippedArmor = armor;
            sumWeapon = 0;
            player = p;
            baseHealth = 200;
            maxHealth = baseHealth * equippedArmor.factor;
            currentHealth = maxHealth;
            maxActions = 2;
            actionsLeft = maxActions;
            staticDescription = "Корабль-колонизатор" + "\nHP - " + currentHealth + "/" + baseHealth
                + "\nactions - " + actionsLeft + "/" + maxActions;
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
