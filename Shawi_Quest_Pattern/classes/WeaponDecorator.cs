// implemente IWeapon et wrap une autre IWeapon
public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon _wrappedWeapon;

    public WeaponDecorator(IWeapon weapon)
    {
        _wrappedWeapon = weapon;
    }

    public virtual int GetBaseDamage()
    {
        return _wrappedWeapon.GetBaseDamage();
    }

    public virtual string GetDescription()
    {
        return _wrappedWeapon.GetDescription();
    }
}