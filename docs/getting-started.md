# Getting Started

To make your own page first create a new project, ideally using the modding [template](https://github.com/Graicc/GorillaTagModTemplate). Then create a new class that impliments [MonkeStatistics.UI.IPage](api/MonkeStatistics.UI.IPage.html) and add the following two methods. ``IPage.GetName()`` and ``IPage.GetContent()``. The GetName() method must return a string for the title of the page and the GetContent() should return a new [Content](api/MonkeStatistics.UI.Content.html). You can make one yourself or use one of the two builders to make it easier. The following is an example of a basic page.

```cs
[AutoRegister]
public class MyPage : IPage
{
    public string GetName() => "My Page";

    public Content GetContent()
    {
        var builder = new PageBuilder();

        builder.AddSpacing(2); // adds padding from the title
        if (NetworkSystem.Instance.InRoom)
        {
            builder.AddText("code: {}", NetworkSystem.Instance.RoomName);
            builder.AddLine("You are in a room");
            builder.AddLine("Disconnect", Leave);
        }
        else 
        {
            builder.AddText("Join a room to get started");
        }

        return builder.GetContent();
    }

    private void Leave(LineButton lineButton)
    {
    }
}
```

The ``[AutoRegister]`` attribute tells MonkeStatistics to add this page to the main menu. Without it the page would be inaccessible to the player.
