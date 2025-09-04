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
        GorillaTagger.OnPlayerSpawned(async () =>
        {
            await WatchSpawner.Instance.SpawnAll();
            Main.Log("Finished spawn proc");
        });
    }

    public static void Log(object message, LogLevel level = LogLevel.Info)
    {
        instance?.Logger.Log(level, message);
    }
}
