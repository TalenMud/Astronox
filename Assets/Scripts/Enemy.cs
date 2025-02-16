using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int startinghealth = 4;
    private int enemyHealth;
    public float speed = 0.5f;
    public float chaseSpeed = 1f;
    public float detectionRange = 5f; 
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private Transform player;
    public string EnemyQuestID;
    public int rewardPoints;
    private bool isWaiting = false; 
    private bool isChasing = false;

    [Header("Health System")]
    public float maxHealth = 5f;
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
        currentPoint = PointB.transform;
        enemyHealth = startinghealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        // Create health bar but keep it hidden initially
        healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBarInstance.transform.SetParent(transform); // Parent to enemy
        healthBarInstance.transform.localPosition = new Vector3(0, 1f, 0); // Position above enemy
        healthFillImage = healthBarInstance.transform.Find("Background/Fill").GetComponent<Image>();
        healthBarInstance.SetActive(false);
    }

    void Update()
    {
        if (!isWaiting)
        {
            // Check if player is within detection range
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            if (distanceToPlayer < detectionRange)
            {
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                isChasing = false;
                MoveTowardsWaypoint();
            }
        }

        // Handle health bar visibility
        if (isHealthBarVisible)
        {
            healthBarTimer -= Time.deltaTime;
            if (healthBarTimer <= 0)
            {
                HideHealthBar();
            }
        }
    }

    bool IsPlayerBetweenPoints()
    {
        // Get the bounds of the patrol area
        float minX = Mathf.Min(PointA.transform.position.x, PointB.transform.position.x);
        float maxX = Mathf.Max(PointA.transform.position.x, PointB.transform.position.x);
        float minY = Mathf.Min(PointA.transform.position.y, PointB.transform.position.y);
        float maxY = Mathf.Max(PointA.transform.position.y, PointB.transform.position.y);

        // Check if player is within bounds
        return player.position.x >= minX && player.position.x <= maxX &&
               player.position.y >= minY && player.position.y <= maxY;
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * chaseSpeed;
    }

    void MoveTowardsWaypoint()
    {
        Vector2 direction = currentPoint.position - transform.position;
        rb.linearVelocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D borderCollider)
    {
        if (borderCollider.gameObject.CompareTag("EnemyBorder") && !isChasing)
        {
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(WaitAtWaypoint());
        }

        if (borderCollider.gameObject.CompareTag("gunLazer"))
        {
            TakeDamage(1f);
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        isWaiting = false;

        if (currentPoint == PointB.transform)
        {
            currentPoint = PointA.transform;
        }
        else if (currentPoint == PointA.transform) 
        {
            currentPoint = PointB.transform;
        }
    }

    private void OnDrawGizmos()
    {
        if (PointA != null && PointB != null) // Check if points are assigned
        {
            Gizmos.DrawWireSphere(PointA.transform.position, 1.2f); 
            Gizmos.DrawWireSphere(PointB.transform.position, 1.2f);
            Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        ShowHealthBar();
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            QuestManager.instance.UpdateQuestProgress(EnemyQuestID, rewardPoints);
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