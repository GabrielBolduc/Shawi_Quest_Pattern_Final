

public interface ICombatStrategy
{
    // attaquant effectue une action contre target
    void ExecuteTurn(Player attacker, Player target);
}