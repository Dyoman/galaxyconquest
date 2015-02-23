using System.Drawing;
using GalaxyConquest.Game;

namespace GalaxyConquest.Tactics
{
    public class ShipScout : Ship
    {
        public string staticDescription;
        public override string description()
        {
            return "" + staticDescription + "\n\nБроня:" + equippedArmor.description()
                            + "\n\nВооружение:" + equippedWeapon.description();
        }

        public ShipScout(int p, Weapon weapon, Armor armor)
        {
            objectType = Constants.SHIP;
            classShip = Constants.SCOUT;
            objectImg = Image.FromFile(@"Sprites/ships/player/Bomber2.png");
            baseObjectImg = objectImg;
            equippedWeapon = weapon;
            equippedArmor = armor;
            sumWeapon = 1;
            player = p;
            baseHealth = 50;
            maxHealth = baseHealth * equippedArmor.factor;
            currentHealth = maxHealth;
            maxActions = 7;
            actionsLeft = maxActions;
            staticDescription = "Развед. корабль\nкласса Scout" + "\nHP - " + currentHealth + "/" + baseHealth 
                + "\nactions - " + actionsLeft + "/" + maxActions;
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
