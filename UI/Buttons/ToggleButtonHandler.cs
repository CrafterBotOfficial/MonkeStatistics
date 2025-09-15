using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace MonkeStatistics.UI.Buttons;

public class ToggleButtonHandler : IButtonHandler
{
    public event Action OnToggled_On;
    public event Action OnToggled_Off;
    public event Action<LineButton, bool> OnToggled;
    public bool Toggled { get; private set; }

    public void Press(LineButton button)
    {
        Toggled = !Toggled;
        button.SetMaterial(Toggled);

        try { OnToggled(button, true); }
        catch (Exception ex) { Debug.LogError(ex); }

        if (Toggled)
            InvokeSafely(OnToggled_On);
        else
            InvokeSafely(OnToggled_Off);
    }

    private void InvokeSafely(Action action)
    {
        try { action?.Invoke(); }
        catch (Exception ex) { Debug.LogError(ex); }
    }
}
