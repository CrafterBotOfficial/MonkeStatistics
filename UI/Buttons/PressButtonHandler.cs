using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace MonkeStatistics.UI.Buttons;

public class PressButtonHandler : IButtonHandler
{
    public Action OnPressEvent;

    public PressButtonHandler()
    {

    }

    public PressButtonHandler(Action onPressEvent)
    {
        OnPressEvent = onPressEvent;
    }

    public async void Press(LineButton button)
    {
        button.SetMaterial(true);

        if (OnPressEvent != null)
            try { OnPressEvent(); }
            catch (Exception ex) { Debug.LogError(ex); }

        await Task.Delay(250); // same as debounce period, so no need for locks
        button.SetMaterial(false);
    }
}
