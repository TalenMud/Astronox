using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 0.5f;
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private bool isWaiting = false; // Add waiting flag

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform;
    }

    void Update()
    {
        if (!isWaiting) // Only move if not waiting
        {
            MoveTowardsWaypoint();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D borderCollider)
{
    if (borderCollider.gameObject.CompareTag("EnemyBorder"))
    {
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(WaitAtWaypoint());
        
    }
}
    void MoveTowardsWaypoint()
    {
        Vector2 direction = currentPoint.position - transform.position;
        rb.linearVelocity = direction.normalized * speed; // Use velocity for movement
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
}