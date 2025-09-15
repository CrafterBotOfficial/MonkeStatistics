using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace MonkeStatistics.Patches;

[HarmonyPatch(typeof(GorillaScoreBoard), nameof(GorillaScoreBoard.RedrawPlayerLines))]
public static class GorillaScoreBoardPatch
{
    private static void Postfix()
    {
        Pages.Scoreboard.Instance?.UpdateScoreboard();
    }
}
