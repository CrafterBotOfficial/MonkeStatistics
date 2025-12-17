using System;
using System.Threading.Tasks;
using UnityEngine;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace MonkeStatistics;

internal class WatchSpawner
{
    public static WatchSpawner Instance { get; } = new();

    public Dictionary<VRRig, GameObject> Watches;
    private GameObject watchPrefab;

    public async Task SpawnAll()
    {
        if (Watches is not null)
            return;
        Watches = new Dictionary<VRRig, GameObject>(VRRigCache.Instance.rigAmount);

        var rigs = VRRigCache.Instance.GetAllRigs();
        foreach (var rig in rigs)
        {
            if (rig.isLocal || rig.isMyPlayer) continue;

            var watch = await Spawn(rig);
            watch.gameObject.SetActive(false);
            Watches.Add(rig, watch.gameObject);
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
        if (watchPrefab is null)
        {
            watchPrefab = await AssetLoader.Instance.GetAsset("Watch");
        }

        var watch = GameObject.Instantiate(watchPrefab).transform;
        watch.parent = rig.leftHandTransform; // Configuration.WatchHand.Value == Configuration.Hand.Left ? rig.leftHandTransform : rig.rightHandTransform;
        watch.localPosition = new Vector3(0.0288f, 0.0267f, -0.004f);
        watch.localRotation = Quaternion.Euler(-26.97f, 94.478f, -93.21101f); // TODO Make chin give me offsets for when the watch is on the right hand
        watch.localScale = new Vector3(2.1f, 2.6f, 2.1f);

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

