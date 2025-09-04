using System.Collections.Generic;
using System.Linq;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.UI;

public class ScrollPageBuilder : PageBuilder
{
    private const int ENTRIES_PER_PAGE = 10;

    internal int Scene;
    internal List<Content.Line> builderLines = new List<Content.Line>();

    private Content.Line[] GetLines()
    {
        if (builderLines.Count < 10)
        {
            return builderLines.ToArray();
        }

        var chunks = builderLines.Select((v, i) => new { v, groupIndex = i / ENTRIES_PER_PAGE })
                                 .GroupBy(x => x.groupIndex)
                                 .Select(g => g.Select(x => x.v))
                                 .ToArray();

        return chunks[Scene].ToArray();
    }

    public override void AddSpacing(int count)
    {
        for (int i = 0; i < count; i++)
            builderLines.Add(new Content.Line());
    }

    public override Content.Line AddLine(string text, Content.ButtonType buttonType)
    {
        var line = new Content.Line(text, buttonType);
        builderLines.Add(line);
        return line;
    }

    public override Content.Line AddLine(string text, IButtonHandler buttonHandler)
    {
        var line = new Content.Line(text, buttonHandler);
        builderLines.Add(line);
        return line;
    }


    public override Content GetContent()
    {
        Content.Lines = GetLines();
        return Content;
    }
}
