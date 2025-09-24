using BepInEx.Configuration;

namespace MonkeStatistics;

internal static class Configuration
{
    public static ConfigEntry<Hand> WatchHand;

    public static void Initialize(ConfigFile config)
    {
        WatchHand = config.Bind("Watch", "Hand", Hand.Left, "The hand the watch will be on.");
    }

    public enum Hand
    {
        Left,
        Right,
    }
}
