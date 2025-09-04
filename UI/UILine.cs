using MonkeStatistics.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeStatistics.UI;

public class UILine(GameObject @object)
{
    public GameObject MyObject = @object;
    public Text Text = @object.GetComponent<Text>();
    public LineButton Button = @object.GetComponentInChildren<BoxCollider>().gameObject.AddComponent<LineButton>();
}
