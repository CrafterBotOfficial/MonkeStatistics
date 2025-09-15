using UnityEngine;

namespace MonkeStatistics.UI.Buttons;

internal class WatchFaceButton : LineButton
{
    private void Update()
    {
        if (LocalWatchManager.Instance.Menu.gameObject.activeSelf &&
            !GetFacingUp())
        {
            LocalWatchManager.Instance.DeactivateMenu();
        }
    }

    public override void OnPress()
    {
        Main.Log("watch face press", BepInEx.Logging.LogLevel.Debug);
        if (GetFacingUp())
        {
            LocalWatchManager.Instance.ActivateMenu();
            // LocalWatchManager.Instance.Menu.position = transform.position;
        }
    }

    private bool GetFacingUp()
    {
        float Distance = Vector3.Distance(transform.forward, Vector3.up);
        return Distance <= 0.65;
    }
}
