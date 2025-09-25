using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.UI;

// Should probably simplify this by removing the pagebuilder alltogether and only having scroll pages;
public class ScrollPageBuilder : PageBuilder
{
    internal List<Content.Line> builderLines = new List<Content.Line>();

    private Content.Line[] GetLines()
    {
        if (builderLines.Count < UI.UIManager.MAX_LINES)
        {
            return builderLines.ToArray();
        }

        int groupIndex = 0;
        var chunks = builderLines.GroupBy(line => groupIndex++ / UIManager.MAX_LINES);

        return chunks.ElementAt(LocalWatchManager.Instance.UIManager.SceneIndex).ToArray();
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

    public override Content.Line AddLine(string text, Action<LineButton, bool> onToggle)
    {
        var line = new Content.Line(text, Content.ButtonType.Toggle);
        (line.ButtonHandler as ToggleButtonHandler).OnToggled += onToggle;
        builderLines.Add(line);
        return line;
    }

    public override Content.Line AddLine(string text, Action onPress)
    {
        var line = new Content.Line(text, Content.ButtonType.Press);
        (line.ButtonHandler as PressButtonHandler).OnPressEvent = onPress;
        builderLines.Add(line);
        return line;
    }

    public override Content.Line AddLine(string text, IButtonHandler buttonHandler)
    {
        var line = new Content.Line(text, buttonHandler);
        builderLines.Add(line);
        return line;
    }

    /// <summary>
    /// Gets the lines that **should** be displayed on this page.
    /// </summary>
    public override Content GetContent()
    {
        var chunk = GetLines();
        for (int i = 0; i < chunk.Length; i++)
            Content.Lines[i] = chunk[i];
        Content.ShowScrollButtons = true;
        return Content;
    }
}
