using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ShootingRangeGame : MonoBehaviour
{
    public static ShootingRangeGame instance;
    public GameObject targetPrefab;
    public float spawnInterval = 2f;
    public float targetSpeed = 300f;
    public RectTransform leftSpawnPoint;
    public RectTransform rightSpawnPoint;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float spawnTimer;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        spawnTimer = spawnInterval;
        UpdateScoreText();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnTarget();
            spawnTimer = spawnInterval;
        }

        if (Input.GetMouseButtonDown(0))
        {
            CheckShot();
        }
    }

    void SpawnTarget()
    {
        bool spawnLeft = Random.value > 0.5f;
        RectTransform spawnPoint = spawnLeft ? leftSpawnPoint : rightSpawnPoint;
        
        GameObject target = Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity, transform);
        RectTransform targetRect = target.GetComponent<RectTransform>();
        targetRect.SetParent(transform);
        
        float direction = spawnLeft ? 1 : -1;
        target.GetComponent<ShootingTarget>().Initialize(direction * targetSpeed);
    }

    void CheckShot()
    {
        // Get the current pointer event data
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        // Create a list to store hit results
        List<RaycastResult> results = new List<RaycastResult>();
        
        // Raycast using the UI event system
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Target"))
            {
                score += 2;
                UpdateScoreText();
                Destroy(result.gameObject);
                break;
            }
        }
    }

    public void LosePoint()
    {
        score -= 1;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Planet_2");
    }
} 