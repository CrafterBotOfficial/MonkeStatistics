using System;
using MonkeStatistics.Extensions;
using MonkeStatistics.Pages;
using MonkeStatistics.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeStatistics.UI;

public class UIManager
{
    public IPage CurrentPage { get; private set; }
    public const int MAX_LINES = 10;

    public IPage ReturnPage;

    public MainPage MainPage;
    private Text titleField;
    private Text authorField;
    private LineButton scrollDownButton;
    private LineButton scrollUpButton;

    public bool ShowScrollButtons;
    public int SceneIndex; // for scroll page

    public UIManager(Transform menu, LineButton scrollUp, LineButton scrollDown)
    {
        var panel = menu.GetChild(0);
        titleField = panel.Find("Title").GetComponent<Text>();
        authorField = panel.Find("Author").GetComponent<Text>();

        scrollUpButton = scrollUp;
        scrollDownButton = scrollDown;

        scrollUpButton.ButtonHandler = new PressButtonHandler(() => OnScrollButton(-1));
        scrollDownButton.ButtonHandler = new PressButtonHandler(() => OnScrollButton(1));

        MainPage = new MainPage();
        ReturnPage = MainPage;
        SwitchPage(MainPage);
    }

    public void OnScrollButton(int change)
    {
        int newScene = SceneIndex + change;
        if (newScene < 0 || newScene >= MAX_LINES) return;
        SceneIndex = newScene;
        UpdatePage();
    }

    public void SwitchPage(Type newPageType)
    {
        if (newPageType.GetPageInstance() is IPage page)
        {
            SwitchPage(page);
            return;
        }
        Main.Log("Failed to find pages instance from register", BepInEx.Logging.LogLevel.Error);
    }

    public void SwitchPage(IPage newPage)
    {
        if (CurrentPage == newPage) return;
        Main.Log("Setting page to " + newPage.GetName());

        SceneIndex = 0;
        // ReturnPage = CurrentPage;
        CurrentPage = newPage;
        SetLines();
    }

    public void UpdatePage()
    {
        if (CurrentPage is not null)
        {
            SetLines();
        }
    }

    private void SetLines()
    {
        var content = CurrentPage.GetContent();
        if (content.Lines?.Length != 10)
        {
            Main.Log("Bad page, line array must have 10 elements", BepInEx.Logging.LogLevel.Error);
            return;
        }

        ShowScrollButtons = content.ShowScrollButtons;

        titleField.text = CurrentPage.GetName();
        authorField.text = content.Author;

        for (int i = 0; i < content.Lines.Length; i++)
        {
            var info = content.Lines[i];
            var lineObject = LocalWatchManager.Instance.Lines[i];

            lineObject.Text.text = info.Text;

            if (info.ButtonHandler is null)
                lineObject.Button.gameObject.SetActive(false);
            else
            {
                lineObject.Button.ButtonHandler = info.ButtonHandler;
                lineObject.Button.gameObject.SetActive(true);
            }
        }

        scrollUpButton.gameObject.SetActive(ShowScrollButtons);
        scrollDownButton.gameObject.SetActive(ShowScrollButtons);
        LocalWatchManager.Instance.ReturnButton.gameObject.SetActive(CurrentPage != MainPage);
    }
}
