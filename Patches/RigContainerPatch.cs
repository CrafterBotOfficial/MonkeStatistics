using System;
using HarmonyLib;
using MonkeStatistics.Extensions;
using MonkeStatistics.Pages;
using MonkeStatistics.UI;

namespace MonkeStatistics.Patches;

[HarmonyPatch(typeof(RigContainer), "RefreshAllRigVoices")] // RigContainer.RefreshAllRigVoices();
[HarmonyWrapSafe]
internal static class RigContainerPatch
{
    private static void Postfix()
    {
        var instance = typeof(Audio).GetPageInstance();
        if (instance is not null && instance.IsActive())
        {
            LocalWatchManager.Instance.UIManager.UpdatePage();
        }
    }
}
