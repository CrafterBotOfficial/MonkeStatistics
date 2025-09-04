using System.Linq;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.Pages;

internal class MainPage : IPage
{
    public string GetName() => "Monke Statistics";

    public Content GetContent()
    {
        var scrollBuilder = new ScrollPageBuilder();

        var pages = Register.Instance.GetPages();
        if (pages == null || pages.Length == 0)
        {
            Main.Log("No pages in registery, maybe it was robbed?", BepInEx.Logging.LogLevel.Fatal);
            goto null_end;
        }

        foreach (var page in pages)
        {
            scrollBuilder.AddLine(page.GetName(), new OpenPageButton(page));
        }

null_end:
        return scrollBuilder.GetContent();
    }

    private class OpenPageButton(IPage page) : IButtonHandler
    {
        public void Press(LineButton myButton)
        {
            LocalWatchManager.Instance.UIManager.SwitchPage(page);
        }
    }
}
