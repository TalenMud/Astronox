using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlyingEnem : MonoBehaviour
{
     public float flyingHeight = 3f;
    public float swoopSpeed = 5f;
    public float returnSpeed = 3f;
    public float swoopInterval = 5f;
    public float swoopDuration = 2f;
    public LayerMask groundLayer;
    public string requiredQuestID = "Q3P1"; 

    public Inventory inventory;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float swoopTimer;
    private float durationTimer;
    private bool isSwooping = false;

    [Header("Health System")]
    public float maxHealth = 3f; 
    private float currentHealth;
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    private Image healthFillImage;
    private float healthBarShowDuration = 3f;
    private float healthBarTimer;
    private bool isHealthBarVisible = false;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    player = GameObject.FindGameObjectWithTag("Player").transform;

    // Find the spawner.
    GameObject spawner = GameObject.Find("FlyingEnemySpawner"); // Replace with your spawner's name

    if (spawner != null)
    {
        // Set the enemy's position to the spawner's position.
        transform.position = spawner.transform.position;
        // find the ground below the enemy.
        RaycastHit2D flyingenemyfloor = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);
        if (flyingenemyfloor.collider != null)
        {
            startPosition = new Vector2(transform.position.x, flyingenemyfloor.point.y + flyingHeight);
            transform.position = startPosition;
        }

    }
    else
    {
        Debug.LogError("FlyingEnemySpawner not found!");
    }

    swoopTimer = swoopInterval;

    currentHealth = maxHealth;
    healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
    healthBarInstance.transform.SetParent(transform);
    healthBarInstance.transform.localPosition = new Vector3(0, 1f, 0);
    healthFillImage = healthBarInstance.transform.Find("Background/Fill").GetComponent<Image>();
    healthBarInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    healthBarInstance.SetActive(false);
}

    void Update()
    {
        if (inventory.CheckIfHasItem("SpaceGun")) 
        {
            swoopTimer -= Time.deltaTime;

            if (swoopTimer <= 0 && !isSwooping)
            {
                StartSwoop();
            }

            if (isSwooping)
            {
                durationTimer -= Time.deltaTime;
                if (durationTimer <= 0)
                {
                    ReturnToHeight();
                }
            }
        }
        if (isHealthBarVisible)
        {
            healthBarTimer -= Time.deltaTime;
            if (healthBarTimer <= 0)
            {
                HideHealthBar();
            }
        }
    }

    void StartSwoop()
    {
        isSwooping = true;
        durationTimer = swoopDuration;
    }

    void ReturnToHeight()
    {
        isSwooping = false;
        swoopTimer = swoopInterval;
    }

    void FixedUpdate()
    {
        if (isSwooping)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * swoopSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.up * returnSpeed;
            if (transform.position.y >= startPosition.y)
            {
                rb.linearVelocity = Vector2.zero;
                transform.position = startPosition;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        ShowHealthBar();
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ShowHealthBar()
    {
        isHealthBarVisible = true;
        healthBarTimer = healthBarShowDuration;
        healthBarInstance.SetActive(true);
    }

    void HideHealthBar()
    {
        isHealthBarVisible = false;
        healthBarInstance.SetActive(false);
    }

    void UpdateHealthBar()
    {
        healthFillImage.fillAmount = currentHealth / maxHealth;
    }

    private void OnDestroy()
    {
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }
    }
}
