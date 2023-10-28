using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [Range(0.1f, 8.0f)]
    [SerializeField]
    private float buoyancy = 2.0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object is affected by the water (has a rigidbody2D)
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            // Apply an upward force to simulate buoyancy
            rb.AddForce(Vector2.up * buoyancy, ForceMode2D.Impulse);
        }
    }
}
