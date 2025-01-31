using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 7f;
    private bool isFacingRight = true;
    private bool isInWater = false;
    [SerializeField] private LayerMask waterLayer;  // LayerMask to identify water areas
    [SerializeField] private float sinkSpeed = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

                if (isInWater)
        {
            SinkInWater();
        }
    }

    private void SinkInWater()
    {
        if (rb.linearVelocity.y > -2f)  // Prevent the player from sinking too fast
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - sinkSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (waterLayer == (waterLayer | (1 << other.gameObject.layer)))  // Check if the player enters water
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (waterLayer == (waterLayer | (1 << other.gameObject.layer)))  // Check if the player exits water
        {
            isInWater = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}