﻿using System.Drawing;
using GalaxyConquest.Game;

namespace GalaxyConquest.Tactics
{
    public class ShipScout : Ship
    {
        public string staticDescription;
        public override string description()
        {
            return "" + staticDescription + "\nHP - " + currentHealth + "/" + maxHealth + "\nactions - "
                            + actionsLeft + "/" + maxActions + equippedWeapon.description() + "\nmax damage - " + equippedWeapon.maxAttackPower + "\nmin damage - " + equippedWeapon.minAttackPower + "\nRange - " + equippedWeapon.attackRange;
        }

        public ShipScout(int p, Weapon weapon)
        {
            objectType = Constants.SHIP;
            classShip = Constants.SCOUT;
            objectImg = Image.FromFile(@"Sprites/ships/player/Bomber2.png");
            baseObjectImg = objectImg;
            equippedWeapon = weapon;
            sumWeapon = 1;
            player = p;
            maxHealth = 50;
            currentHealth = maxHealth;
            maxActions = 7;
            actionsLeft = maxActions;
            staticDescription = "Развед. корабль\nкласса Scout";
            weaponR = 12;
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
