using System;

public class DialogButton
{
    public string title;
    public Action<Dialog> clickAction;

    public DialogButton(string title, Action<Dialog> clickAction)
    {
        this.title = title;
        this.clickAction = clickAction;
    }
}