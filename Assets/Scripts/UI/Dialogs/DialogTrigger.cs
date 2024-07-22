using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerRule
{
    DEFAULT, // Invoke only when meets first time
    SLIMES_KILLED, // Invokes only if slimes killed
    BOXES_DROWNED, // Villagers boxes from cave is drowned
}

[RequireComponent(typeof(BoxCollider2D))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private string dialogName;
    [SerializeField] private TriggerRule rule;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogsSystem.instance.OpenDialogByName(gameObject, dialogName, rule);
        }
    }
}
