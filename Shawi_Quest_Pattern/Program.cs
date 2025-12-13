using System;
using System.Threading;

// Patterns utilisés : Observer + Decorator + Factory + Strategy + IComparable

public class Program
{
    public static void Main(string[] args)
    {
        // boucle rejouer
        while (true)
        {
            RunSimulation();

            Console.WriteLine("\nSimulation terminée. Espace pour rejouer. Echap pour quitter.");

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Spacebar && key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }

    public static void RunSimulation()
    {
        // accede au Singleton pour définir l'etat de la simulation
        GameSettings.Instance.IsSimulationRunning = true;

        // recupere le Rng
        Random rng = GameSettings.Instance.Rng;

        // defini la durer aleatoire via le Singleton
        int durationMs = rng.Next(
            GameSettings.Instance.MinSimulationDuration,
            GameSettings.Instance.MaxSimulationDuration
        );

        var simTimer = new System.Timers.Timer(durationMs);

        simTimer.Elapsed += (sender, e) =>
        {
            Console.WriteLine("\n--- TEMPS ECOULE (ARRET FORCÉ) ---");
            // Singleton pour arreter le combat
            GameSettings.Instance.IsSimulationRunning = false;
        };
        simTimer.AutoReset = false;
        simTimer.Start();

        Console.Clear();
        Console.WriteLine($"--- NOUVELLE SIMULATION ---");


        Player player1 = new Player("Yvan, le Barbare");
        Player player2 = new Player("Cedrick, le chevalier");

        // creer l'Observateur 
        GameOverManager gameOverManager = new GameOverManager();

        player1.Attach(gameOverManager);
        player2.Attach(gameOverManager);

        // 1. On crée la Factory (nécessaire pour la stratégie d'attaque)
        IWeaponFactory weaponFactory = new RandomWeaponFactory();

        // 2. On assigne les stratégies aux joueurs (Strategy Pattern)
        // Les deux utilisent la stratégie "Intelligente"
        player1.CombatStrategy = new SmartStrategy(weaponFactory);
        player2.CombatStrategy = new SmartStrategy(weaponFactory);

        bool isPlayer1Turn = true;

        // Boucle principale du jeu
        while (GameSettings.Instance.IsSimulationRunning)
        {
            if (isPlayer1Turn)
            {
                Console.WriteLine($"\nTour de {player1.Name} :");
                player1.PerformTurn(player2);
            }
            else
            {
                Console.WriteLine($"\nTour de {player2.Name} :");
                player2.PerformTurn(player1);
            }

            // changer de tour
            isPlayer1Turn = !isPlayer1Turn;

            Thread.Sleep(GameSettings.Instance.TempsAttenteEntreTours);
        }

        simTimer.Stop();

        // --- UTILISATION DE ICOMPARABLE ---
        // Une fois la boucle terminée, on utilise CompareTo pour voir qui est en meilleur état

        Console.WriteLine("\n-------------------------------------------");
        Console.WriteLine("--- ANALYSE DE FIN DE PARTIE (IComparable) ---");
        Console.WriteLine("-------------------------------------------");

        // Utilisation de la méthode CompareTo implémentée dans Player.cs
        int resultatComparaison = player1.CompareTo(player2);

        Console.WriteLine($"PV Finaux -> {player1.Name}: {player1.Health} | {player2.Name}: {player2.Health}");

        if (resultatComparaison > 0)
        {
            Console.WriteLine($"=> RÉSULTAT : {player1.Name} est en meilleure forme (Il a plus de PV).");
        }
        else if (resultatComparaison < 0)
        {
            Console.WriteLine($"=> RÉSULTAT : {player2.Name} est en meilleure forme (Il a plus de PV).");
        }
        else
        {
            Console.WriteLine($"=> RÉSULTAT : Égalité parfaite des points de vie !");
        }
    }
}