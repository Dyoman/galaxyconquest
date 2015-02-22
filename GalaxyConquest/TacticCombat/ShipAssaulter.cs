using System.Drawing;

namespace GalaxyConquest.Tactics
{
    public class ShipAssaulter : Ship
    {
        public string staticDescription;
        public override string description()
        {
            return "" + staticDescription + "\nhp - " + currentHealth + "/" + maxHealth + "\nactions - "
                            + actionsLeft + "/" + maxActions + equippedWeapon.description() + "\nAP - " + equippedWeapon.attackPower + "\nRange - " + equippedWeapon.attackRange;
        }

        public ShipAssaulter(int p, Weapon weapon)
        {
            objectType = Constants.SHIP;
            //Sprites\ships\player
            objectImg = Image.FromFile(@"Sprites/ships/player/Assaultboat.png");
            baseObjectImg = objectImg;
            equippedWeapon = weapon;
            player = p;
            maxHealth = 100;
            currentHealth = maxHealth;
            maxActions = 4;
            actionsLeft = maxActions;
            staticDescription = "Средний боевой корабль\nкласса Assaulter";
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
