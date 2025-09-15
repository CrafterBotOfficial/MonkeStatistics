using System.Linq;
using System.Text;

namespace MonkeStatistics.UI;

public partial class PageBuilder
{
    // TODO: Make look fabules
    public static Content GetErrorPage(string errorMessage)
    {
        var builder = new ScrollPageBuilder();
        builder.AddText("[Error]");
        builder.AddText(errorMessage);
        return builder.GetContent();
    }

    public static Content GetNotInRoomPage()
    {
        var builder = new PageBuilder();
        builder.AddText("You must be in a room to do this");
        return builder.GetContent();
    }

    public static Content GetNotInModdedRoomPage(IPage calling)
    {
        var builder = new PageBuilder();
        builder.AddText("Not in modded room");
        builder.AddText("You must be in a modded room to use " + calling.GetName());
        return builder.GetContent();
    }
}
