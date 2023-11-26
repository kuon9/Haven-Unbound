using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask Ground;
    public Transform GroundCheck;
    public Rigidbody2D rb;
    public float moveSpeed = 5.0f;
    public float jumpForce;
    public bool isGrounded;
    

    Vector2 moveDirection = Vector2.zero;

    public PlayerBaseInputs playerControls;
    private InputAction move;
    private InputAction jump;
    private InputAction dash;

    public bool canDash;
    public bool canMove;
    public bool canDoubleJump;
    public bool isDashing;
    public bool isJumping;
    public float dashSpeed, dashTime, dashCooldown;
    public float gravityScale = 1f;
    public float fallGravityMultiplier = 5f;
    AbilityTracker abilityTracker;


    
    private void Awake()
    {
        playerControls = new PlayerBaseInputs();
        rb = GetComponent<Rigidbody2D>();
        abilityTracker = GetComponent<AbilityTracker>();
        canMove = true;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        dash = playerControls.Player.Dash;
        jump = playerControls.Player.Jump;

        jump.Enable();
        move.Enable();
        dash.Enable();

        dash.performed += Dash;
        jump.performed +=Jump;
    }
    private void OnDisable()
    {
        move.Disable();
        dash.Disable();
    }
    private void Update()
    {
        if(isDashing || !canMove) {return;}
        {
            moveDirection = move.ReadValue<Vector2>();
            FlipDirection();   
        }
    }    

    private void FixedUpdate()
    {
        if(isDashing) {return;}
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, .5f, Ground);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(!canMove) { return;}
        if(context.started &&(isGrounded || (canDoubleJump && abilityTracker.canDoubleJump)))
        {
            if(isGrounded)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;
            }
        rb.velocity = Vector2.up * jumpForce;
        isJumping = true;            
        }
    }

    public void Falling(InputAction.CallbackContext context)
    {
        if(context.started && isJumping)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else if (isGrounded)
        {
            isJumping = false;
            rb.gravityScale = gravityScale;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && canDash && abilityTracker.canDash)
        {
            StartCoroutine(Dashing());
        }
    }
    IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;  
    }
        void FlipDirection()
    {
        if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
        else if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }
}