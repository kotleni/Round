using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogsSystem : MonoBehaviour
{
    [SerializeField] private RectTransform canvasContainer;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogTitle;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogContainer;
    [SerializeField] private GameObject dialogButtonRef;

    private Dialog currentDialog = null;
    private List<GameObject> createdButtons = new List<GameObject>();
    
    private Action<Dialog> defaultCallback = dialog =>
    {
        DialogsSystem.instance.CloseDialog();
    };
    
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
                    OpenDialogByName(caller, "frog_second");
                };
                dialog = new Dialog(
                    "Denis a Frog", 
                    "Hello! How did you end up here? Ah... it doesn't matter.", 
                    new []
                    {
                        new DialogButton("Hello. Where i am?", callback), 
                        new DialogButton("Where i am?.", callback), 
                    });
                break;
            case "frog_second":
                Action<Dialog> callback3 = dialog =>
                {
                    CloseDialog();
                    OpenDialogByName(caller, "frog_where");
                };
                dialog = new Dialog(
                    "Denis a Frog", 
                    "You are on our island... I see, you have a sword. You can kill slimes?", 
                    new []
                    {
                        new DialogButton("Yes.", callback3),
                        new DialogButton("Okay, i can.", callback3), 
                    });
                break;
            case "frog_where":
                dialog = new Dialog(
                    "Denis a Frog", 
                    "Just go straight... And you'll find them.", 
                    new []
                    {
                        new DialogButton("Ok", defaultCallback), 
                    });
                break;
            case "slime_hello":
                dialog = new Dialog(
                    "Slime", 
                    "*slurping*", 
                    new []
                    {
                        new DialogButton("Oh...", defaultCallback), 
                    });
                break;
        }
        if(dialog != null)
            OpenDialog(caller, dialog);
    }

    public bool IsDialogOpened()
    {
        return currentDialog != null;
    }

    private GameObject CreateButton(Dialog dialog, DialogButton dialogButton)
    {
        dialogButtonRef.SetActive(true);
        
        GameObject button = Instantiate(dialogButtonRef);
        Button button2 = button.GetComponent<Button>();
        
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.text = dialogButton.title;
        button2.onClick.AddListener(() => dialogButton.clickAction.Invoke(dialog));
        
        dialogButtonRef.SetActive(false);
        return button;
    }

    private void UpdateDialogPos()
    {
        // FIXME: it's not working normally, put pin to top corner
        RectTransform rectTransform = dialogPanel.GetComponent<RectTransform>();
        //float y = canvasContainer.rect.height;// - rectTransform.rect.height;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
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
            buttonObj.transform.SetAsLastSibling();
            
            // Store created button
            createdButtons.Add(buttonObj);
            
            // MARK: Unity UI for all new items in container set scale to 1.4
            // MARK: It's bug, or i so stupid :)
            // Reset UI element scale
            RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
        }
        
        UpdateDialogPos();
        
        PlayerController.instance.ResetVelocity();
        SmoothCamera.instance.SetTarget(caller.transform);
    }

    void CloseDialog()
    {
        SmoothCamera.instance.ResetTarget();
        dialogPanel.gameObject.SetActive(false);
        currentDialog = null;

        // Remove all created buttons
        foreach (var button in createdButtons)
        {
            Destroy(button);
        }
    }
}
