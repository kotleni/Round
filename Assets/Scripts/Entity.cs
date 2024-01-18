using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum State
    {
        IDLE, RUN, JUMP, SWIM    
    }

    [SerializeField] private bool _isLockRotation;
    
    private float _maxSpeed = 3.0f;
    private float _jumpSpeed = 5.0f;
    private float _swimSpeed = 4.0f;
    private float _maxHealth = 1f;
    private float _health = 1f;

    private bool _isAllowAirJump = false;

    private float lastJumpTime = 0f;
    private float jumpDelay = 1.0f;
    
    private float lastDashTime = 0f;
    private float dashDelay = 1.5f;
    private float dashImpulse = 10.0f;

    private Vector2 initialScale;

    private bool _isDrawHealthBar = true;
    private bool _isDrawXFlipped = false;
    
    public bool isTurnRight = true;
    
    private State currentState = State.IDLE;
    private Animator _animator;

    public Rigidbody2D _rigidbody;
    
    protected abstract void OnInit();
    protected abstract void OnUpdate();
    protected abstract void OnDie();
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
        OnInit();

        _rigidbody.freezeRotation = _isLockRotation;
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnGUI()
    {
        if(!_isDrawHealthBar) return;
        
        // Draw health bar
        
        Vector3 worldPosition = transform.position;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        
        float barWidth = 50f;
        float barHeight = 4f;
        float xPos = screenPosition.x - barWidth / 2;
        float yPos = Screen.height - screenPosition.y - 20;

        float healthBarFill = Mathf.Clamp01(_health / _maxHealth);

        GUI.color = new Color(0.5f, 0.3f, 0.3f);
        GUI.DrawTexture(new Rect(xPos, yPos, barWidth, barHeight), Texture2D.whiteTexture, ScaleMode.StretchToFill, false);

        GUI.color = new Color(1f, 0.5f, 0.5f);
        GUI.DrawTexture(new Rect(xPos, yPos, barWidth * healthBarFill, barHeight), Texture2D.whiteTexture, ScaleMode.StretchToFill, false);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         Enemy enemy = other.gameObject.GetComponent<Enemy>();
    //         if(enemy == null) return;

    //         float damage = 25f;
    //         float damageVelocity = 6.1f;
    //         if (_rigidbody.velocity.magnitude >= damageVelocity)
    //         {
    //             enemy.Hit(damage, gameObject);
    //         }
    //     }
    // }

    private bool CheckIsDamageAnimation()
    {
        return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerHit";
    }

    private void UpdateAnimation(State state)
    {
        if (state == currentState && !CheckIsDamageAnimation())
            return;

        _animator.ResetTrigger("Idle");
        _animator.ResetTrigger("Run");
        _animator.ResetTrigger("Jump");
        _animator.ResetTrigger("Hit");
        _animator.ResetTrigger("WallJump");

        currentState = state;
        switch (state)
        {
            case State.IDLE:
                _animator.SetTrigger("Idle");
                break;
            case State.RUN:
                _animator.SetTrigger("Run");
                break;
            case State.JUMP:
                _animator.SetTrigger("Jump");
                break;
            case State.SWIM:
                _animator.SetTrigger("Run");
                break;
        }
    }

    public void UpdateState(State state)
    {
        UpdateAnimation(state);
    }

    public void Hit(float damage, GameObject from = null)
    {
        SetHealth(GetHealth() - damage);
        Debug.Log($"Damage {damage} taken to {gameObject.name} from {from.name}. (Now HP is {GetHealth()})");
        
        if (_health <= 0f)
        {
            OnDie();
        }
        
        // Make impulse from enemy
        if (from != null)
        {
            _rigidbody.velocity += Vector2.up * 3.5f;
        }
        if(_animator != null)
            _animator.SetTrigger("Hit");
    }

    public void Dash(Vector2 to)
    {
        if (Time.time - lastDashTime > dashDelay)
        {
            _rigidbody.velocity += to * dashImpulse;
            lastDashTime = Time.time;
        }
    }
    
    public void SetMaxHealth(float value)
    {
        _maxHealth = value;
        _health = _maxHealth;
    }
    
    public void SetMaxSpeed(float value)
    {
        _maxSpeed = value;
    }

    public void SetJumpSpeed(float value)
    {
        _jumpSpeed = value;
    }

    public void SetSwimSpeed(float value)
    {
        _swimSpeed = value;
    }

    public void SetHealthBarVisibility(bool isVisible)
    {
        _isDrawHealthBar = isVisible;
    }

    public void SetInvertedXFlipping(bool isFlipped)
    {
        _isDrawXFlipped = isFlipped;
    }

    public void SetAllowAirJump(bool isAllow) {
        _isAllowAirJump = isAllow;
    }

    public void SetJumpDelay(float delay) {
        jumpDelay = delay;
    }

    public void SetHealth(float value)
    {
        _health = Math.Min(value, _maxHealth);
    }

    public void AddHealth(float value)
    {
        SetHealth(GetHealth() + value);
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void MoveHorizontal(float value)
    {
        var moveX = value * _maxSpeed * Time.deltaTime;
        transform.position += new Vector3(moveX, 0f, 0f);
        
        if (value > 0)
        {
            transform.localScale = new Vector3(_isDrawXFlipped ? -initialScale.x : initialScale.x, initialScale.y, 0f);
            isTurnRight = true;
        }

        if (value < 0)
        {
            transform.localScale = new Vector3(_isDrawXFlipped ? initialScale.x : -initialScale.x, initialScale.y, 0f);
            isTurnRight = false;
        }
    }

    public void SwimUp()
    {
        _rigidbody.velocity += new Vector2(0f, _swimSpeed * Time.deltaTime);
    }
    
    public void ImpulsedSwimUp()
    {
        _rigidbody.velocity += new Vector2(0f, _swimSpeed * (Time.deltaTime + 0.5f));
    }

    public void SwimDown()
    {
        _rigidbody.velocity += new Vector2(0f, -_swimSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if ((IsGrounded() || _isAllowAirJump) && Time.time - lastJumpTime > jumpDelay)
        {
            _rigidbody.velocity += new Vector2(0f, _jumpSpeed);
            lastJumpTime = Time.time;
        }
    }
    
    public bool IsInWater()
    {
        float rayLength = transform.localScale.y / 5.7f;
        Vector2 rayOrigin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, LayerMask.NameToLayer("Entity"));
        return hit.collider != null && hit.transform.gameObject.name.Contains("Water");
    }

    public bool IsGrounded()
    {
        Vector2 boxCenter = transform.position;
        float boxWidth = transform.localScale.x / 4.4f; // Adjust this value based on your needs.
        float boxHeight = 0.02f; // Adjust this value based on the size of the ground contact area.

        // You can set the origin of the box to the character's feet or adjust it as needed.
        Vector2 boxOrigin = new Vector2(boxCenter.x, boxCenter.y - transform.localScale.y / 6.0f);

        // Perform an overlap check with the ground layer.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxOrigin, new Vector2(boxWidth, boxHeight), 0, LayerMask.NameToLayer("Entity"));

        // If there's at least one collider in the array, the character is grounded.
        return colliders.Length > 0;
    }
}