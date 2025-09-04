using System.Text;
using GorillaGameModes;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.Pages;

public class RoomInfo : IPage
{
    public string GetName() => "Room Info";

    public Content GetContent()
    {
        if (NetworkSystem.Instance is not NetworkSystem network)
            return new Content("Error");

        if (NetworkSystem.Instance.InRoom)
        {
            var builder = new PageBuilder();
            builder.AddText(string.Format("Name: {0}", network.RoomName));
            builder.AddText(string.Format("Players: {0}/{1}", network.AllNetPlayers.Length, network.CurrentRoom.MaxPlayers));
            builder.AddText("GameMode: " + GorillaGameManager.instance.GameModeName() ?? "Unknown");

            builder.AddSpacing(1);

            var line = builder.AddLine("Disconnect", Content.ButtonType.Press);
            (line.ButtonHandler as PressButtonHandler).OnPressEvent += Disconnect;

            return builder.GetContent();
        }
        else
        {
            return new Content("Not in a room");
        }
    }

    private void Disconnect()
    {
        Main.Log("Disconnecting... sneaking sneaky");
        NetworkSystem.Instance.ReturnToSinglePlayer();
    }
}
