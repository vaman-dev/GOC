using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] private Transform target;

    [Header("Offset and Smoothness")]
    [SerializeField] private Vector3 offset = new Vector3(3f, 1f, -10f);  // Slightly ahead and above the player
    [SerializeField] private float smoothSpeed = 0.25f;  // Smooth trailing follow

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate the desired camera position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate to that position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the final camera position
        transform.position = smoothedPosition;
    }
}
