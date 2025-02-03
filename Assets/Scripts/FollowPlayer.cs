using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;      // The player's transform (drag and drop the player object here in the Inspector)
    public Vector2 offset;       // Offset from the player (e.g., how far behind the player the camera should be)
    public float smoothSpeed = 0.125f; // How quickly the camera catches up with the player (smoothing factor)

    void LateUpdate()
    {
        // Calculate the desired position (player position + offset)
        Vector2 desiredPosition = player.position + (Vector3)offset; // Offset must be a Vector3 for this addition

        // Smoothly move the camera towards the desired position
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position (we keep the Z-axis fixed to the original camera Z)
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
