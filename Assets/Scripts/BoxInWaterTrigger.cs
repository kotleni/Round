using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxInWaterTrigger : MonoBehaviour
{
    public static bool isDrowned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("VillagersBox"))
        {
            isDrowned = true;
            Npc tikiNpc = NpcManager.instance.FindNpc("tikiboy");
            if(tikiNpc != null)
            {
                DialogsSystem.instance.OpenDialogByName(tikiNpc.gameObject, "tikiboy_drownedboxes");
            }

            // Made it looks dry
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}

