using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace MonkeStatistics.Patches;

[HarmonyPatch(typeof(VRRigCache), "TryGetVrrig")] // TryGetVrrig(NetPlayer targetPlayer, out RigContainer playerRig)
internal static class VRRigCachePatch
{
    [HarmonyPostfix]
    [HarmonyWrapSafe]
    private static void SpawnRig(bool __result, NetPlayer targetPlayer, RigContainer playerRig)
    {
        if (!__result || targetPlayer.IsLocal) return;

        if (targetPlayer.GetPlayerRef().CustomProperties.ContainsKey(Main.GUID))
        {
            Main.Log(targetPlayer.SanitizedNickName + " has watch");
            if (!WatchSpawner.Instance.Watches.TryGetValue(playerRig.Rig, out var watchObject))
            {
                Main.Log("No watch for rig", BepInEx.Logging.LogLevel.Warning);
                return;
            }
            watchObject.SetActive(true);
        }
    }
}
