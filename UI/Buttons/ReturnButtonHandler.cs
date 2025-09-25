using MonkeStatistics.Pages;

namespace MonkeStatistics.UI.Buttons;

internal class ReturnButtonHandler : IButtonHandler
{
    public void Press(LineButton button)
    {
        var instance = LocalWatchManager.Instance.UIManager;
        if (instance.ReturnPage == null)
        {
            Main.Log("No page to return back to", BepInEx.Logging.LogLevel.Warning);
            instance.SwitchPage(instance.MainPage);
        }
        button.SetMaterial(true);
        instance.SwitchPage(instance.ReturnPage);
    }
}
