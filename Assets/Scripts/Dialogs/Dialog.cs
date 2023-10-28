public class Dialog
{
    public string label;
    public string text;
    public DialogButton[] dialogButtons;

    public Dialog(string label, string text, DialogButton[] dialogButtons)
    {
        this.label = label;
        this.text = text;
        this.dialogButtons = dialogButtons;
    } 
}