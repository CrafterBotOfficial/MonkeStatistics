using System.Linq;
using System.Text;

namespace MonkeStatistics.UI;

    // TODO: Make look fabules
public partial class PageBuilder
{
    /// <summary>
    /// Creates a error page with the error message.
    /// </summary>
    /// <returns>Premade page content</returns>
    public static Content GetErrorPage(string errorMessage)
    {
        Main.Log(errorMessage, BepInEx.Logging.LogLevel.Error);
        var builder = new ScrollPageBuilder();
        builder.AddText("[Error]");
        builder.AddText(errorMessage);
        return builder.GetContent();
    }

    /// <summary>
    /// Creates a warning page that this mod must be used in a room.
    /// </summary>
    /// <returns>Premade page content</returns>
    public static Content GetNotInRoomPage()
    {
        var builder = new PageBuilder();
        builder.AddText("You must be in a room to do this");
        return builder.GetContent();
    }

    /// <summary>
    /// Creates a warning page that this mod cannot be used in a none-modded room.
    /// </summary>
    /// <returns>Premade page content</returns>
    public static Content GetNotInModdedRoomPage(IPage calling)
    {
        var builder = new PageBuilder();
        builder.AddText("Not in modded room");
        builder.AddText("You must be in a modded room to use " + calling.GetName());
        return builder.GetContent();
    }
}
