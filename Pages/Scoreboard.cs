using System.Linq;
using GorillaNetworking;
using MonkeStatistics.Extensions;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;
using UnityEngine;

namespace MonkeStatistics.Pages;

public class Scoreboard : IPage
{
    public string GetName() => "Scoreboard";

    public Scoreboard()
    {
        NetworkSystem.Instance.OnPlayerJoined += _ => UpdateScoreboard();
        NetworkSystem.Instance.OnPlayerLeft += _ => UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        if (this.IsActive())
        {
            Main.Log("Updating scoreboard");
            this.Redraw();
        }
    }

    public Content GetContent()
    {
        if (NetworkSystem.Instance is not NetworkSystem networkSystem || !networkSystem.InRoom)
        {
            return PageBuilder.GetNotInRoomPage();
        }

        var builder = new PageBuilder();
        builder.SetAuthor("Player Count: " + networkSystem.RoomPlayerCount);

        foreach (var player in networkSystem.AllNetPlayers)
        {
            if (player.IsLocal)
            {
                builder.AddLine(player.SanitizedNickName);
                continue;
            }
            builder.AddLine(player.SanitizedNickName, new SelectPlayerButtonHandler(player));
        }

        return builder.GetContent();
    }

    private class SelectPlayerButtonHandler(NetPlayer player) : IButtonHandler
    {
        public NetPlayer Player = player;

        public void Press(LineButton button)
        {
            if (!Player.InRoom)
                return;

            if (GameObject.FindFirstObjectByType<GorillaScoreboardSpawner>() is not GorillaScoreboardSpawner spawner)
            {
                Main.Log("No scoreboard spawner", BepInEx.Logging.LogLevel.Error);
                return;
            }

            if (spawner.currentScoreboard)
            {
                var playerLine = spawner.currentScoreboard.lines.Find(line => line.playerActorNumber == Player.ActorNumber);
                if (playerLine == null)
                {
                    Main.Log("No player line, maybe it hasn't spawned yet?", BepInEx.Logging.LogLevel.Warning);
                    return;
                }
                bool muted = playerLine.mute != 0;
                playerLine.PressButton(!muted, GorillaPlayerLineButton.ButtonType.Mute);
                button.SetMaterial(muted);
            }
        }
    }
}
