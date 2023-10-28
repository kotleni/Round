using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tampoline : MonoBehaviour
{
    [SerializeField] private Vector2 impulse;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb)
        {
            // Force reset velocity to impulse
            rb.velocity = impulse;
            _animator.Play("TrampolineActive", -1, 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (new Vector3(impulse.x, impulse.y).normalized * 2f));
    }
}
