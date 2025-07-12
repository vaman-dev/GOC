using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] private Transform target;

    [Header("Offset and Smoothness")]
    [SerializeField] private Vector3 offset = new Vector3(3f, 1f, -10f);
    [SerializeField] private float smoothSpeed = 0.15f;  // Recommended

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
