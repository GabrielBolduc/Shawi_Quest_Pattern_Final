using System;
public class GameOverManager : IObserver
{
    // methode que le joueur va appeler
    public void Update(ISubject subject)
    {
        // cast le sujet pour récupérer l'objet Joueur
        Player player = (Player)subject;

        // verifie son etat
        if (player.Health <= 0)
        {
            Console.WriteLine($"\n!!! {player.Name} a été vaincu ! !!!");
            Console.WriteLine("--- GAME OVER ---");

            // singleton pour arreter la boucle de jeu
            GameSettings.Instance.IsSimulationRunning = false;
        }
    }
}