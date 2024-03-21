using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player's transform

    [Space(5)]

    [Range(0, 5)]
    public float smoothSpeed = 5f; // How smoothly the camera catches up to its target

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate desired position
            Vector3 desiredPosition = player.position;
            // Smoothly interpolate between the camera's current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update the camera's position
            transform.position = smoothedPosition;

            // Ensure the camera's z value stays the same
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}