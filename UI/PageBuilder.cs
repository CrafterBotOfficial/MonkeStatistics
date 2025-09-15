using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using GorillaTagScripts.CustomMapSupport;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.UI;

public partial class PageBuilder
{
    internal Content Content;
    private int lineIndex;

    public int RemainingLines => UIManager.MAX_LINES - lineIndex;

    public PageBuilder()
    {
        Content = new Content();
    }

    public void SetAuthor(string text)
    {
        Content.Author = text;
    }

    public virtual void AddSpacing(int count)
    {
        if (lineIndex + count >= UIManager.MAX_LINES)
        {
            Main.Log("Cannot add spacing outside array, consider using a ScrollPageBuilder instead.", BepInEx.Logging.LogLevel.Warning);
            return;
        }
        lineIndex += count;
    }

    public virtual void AddText(string template, params object[] args)
    {
        string format = string.Format(template, args);
        AddText(format);
    }

    public virtual void AddText(string text)
    {
        const int lineLength = 17;
        int lineCount = (int)Math.Ceiling((double)text.Length / lineLength);

        if (0 > RemainingLines - lineCount)
            Main.Log("AddText will try to add more lines then allowed, use scroll page builder instead.", BepInEx.Logging.LogLevel.Warning);

        for (int lineIndex = 0; lineIndex < lineCount; lineIndex++)
        {
            int start = lineIndex * lineLength;
            int length = Math.Min(lineLength, text.Length - start);
            string line = text.Substring(start, length);

            AddLine(line);
        }
    }

    public virtual Content.Line AddLine(string text)
    {
        if (MaxLinesReached()) return default;
        var line = new Content.Line(text);
        Content.Lines[lineIndex] = line;
        lineIndex++;
        return line;
    }

    public virtual Content.Line AddLine(string text, Action<LineButton, bool> onToggle)
    {
        if (MaxLinesReached()) return default;
        var line = new Content.Line(text, Content.ButtonType.Toggle);
        (line.ButtonHandler as ToggleButtonHandler).OnToggled += onToggle;
        Content.Lines[lineIndex] = line;
        lineIndex++;
        return line;
    }

    public virtual Content.Line AddLine(string text, Action onPress)
    {
        if (MaxLinesReached()) return default;
        var line = new Content.Line(text, Content.ButtonType.Press);
        (line.ButtonHandler as PressButtonHandler).OnPressEvent = onPress;
        Content.Lines[lineIndex] = line;
        lineIndex++;
        return line;
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
