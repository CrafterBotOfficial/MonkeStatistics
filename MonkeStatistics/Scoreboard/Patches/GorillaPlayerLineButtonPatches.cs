using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.Patches
{
    [HarmonyPatch(typeof(GorillaPlayerLineButton))]
    public class GorillaPlayerLineButtonPatches
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        [HarmonyWrapSafe]
        public static void StartPostfix(GorillaPlayerLineButton __instance)
        {
            if (MonkeStatistics.UIManager.CurrentPage is Pages.PlayerSelectionPage)
            {
                var page = MonkeStatistics.UIManager.CurrentPage as Pages.PlayerSelectionPage;
                page.DrawPage();
            }
        }
    }
}
