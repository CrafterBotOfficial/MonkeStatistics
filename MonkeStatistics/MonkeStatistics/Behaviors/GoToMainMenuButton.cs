using System;

namespace MonkeStatistics.Behaviors
{
    internal class GoToMainMenuButton : PreConfiguredButton
    {
        public static Type ReturnPage;

        public override void ButtonActivation()
        {
            StartCoroutine(ButtonDelay());
            UIManager.Instance.ShowPage(ReturnPage);
        }
    }
}
