using BepInEx.Logging;

namespace MonkeStatistics
{
    internal static class ExtensionMethods
    {
        internal static void Log(this object obj, LogLevel logLevel = LogLevel.Info)
        {
            if (Main.Instance is object)
                Main.Instance.manualLogSource.Log(logLevel, obj);
        }
    }
}
