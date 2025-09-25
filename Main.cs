using BepInEx;
using BepInEx.Logging;

namespace MonkeStatistics;

[BepInPlugin(GUID, "MonkeStatistics", "2.0.0")]
internal class Main : BaseUnityPlugin
{
    public const string GUID = "Crafterbot.MonkeStatistics";
    public static Main instance;

    private void Awake()
    {
        instance = this;
        Configuration.Initialize(Config);
        VRRigCache.OnPostInitialize += () =>
        {
            WatchSpawner.Instance.SpawnAll().ContinueWith(t =>
            {
                Log(t.Exception, LogLevel.Fatal);
            }, System.Threading.Tasks.TaskContinuationOptions.OnlyOnFaulted);
        };
    }

    public static void Log(object message, LogLevel level = LogLevel.Info)
    {
        instance?.Logger.Log(level, message);
    }
}
