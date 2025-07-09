using UnityEngine;

public class ClearAllPins : MonoBehaviour
{
    [Header("Pin Tag")]
    public string pinTag = "Pin"; // Make sure all instantiated pins have this tag

    // Call this method from the button's OnClick event
    public void RemoveAllPins()
    {
        GameObject[] allPins = GameObject.FindGameObjectsWithTag(pinTag);
        foreach (GameObject pin in allPins)
        {
            Destroy(pin);
        }

        Debug.Log("All pins removed.");
    }
}

