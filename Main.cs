using BepInEx;
using BepInEx.Logging;

namespace MonkeStatistics;

[BepInPlugin("Crafterbot.MonkeStatistics", "MonkeStatistics", "2.0.0")]
internal class Main : BaseUnityPlugin
{
    public static Main instance;

    private void Awake()
    {
        instance = this;
        Configuration.Initialize(Config);
        GorillaTagger.OnPlayerSpawned(() =>
        {
            _ = WatchSpawner.Instance.SpawnOther();
        });
    }

    public static void Log(object message, LogLevel level = LogLevel.Info)
    {
        instance?.Logger.Log(level, message);
    }
}
