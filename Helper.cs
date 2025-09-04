namespace MonkeStatistics.UI;

public static class Helper
{
    // public static void SetTitle(this Content content, string text) { content.Title = text; }
    public static void SetAuthor(this Content content, string text) { content.Author = text; }

    public static void AddLine(this Content content, Content.Line line)
    {
        AddLine(content.Lines, line);
    }

    public static void AddText(this Content content, string text)
    {
        AddText(content.Lines, text);
    }

    public static void AddLine(Content.Line[] lines, Content.Line line)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Equals(default(Content.Line)))
            {
                lines[i] = line;
                return;
            }
        }
        Main.Log("No empty lines found", BepInEx.Logging.LogLevel.Warning);
    }

    public static void AddText(Content.Line[] lines, string text)
    {
        if (lines.Length > 10) Main.Log("To many lines, max is 10", BepInEx.Logging.LogLevel.Warning);

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Text.IsNullOrEmpty())
            {
                lines[i].Text = text;
                return;
            }
        }
        Main.Log("No empty lines found", BepInEx.Logging.LogLevel.Warning);
    }
}
