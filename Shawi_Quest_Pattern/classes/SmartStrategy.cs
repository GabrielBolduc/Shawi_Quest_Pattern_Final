using System;

public class SmartStrategy : ICombatStrategy
{
    private IWeaponFactory _weaponFactory;

    // NOUVEAU : Une variable pour retenir si on a déjà utilisé le soin
    private bool _hasHealed = false;

    public SmartStrategy(IWeaponFactory weaponFactory)
    {
        _weaponFactory = weaponFactory;
    }

    public void ExecuteTurn(Player attacker, Player target)
    {
        // LOGIQUE : On se soigne SEULEMENT si :
        // 1. La vie est sous 50 PV
        // 2. ET qu'on ne s'est pas encore soigné (_hasHealed est faux)
        if (attacker.Health < 50 && !_hasHealed)
        {
            // --- ACTION DE SOIN (1 FOIS SEULEMENT) ---

            // On marque le soin comme utilisé tout de suite
            _hasHealed = true;

            int healAmount = 15; // On peut augmenter un peu le soin vu qu'il est unique
            attacker.Heal(healAmount);

            Console.WriteLine($"\t[Stratégie] {attacker.Name} est en danger ! Il utilise sa DERNIÈRE potion (+{healAmount} PV).");
        }
        else
        {
            // --- ACTION D'ATTAQUE (LE RESTE DU TEMPS) ---

            IWeapon weapon = _weaponFactory.CreateWeapon();

            // Petit bonus visuel : message différent si on a déjà utilisé la potion
            if (_hasHealed && attacker.Health < 50)
            {
                Console.WriteLine($"\t[Stratégie] {attacker.Name} est faible mais n'a plus de potion... Il attaque en désespoir de cause !");
            }
            else
            {
                Console.WriteLine($"\t[Stratégie] {attacker.Name} attaque avec : {weapon.GetDescription()}");
            }

            target.TakeDamage(weapon.GetBaseDamage());
        }
    }
}