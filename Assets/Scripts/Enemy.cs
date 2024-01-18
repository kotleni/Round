using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    [SerializeField] private float visionDistance = 12f;
    [SerializeField] private float movingSpeed = 2.0f;
    [SerializeField] private float jumpSpeed = 2.0f;
    [SerializeField] private float swimSpeed = 9.0f;
    [SerializeField] private float maxHealth = 45f;
    [SerializeField] private bool isFlipX = false;
    
    private float moveTo = 0f;

    protected override void OnInit()
    {
        SetMaxSpeed(movingSpeed);
        SetJumpSpeed(jumpSpeed);
        SetSwimSpeed(swimSpeed);
        SetMaxHealth(maxHealth);
        SetInvertedXFlipping(isFlipX);
    }

    protected override void OnDie()
    {
        Destroy(gameObject);
    }

    protected override void OnUpdate()
    {
        if(PlayerController.instance.GetHealth() <= 0) return;
        
        Vector3 playerPos = PlayerController.instance.transform.position;
        float distance = Vector3.Distance(transform.position, playerPos);
        
        if(distance > visionDistance)
            return;
        
        bool isInWater = IsInWater();
        bool isGrounded = IsGrounded();
        
        if (Random.Range(0, 10) > 8)
        {
            moveTo = playerPos.x > transform.position.x ? 1f : -1f;
        }

        MoveHorizontal(moveTo);
        if (isInWater)
        {
            if(playerPos.y > transform.position.y)
                SwimUp();
            else
                SwimDown();
        }
        else if (isGrounded)
        {
            Jump();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, visionDistance);
    }
}