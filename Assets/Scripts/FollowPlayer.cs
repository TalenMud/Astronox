using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;     
    public Vector2 offset;       
    public float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
    
        Vector2 desiredPosition = player.position + (Vector3)offset; 

       
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);

       
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
