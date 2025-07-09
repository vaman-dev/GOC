using UnityEngine;

public class PinDropper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Hide the sprite initially
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z); // Set depth from camera for 2D
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.position = new Vector3(worldPos.x, worldPos.y, 0f); // Z=0 for 2D
            spriteRenderer.enabled = true;

            Debug.Log("World Position of Sprite Pin: " + worldPos);
        }
    }
}
