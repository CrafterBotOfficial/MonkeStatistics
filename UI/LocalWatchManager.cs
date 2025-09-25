using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using MonkeStatistics.UI.Buttons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonkeStatistics.UI;

public class LocalWatchManager : MonoBehaviour
{
    public static LocalWatchManager Instance;

    public UIManager UIManager;

    public Transform Watch;
    public Transform Menu;
    public LineButton ReturnButton;
    public UILine[] Lines;

    private async Task Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        Main.Log("Making watch for local player", BepInEx.Logging.LogLevel.Message);
        var result = await WatchSpawner.Instance.SpawnLocal();
        Watch = result.Watch;
        Menu = result.Menu;
        ReturnButton = result.ReturnButton;
        Lines = result.Lines;

        UIManager = new UIManager(Menu, result.up, result.down);
    }

    public void EnumerateLines(Action<UILine> onLine)
    {
        foreach (var line in Lines)
            try
            {
                onLine(line);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
    }

    public void ActivateMenu()
    {
        Menu.gameObject.SetActive(true);
        UIManager.UpdatePage();
    }

    public void DeactivateMenu()
    {
        Menu.gameObject.SetActive(false);
    }
}
