using UnityEngine;

public class SpriteMapClickHandler : MonoBehaviour
{
    public SpriteRenderer mapRenderer;           // Assign MAP_0 in Inspector
    public MapBoundaries boundaries;             // Lat/Long boundaries
    public GameObject pinPrefab;                 // Assign Pin prefab
    public ShowCoordinates showCoordinates;      // Assign UI handler script in Inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Mathf.Abs(Camera.main.transform.position.z)
            ));

            Vector2 mapPos = new Vector2(worldPos.x, worldPos.y);

            if (mapRenderer.bounds.Contains(worldPos))
            {
                Vector2 spriteSizeWorld = mapRenderer.bounds.size;
                Vector2 normalized = WorldToNormalized(mapPos, spriteSizeWorld);
                Vector2 latLong = NormalizedToLatLong(normalized);

                Debug.Log($"Latitude: {latLong.x:F4}, Longitude: {latLong.y:F4}");

                // Drop pin
                Instantiate(pinPrefab, new Vector3(mapPos.x, mapPos.y, 0), Quaternion.identity);

                // Show coordinates on UI
                if (showCoordinates != null)
                {
                    showCoordinates.UpdateCoordinates(latLong.x, latLong.y);
                }
            }
            else
            {
                Debug.Log("Click was outside the map.");
            }
        }
    }

    Vector2 WorldToNormalized(Vector2 pos, Vector2 spriteSize)
    {
        Vector2 bottomLeft = mapRenderer.bounds.min;
        float normalizedX = (pos.x - bottomLeft.x) / spriteSize.x;
        float normalizedY = (pos.y - bottomLeft.y) / spriteSize.y;
        return new Vector2(normalizedX, normalizedY);
    }

    Vector2 NormalizedToLatLong(Vector2 norm)
    {
        float longitude = Mathf.Lerp(boundaries.leftLongitude, boundaries.rightLongitude, norm.x);
        float latitude = Mathf.Lerp(boundaries.bottomLatitude, boundaries.topLatitude, norm.y);
        return new Vector2(latitude, longitude);
    }
}
