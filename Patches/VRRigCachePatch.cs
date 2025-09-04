/*
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace MonkeStatistics.Patches;

[HarmonyPatch(typeof(VRRigCache), "SpawnRig")]
internal static class VRRigCachePatch
{
    [HarmonyPostfix]
    private static void SpawnRig(RigContainer __result)
    {
        Main.Log("hi " + __result.vrrig.isLocal);
        if (__result.vrrig.isLocal || __result.vrrig.isOfflineVRRig)
        {
            __result.vrrig.AddComponent<UI.LocalWatchManager>();
            return;
        }
        WatchSpawner.Instance.Spawn(__result.vrrig);
    }
}
*/
