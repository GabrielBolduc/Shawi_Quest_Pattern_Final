using System;

public class SmartStrategy : ICombatStrategy
{
    private IWeaponFactory _weaponFactory;

    // flag pour heal 1 fois
    private bool _hasHealed = false;

    public SmartStrategy(IWeaponFactory weaponFactory)
    {
        _weaponFactory = weaponFactory;
    }

    public void ExecuteTurn(Player attacker, Player target)
    {
        if (attacker.Health < 50 && !_hasHealed)
        {
            
            _hasHealed = true;

            int healAmount = 15;
            attacker.Heal(healAmount);

            Console.WriteLine($"\t[Stratégie] {attacker.Name} est en danger ! Il utilise sa DERNIÈRE potion (+{healAmount} PV).");
        }
        else
        {

            IWeapon weapon = _weaponFactory.CreateWeapon();

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