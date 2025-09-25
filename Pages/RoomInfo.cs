using System.Text;
using Constants;
using GorillaGameModes;
using MonkeStatistics.Extensions;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;
using Photon.Pun;

namespace MonkeStatistics.Pages;

internal class RoomInfo : IPage
{
    public string GetName() => "Room Info";

    public RoomInfo()
    {
        NetworkSystem.Instance.OnJoinedRoomEvent += RefreshPage;
        NetworkSystem.Instance.OnReturnedToSinglePlayer += RefreshPage;
        NetworkSystem.Instance.OnPlayerJoined += _ => RefreshPage();
        NetworkSystem.Instance.OnPlayerLeft += _ => RefreshPage();
    }

    private void RefreshPage()
    {
        if (this.IsActive()) // stops warnings
        {
            this.Redraw();
        }
    }

    public Content GetContent()
    {
        if (NetworkSystem.Instance is not NetworkSystem network)
            return PageBuilder.GetErrorPage("NetworkSystem isn't setup yet!");

        if (NetworkSystem.Instance.InRoom)
        {
            int maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers; // TODO Use network system, note the currentroom is null

            var builder = new PageBuilder();
            builder.AddText("Name: {0}", network.RoomName);
            builder.AddText("Players: {0}/{1}", network.AllNetPlayers.Length, maxPlayers);
            builder.AddText("GameMode: {0}", GorillaGameManager.instance?.GameModeName() ?? "Unknown");

            builder.AddSpacing(1);

            var line = builder.AddLine("Disconnect", Disconnect); // Always be press button

            return builder.GetContent();
        }
        else
        {
            return new Content("Not in a room");
        }
    }

    private void Disconnect()
    {
        Main.Log("Disconnecting... sneaky");
        NetworkSystem.Instance.ReturnToSinglePlayer();
    }
}
