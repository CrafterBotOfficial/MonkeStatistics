using GorillaNetworking;
using MonkeStatistics.UI;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.Pages;

public class Audio : IPage
{
    public static Audio Instance;

    public string GetName() => "Audio Settings";

    public Content GetContent()
    {
        var builder = new PageBuilder();
        if (!KIDManager.HasPermissionToUseFeature(EKIDFeatures.Voice_Chat))
        {
            builder.AddText("Not Permitted to use voicechat");
            return builder.GetContent();
        }

        // TODO Add weird monke voice thingy 
        builder.AddSpacing(2);
        builder.AddText("Voice: " + GorillaComputer.instance.voiceChatOn);
        builder.AddLine("Mute", OnMute);

        return builder.GetContent();
    }

    private void OnMute(LineButton lineButton, bool _)
    {
        // TODO Impliment
    }
}
