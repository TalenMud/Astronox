using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
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

        if (Vector2.Distance(transform.position, currentPoint.position) < 1f) // Check close enough
        {
            StartCoroutine(WaitAtWaypoint()); // Start waiting
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
        yield return new WaitForSeconds(2f); // Wait time (adjust as needed)
        isWaiting = false;

        // Switch waypoints
        if (currentPoint == PointB.transform)
        {
            currentPoint = PointA.transform;
        }
        else
        {
            currentPoint = PointB.transform;
        }
    }

    private void OnDrawGizmos()
    {
        if (PointA != null && PointB != null) // Check if points are assigned
        {
            Gizmos.DrawWireSphere(PointA.transform.position, 0.5f); // Smaller gizmos
            Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
            Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
        }
    }
}