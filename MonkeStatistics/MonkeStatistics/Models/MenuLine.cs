using MonkeStatistics.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeStatistics.Models
{
    public class MenuLine
    {
        public GameObject Line;
        public Text Text;
        public LineButton lineButton;

        public MenuLine(GameObject line, Text text, LineButton lineButton)
        {
            Line = line;
            Text = text;
            this.lineButton = lineButton;
        }
    }
}
