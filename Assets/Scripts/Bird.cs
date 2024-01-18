using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

class Bird: Entity {
    // IDEA: Birds can get off from player
    //[SerializeField] private float visionDistance = 12f;
    [SerializeField] private float movingSpeed = 2.0f;
    [SerializeField] private float jumpSpeed = 6.0f;
    [SerializeField] private float swimSpeed = 9.0f;
    [SerializeField] private float maxHealth = 45f;

    [SerializeField] private Sprite[] _flyAnimSprites;
    private int _currentFlyAnimSpriteIndex = 0;

    override protected void OnInit() {
        SetMaxSpeed(movingSpeed);
        SetJumpSpeed(jumpSpeed);
        SetSwimSpeed(swimSpeed);
        SetMaxHealth(maxHealth);
        SetJumpDelay(0f); // Self controlled
        SetAllowAirJump(true);
        SetHealthBarVisibility(false);
        SetInvertedXFlipping(true);

        ChangeDirection();
    }

    private float lastJumpTime;
    private float lastDirectionChangeTime;
    private float _lastFlySpriteChangeTime;
    private float direction = 0f;

    override protected void OnUpdate() {
        if(PlayerController.instance.GetHealth() <= 0) return;
        
        if(Time.time - lastJumpTime > 0.5f) {
            Jump();
            lastJumpTime = Time.time;
        }

        if(Time.time - lastDirectionChangeTime > 12f) {
            ChangeDirection();
            lastDirectionChangeTime = Time.time;
        }

        if(Time.time - _lastFlySpriteChangeTime > 0.25f) {
            NextFlySprite();
            _lastFlySpriteChangeTime = Time.time;
        }

        MoveHorizontal(direction);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Jump();
        // Inverse direction
        direction = direction > 0f ? -1f : 1f;
    }

    private void NextFlySprite() {
        _currentFlyAnimSpriteIndex++;
        if(_currentFlyAnimSpriteIndex >= _flyAnimSprites.Length)
            _currentFlyAnimSpriteIndex = 0;
        Sprite sprite = _flyAnimSprites[_currentFlyAnimSpriteIndex];
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void ChangeDirection() {
        direction = Random.Range(0, 2) == 0 ? 1f : -1f;
    }

    override protected void OnDie() {
        // IDEA: Maybe in future, i can just enable falling & don't remove it
        Destroy(gameObject);
    }
}