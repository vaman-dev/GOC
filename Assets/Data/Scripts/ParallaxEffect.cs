using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0.5f;
    [SerializeField] private float smoothTime = 0.3f; // Controls smoothness

    private Transform cam;
    private Vector3 previousCamPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private float initialY; // To lock Y position

    private void Start()
    {
        cam = Camera.main.transform;
        previousCamPosition = cam.position;
        targetPosition = transform.position;
        initialY = transform.position.y; // Save the Y position at start
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - previousCamPosition;

        // Only update X-axis for parallax
        targetPosition += new Vector3(
            deltaMovement.x * parallaxMultiplier,
            0f, // Y locked
            0f
        );

        // Lock Y and Z
        Vector3 desiredPosition = new Vector3(targetPosition.x, initialY, transform.position.z);

        // Smooth horizontal movement
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );

        previousCamPosition = cam.position;
    }
}
