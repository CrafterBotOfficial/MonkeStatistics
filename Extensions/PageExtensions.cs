using System;
using System.Linq;
using MonkeStatistics.UI;

namespace MonkeStatistics.Extensions;

public static class PageExtensions
{
    public static bool IsActive(this IPage self)
    {
        return LocalWatchManager.Instance?.UIManager?.CurrentPage == self;
    }

    public static void Redraw(this IPage self)
    {
        if (!IsActive(self))
        {
            Main.Log("Can't redraw page if page isn't active " + self.GetName(), BepInEx.Logging.LogLevel.Warning);
            return;
        }
        LocalWatchManager.Instance?.UIManager?.UpdatePage();
    }
}
