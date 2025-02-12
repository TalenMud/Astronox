using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour

{
     
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 7f;
    public bool isFacingRight = true;
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

        if (Input.GetMouseButton(0)) // Left-click
    {
        TryMineOre();
    }
    }

    void TryMineOre()
    {
        // Get the mouse position in screen space
        Vector2 mousePosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Cast a ray from the mouse position to the world
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        // If we hit something, check if it's an ore
        if (hit.collider != null && hit.collider.CompareTag("Ore"))
        {
            Ore ore = hit.collider.GetComponent<Ore>();
            if (ore != null)
            {
                ore.StartMining();  // Start mining the ore if the conditions are met
            }
        }

        // Visualize the ray for debugging purposes (optional)
        Debug.DrawRay(worldPosition, Vector2.zero, Color.red, 0.1f);
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
        if (other.gameObject.CompareTag("Rocket") && QuestManager.instance.AllQuestsPlanet1Done())
        {
              SceneManager.LoadScene("Planet_2");
        }
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
            transform.Rotate(0f, 180f, 0f);
           
        }
    }
}