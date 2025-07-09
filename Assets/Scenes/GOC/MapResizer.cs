using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MapResizer : MonoBehaviour
{
    void Start()
    {
        FitToScreen();
    }
    
    void Update()
    {
        // Optional: Call FitToScreen in Update if you want to adjust dynamically
        FitToScreen();
    }

    void FitToScreen()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        // Get the world size of the camera view
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // Get sprite size
        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        // Calculate scale to fit
        Vector3 scale = transform.localScale;
        scale.x = screenWidth / spriteWidth;
        scale.y = screenHeight / spriteHeight;

        transform.localScale = scale;
    }
}
