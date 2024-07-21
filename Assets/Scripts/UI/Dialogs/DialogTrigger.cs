using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TriggerRule
{
    DEFAULT, // Invoke only when meets first time
    SLIMES_KILLED, // Invokes only if slimes killed
    BOXES_DROWNED, // Villagers boxes from cave is drowned
}

[RequireComponent(typeof(BoxCollider2D))]
public class DialogTrigger : MonoBehaviour
{
    private static List<string> activatedDialogs = new List<string>();

    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private string dialogName;
    [SerializeField] private TriggerRule rule;

    /// <summary>
    /// Check is all enemies by id is die by foreaching all entities
    /// </summary>
    /// <returns>True if no any enemies here.</returns>
    private bool IsAllEnemiesKindDie(string enemyId)
    {
        var enemies = GameObject.FindSceneObjectsOfType(typeof(Enemy));
        foreach(Enemy enemy in enemies)
        {
            if (enemy.GetId() == enemyId) return false;
        }
        return true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (activatedDialogs.Contains(dialogName))
            {
                return; // Already invoked before
            }

            switch(rule) {
                case TriggerRule.SLIMES_KILLED:
                    if (!IsAllEnemiesKindDie("slime")) return; // Return if has slimes
                    break;
                case TriggerRule.BOXES_DROWNED:
                    if (!BoxInWaterTrigger.isDrowned) return;
                    break;
            }

            activatedDialogs.Add(dialogName);
            DialogsSystem.instance.OpenDialogByName(gameObject, dialogName);
        }
    }
}
