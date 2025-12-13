public class HeavyAxe : IWeapon
{
    public int GetBaseDamage()
    {
        // return un degats de base
        return 10;
    }

    public string GetDescription()
    {
        return "Hache lourde";
    }
}