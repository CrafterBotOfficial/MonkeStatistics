using BepInEx;
using UnityEngine;

namespace MonkeStatistics
{
    [BepInPlugin("Crafterbot.MonkeStatistics", "MonkeStatistics", "1.1.0")]
    internal class Main : BaseUnityPlugin
    {
        private static Main Instance;

        internal Main()
        {
            Instance = this;
            Log("Initializing MonkeStatistics");

            new HarmonyLib.Harmony(Info.Metadata.GUID).PatchAll(typeof(Patches));
        }

        internal static async void AppendWatchToRig(VRRig Rig)
        {
            Transform WatchTransform = (Instantiate(await LoadAsset("Watch"), Rig.transform.Find("/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L")) as GameObject).transform;

            WatchTransform.SetParent(GameObject.Find("Global/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L").transform);
            WatchTransform.localPosition = new Vector3(0.0288f, 0.0267f, -0.004f);
            WatchTransform.localRotation = Quaternion.Euler(-26.97f, 94.478f, -93.21101f);

            if (Rig.isOfflineVRRig)
            {
                WatchTransform.gameObject.AddComponent<Behaviours.UIManager>();

                GameObject Trigger = WatchTransform.Find("Trigger").gameObject;
                Trigger.layer = 18;
                // Trigger.AddComponent<>();
            }
        }

        internal static void DeappendWatchFromRig(VRRig Rig)
        {
            Transform WatchTransform = Rig.transform.Find("/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/Watch(Clone)");
            if (WatchTransform is object)
                Destroy(WatchTransform.gameObject);
        }

        internal static async System.Threading.Tasks.Task<UnityEngine.Object> LoadAsset(string Name)
        {
            using (System.IO.Stream Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("MonkeStatistics.Resources.watch"))
            {
                AssetBundleCreateRequest AssetBundleCreateRequest = AssetBundle.LoadFromStreamAsync(Stream);
                await System.Threading.Tasks.Task.Run(() => AssetBundleCreateRequest.isDone); // prevents game from crashing
                AssetBundleRequest assetBundleRequest = AssetBundleCreateRequest.assetBundle.LoadAssetAsync(Name, typeof(GameObject));
                await System.Threading.Tasks.Task.Run(() => assetBundleRequest.isDone); // prevents game from crashing
                return assetBundleRequest.asset;
            }
        }

        internal static void Log(object data, BepInEx.Logging.LogLevel logLevel = BepInEx.Logging.LogLevel.Info)
        {
            if (Instance is object)
                Instance.Logger.Log(logLevel, data);
            else UnityEngine.Debug.Log($"[MonkeStatistics : {System.DateTime.Now.ToString("MM:dd:HH:ss")}]" + data);
        }

        internal static string GUID
        {
            get
            {
                return Instance.Info.Metadata.GUID;
            }
        }
    }
}