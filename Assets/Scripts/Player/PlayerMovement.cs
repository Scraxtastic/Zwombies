using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float acceleration = 70f;
    [SerializeField] private float maxSpeedX = 13f;
    [SerializeField] private float maxFallspeed = 25f;
    [SerializeField] private float keepSpeedValue = 0.99f;
    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float ledgeDetectionHeight = 0.2f;

    [Header("Jump")] [SerializeField] private float jumpForce = 35;
    [SerializeField] public bool hasJump = false;
    [SerializeField] public bool hasDoubleJump = false;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private bool isContinousJump = false;
    [SerializeField] private float jumpBufferTime = 0.02f;

    [Tooltip("Time after leaving ground where you can still jump")] [SerializeField]
    private float coyoteTime = 0.1f;

    [Header("Dash")] [SerializeField] public bool hasDash = false;
    [SerializeField] private float dashDistance = 5;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1f;

    [Header("Dash Denies")] [SerializeField]
    private bool dashDeniesGravity = true;

    [SerializeField] private bool dashDeniesJump = true;
    [SerializeField] private bool dashDeniesDoubleJump = true;
    [SerializeField] private bool dashDeniesYVelocity = true;
    [SerializeField] private bool dashDeniesXVelocity = true;
    [SerializeField] private bool dashDeniesXAcceleration = true;

    [Header("Animations")] [SerializeField]
    private Animator animator;

    [Header("DEBUG")]
    // Input variables
    private PlayerController playerControls;

    private PlayerController.MovementActions movement;

    // other variables
    private Rigidbody2D rb;
    private BoxCollider2D groundCheckCollider;
    private SpriteRenderer sprite;

    // Jump variables
    [SerializeField] private bool isGrounded = false;
    private bool isJumping = false;
    [SerializeField] private bool usedDoubleJump = false;
    [SerializeField] private float lastJumpTriggeredTime = 0;
    [SerializeField] private bool hasInfinityJumps = false;

    [SerializeField] private float lastCoyoteJumpTriggerTime = 0;

    // Dash variables
    private bool isDashing = false;
    private float lastDashTime = 0;
    private float dashStartTime = 0;
    private bool dashLeft = true;

    // For friction calculation
    private float keepSpeed = 1;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        groundCheckCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        playerControls = new PlayerController();
        playerControls.Enable();
        movement = playerControls.Movement;
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        CorrectMovementForLedge();
    }

    private void Update()
    {
        CheckGrounded();
        AddMoveSpeed();
        LimitSpeed();

        CheckJump();

        if (rb.velocity.y <= 0)
        {
            isJumping = false;
        }

        CheckDash();
        Dash();
    }

    private void AddMoveSpeed()
    {
        float leftValue = movement.Left.ReadValue<float>();
        float rightValue = movement.Right.ReadValue<float>();
        Vector2 speedVec = new Vector2();
        speedVec.x -= acceleration * leftValue;
        speedVec.x += acceleration * rightValue;
        if (speedVec.x != 0)
        {
            keepSpeed = 1;
        }
        else
        {
            keepSpeed *= keepSpeedValue;
            if (keepSpeed < 0.01f)
            {
                keepSpeed = 0;
            }
        }

        Vector2 vel = rb.velocity;
        vel.x *= keepSpeed;
        //if objects can move player, the dash shouldn't set the x velocity (might cause problems)
        if (!isDashing && dashDeniesXAcceleration)
        {
            rb.velocity = vel + speedVec;
        }

        if (speedVec.x < 0)
        {
            sprite.flipX = true;
        }
        else if (speedVec.x > 0)
        {
            sprite.flipX = false;
        }
    }

    private void CorrectMovementForLedge()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.2f)
        {
            return;
        }

        //Ledge detection
        Vector2 direction = sprite.flipX ? Vector2.left : Vector2.right;
        Vector2 size = groundCheckCollider.size;
        size.y = ledgeDetectionHeight;
        //Set the maxDistance to position bottom + bit of delta (like 0.2f or less)
        float maxDistance = 0.02f;
        Vector3 localScale = transform.localScale;
        Vector3 startPosition = groundCheckCollider.offset;
        startPosition.x *= localScale.x;
        startPosition.y *= localScale.y;
        startPosition.y -= groundCheckCollider.size.y / 2 - ledgeDetectionHeight / 2;
        startPosition += transform.position;

        RaycastHit2D hit =
            Physics2D.BoxCast(startPosition, size, transform.eulerAngles.z, direction,
                maxDistance, groundLayers);
        // Debug.DrawRay(startPosition, direction, Color.blue);
        if (hit.collider == null)
        {
            return;
        }

        float delta = 0.00f;
        Vector3 raycastPosition = startPosition;
        raycastPosition.y += ledgeDetectionHeight / 2;
        raycastPosition.x += (size.x / 2 + maxDistance) * direction.x;

        RaycastHit2D hitUp = Physics2D.Raycast(raycastPosition, Vector2.up, 0.2f + delta, groundLayers);
        if (hitUp.collider != null)
        {
            return;
        }

        RaycastHit2D hiDown = Physics2D.Raycast(raycastPosition, Vector2.down, 0.2f + delta, groundLayers);

        // Debug.DrawRay(raycastPosition, Vector2.down * (0.02f + delta), Color.yellow);
        if (hiDown.collider == null)
        {
            return;
        }


        Vector3 newPos = transform.position;
        newPos.y = hiDown.point.y + (groundCheckCollider.size.y * transform.localScale.y / 2) + 0.02f;
        transform.position = newPos;
    }

    private void LimitSpeed()
    {
        if (rb.velocity.x > maxSpeedX)
        {
            rb.velocity = new Vector2(maxSpeedX, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeedX)
        {
            rb.velocity = new Vector2(-maxSpeedX, rb.velocity.y);
        }

        if (rb.velocity.y < -maxFallspeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxFallspeed);
        }

        // animator.SetFloat(0, Mathf.Abs(rb.velocity.x));
        animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        // animator.SetFloat("SpeedY", rb.velocity.y);
        // animator.SetBool("IsJumping", isJumping);
    }

    private void CheckJump()
    {
        if (isDashing && dashDeniesJump) return;
        if (!hasJump) return;


        // Use Jump Buffering
        if (isGrounded)
        {
            if (Time.time < lastJumpTriggeredTime + jumpBufferTime)
            {
                Jump();
            }
        }

        if (!isContinousJump && !movement.Jump.triggered) return;
        if (isContinousJump && movement.Jump.ReadValue<float>() < 0.5f) return;
        lastJumpTriggeredTime = Time.time;
        if (hasInfinityJumps)
        {
            Jump();
            return;
        }

        if (!isJumping && Time.time < lastCoyoteJumpTriggerTime + coyoteTime)
        {
            Jump();
        }

        if (isGrounded)
        {
            usedDoubleJump = false;
            if (isJumping) return;
            Jump();
        }
        else
        {
            CheckDoubleJump();
        }
    }

    private void CheckDoubleJump()
    {
        if (!hasDoubleJump) return;
        if (usedDoubleJump) return;
        if (isDashing && dashDeniesDoubleJump) return;
        if (!movement.Jump.triggered) return;
        usedDoubleJump = true;
        Jump();
    }

    private void Jump()
    {
        rb.gravityScale = gravityScale;
        rb.velocity = new Vector3(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        isJumping = true;
    }

    private void CheckDash()
    {
        if (!hasDash) return;
        if (isDashing) return;
        if (Time.time < lastDashTime + dashCooldown) return;
        if (!movement.Dash.triggered) return;
        if (movement.Dash.ReadValue<float>() < 0.5f) return;
        StartDash();
    }

    private void StartDash()
    {
        isDashing = true;
        dashStartTime = Time.time;
        dashLeft = sprite.flipX;
        if (dashDeniesGravity)
        {
            rb.gravityScale = 0;
        }

        if (dashDeniesYVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (dashDeniesXVelocity)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            return;
        }

        if (Time.time > dashStartTime + dashDuration)
        {
            rb.gravityScale = gravityScale;
            isDashing = false;
            return;
        }

        float dashSpeed = dashDistance / dashDuration;
        Vector2 speed = new Vector3();
        if (dashLeft)
        {
            speed.x -= dashSpeed;
        }
        else
        {
            speed.x += dashSpeed;
        }

        rb.velocity += speed;
        lastDashTime = Time.time;
    }

    private void CheckGrounded()
    {
        Vector2 size = groundCheckCollider.size;
        Vector2 direction = Vector2.down;
        //Set the maxDistance to position bottom + bit of delta (like 0.2f or less)
        float maxDistance = 0.2f;
        Vector3 localScale = transform.localScale;
        Vector3 startPosition = groundCheckCollider.offset;
        startPosition.x *= localScale.x;
        startPosition.y *= localScale.y;
        startPosition += transform.position;

        RaycastHit2D hit =
            Physics2D.BoxCast(startPosition, size, transform.eulerAngles.z, direction,
                maxDistance, groundLayers);
        // If it hits nothing
        if (hit.collider == null)
        {
            if (isGrounded)
            {
                lastCoyoteJumpTriggerTime = Time.time;
            }

            isGrounded = false;
            return;
        }

        // float distance = Mathf.Sqrt(Mathf.Pow(hit.point.y - transform.position.y, 2) +
        //                             Mathf.Pow(hit.point.x - transform.position.x, 2));

        // Debug.Log("BoxCast" + " " + this.name + " " + distance);
        isGrounded = true;
    }

    private void Log(string message)
    {
        Debug.Log(message);
    }
}