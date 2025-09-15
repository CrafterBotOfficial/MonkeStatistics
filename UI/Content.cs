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
    public Line[] Lines;
    public bool ShowScrollButtons;

    public Content()
    {
        Author = string.Empty;
        Lines = new Line[10];
        ShowScrollButtons = false;
    }

    public Content(string text)
    {
        Author = string.Empty;
        Lines = new Line[10];
        Lines[0].Text = text;
        ShowScrollButtons = false;
    }

    public Content(string[] lines)
    {
        Author = string.Empty;
        Lines = new Line[10];
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i].Text = lines[i];
        }
        ShowScrollButtons = false;
    }

    public Content(IEnumerable<Line> lines)
    {
        Author = string.Empty;
        Lines = new Line[10];
        var array = lines.ToArray();
        if (array.Length > 10)
        {
            Main.Log("Max line count is 10", BepInEx.Logging.LogLevel.Error);
            return;
        }
        Lines = array;
        ShowScrollButtons = false;
    }

#nullable enable
    public struct Line
    {
        public string Text;
        public IButtonHandler? ButtonHandler;

        public Line(string text)
        {
            Text = text;
            ButtonHandler = null;
        }

        public Line(string text, ButtonType type)
        {
            Text = text;
            ButtonHandler = type switch
            {
                ButtonType.Press => new PressButtonHandler(),
                ButtonType.Toggle => new ToggleButtonHandler(),
                _ => throw new ArgumentException("Not ButtonType")
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
        Toggle
    }
}
