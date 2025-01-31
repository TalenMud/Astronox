using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;      // The player's transform (drag and drop the player object here in the Inspector)
    public Vector3 offset;       // Offset from the player (e.g., how far behind the player the camera should be)
    public float smoothSpeed = 0.125f; // How quickly the camera catches up with the player (smoothing factor)

    void LateUpdate()
    {
        // Calculate the desired position (player position + offset)
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        //  keep the camera rotation fixed (remove if you want to allow rotation)
        transform.LookAt(player);
    }
}
