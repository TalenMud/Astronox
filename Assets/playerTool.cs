using UnityEngine;

public class playerTool : MonoBehaviour
{
public SpriteRenderer spriteRenderer;
public float rotationLimit = 180f;
public Transform Pivot;

    void Start()
    {
        
    }


    void Update()
    {
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = mousePosition - Pivot.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

    if (angle > 90 || angle < -55)
    {
        spriteRenderer.flipY = true;
    }
    else
    {
        spriteRenderer.flipY = false;
    }

    float limitedAngle = Mathf.Clamp(angle, -rotationLimit, rotationLimit); 

    Pivot.rotation = Quaternion.AngleAxis(limitedAngle, Vector3.forward);

    }
}
