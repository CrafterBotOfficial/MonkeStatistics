using BepInEx;
using BepInEx.Logging;
using System.IO;
/*
    The code for this project is a bit underwelming, unfortantly I made this a while ago
    and it has fallen under my current standards. I cannot rewrite it due to some other mods
    requiring it:/
*/
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using Utilla;

namespace MonkeStatistics
{
    [BepInPlugin(Id, Name, Version), BepInDependency("org.legoandmars.gorillatag.utilla")]
    [ModdedGamemode]
    internal class Main : BaseUnityPlugin
    {
        internal const string
            Id = "Crafterbot.MonkeStatistics",
            Name = "MonkeStatistics",
            Version = "1.0.5";
        internal static Main Instance;

        internal ManualLogSource manualLogSource => Logger;
        internal bool RoomModded;

        internal Main()
        {
            Instance = this;
            API.Registry.Register();

            Utilla.Events.RoomLeft += Events_RoomLeft;
            new HarmonyLib.Harmony(Id).PatchAll(Assembly.GetExecutingAssembly());
        }

        private AssetBundle _assetBundle;
        internal async Task<GameObject> LoadAsset(string Name)
        {
            const string BundlePath = "MonkeStatistics.Resources.watch";
            if (!(_assetBundle is object))
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(BundlePath))
                {
                    AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromStreamAsync(stream);
                    new WaitUntil(() => assetBundleCreateRequest.isDone);
                    _assetBundle = assetBundleCreateRequest.assetBundle;
                }
            AssetBundleRequest assetBundleRequest = _assetBundle.LoadAssetAsync<GameObject>(Name);
            new WaitUntil(() => assetBundleRequest.isDone);
            return assetBundleRequest.asset as GameObject;
        }

        #region Game Events
        [ModdedGamemodeJoin]
        private void OnJoin()
        {
            RoomModded = true;
        }
        private void Events_RoomLeft(object sender, Events.RoomJoinedArgs e)
        {
            RoomModded = false;
            UIManager.Instance.ForceClose();
        }
        #endregion
    }
}
