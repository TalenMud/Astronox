using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed = 2f;
    public float waitTime = 1f;

    private Vector3 targetPos;
    private bool isWaiting = false;

    void Start()
    {
        transform.position = pointA.transform.position;
        targetPos = pointB.transform.position;
    }

    void Update()
    {
        if (!isWaiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                StartCoroutine(WaitAndChangeDirection());
            }
        }
    }

    IEnumerator WaitAndChangeDirection()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        targetPos = (targetPos == pointA.transform.position) ? pointB.transform.position : pointA.transform.position;
        isWaiting = false;
    }

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }
    }
}
