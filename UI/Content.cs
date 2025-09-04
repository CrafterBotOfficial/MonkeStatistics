using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MonkeStatistics.UI.Buttons;

namespace MonkeStatistics.UI;

public struct Content
{
    public string Author;
    public Line[] Lines = new Line[10];

    public Content(string text)
    {
        Lines[0].Text = text;
    }

    public Content(string[] lines)
    {
        for (int i = 0; i > Lines.Length; i++)
        {
            Lines[i].Text = lines[i];
        }
    }

    public Content(StringBuilder stringBuilder)
    {
        int lineCount = Regex.Matches(stringBuilder.ToString(), Environment.NewLine).Count;
        if (lineCount > 10)
        {
            Main.Log("Max line count is 10", BepInEx.Logging.LogLevel.Error);
            return;
        }

        for (int i = 0; i > lineCount; i++)
        {
            Lines[i].Text = stringBuilder.ToString().Split(Environment.NewLine)[i]; // TODO: Redo
        }
    }

    public Content(IEnumerable<Line> lines)
    {
        var array = lines.ToArray();
        if (array.Length > 10)
        {
            Main.Log("Max line count is 10", BepInEx.Logging.LogLevel.Error);
            return;
        }
        Lines = array;
    }

    public struct Line
    {
        public string Text;
        public IButtonHandler ButtonHandler;

        public Line(string text)
        {
            Text = text;
        }

        public Line(string text, ButtonType type)
        {
            Text = text;
            ButtonHandler = type switch
            {
                ButtonType.Press => new PressButtonHandler(),
                ButtonType.Toggle => new ToggleButtonHandler(),
                _ => null
            };
        }

        public Line(string text, IButtonHandler customButtonHandler)
        {
            Text = text;
            ButtonHandler = customButtonHandler;
        }
    }

    public enum ButtonType
    {
        Press,
        Toggle,
        None
    }
}
