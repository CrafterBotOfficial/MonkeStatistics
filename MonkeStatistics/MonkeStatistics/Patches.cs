using HarmonyLib;
using UnityEngine;

namespace MonkeStatistics
{
    [HarmonyPatch]
    internal class Patches
    {
        [HarmonyPatch(typeof(GorillaLocomotion.Player), "Awake"), HarmonyPostfix]
        private static async void PlayerAwake()
        {
            /* Creates the watch */

            GameObject WatchObj = UnityEngine.Object.Instantiate(await Main.Instance.LoadAsset("Watch"));
            Transform WatchTransform = WatchObj.transform;

            WatchTransform.SetParent(GameObject.Find("Global/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L").transform);
            WatchTransform.localPosition = new Vector3(0.0288f, 0.0267f, -0.004f);
            WatchTransform.localRotation = Quaternion.Euler(-26.97f, 94.478f, -93.21101f);

            new UIManager(WatchObj);

            GameObject Trigger = WatchTransform.Find("Trigger").gameObject;
            Trigger.layer = 18;
            Trigger.AddComponent<Behaviors.WatchButton>();
        }
    }
}
