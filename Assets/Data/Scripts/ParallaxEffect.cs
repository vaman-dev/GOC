using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0.5f;
    [SerializeField] private float smoothness = 5f; // Higher = smoother

    private Transform cam;
    private Vector3 previousCamPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        cam = Camera.main.transform;
        previousCamPosition = cam.position;
        targetPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - previousCamPosition;
        targetPosition += new Vector3(
            deltaMovement.x * parallaxMultiplier,
            deltaMovement.y * parallaxMultiplier,
            0f
        );

    
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);

        previousCamPosition = cam.position;
    }
}