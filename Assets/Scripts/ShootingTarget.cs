using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    private float speed;
    private RectTransform rectTransform;

    public void Initialize(float moveSpeed)
    {
        speed = moveSpeed;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
        
        // Check if target has left the screen
        if (Mathf.Abs(rectTransform.anchoredPosition.x) > 1000) // Adjust based on your canvas size
        {
            ShootingRangeGame.instance.LosePoint();
            Destroy(gameObject);
        }
    }
} 