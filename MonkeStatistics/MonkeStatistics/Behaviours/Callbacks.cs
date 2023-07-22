using Photon.Pun;
using Photon.Realtime;
using System.Threading.Tasks;

namespace MonkeStatistics.Behaviours
{
    internal class Callbacks : MonoBehaviourPunCallbacks
    {
        public override async void OnPlayerEnteredRoom(Player newPlayer)
        {
            await Task.Yield(); // The players VRRig should be initialized by now

            var customProperties = newPlayer.CustomProperties;
            if (customProperties.TryGetValue("mods", out object value) && value.ToString().Contains(Main.GUID) && GorillaGameManager.instance is object)
            {
                Main.Log($"Player {newPlayer.NickName} has MonkeStatistics installed.");
                Main.AppendWatchToRig(GorillaGameManager.instance.FindPlayerVRRig(newPlayer));
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (GorillaGameManager.instance is object)
            {
                VRRig Rig = GorillaGameManager.instance.FindPlayerVRRig(otherPlayer);
                if (Rig is object)
                    Main.DeappendWatchFromRig(Rig); // If the player doesn't have the watch in the first place, this will do nothing.
            }
        }
    }
}
