using System.Collections;
using UnityEngine;

namespace MonkeStatistics.Behaviors
{
    internal class WatchButton : GorillaPressableButton
    {
        internal static WatchButton Instance;

        public override void Start()
        {
            Instance = this;
            gameObject.layer = 18;

            onPressButton = new UnityEngine.Events.UnityEvent();
            onPressButton.AddListener(new UnityEngine.Events.UnityAction(ButtonActivation));
        }
        public void ButtonActivation()
        {
            if (GetFacingUp())
                UIManager.Instance.WatchButtonPressed();
        }
        private void Update()
        {
            if (!GetFacingUp() && UIManager.Instance.MenuObj.activeSelf)
            {
                UIManager.Instance.ShowPage(typeof(Pages.MainPage));
                UIManager.Instance.MenuObj.SetActive(false);
            }
        }

        private IEnumerator ButtonDelay()
        {
            isOn = true;
            UpdateColor();
            yield return new WaitForSeconds(0.35f);
            isOn = false;
            UpdateColor();
        }

        internal bool GetFacingUp()
        {
            float Distance = Vector3.Distance(transform.forward, Vector3.up);
            return Distance <= 0.65;
        }
    }
}
