// classes/RandomWeaponFactory.cs
using System;

public class RandomWeaponFactory : IWeaponFactory
{
    public IWeapon CreateWeapon()
    {
        // On récupère le générateur aléatoire du Singleton
        Random rng = GameSettings.Instance.Rng;

        IWeapon weapon;

        // arme de base
        if (rng.Next(0, 2) == 0)
        {
            weapon = new BasicSword();
        }
        else
        {
            weapon = new HeavyAxe();
        }


        // Appliquer decorator
        // 50% change decorator fire
        if (rng.Next(0, 2) == 0)
        {
            weapon = new FireEnchantment(weapon);
        }

        // 33% change decorator sharpness
        if (rng.Next(0, 3) == 0)
        {
            weapon = new SharpnessEnchantment(weapon);
        }

        return weapon;
    }
}