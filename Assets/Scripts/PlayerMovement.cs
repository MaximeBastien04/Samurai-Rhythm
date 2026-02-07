using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    private PlayerControls controls;
    private Vector2 move;
    private Rigidbody2D rb;
    public bool canMove = true;
    public float moveSpeed = 5f;
    public float runSpeed = 5f;
    public float jumpHeight = 5f;
    private bool isGrounded = false;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0f;
    public int maxJumps = 2;
    private int remainingJumps;


    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundedCheck();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    void OnEnable() => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();

    private void Move()
    {
        if (move.x != 0)
        {
            rb.linearVelocity = new Vector2(move.x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }
    }

    private void Jump()
    {
        Debug.Log("Jump called");
        if (coyoteTimeCounter > 0f || remainingJumps > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight * 10); ;
            coyoteTimeCounter = 0f;

            if (!isGrounded)
            {
                remainingJumps--;
            }
            else
            {
                // We jumped from the ground, so reset remainingJumps - 1 here to prevent extra jumps
                remainingJumps = maxJumps - 1;
            }
        }
    }
    
    void GroundedCheck()
    {
        Debug.Log(isGrounded);
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.6f;
        bool wasGrounded = isGrounded;
        float rayLength = 0.05f;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength);
        isGrounded = hit.collider != null;

        if (isGrounded && !wasGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            remainingJumps = maxJumps;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
}
