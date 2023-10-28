using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Food : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    public float healthBonus = 5f;

    private Vector2 originalPos;
    private float animScale = 0.3f;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        transform.position = originalPos + new Vector2(0f, (float)Math.Sin(Time.time) * animScale);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pl = other.gameObject.GetComponent<PlayerController>();
        if (pl)
        {
            pl.AddHealth(healthBonus);
            Destroy(gameObject);
        }
    }
}
