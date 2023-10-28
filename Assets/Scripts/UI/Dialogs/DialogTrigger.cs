using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private string dialogName;
    [SerializeField] private bool removeAfterUse = true;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogsSystem.instance.OpenDialogByName(gameObject, dialogName);
            if (removeAfterUse)
            {
                Destroy(this);
                Destroy(boxCollider2D);
            }
        }
    }
}
