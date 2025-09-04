using System;
using UnityEngine;

namespace MonkeStatistics.UI.Buttons;

public class LineButton : MonoBehaviour
{
    private Collider collider;

    private const float DEBOUNCE_PERIOD = .25f;
    private float debounceTime;

    private Renderer renderer;
    private Material pressMaterial;
    private Material unpressMaterial;

    public IButtonHandler ButtonHandler;

    private void Start()
    {
        gameObject.layer = 18;
        if (gameObject.GetComponent<Collider>() is Collider box)
            collider = box;
        else
        {
            Main.Log("No collider in button", BepInEx.Logging.LogLevel.Debug);
            collider = gameObject.AddComponent<BoxCollider>();
        }

        if (GetComponent<Renderer>() is Renderer buttonRenderer)
        {
            var wardrobeFunctionButton = FindObjectOfType<GorillaPressableButton>();
            if (wardrobeFunctionButton == null)
            {
                Main.Log("Not model button found to copy from.", BepInEx.Logging.LogLevel.Fatal);
                Destroy(this);
            }

            renderer = buttonRenderer;
            pressMaterial = wardrobeFunctionButton.pressedMaterial;
            unpressMaterial = wardrobeFunctionButton.unpressedMaterial;
        }
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.GetComponent<GorillaTriggerColliderHandIndicator>() is not GorillaTriggerColliderHandIndicator indicator)
        {
            return;
        }
        if (debounceTime > Time.time)
        {
            return;
        }
        debounceTime = Time.time + DEBOUNCE_PERIOD;

        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, indicator.isLeftHand, 0.05f);
        GorillaTagger.Instance.StartVibration(indicator.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);

        try { OnPress(); } catch (Exception ex) { Main.Log(ex, BepInEx.Logging.LogLevel.Error); }
    }

    public void SetMaterial(bool on)
    {
        renderer.material = on
            ? pressMaterial
            : unpressMaterial;
    }

    public virtual void OnPress()
    {
        try
        {
            ButtonHandler.Press(this);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}
