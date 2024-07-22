using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LineBrokeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(GameObject item in items)
            {
                item.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
    }
}
