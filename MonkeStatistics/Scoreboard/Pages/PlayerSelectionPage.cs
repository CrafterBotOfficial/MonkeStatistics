using MonkeStatistics.API;
using Photon.Pun;
using System.Linq;
using UnityEngine;

namespace Scoreboard.Pages
{
    [DisplayInMainMenu("Scoreboard", true)]
    internal class PlayerSelectionPage : Page
    {
        public override void OnPageOpen()
        {
            if (PhotonNetwork.InRoom)
                DrawPage();
            else
                GoToMainPage();
        }

        public void DrawPage()
        {
            base.OnPageOpen(); // this will setup the page

            SetTitle("Scoreboard");
            SetAuthor($"Player Count:{PhotonNetwork.CurrentRoom.PlayerCount}");

            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                var player = PhotonNetwork.PlayerList[i];
                bool IsMuted = PlayerPrefs.GetInt(player.UserId, 0) == 1;
                if (player.IsLocal)
                    AddLine(player.NickName);
                else
                    AddLine(player.NickName, new ButtonInfo(i, ButtonInfo.ButtonType.Toggle, IsMuted));
            }
            SetLines();
        }

        public override void OnButtonPress(int ReturnIndex)
        {
            Debug.Log("Button pressed | " + ReturnIndex);

            string UserId = PhotonNetwork.PlayerList[ReturnIndex].UserId;
            GameObject
                .FindObjectsOfType<GorillaPlayerScoreboardLine>()
                .First(x => x.linePlayer.UserId == UserId)
                .PressButton(PlayerPrefs.GetInt(UserId, 0) != 0, GorillaPlayerLineButton.ButtonType.Mute);
            //DrawPage();
        }
    }
}
