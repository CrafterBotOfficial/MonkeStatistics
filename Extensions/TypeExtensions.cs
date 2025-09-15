using System;
using System.Linq;
using MonkeStatistics.UI;

namespace MonkeStatistics.Extensions;

public static class TypeExtensions
{
    public static IPage GetPageInstance(this Type type)
    {
        if (type.IsAssignableFrom(typeof(IPage)))
        {
            var page = Register.Instance.GetPages().FirstOrDefault(x => x.GetType() == type);
            if (page is not null)
            {
                return page;
            }

            // if hidden page/not in registry
            if (type == LocalWatchManager.Instance?.UIManager?.CurrentPage?.GetType())
            {
                return LocalWatchManager.Instance.UIManager.CurrentPage;
            }

            // main/home page isnt in registry so we need to check for it manually
            if (type == LocalWatchManager.Instance?.UIManager?.MainPage.GetType())
            {
                return LocalWatchManager.Instance.UIManager.MainPage;
            }
        }
        Main.Log("Couldn't find page instance", BepInEx.Logging.LogLevel.Warning);
        return null;
    }
}
