using HarmonyLib;
using UnityEngine;

namespace MonkeStatistics
{
    internal class Patches
    {
        [HarmonyPatch(typeof(GorillaTagger), "Start"), HarmonyPostfix]
        private static void GorillaTagger_Start_Postfix()
        {
            Main.Log("GorillaTagger.Start");

            new GameObject("Callbacks").AddComponent<Behaviours.Callbacks>();

            // Append the watch menu to the offline rig
            Main.AppendWatchToRig(GorillaTagger.Instance.offlineVRRig);
        }
    }
}
