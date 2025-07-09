using TMPro;
using UnityEngine;

public class ShowCoordinates : MonoBehaviour
{
    [Header("UI Text Reference")]
    public TextMeshProUGUI coordinatesText;

    // Public method to update the displayed coordinates
    public void UpdateCoordinates(float latitude, float longitude)
    {
        coordinatesText.text = $"Lat: {latitude:F4}\nLong: {longitude:F4}";
    }
}
