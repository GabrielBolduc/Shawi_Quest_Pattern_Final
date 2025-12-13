public class BasicSword : IWeapon
{
    public int GetBaseDamage()
    {
        // return un degats de base
        return 8;
    }

    public string GetDescription()
    {
        return "Épée de base";
    }
}