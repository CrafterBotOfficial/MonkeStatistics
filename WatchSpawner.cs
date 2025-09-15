using System;
using System.Threading.Tasks;
using UnityEngine;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;
using UnityEngine.UI;

namespace MonkeStatistics;

internal class WatchSpawner
{
    public static WatchSpawner Instance { get; } = new();

    public async Task SpawnAll()
    {
        foreach (var rig in VRRigCache.Instance.GetAllRigs())
        {
            if (rig.isLocal || rig.isMyPlayer) continue;
            await Spawn(rig);
        }
        VRRig.LocalRig.AddComponent<UI.LocalWatchManager>();
    }

    public async Task<Result> SpawnLocal()
    {
        var watch = await Spawn(VRRig.LocalRig);
        CreateWatchFaceTrigger(watch);

        var menu = GameObject.Instantiate(await AssetLoader.Instance.GetAsset("MenuObj")).transform;
        menu.AddComponent<MenuFollower>();

        var panel = menu.GetChild(0);
        var returnButton = panel.Find("ReturnMain").AddComponent<LineButton>();
        returnButton.ButtonHandler = new ReturnButtonHandler();

        var lines = new UILine[UIManager.MAX_LINES];
        CreateLines(menu, lines);

        // scroll buttons
        var scrollDownButton = panel.Find("Down").AddComponent<LineButton>();
        var scrollUpButton = panel.Find("Up").AddComponent<LineButton>();

        scrollDownButton.gameObject.SetActive(false);
        scrollUpButton.gameObject.SetActive(false);

        return new Result(
            watch,
            menu,
            returnButton,
            lines,
            scrollUpButton,
            scrollDownButton
        );
    }

    public async Task<Transform> Spawn(VRRig rig)
    {
        var watch = GameObject.Instantiate(await AssetLoader.Instance.GetAsset("Watch")).transform;
        watch.parent = rig.leftHandTransform;
        watch.localPosition = new Vector3(0.0288f, 0.0267f, -0.004f);
        watch.localRotation = Quaternion.Euler(-26.97f, 94.478f, -93.21101f);

        return watch;
    }

    private void CreateLines(Transform menu, UILine[] lines)
    {
        var lineParent = menu.GetChild(0).Find("Lines");
        var line = lineParent.GetChild(0);

        for (int i = 0; i < UIManager.MAX_LINES; i++)
        {
            var lineObject = GameObject.Instantiate(line, lineParent).gameObject;
            lines[i] = new UILine(lineObject); // also creates the button
            lines[i].Button.gameObject.SetActive(false);
        }
        line.gameObject.SetActive(false); // template line
    }

    private void CreateWatchFaceTrigger(Transform watch)
    {
        watch.Find("Trigger").AddComponent<WatchFaceButton>();
    }

    public record struct Result(Transform watch, Transform menu, LineButton returnButton, UILine[] lines, LineButton up, LineButton down)
    {
        public Transform Watch = watch;
        public Transform Menu = menu;
        public LineButton ReturnButton = returnButton;
        public UILine[] Lines = lines;
        public LineButton ScrollUp = up;
        public LineButton ScrollDown = down;
    }
}

