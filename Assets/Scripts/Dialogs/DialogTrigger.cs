using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private string dialogName;
    
    private bool isActivated = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isActivated) return;

        if (other.CompareTag("Player"))
        {
            DialogsSystem.instance.OpenDialogByName(gameObject, dialogName);
            isActivated = true;
        }
    }
}
