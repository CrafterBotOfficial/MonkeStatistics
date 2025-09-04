using System.ComponentModel;
using System.Runtime.InteropServices;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.UI;

public class PageBuilder
{
    internal Content Content;
    private int lineIndex;

    public int RemainingLines => 10 - lineIndex;

    public PageBuilder()
    {
        Content = new Content();
    }

    public void SetAuthor(string text)
    {
        Content.SetAuthor(text);
    }

    public virtual void AddSpacing(int count)
    {
        if (lineIndex + count >= 10)
        {
            Main.Log("Cannot add spacing outside array, consider using a ScrollPageBuilder instead.", BepInEx.Logging.LogLevel.Warning);
            return;
        }
        lineIndex += count;
    }

    public virtual void AddText(string text)
    {
        AddLine(text, Content.ButtonType.None);
    }

    public virtual Content.Line AddLine(string text, Content.ButtonType buttonType)
    {
        if (MaxLinesReached()) return default;
        var line = new Content.Line(text, buttonType);
        Content.Lines[lineIndex] = line;
        lineIndex++;
        return line;
    }

    public virtual Content.Line AddLine(string text, IButtonHandler buttonHandler)
    {
        if (MaxLinesReached()) return default;
        var line = new Content.Line(text, buttonHandler);
        Content.Lines[lineIndex] = line;
        lineIndex++;
        return line;
    }

    private bool MaxLinesReached()
    {
        if (lineIndex >= 10)
        {
            Main.Log("Max lines reached for page", BepInEx.Logging.LogLevel.Warning);
            return true;
        }
        return false;
    }

    public virtual Content GetContent()
    {
        return Content;
    }
}
