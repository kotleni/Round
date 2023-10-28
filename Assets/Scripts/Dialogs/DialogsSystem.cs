using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogsSystem : MonoBehaviour
{
    [SerializeField] private Image dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogTitle;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private GameObject dialogButtonRef;

    private Dialog currentDialog = null;
    
    public static DialogsSystem instance;
    
    public DialogsSystem()
    {
        instance = this;
    }

    private void Start()
    {
        CloseDialog();
    }

    public void OpenDialogByName(GameObject caller, string name)
    {
        Dialog dialog = null;
        switch (name)
        {
            case "frog_hello":
                Action<Dialog> callback = dialog =>
                {
                    CloseDialog();
                };
                dialog = new Dialog(
                    "Denis a Frog", 
                    "Hello! How did you end up here? Ah... it doesn't matter.", 
                    new []
                    {
                        new DialogButton("Hello.", callback), 
                        new DialogButton("Bye.", callback), 
                    });
                break;
        }
        if(dialog != null)
            OpenDialog(caller, dialog);
    }

    private GameObject CreateButton(Dialog dialog, DialogButton dialogButton)
    {
        GameObject button = Instantiate(dialogButtonRef);
        button.SetActive(true);
        Button button2 = button.GetComponent<Button>();
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.text = dialogButton.title;
        button2.onClick.AddListener(() => dialogButton.clickAction.Invoke(dialog));
        return button;
    }

    void OpenDialog(GameObject caller, Dialog dialog)
    {
        currentDialog = dialog;
        dialogPanel.gameObject.SetActive(true);

        dialogTitle.text = dialog.label;
        dialogText.text = dialog.text;

        foreach (var button in dialog.dialogButtons)
        {
            GameObject buttonObj = CreateButton(dialog, button);
            buttonObj.transform.SetParent(dialogContainer.transform);
        }
        
        SmoothCamera.instance.SetTarget(caller.transform);
    }

    void CloseDialog()
    {
        SmoothCamera.instance.ResetTarget();
        dialogPanel.gameObject.SetActive(false);
        currentDialog = null;
    }
}
