using System.Drawing;

namespace GalaxyConquest.Tactics
{
    public class ShipAssaulter : Ship
    {
        public string staticDescription;
        public override string description()
        {
            return "" + staticDescription + "\n\nБроня:" + equippedArmor.description()
                            + "\n\nВооружение:" + equippedWeapon.description();
        }

        public ShipAssaulter(int p, Weapon weapon, Armor armor)
        {
            objectType = Constants.SHIP;
            classShip = Constants.ASSAULTER;
            objectImg = Image.FromFile(@"Sprites/ships/player/Bomber.png");
            baseObjectImg = objectImg;
            equippedWeapon = weapon;
            equippedArmor = armor;
            sumWeapon = 3;
            player = p;
            baseHealth = 100;
            maxHealth = baseHealth * equippedArmor.factor;
            currentHealth = maxHealth;
            maxActions = 4;
            actionsLeft = maxActions;
            staticDescription = "Средний боевой корабль\nкласса Assaulter" + "\nHP - " + currentHealth + "/" + baseHealth 
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
