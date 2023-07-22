using System;
using UnityEngine;

namespace MonkeStatistics.Behaviours
{
    public class LineButton : GorillaPressableButton
    {
        private const int EffectTime = 250; // MS

        public bool IsToggle { get; internal set; }
        public Action<bool> OnPress { get; private set; }

        public override void Start()
        {
            WardrobeFunctionButton wardrobeFunctionButton = FindObjectOfType<WardrobeFunctionButton>();
            this.buttonRenderer = GetComponent<MeshRenderer>();
            this.pressedMaterial = wardrobeFunctionButton.pressedMaterial;
            this.unpressedMaterial = wardrobeFunctionButton.unpressedMaterial;

            this.onPressButton = new UnityEngine.Events.UnityEvent();
            this.onPressButton.AddListener(HandlePress);
        }

        internal void Init(bool IsToggle, bool Active, bool ObjectEnabled, Action<bool> action)
        {
            this.IsToggle = IsToggle;
            isOn = Active;
            gameObject.SetActive(ObjectEnabled);
            OnPress = action;
        }

        private async void HandlePress()
        {
            isOn = !isOn;
            OnPress?.Invoke(isOn);

            if (IsToggle)
            {
                UpdateColor();
                return;
            }

            UpdateColor();
            await System.Threading.Tasks.Task.Delay(EffectTime);
            isOn = false;
            UpdateColor();
        }
    }
}
