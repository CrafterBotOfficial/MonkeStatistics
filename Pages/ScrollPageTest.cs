#if DEBUG

using System.Linq;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.Pages;

[AutoRegister]
internal class ScrollPageTest : IPage
{
    public string GetName() => "Scroll Page Test";

    public Content GetContent()
    {
        var scrollBuilder = new ScrollPageBuilder();
        const int LINE_COUNT = 31;
        for (int i = 0; i < LINE_COUNT; i++)
        {
            scrollBuilder.AddLine($"[{i}] Hello", () => Main.Log(i));
        }

        return scrollBuilder.GetContent();
    }
}

#endif
