using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    public static PlayerController instance;
    
    [SerializeField] private Collider2D levelBounds;
    [SerializeField] private ParticleSystem _particleSystem;
    
    private float forceMove = 0f;

    public PlayerController()
    {
        instance = this;
    }

    protected override void OnInit()
    {
        SetMaxSpeed(5.0f);
        SetJumpSpeed(9.0f);
        SetSwimSpeed(6.0f);
        SetMaxHealth(100f);
        SetHealthBarVisibility(false);
    }
    
    protected override void OnDie()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
    
    protected override void OnUpdate()
    {
        State state = State.IDLE;
        
        bool isGrounded = IsGrounded();
        bool isInWater = IsInWater();

        if (IsInDialog())
            state = State.IDLE;
        else if (isInWater)
            state = State.SWIM;
        else if (isGrounded)
        {
            if (Input.GetAxis("Horizontal") + forceMove == 0)
                state = State.IDLE;
            else
                state = State.RUN;
        }
        else
        {
            state = State.JUMP;
        }

        // if (state == State.RUN)
        // {
        //     if (!_particleSystem.isPlaying)
        //     {
        //         _particleSystem.Play(); // Play the particles if they are not already playing.
        //     }
        // }
        // else
        // {
        //     if (_particleSystem.isPlaying)
        //     {
        //         _particleSystem.Stop(); // Stop the particles if they are playing and the state is not RUN.
        //     }
        // }
        
        UpdateState(state);

        if (!IsInDialog())
        {
            MoveHorizontal(forceMove == 0f ? Input.GetAxis("Horizontal") : forceMove);

            if (Input.GetKeyDown(KeyCode.Space))
                ControlJump();
        
            if(Input.GetKeyDown(KeyCode.F))
                ControlDash();
        }
    }

    public bool IsInDialog()
    {
        return DialogsSystem.instance.IsDialogOpened();
    }
    
    public void ControlJump()
    {
        if (IsInDialog()) return;
        
        if(IsInWater())
            ImpulsedSwimUp();
        else if(IsGrounded())
            Jump();
    }

    public void ControlRight()
    {
        if (IsInDialog()) return;
        
        forceMove = 1f;
    }
    
    public void ControlLeft()
    {
        if (IsInDialog()) return;
        
        forceMove = -1f;
    }
    
    public void ControlMovementRelease()
    {
        if (IsInDialog()) return;
        
        forceMove = 0f;
    }
    
    public void ControlDash()
    {
        if (IsInDialog()) return;
        
        Dash((isTurnRight ? Vector2.right : Vector2.left));
    }
}
