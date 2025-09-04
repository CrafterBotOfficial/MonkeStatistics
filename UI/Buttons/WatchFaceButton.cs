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
        if (GetFacingUp())
        {
            LocalWatchManager.Instance.ActivateMenu();
        }
    }

    private bool GetFacingUp()
    {
        float Distance = Vector3.Distance(transform.forward, Vector3.up);
        return Distance <= 0.65;
    }
}
