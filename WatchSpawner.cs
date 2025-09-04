using System;
using System.Threading.Tasks;
using UnityEngine;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;
using UnityEngine.UI;

namespace MonkeStatistics;

internal class WatchSpawner
{
    private static Lazy<WatchSpawner> instance = new Lazy<WatchSpawner>(() => new WatchSpawner());
    public static WatchSpawner Instance => instance.Value;

    public async Task SpawnAll()
    {
        foreach (var rig in VRRigCache.Instance.GetAllRigs())
        {
            await WatchSpawner.Instance.Spawn(rig);
        }
        VRRig.LocalRig.AddComponent<UI.LocalWatchManager>();
    }

    public async Task<Result> SpawnLocal()
    {
        var watch = await Spawn(VRRig.LocalRig);
        watch.Find("Trigger").AddComponent<UI.Buttons.WatchFaceButton>();

        var menu = GameObject.Instantiate(await AssetLoader.Instance.GetAsset("MenuObj")).transform;

        var panel = menu.GetChild(0);
        var returnButton = panel.Find("ReturnMain").AddComponent<LineButton>();
        returnButton.ButtonHandler = new ReturnButtonHandler();

        var lines = new UILine[10];
        CreateLines(menu, lines);

        return new Result(watch, menu, returnButton, lines);
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

        for (int i = 0; i < 10; i++)
        {
            var lineObject = GameObject.Instantiate(line, lineParent).gameObject;
            lines[i] = new UILine(lineObject); // also creates the button
            lines[i].MyObject.SetActive(false);
        }
    }

    public struct Result(Transform watch, Transform menu, LineButton returnButton, UILine[] lines)
    {
        public Transform Watch = watch;
        public Transform Menu = menu;
        public LineButton ReturnButton = returnButton;
        public UILine[] Lines = lines;
    }
}

