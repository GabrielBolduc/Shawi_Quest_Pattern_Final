using System;

public sealed class GameSettings
{
    // implémentation du singleton
    // 'Lazy<T>' gère le "thread-safety" pour nous.
    private static readonly Lazy<GameSettings> _lazyInstance = new Lazy<GameSettings>(() => new GameSettings());

    public static GameSettings Instance => _lazyInstance.Value;


    private GameSettings()
    {

        Rng = new Random();
    }

    public Random Rng { get; private set; }

    public bool IsSimulationRunning { get; set; }


    public int TempsAttenteEntreTours => 700; // 0.7 secondes

    public float DamageModifier => 1.1f; // +10% de dégâts sur tout

    public int BaseHealth => 200;
    public int MinSimulationDuration => 20000;
    public int MaxSimulationDuration => 40001;
}