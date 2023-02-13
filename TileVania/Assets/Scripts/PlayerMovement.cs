using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D rigidBody;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    ///feet collider
    BoxCollider2D boxCollider;
    [SerializeField]
    float jumpSpeed = 5f;
    [SerializeField]
    float climbSpeed = 5f;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    private float _initialGravity;
    private bool _isAlive = true;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    bool PlayerHasHorizontalSpeed => Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon; //epsilon is near zero but not zero.
    bool PlayerHasVerticalSpeed => Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        _initialGravity = rigidBody.gravityScale;
    }

    void Update()
    {
        if (!_isAlive)
            return;

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void ClimbLadder()
    {
        rigidBody.gravityScale = _initialGravity;
        animator.SetBool("isClimbing", false);
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            return;


        animator.SetBool("isClimbing", PlayerHasVerticalSpeed);
        //dont slide down
        rigidBody.gravityScale = 0;

        Vector2 climbVelocity = new Vector2(rigidBody.velocity.x, moveInput.y * climbSpeed);
        rigidBody.velocity = climbVelocity;
    }

    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        animator.SetBool("isRunning", PlayerHasHorizontalSpeed);
    }

    private void Die()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            _isAlive = false;
            animator.SetTrigger("Dying");
            rigidBody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue value)
    {
        if (!_isAlive)
            return;

        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!_isAlive)
            return;

        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {

        if (!_isAlive)
            return;

        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

        if (value.isPressed)
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
    }
}
