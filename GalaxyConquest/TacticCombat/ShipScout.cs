using System.Drawing;

namespace GalaxyConquest.Tactics
{
    public class ShipScout : Ship
    {
        public string staticDescription;
        public override string description()
        {
            return "" + staticDescription + "\nhp - " + currentHealth + "/" + maxHealth + "\nactions - "
                            + actionsLeft + "/" + maxActions + equippedWeapon.description() + "\nAP - " + equippedWeapon.attackPower + "\nRange - " + equippedWeapon.attackRange;
        }

        public ShipScout(int p, Weapon weapon)
        {
            objectType = Constants.SHIP;
            equippedWeapon = weapon;
            objectImg = Image.FromFile(@"Sprites/ships/player/Bomber2.png");
            baseObjectImg = objectImg;
            player = p;
            maxHealth = 50;
            currentHealth = maxHealth;
            maxActions = 7;
            actionsLeft = maxActions;
            staticDescription = "Лёгкий корабль\nкласса Scout";
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
