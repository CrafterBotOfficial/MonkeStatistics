namespace MonkeStatistics.UI.Buttons;

public interface IButtonHandler
{
    /// <summary>
    /// When the button is pressed.
    /// </summary>
    public void Press(LineButton button);
}
