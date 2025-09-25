namespace MonkeStatistics.UI;

public interface IPage
{
    /// <summary>
    /// The text that will be displayed in the menu (if registered) and at the top of the page.
    /// </summary>
    public string GetName();

    /// <summary>
    /// The actual content of the page, all 10 lines must be present.
    /// </summary>
    public Content GetContent();
}
