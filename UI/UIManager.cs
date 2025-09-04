using MonkeStatistics.Pages;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeStatistics.UI;

public class UIManager
{
    public IPage CurrentPage { get; private set; }

    private MainPage mainPage;
    private Text titleField;
    private Text authorField;

    public UIManager(Transform menu)
    {
        var panel = menu.GetChild(0);
        titleField = panel.Find("Title").GetComponent<Text>();
        authorField = panel.Find("Author").GetComponent<Text>();

        mainPage = new MainPage();
        SwitchPage(mainPage);
    }

    public void SwitchPage(IPage newPage)
    {
        if (CurrentPage == newPage) return;
        Main.Log("Setting page to " + newPage.GetName());

        CurrentPage = newPage;
        SetLines();
    }

    public void UpdatePage()
    {
        if (CurrentPage != null)
        {
            SetLines();
        }
    }

    private void SetLines()
    {
        var content = CurrentPage.GetContent();
        Main.Log($"{content.Lines.Length}");

        titleField.text = CurrentPage.GetName();
        authorField.text = content.Author;

        for (int i = 0; i < 10; i++)
        {
            var info = content.Lines[i];
            var lineObject = LocalWatchManager.Instance.Lines[i];

            if (info.Text.IsNullOrEmpty())
            {
                lineObject.Text.text = string.Empty;
                lineObject.Button.gameObject.SetActive(false);
                continue;
            }

            Main.Log(lineObject.Text, BepInEx.Logging.LogLevel.Message);
            lineObject.Text.text = info.Text;

            if (info.ButtonHandler == null)
                lineObject.Button.gameObject.SetActive(false);
            else
            {
                lineObject.Button.ButtonHandler = info.ButtonHandler;
                lineObject.Button.gameObject.SetActive(true);
            }
        }
    }
}
