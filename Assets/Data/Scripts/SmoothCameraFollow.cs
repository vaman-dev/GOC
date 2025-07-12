using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] private Transform target;

    [Header("Offset and Smoothness")]
    [SerializeField] private Vector3 offset = new Vector3(3f, 1f, -10f);
    [SerializeField] private float smoothSpeed = 0.15f;

    private Vector3 velocity = Vector3.zero;
    private float fixedY; // Lock Y axis

    private void Start()
    {
        // Save the Y position at the start to lock it
        fixedY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Only follow X and Z, keep Y fixed
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            fixedY, // Lock Y
            target.position.z + offset.z
        );

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
