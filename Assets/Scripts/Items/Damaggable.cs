using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damaggable : MonoBehaviour
{
    [Range(1f, 9999f)] 
    [SerializeField] 
    private float damage;
    
    private float _lastDmgTime;
    private const float DmgDelay = 0.7f;
    private void MakeDamage(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            if(Time.time - _lastDmgTime < DmgDelay)
            return;

            _lastDmgTime = Time.time;

            PlayerController pl = gameObject.GetComponent<PlayerController>();
            pl.Hit(damage, gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        MakeDamage(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        MakeDamage(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MakeDamage(other.gameObject);
    }

     private void OnTriggerStay2D(Collider2D other)
    {
        MakeDamage(other.gameObject);
    }
    
    void OnDrawGizmos()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            // Draw gizmos to visualize the camera boundaries
            Gizmos.color = Color.red; // You can change the color as needed
            Bounds bounds = GetComponent<BoxCollider2D>().bounds;

            // Adjust bounds considering camera size
            float minX = bounds.min.x;
            float maxX = bounds.max.x;
            float minY = bounds.min.y;
            float maxY = bounds.max.y;

            // Draw the bounds using Gizmos.DrawLine
            Vector3 topLeft = new Vector3(minX, maxY, 0);
            Vector3 topRight = new Vector3(maxX, maxY, 0);
            Vector3 bottomLeft = new Vector3(minX, minY, 0);
            Vector3 bottomRight = new Vector3(maxX, minY, 0);

            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(topLeft, bottomRight);
            Gizmos.DrawLine(topRight, bottomLeft);
        }
    }
}
