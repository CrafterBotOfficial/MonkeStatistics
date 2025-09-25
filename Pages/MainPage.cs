using System.Linq;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.Pages;

internal class MainPage : IPage
{
    public string GetName() => "Monke Statistics";

    public Content GetContent()
    {
        var pages = Register.Instance.GetPages();
        if (pages is null || pages.Length == 0)
        {
            Main.Log("No pages in registery, maybe it was robbed?", BepInEx.Logging.LogLevel.Error);
            return PageBuilder.GetErrorPage("No pages");
        }

        var scrollBuilder = new ScrollPageBuilder();
        pages.ForEach(page =>
            scrollBuilder.AddLine(page.GetName(), () => SwitchPage(page))
        );

        return scrollBuilder.GetContent();
    }

    private void SwitchPage(IPage page)
    {
        LocalWatchManager.Instance.UIManager.SwitchPage(page);
    }
}
